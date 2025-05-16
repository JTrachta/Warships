using System;
namespace WarshipsApp.DomainLayer.Models
{
    public class OceanSpace
    {
        public bool IsOccupied { get; set; } = false;
        public bool IsHit { get; set; } = false;
        public Guid ShipSectionId { get; private set; } = Guid.Empty;

        public void PlaceShipSection(Guid shipSectionId)
        {
            ShipSectionId = shipSectionId;
            IsOccupied = true;
        }
    }
}
