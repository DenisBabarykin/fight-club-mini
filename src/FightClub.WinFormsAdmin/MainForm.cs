using System.Diagnostics.Eventing.Reader;
using FightClub.Dto;
using FightClub.HttpClient;
using Newtonsoft.Json;

namespace FightClub.WinFormsAdmin;

public partial class MainForm : Form
{
    private readonly FightClubHttpClient _fightClubHttpClient = new FightClubHttpClient();

    private bool IsValid { get; set; } = true;

    public MainForm()
    {
        InitializeComponent();
    }

    private async void deleteCurrentGameToolStripMenuItem_Click(object sender, EventArgs e)
    {
        await _fightClubHttpClient.DeleteBattleAsync();
    }

    private async void btnStartGame_Click(object sender, EventArgs e)
    {
        try
        {
            if (!IsValid)
            {
                throw new ArgumentException("Не удалось начать новую игру: конфиг не валиден!");
            }

            var battleConfig = JsonConvert.DeserializeObject<BattleConfig>(tbxBattleConfig.Text);
            if (battleConfig != null)
            {
                await _fightClubHttpClient.StartNewBattleAsync(battleConfig);
            }
            else
            {
                throw new NullReferenceException("Не удалось собрать конфиг.");
            }
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, exception.GetType().ToString());
        }
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        var battleConfig = new BattleConfig(
            new Team(new List<Player>()
            {
                new Player("Ден", "Дену", 1, 15, 12, 5, 5, 150, 121, new List<int>() { 1, 2}),
                new Player("Вика", "Вике", 2, 5, 5, 20, 5, 50, 45, new List<int>() { 3, 4 }),
                new Player("Макс", "Максу", 3, 6, 7, 6, 16, 60, 0, new List<int>() { 5, 6 })
            }),
            new Team(new List<Player>()
            {
                new Player("Света", "Свете", 4, 6, 7, 6, 16, 60, 15, new List<int>() { 7, 8 }),
                new Player("Маша", "Маше", 5, 15, 12, 5, 5, 150, 121, new List<int>() { 9, 10 }),
                new Player("Вова", "Вове", 6, 5, 5, 20, 5, 50, 45, new List<int>() { 11, 12 })
            })
        );
        tbxBattleConfig.Text = JsonConvert.SerializeObject(battleConfig, Formatting.Indented);
        toolStripStatusLabel.Text = "Конфиг валиден!";
        toolStripStatusLabel.ForeColor = Color.White;
        toolStripStatusLabel.BackColor = Color.Green;
    }

    private void tbxBattleConfig_TextChanged(object sender, EventArgs e)
    {
        bool valid = true;
        try
        {
            var battleConfig = JsonConvert.DeserializeObject<BattleConfig>(tbxBattleConfig.Text);
            if (battleConfig == null || !battleConfig.TeamOne.Players.Any() || !battleConfig.TeamTwo.Players.Any())
            {
                valid = false;
            }
        }
        catch (Exception exception)
        {
            valid = false;
        }

        if (valid)
        {
            toolStripStatusLabel.Text = "Конфиг валиден!";
            toolStripStatusLabel.ForeColor = Color.White;
            toolStripStatusLabel.BackColor = Color.Green;
            IsValid = true;
        }
        else
        {
            toolStripStatusLabel.Text = "Ошибка в конфиге!";
            toolStripStatusLabel.ForeColor = Color.White;
            toolStripStatusLabel.BackColor = Color.Red;
            IsValid = false;
        }
    }
}