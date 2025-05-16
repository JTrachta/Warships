using WarshipsApp.DomainLayer.Models;

namespace WarshipsApp.DomainLayer.Extensions
{
    public static class GamePlanExtensions
    {
        private class Position
        {
            public int x { get; set; }
            public int y { get; set; }
        }

        public static void PlaceShipRandomly(this GamePlan gamePlan, Ship ship)
        {
            var result = new List<OceanSpace>();
            var shipLength = GetShipLength(ship.Type);

            while (result.Count < shipLength)
            {
                var originalPosition = new Position
                {
                    x = Random.Shared.Next(0, gamePlan.Map.GetUpperBound(0)),
                    y = Random.Shared.Next(0, gamePlan.Map.GetUpperBound(1))
                };

                var direction = Random.Shared.Next(0, 2);

                for (var i = 0; i < shipLength; i++)
                {
                    var currentShipSegmentPosition = new Position
                    {
                        x = direction == 0 ? originalPosition.x + i : originalPosition.x,
                        y = direction == 1 ? originalPosition.y + i : originalPosition.y
                    };

                    if ((ship.Type == ShipType.AircraftCarrier || ship.Type == ShipType.Battleship) && i == 1)
                    {
                        var specShipSegmentPosition1 = new Position
                        {
                            x = direction == 0 ? currentShipSegmentPosition.x : currentShipSegmentPosition.x - 1,
                            y = direction == 1 ? currentShipSegmentPosition.y : currentShipSegmentPosition.y - 1
                        };

                        if (!canHullSectionBePlacedHere(gamePlan, specShipSegmentPosition1))
                        {
                            result.Clear();
                            break;
                        }

                        result.Add(gamePlan.Map[specShipSegmentPosition1.x ,specShipSegmentPosition1.y]);

                        if (ship.Type == ShipType.Battleship)
                        {
                            var specShipSegmentPosition2 = new Position
                            {
                                x = direction == 0 ? originalPosition.x + i : originalPosition.x + 1,
                                y = direction == 1 ? originalPosition.y + i : originalPosition.y + 1
                            };

                            if (!canHullSectionBePlacedHere(gamePlan, specShipSegmentPosition2))
                            {
                                result.Clear();
                                break;
                            }

                            result.Add(gamePlan.Map[specShipSegmentPosition2.x, specShipSegmentPosition2.y]);
                        }
                    }

                    if (!canHullSectionBePlacedHere(gamePlan, currentShipSegmentPosition))
                    { 
                        result.Clear();
                        break;
                    }

                    result.Add(gamePlan.Map[currentShipSegmentPosition.x, currentShipSegmentPosition.y]);
                }
            }

            //place all hull segments when we know theere is nothing blocking them
            foreach (var item in result.Select((oceanSpace, index) => new { oceanSpace, index }))
            {
                item.oceanSpace.PlaceShipSection(ship.HullSections[item.index].Id);
            }
        }

        private static bool canHullSectionBePlacedHere(GamePlan gamePlan, Position pos)
        {
           return isFieldWithinBounds(gamePlan, pos.x, pos.y) && (gamePlan.Map[pos.x, pos.y].IsOccupied == false) && !areAdjacentFieldsOccupied(gamePlan, pos.x, pos.y);
        }

        private static bool areAdjacentFieldsOccupied(GamePlan gamePlan, int x, int y)
        {
            //check left and right columns
            for (int i = -1; i < 2; i++)
            {
                if (isFieldWithinBounds(gamePlan, x - 1, y + i) && gamePlan.Map[x - 1, y + i].IsOccupied)
                    return true;

                if (isFieldWithinBounds(gamePlan, x + 1, y + i) && gamePlan.Map[x + 1, y + i].IsOccupied)
                    return true;
            }

            //check upper and lower fiels
            if (isFieldWithinBounds(gamePlan, x , y - 1 ) && gamePlan.Map[x , y - 1].IsOccupied)
                return true;

            if (isFieldWithinBounds(gamePlan, x , y + 1 ) && gamePlan.Map[x , y + 1].IsOccupied)
                return true;

            return false;
        }

        private static bool isFieldWithinBounds(GamePlan gamePlan, int x, int y)
        {
            if (x < 0 || x > gamePlan.Map.GetUpperBound(0) || y < 0 || y > gamePlan.Map.GetUpperBound(1))
            {
                return false;
            }

            return true;
        }

        private static int GetShipLength(ShipType shipType)
        {
            return shipType switch
            {
                ShipType.Frigate => 1,
                ShipType.Destroyer => 2,
                ShipType.Cruiser or ShipType.Battleship => 3,
                ShipType.AircraftCarrier => 4,
                _ => throw new ArgumentOutOfRangeException($"ShipType '{shipType}' is not recognized")
            };
        }
    }
}