using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using FightClub.Dto;
using Newtonsoft.Json;

namespace FightClub.HttpClient;

public class FightClubHttpClient : IFightClubApiClient
{
    private readonly System.Net.Http.HttpClient _httpClient;

    public FightClubHttpClient()
    {
        _httpClient = new System.Net.Http.HttpClient();
        _httpClient.BaseAddress = new Uri("http://95.165.152.195:5000/api/");
    }

    public async Task<Battle?> GetBattleAsync()
    {
        var response = await _httpClient.GetAsync("battle");
        Battle? result = null;
        if (response.IsSuccessStatusCode)
        {
            result = JsonConvert.DeserializeObject<Battle>(await response.Content.ReadAsStringAsync());
        }

        return result;
    }

    public async Task<RoundLog?> GetRoundLogAsync(int round)
    {
        var response = await _httpClient.GetAsync($"logs/{round}");
        RoundLog? result = null;
        if (response.IsSuccessStatusCode)
        {
            result = JsonConvert.DeserializeObject<RoundLog>(await response.Content.ReadAsStringAsync());
        }

        return result;
    }

    public async Task DeleteBattleAsync()
    {
        await _httpClient.DeleteAsync("battle");
    }

    public async Task StartNewBattleAsync(BattleConfig battleConfig)
    {
        string stringPayload = JsonConvert.SerializeObject(battleConfig, Formatting.None);
        await _httpClient.PostAsync("battle", new StringContent(stringPayload, Encoding.UTF8, "application/json"));
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}