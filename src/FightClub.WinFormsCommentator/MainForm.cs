using FightClub.Dto;
using FightClub.HttpClient;
using System.Text;

namespace FightClub.WinFormsCommentator
{
    public partial class MainForm : Form
    {
        private readonly FightClubHttpClient _fightClubHttpClient = new FightClubHttpClient();

        public MainForm()
        {
            InitializeComponent();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            while (true)
            {
                ResetAll();
                Battle battle = await _fightClubHttpClient.GetBattleAsync();
                if (battle != null)
                {
                    await ProcessGameAsync(battle);
                }

                await Task.Delay(2000);
            }
        }

        private void ResetAll()
        {
            lblRound.Text = "Раунд:";
            rtbLogs.Clear();
        }

        private void ProcessNewRound(Battle battle, RoundLog roundLog)
        {
            lblRound.Text = "Раунд: " + battle.Round;
            rtbLogs.AppendText($"\n----------Раунд {battle.Round}----------\n");

            foreach (var skirmishLog in roundLog.SkirmishLogs)
            {
                rtbLogs.AppendText(skirmishLog.PlayerOneAttackText + "\n");
                rtbLogs.AppendText(skirmishLog.PlayerTwoAttackText + "\n\n");
            }

            if (battle.IsFinished)
            {
                rtbLogs.AppendText("\n----------Игра закончена!----------\n");
            }
        }

        private async Task ProcessGameAsync(Battle initBattle)
        {
            RoundLog initLog = await _fightClubHttpClient.GetRoundLogAsync(initBattle.Round);
            ProcessNewRound(initBattle, initLog);
            int lastProcessedRound = initBattle.Round;
            bool finished = false;

            while (!finished)
            {
                RoundLog currentLog = await _fightClubHttpClient.GetRoundLogAsync(lastProcessedRound + 1);
                if (currentLog != null)
                {
                    Battle currentBattle = await _fightClubHttpClient.GetBattleAsync();
                    ProcessNewRound(currentBattle, currentLog);
                    finished = currentBattle.IsFinished;
                }

                await Task.Delay(5000); // потом надо будет уменьшить до 2000 мс
            }
        }

        private async void deleteGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await _fightClubHttpClient.DeleteBattleAsync();
            _fightClubHttpClient.Dispose();
            Environment.Exit(0);
        }
    }
}