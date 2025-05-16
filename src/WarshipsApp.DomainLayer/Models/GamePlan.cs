namespace WarshipsApp.DomainLayer.Models
{
    public class GamePlan
    {
        public GamePlan(int size)
        {
            Map = new OceanSpace[size, size];

            // initialize the map with OceanSpace objects
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Map[i, j] = new OceanSpace();
                }
            }
        }

        public OceanSpace[,] Map { get; }
    }
}
