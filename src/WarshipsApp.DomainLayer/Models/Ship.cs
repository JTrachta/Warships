namespace WarshipsApp.DomainLayer.Models
{
    public class Ship
    {
        public Ship(ShipType shipType)
        {
            Type = shipType;
            HullSections = shipType switch
            {
                ShipType.Frigate => populateHullSections(1),
                ShipType.Destroyer => populateHullSections(2),
                ShipType.Cruiser => populateHullSections(3),
                ShipType.Battleship or ShipType.AircraftCarrier => populateHullSections(5),
                _ => throw new ArgumentOutOfRangeException($"ShipType '{shipType}' is not recognized")
            };
        }

        public ShipType Type { get; }
        public bool IsDestroyed { get; set; }
        public HullSection[] HullSections { get; } = Array.Empty<HullSection>();

        internal static HullSection[] populateHullSections(int shipSize)
        {
            var hullSections = new HullSection[shipSize];
            for (int i = 0; i < hullSections.Length; i++)
            {
                hullSections[i] = new HullSection();
            }
            return hullSections;
        }       
    }
}
