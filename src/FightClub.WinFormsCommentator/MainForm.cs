using FightClub.Dto;
using FightClub.HttpClient;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FightClub.WinFormsCommentator
{
    public partial class MainForm : Form
    {
        private readonly FightClubHttpClient _fightClubHttpClient = new FightClubHttpClient();

        public string pathToImg = "";
        public MainForm()
        {
            InitializeComponent();

            lblPlayer1Name.TextAlign = ContentAlignment.MiddleCenter;
            lblPlayer2Name.TextAlign = ContentAlignment.MiddleCenter;
            lblPlayer3Name.TextAlign = ContentAlignment.MiddleCenter;
            lblPlayer4Name.TextAlign = ContentAlignment.MiddleCenter;
            lblPlayer5Name.TextAlign = ContentAlignment.MiddleCenter;
            lblPlayer6Name.TextAlign = ContentAlignment.MiddleCenter;

            lblPlayer1Name.ForeColor = Color.Yellow;
            lblPlayer2Name.ForeColor = Color.Yellow;
            lblPlayer3Name.ForeColor = Color.Yellow;
            lblPlayer4Name.ForeColor = Color.Cyan;
            lblPlayer5Name.ForeColor = Color.Cyan;
            lblPlayer6Name.ForeColor = Color.Cyan;

            lblP1hp.TextAlign = ContentAlignment.MiddleCenter;
            lblP2hp.TextAlign = ContentAlignment.MiddleCenter;
            lblP3hp.TextAlign = ContentAlignment.MiddleCenter;
            lblP4hp.TextAlign = ContentAlignment.MiddleCenter;
            lblP5hp.TextAlign = ContentAlignment.MiddleCenter;
            lblP6hp.TextAlign = ContentAlignment.MiddleCenter;

            string curDir = Environment.CurrentDirectory.ToString();
            string[] partOfDir = curDir.Split('\\');
            for (int i = 0; i < partOfDir.Length-3; i++)
            {
                pathToImg += partOfDir[i] + '\\';
            }
            pathToImg += "img\\";
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            while (true)
            {
                ResetAll();
                Battle battle = await _fightClubHttpClient.GetBattleAsync();
                if (battle != null && !battle.IsFinished)
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
            rtbLogs.AppendText($"\n------------------------------------- Раунд {battle.Round} ------------------------------------\n");

            // battle.TeamOne.Players.  лист игроков

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

            int start_pos_for_red = rtbLogs.Text.Length;
            foreach (var skirmishLog in roundLog.SkirmishLogs)
            {
                rtbLogs.AppendText(BeautifyLog(skirmishLog.PlayerOneAttackText) + "\n");
                rtbLogs.AppendText(BeautifyLog(skirmishLog.PlayerTwoAttackText) + "\n\n");
            }

            if (battle.IsFinished)
            {
                rtbLogs.AppendText("\n----------Игра закончена!----------\n");
            }

            rtbLogs.SelectAll();
            rtbLogs.SelectionAlignment = HorizontalAlignment.Center;
            rtbLogs.DeselectAll();

            for (int k = start_pos_for_red; k < rtbLogs.Text.Length; k++)
            {
                if (Char.IsDigit(rtbLogs.Text[k]))
                {
                    rtbLogs.SelectionStart = k;
                    rtbLogs.SelectionLength = 1;
                    rtbLogs.SelectionColor = Color.Red;
                }
            }

            int temp_start_pos = start_pos_for_red;

            foreach (var player in battle.TeamOne.Players)
            {
                int start_select = -1;
                int pos = 0;
                start_pos_for_red = temp_start_pos;
                do
                {
                    start_select = rtbLogs.Find(player.Name, start_pos_for_red + pos, RichTextBoxFinds.WholeWord);
                    if (start_select > 0)
                    {
                        rtbLogs.SelectionStart = start_select;
                        rtbLogs.SelectionLength = player.Name.Length;
                        rtbLogs.SelectionColor = Color.Yellow;

                        start_pos_for_red = start_select;
                        pos = player.Name.Length;
                    }
                    
                } while (start_select > 0);

                start_select = -1;
                pos = 0;
                start_pos_for_red = temp_start_pos;
                do
                {
                    start_select = rtbLogs.Find(player.DativeName, start_pos_for_red + pos, RichTextBoxFinds.WholeWord);
                    if (start_select > 0)
                    {
                        rtbLogs.SelectionStart = start_select;
                        rtbLogs.SelectionLength = player.DativeName.Length;
                        rtbLogs.SelectionColor = Color.Yellow;

                        start_pos_for_red = start_select;
                        pos = player.DativeName.Length;
                    }
                } while (start_select > 0);

                rtbLogs.DeselectAll();
            }

            start_pos_for_red = temp_start_pos;
            foreach (var player in battle.TeamTwo.Players)
            {
                int start_select = -1;
                int pos = 0;
                start_pos_for_red = temp_start_pos;
                do
                {
                    start_select = rtbLogs.Find(player.Name, start_pos_for_red + pos, RichTextBoxFinds.WholeWord);
                    if (start_select > 0)
                    {
                        rtbLogs.SelectionStart = start_select;
                        rtbLogs.SelectionLength = player.Name.Length;
                        rtbLogs.SelectionColor = Color.Cyan;

                        start_pos_for_red = start_select;
                        pos = player.Name.Length;
                    }

                } while (start_select > 0);

                start_select = -1;
                pos = 0;
                start_pos_for_red = temp_start_pos;
                do
                {
                    start_select = rtbLogs.Find(player.DativeName, start_pos_for_red + pos, RichTextBoxFinds.WholeWord);
                    if (start_select > 0)
                    {
                        rtbLogs.SelectionStart = start_select;
                        rtbLogs.SelectionLength = player.DativeName.Length;
                        rtbLogs.SelectionColor = Color.Cyan;

                        start_pos_for_red = start_select;
                        pos = player.DativeName.Length;
                    }
                } while (start_select > 0);

                rtbLogs.DeselectAll();
            }

            rtbLogs.DeselectAll();
        }

        private async Task ProcessGameAsync(Battle initBattle)
        {
            RoundLog initLog = await _fightClubHttpClient.GetRoundLogAsync(initBattle.Round);
            if (initLog != null)
            {
                ProcessNewRound(initBattle, initLog);
            }

            int lastProcessedRound = initBattle.Round;
            bool finished = false;

            int i = 0;
            foreach (var player in initBattle.TeamOne.Players)
            {
                FirstDraw(i, player);
                i++;
            }

            foreach (var player in initBattle.TeamTwo.Players)
            {
                FirstDraw(i, player);
                i++;
            }

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
                await Task.Delay(2000); // потом надо будет уменьшить до 2000 мс 
            }
        }

        private void FirstDraw(int num, Player player)
        {
            initiateItems(num, player);
            if (num == 0)
            {
                pbPlayer1.Image = Image.FromFile(pathToImg+player.AvatarId+".png");

                lblPlayer1Name.Text = player.Name;
                pbP1hp.Maximum = player.MaxHp;
                pbP1hp.Value = player.CurrentHp;
                lblP1hp.Text = player.CurrentHp + "/" + player.MaxHp;
                pbP1hp.SetState(2);

                rtbP1stats.AppendText("Сила\n");
                rtbP1stats.AppendText(player.Strength.ToString() + "\n\n");
                rtbP1stats.AppendText("Выносливость\n");
                rtbP1stats.AppendText(player.Endurance.ToString() + "\n\n");
                rtbP1stats.AppendText("Ловкость\n");
                rtbP1stats.AppendText(player.Agility.ToString() + "\n\n");
                rtbP1stats.AppendText("Интуиция\n");
                rtbP1stats.AppendText(player.Intuition.ToString() + "\n\n");

                rtbP1stats.SelectAll();
                rtbP1stats.SelectionAlignment = HorizontalAlignment.Center;
                rtbP1stats.DeselectAll();
            }
            if (num == 1)
            {
                pbPlayer2.Image = Image.FromFile(pathToImg + player.AvatarId+".png");

                lblPlayer2Name.Text = player.Name;
                pbP2hp.Maximum = player.MaxHp;
                pbP2hp.Value = player.CurrentHp;
                lblP2hp.Text = player.CurrentHp + "/" + player.MaxHp;
                pbP2hp.SetState(2);

                rtbP2stats.AppendText("Сила\n");
                rtbP2stats.AppendText(player.Strength.ToString() + "\n\n");
                rtbP2stats.AppendText("Выносливость\n");
                rtbP2stats.AppendText(player.Endurance.ToString() + "\n\n");
                rtbP2stats.AppendText("Ловкость\n");
                rtbP2stats.AppendText(player.Agility.ToString() + "\n\n");
                rtbP2stats.AppendText("Интуиция\n");
                rtbP2stats.AppendText(player.Intuition.ToString() + "\n\n");

                rtbP2stats.SelectAll();
                rtbP2stats.SelectionAlignment = HorizontalAlignment.Center;
                rtbP2stats.DeselectAll();
            }
            if (num == 2)
            {
                pbPlayer3.Image = Image.FromFile(pathToImg + player.AvatarId+".png");

                lblPlayer3Name.Text = player.Name;
                pbP3hp.Maximum = player.MaxHp;
                pbP3hp.Value = player.CurrentHp;
                lblP3hp.Text = player.CurrentHp + "/" + player.MaxHp;
                pbP3hp.SetState(2);

                rtbP3stats.AppendText("Сила\n");
                rtbP3stats.AppendText(player.Strength.ToString() + "\n\n");
                rtbP3stats.AppendText("Выносливость\n");
                rtbP3stats.AppendText(player.Endurance.ToString() + "\n\n");
                rtbP3stats.AppendText("Ловкость\n");
                rtbP3stats.AppendText(player.Agility.ToString() + "\n\n");
                rtbP3stats.AppendText("Интуиция\n");
                rtbP3stats.AppendText(player.Intuition.ToString() + "\n\n");

                rtbP3stats.SelectAll();
                rtbP3stats.SelectionAlignment = HorizontalAlignment.Center;
                rtbP3stats.DeselectAll();

            }
            if (num == 3)
            {
                pbPlayer4.Image = Image.FromFile(pathToImg + player.AvatarId + ".png");

                lblPlayer4Name.Text = player.Name;
                pbP4hp.Maximum = player.MaxHp;
                pbP4hp.Value = player.CurrentHp;
                lblP4hp.Text = player.CurrentHp + "/" + player.MaxHp;
                pbP4hp.SetState(2);

                rtbP4Stats.AppendText("Сила\n");
                rtbP4Stats.AppendText(player.Strength.ToString() + "\n\n");
                rtbP4Stats.AppendText("Выносливость\n");
                rtbP4Stats.AppendText(player.Endurance.ToString() + "\n\n");
                rtbP4Stats.AppendText("Ловкость\n");
                rtbP4Stats.AppendText(player.Agility.ToString() + "\n\n");
                rtbP4Stats.AppendText("Интуиция\n");
                rtbP4Stats.AppendText(player.Intuition.ToString() + "\n\n");

                rtbP4Stats.SelectAll();
                rtbP4Stats.SelectionAlignment = HorizontalAlignment.Center;
                rtbP4Stats.DeselectAll();
            }
            if (num == 4)
            {
                pbPlayer5.Image = Image.FromFile(pathToImg + player.AvatarId + ".png");

                lblPlayer5Name.Text = player.Name;
                pbP5hp.Maximum = player.MaxHp;
                pbP5hp.Value = player.CurrentHp;
                lblP5hp.Text = player.CurrentHp + "/" + player.MaxHp;
                pbP5hp.SetState(2);

                rtbP5Stats.AppendText("Сила\n");
                rtbP5Stats.AppendText(player.Strength.ToString() + "\n\n");
                rtbP5Stats.AppendText("Выносливость\n");
                rtbP5Stats.AppendText(player.Endurance.ToString() + "\n\n");
                rtbP5Stats.AppendText("Ловкость\n");
                rtbP5Stats.AppendText(player.Agility.ToString() + "\n\n");
                rtbP5Stats.AppendText("Интуиция\n");
                rtbP5Stats.AppendText(player.Intuition.ToString() + "\n\n");

                rtbP5Stats.SelectAll();
                rtbP5Stats.SelectionAlignment = HorizontalAlignment.Center;
                rtbP5Stats.DeselectAll();
            }
            if (num == 5)
            {
                pbPlayer6.Image = Image.FromFile(pathToImg + player.AvatarId + ".png");

                lblPlayer6Name.Text = player.Name;
                pbP6hp.Maximum = player.MaxHp;
                pbP6hp.Value = player.CurrentHp;
                lblP6hp.Text = player.CurrentHp + "/" + player.MaxHp;
                pbP6hp.SetState(2);

                rtbP6Stats.AppendText("Сила\n");
                rtbP6Stats.AppendText(player.Strength.ToString() + "\n\n");
                rtbP6Stats.AppendText("Выносливость\n");
                rtbP6Stats.AppendText(player.Endurance.ToString() + "\n\n");
                rtbP6Stats.AppendText("Ловкость\n");
                rtbP6Stats.AppendText(player.Agility.ToString() + "\n\n");
                rtbP6Stats.AppendText("Интуиция\n");
                rtbP6Stats.AppendText(player.Intuition.ToString() + "\n\n");

                rtbP6Stats.SelectAll();
                rtbP6Stats.SelectionAlignment = HorizontalAlignment.Center;
                rtbP6Stats.DeselectAll();
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
                pbP1hp.SetState(2);
            }
            if (num == 1)
            {
                lblPlayer2Name.Text = player.Name;
                pbP2hp.Maximum = player.MaxHp;
                pbP2hp.Value = player.CurrentHp;
                lblP2hp.Text = player.CurrentHp + "/" + player.MaxHp;
                pbP2hp.SetState(2);
            }
            if (num == 2)
            {
                lblPlayer3Name.Text = player.Name;
                pbP3hp.Maximum = player.MaxHp;
                pbP3hp.Value = player.CurrentHp;
                lblP3hp.Text = player.CurrentHp + "/" + player.MaxHp;
                pbP3hp.SetState(2);

            }
            if (num == 3)
            {
                lblPlayer4Name.Text = player.Name;
                pbP4hp.Maximum = player.MaxHp;
                pbP4hp.Value = player.CurrentHp;
                lblP4hp.Text = player.CurrentHp + "/" + player.MaxHp;
                pbP4hp.SetState(2);

            }
            if (num == 4)
            {
                lblPlayer5Name.Text = player.Name;
                pbP5hp.Maximum = player.MaxHp;
                pbP5hp.Value = player.CurrentHp;
                lblP5hp.Text = player.CurrentHp + "/" + player.MaxHp;
                pbP5hp.SetState(2);

            }
            if (num == 5)
            {
                lblPlayer6Name.Text = player.Name;
                pbP6hp.Maximum = player.MaxHp;
                pbP6hp.Value = player.CurrentHp;
                lblP6hp.Text = player.CurrentHp + "/" + player.MaxHp;
                pbP6hp.SetState(2);
            }
        }

        private void initiateItems(int num, Player player)
        {
            
            if (num == 0)
                foreach (var item in player.Items)
                {
                    if (item >= 10 && item < 20)
                    {
                        pbPlayer1item1.Image = Image.FromFile(pathToImg+item+".png");
                    }
                    if (item >= 20 && item < 30)
                    {
                        pbPlayer1item2.Image = Image.FromFile(pathToImg + item + ".png");
                    }
                    if (item >= 30 && item < 40)
                    {
                        pbPlayer1item3.Image = Image.FromFile(pathToImg + item + ".png");
                    }
                    if (item >= 40)
                    {
                        pbPlayer1item4.Image = Image.FromFile(pathToImg + item + ".png");
                    }
                }
            if (num == 1)
                foreach (var item in player.Items)
                {
                    if (item >= 10 && item < 20)
                    {
                        pbPlayer2item1.Image = Image.FromFile(pathToImg + item + ".png");
                    }
                    if (item >= 20 && item < 30)
                    {
                        pbPlayer2item2.Image = Image.FromFile(pathToImg + item + ".png");
                    }
                    if (item >= 30 && item < 40)
                    {
                        pbPlayer2item3.Image = Image.FromFile(pathToImg + item + ".png");
                    }
                    if (item >= 40)
                    {
                        pbPlayer2item4.Image = Image.FromFile(pathToImg + item + ".png");
                    }
                }
            if (num == 2)
                foreach (var item in player.Items)
                {
                    if (item >= 10 && item < 20)
                    {
                        pbPlayer3item1.Image = Image.FromFile(pathToImg + item + ".png");
                    }
                    if (item >= 20 && item < 30)
                    {
                        pbPlayer3item2.Image = Image.FromFile(pathToImg + item + ".png");
                    }
                    if (item >= 30 && item < 40)
                    {
                        pbPlayer3item3.Image = Image.FromFile(pathToImg + item + ".png");
                    }
                    if (item >= 40)
                    {
                        pbPlayer3item4.Image = Image.FromFile(pathToImg + item + ".png");
                    }
                }
            if (num == 3)
                foreach (var item in player.Items)
                {
                    if (item >= 10 && item < 20)
                    {
                        pbPlayer4item1.Image = Image.FromFile(pathToImg + item + ".png");
                    }
                    if (item >= 20 && item < 30)
                    {
                        pbPlayer4item2.Image = Image.FromFile(pathToImg + item + ".png");
                    }
                    if (item >= 30 && item < 40)
                    {
                        pbPlayer4item3.Image = Image.FromFile(pathToImg + item + ".png");
                    }
                    if (item >= 40)
                    {
                        pbPlayer4item4.Image = Image.FromFile(pathToImg + item + ".png");
                    }
                }
            if (num == 4)
                foreach (var item in player.Items)
                {
                    if (item >= 10 && item < 20)
                    {
                        pbPlayer5item1.Image = Image.FromFile(pathToImg + item + ".png");
                    }
                    if (item >= 20 && item < 30)
                    {
                        pbPlayer5item2.Image = Image.FromFile(pathToImg + item + ".png");
                    }
                    if (item >= 30 && item < 40)
                    {
                        pbPlayer5item3.Image = Image.FromFile(pathToImg + item + ".png");
                    }
                    if (item >= 40)
                    {
                        pbPlayer5item4.Image = Image.FromFile(pathToImg + item + ".png");
                    }
                }
            if (num == 5)
                foreach (var item in player.Items)
                {
                    if (item >= 10 && item < 20)
                    {
                        pbPlayer6item1.Image = Image.FromFile(pathToImg + item + ".png");
                    }
                    if (item >= 20 && item < 30)
                    {
                        pbPlayer6item2.Image = Image.FromFile(pathToImg + item + ".png");
                    }
                    if (item >= 30 && item < 40)
                    {
                        pbPlayer6item3.Image = Image.FromFile(pathToImg + item + ".png");
                    }
                    if (item >= 40)
                    {
                        pbPlayer6item4.Image = Image.FromFile(pathToImg + item + ".png");
                    }
                }
            
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            await _fightClubHttpClient.DeleteBattleAsync();
            _fightClubHttpClient.Dispose();
            Environment.Exit(0);
        }

        private string BeautifyLog(string text)
        {
            int index_of_dmg = 0;
            string formatted_string = "";
            
            index_of_dmg = text.IndexOf("Нанесено урона");
            formatted_string = text.Insert(index_of_dmg, "\n");

            return formatted_string;
        }

        private void rtbLogs_Enter(object sender, EventArgs e)
        {
            ActiveControl = lblP1hp;
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

    public class RichEdit50 : RichTextBox
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]

        static extern IntPtr LoadLibrary(string lpFileName);

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams prams = base.CreateParams;
                if (LoadLibrary("msftedit.dll") != IntPtr.Zero)
                {
                    //prams.ExStyle |= 0x020; // transparent
                    prams.ClassName = "RICHEDIT50W";
                }
                return prams;
            }
        }
    }
}