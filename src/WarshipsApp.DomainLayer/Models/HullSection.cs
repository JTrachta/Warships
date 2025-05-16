namespace WarshipsApp.DomainLayer.Models
{
    public class HullSection
    {
        public Guid Id { get; } = Guid.NewGuid();
        public bool IsHit { get; set; } = false;
    }
}
