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
                throw new ArgumentException("�� ������� ������ ����� ����: ������ �� �������!");
            }

            var battleConfig = JsonConvert.DeserializeObject<BattleConfig>(tbxBattleConfig.Text);
            if (battleConfig != null)
            {
                await _fightClubHttpClient.StartNewBattleAsync(battleConfig);
            }
            else
            {
                throw new NullReferenceException("�� ������� ������� ������.");
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
            new TeamConfig(new List<PlayerConfig>()
            {
                new PlayerConfig("���", "����", 1, 15, 12, 5, 5, new List<int>() { 1, 2}),
                new PlayerConfig("����", "����", 2, 5, 5, 20, 5, new List<int>() { 3, 4 }),
                new PlayerConfig("����", "�����", 3, 6, 7, 6, 16, new List<int>() { 5, 6 })
            }),
            new TeamConfig(new List<PlayerConfig>()
            {
                new PlayerConfig("�����", "�����", 4, 6, 7, 6, 16, new List<int>() { 7, 8 }),
                new PlayerConfig("����", "����", 5, 15, 12, 5, 5, new List<int>() { 9, 10 }),
                new PlayerConfig("����", "����", 6, 5, 5, 20, 5, new List<int>() { 11, 12 })
            }),
            new FightEngineParams()
        );
        tbxBattleConfig.Text = JsonConvert.SerializeObject(battleConfig, Formatting.Indented);
        toolStripStatusLabel.Text = "������ �������!";
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
            toolStripStatusLabel.Text = "������ �������!";
            toolStripStatusLabel.ForeColor = Color.White;
            toolStripStatusLabel.BackColor = Color.Green;
            IsValid = true;
        }
        else
        {
            toolStripStatusLabel.Text = "������ � �������!";
            toolStripStatusLabel.ForeColor = Color.White;
            toolStripStatusLabel.BackColor = Color.Red;
            IsValid = false;
        }
    }
}