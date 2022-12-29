using FightClub.Dto;
using FightClub.HttpClient;
using System.Text;
using System.Runtime.InteropServices;

namespace FightClub.WinFormsCommentator
{
    public partial class MainForm : Form
    {
        private readonly FightClubHttpClient _fightClubHttpClient = new FightClubHttpClient();

        public MainForm()
        {
            InitializeComponent();

            pbP1hp.SetState(2);
            pbP2hp.SetState(2);
            pbP3hp.SetState(2);
            pbP4hp.SetState(2);
            pbP5hp.SetState(2);
            pbP6hp.SetState(2);
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

            // battle.TeamOne.Players.  лист игроков
            pbPlayer1.Image = Image.FromFile("E:\\VS repos\\src\\FightClub.WinFormsCommentator\\img\\Вика.png");
            pbPlayer2.Image = Image.FromFile("E:\\VS repos\\src\\FightClub.WinFormsCommentator\\img\\Маша.png");
            pbPlayer3.Image = Image.FromFile("E:\\VS repos\\src\\FightClub.WinFormsCommentator\\img\\Света.png");
            pbPlayer4.Image = Image.FromFile("E:\\VS repos\\src\\FightClub.WinFormsCommentator\\img\\Денчик.png");
            pbPlayer5.Image = Image.FromFile("E:\\VS repos\\src\\FightClub.WinFormsCommentator\\img\\Вова.png");
            pbPlayer6.Image = Image.FromFile("E:\\VS repos\\src\\FightClub.WinFormsCommentator\\img\\Макс.png");

            int i = 0;
            foreach (var player in battle.TeamOne.Players)
            {
                DrawPlayer(i, player);
                i++;
            }

            foreach (var player in battle.TeamTwo.Players)
            {
                DrawPlayer(i, player);
                i++;
            }

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
                    lastProcessedRound++;
                }

                await Task.Delay(5000); // потом надо будет уменьшить до 2000 мс 
            }
        }

        private void DrawPlayer (int num, Player player)
        {
            if (num == 0)
            {
                lblPlayer1Name.Text = player.Name;
                pbP1hp.Maximum = player.MaxHp;
                pbP1hp.Value = player.CurrentHp;
                lblP1hp.Text = player.CurrentHp + "/" + player.MaxHp;
            }
            if (num == 1)
            {
                lblPlayer2Name.Text = player.Name;
                pbP2hp.Maximum = player.MaxHp;
                pbP2hp.Value = player.CurrentHp;
                lblP2hp.Text = player.CurrentHp + "/" + player.MaxHp;
            }
            if (num == 2)
            {
                lblPlayer3Name.Text = player.Name;
                pbP3hp.Maximum = player.MaxHp;
                pbP3hp.Value = player.CurrentHp;
                lblP3hp.Text = player.CurrentHp + "/" + player.MaxHp;
            }
            if (num == 3)
            {
                lblPlayer4Name.Text = player.Name;
                pbP4hp.Maximum = player.MaxHp;
                pbP4hp.Value = player.CurrentHp;
                lblP4hp.Text = player.CurrentHp + "/" + player.MaxHp;
            }
            if (num == 4)
            {
                lblPlayer5Name.Text = player.Name;
                pbP5hp.Maximum = player.MaxHp;
                pbP5hp.Value = player.CurrentHp;
                lblP5hp.Text = player.CurrentHp + "/" + player.MaxHp;
            }
            if (num == 5)
            {
                lblPlayer6Name.Text = player.Name;
                pbP6hp.Maximum = player.MaxHp;
                pbP6hp.Value = player.CurrentHp;
                lblP6hp.Text = player.CurrentHp + "/" + player.MaxHp;
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await _fightClubHttpClient.DeleteBattleAsync();
            _fightClubHttpClient.Dispose();
            Environment.Exit(0);
        }
    }
    public static class ModifyProgressBarColor
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
        public static void SetState(this ProgressBar pBar, int state)
        {
            SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
        }
    }
}