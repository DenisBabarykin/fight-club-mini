using FightClub.Dto;

namespace FightClub.HttpClient;

/// <summary>
/// Интерфейс клиента к серверу FightClub.
/// </summary>
public interface IFightClubApiClient
{
    /// <summary>
    /// Получить текущее состояние боя.
    /// </summary>
    /// <returns>Возвращает текущее состояние боя, если он уже идет или закончился. Или null, если он ещё не начался.</returns>
    Task<Battle?> GetBattleAsync();

    /// <summary>
    /// Получить общий лог для указанного раунда.
    /// </summary>
    /// <param name="round">Номер раунда.</param>
    /// <returns>Общий лог для раунда, если он уже есть. Или null, если раунд ещё не был завершен.</returns>
    Task<RoundLog?> GetRoundLogAsync(int round);

    /// <summary>
    /// Удаляет текущую игру.
    /// </summary>
    Task DeleteBattleAsync();
}
