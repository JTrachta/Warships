using WarshipsApp.DomainLayer.Extensions;

namespace WarshipsApp.DomainLayer.Models
{
    public class PlayerInfo
    {
        public PlayerInfo(string playerName, int gamePlanSize)
        {
            PlayerName = playerName ?? throw new ArgumentNullException(nameof(playerName));
            GamePlan = new GamePlan(gamePlanSize);
            Ships =
                [
                new Ship(ShipType.Frigate),
                new Ship(ShipType.Frigate),
                new Ship(ShipType.Destroyer),
                new Ship(ShipType.Destroyer),
                new Ship(ShipType.Cruiser),
                new Ship(ShipType.Battleship),
                new Ship(ShipType.AircraftCarrier)
                ];

            Ships.ForEach(s => GamePlan.PlaceShipRandomly(s));
        }

        public string PlayerName { get; } = string.Empty;
        public GamePlan GamePlan { get; }
        public List<Ship> Ships { get; }
    }
}
