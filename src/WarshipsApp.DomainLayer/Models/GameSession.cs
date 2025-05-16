namespace WarshipsApp.DomainLayer.Models
{
    public class GameSession
    {
        public GameSession(string namePlayer1, string namePlayer2, int gamePlanSize)
        {
            Players = [new PlayerInfo(namePlayer1, gamePlanSize), new PlayerInfo(namePlayer2, gamePlanSize)];
            MapSize = gamePlanSize;
        }

        public PlayerInfo[] Players { get; }

        public int _currentPlayerIndex = 0;

        public int MapSize;
    }
}
