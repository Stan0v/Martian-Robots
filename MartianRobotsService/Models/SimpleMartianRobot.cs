using MartianRobotsService.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartianRobotsService.Models
{
    public class SimpleMartianRobot : IRobot
    {
        public IGrid<_2DCoordinate, _2DDirection> Grid { get; }
        public bool isLost { get; private set; }
        public CoordinateBase SelfCoordinate { get; private set; }
        public DirectionBase SelfDirection { get; private set; }
        //TODO add grid type checkings
        public SimpleMartianRobot(_2DCoordinate coordinate, _2DDirection direction, IGrid<_2DCoordinate, _2DDirection> grid)
        {
            isLost = false;
            SelfCoordinate = coordinate;
            SelfDirection = direction;
            Grid = grid;
        }

        public void Move(int distance)
        {
            if (distance == 0)
            {
                return;
            }

            SelfCoordinate = GetNextCoordinate(distance);
            var nextCoordinateWithin = Grid.IsCoordinateWithin(SelfCoordinate as _2DCoordinate);

            if (!nextCoordinateWithin)
            {
                _2DCoordinate coordinateToBeScented = null;
                
                switch (SelfDirection.Direction[0])
                {
                    case 0:
                        coordinateToBeScented = new _2DCoordinate(SelfCoordinate.Point[0], Grid.GridShape[1].Point[1]);
                        break;
                    case 90:
                        coordinateToBeScented = new _2DCoordinate(Grid.GridShape[1].Point[0], SelfCoordinate.Point[1]);
                        break;
                    case 180:
                        coordinateToBeScented = new _2DCoordinate(SelfCoordinate.Point[0], Grid.GridShape[0].Point[1]);
                        break;
                    case 270:
                        coordinateToBeScented = new _2DCoordinate(Grid.GridShape[0].Point[0], SelfCoordinate.Point[1]);
                        break;
                    default:
                        //potentially coordinates could be lost
                        throw new NotImplementedException();
                }

                if (!Grid.IsCoordinateScented(coordinateToBeScented))
                {
                    Grid.ScenteCoordinate(coordinateToBeScented);
                    isLost = true;
                }
                else
                    SelfCoordinate = coordinateToBeScented;
            }
        }
        
        public void Turn(uint degrees)
        {
            if (degrees != 0)
            {
                SelfDirection.Direction[0] = (SelfDirection.Direction[0] + degrees) % 360;
            }
        }

        public string GetState()
        {
            string lost = isLost ? "LOST" : string.Empty;
            return string.Format($"{SelfCoordinate.ToString()} {SelfDirection.ToOrientation()} {lost}");

        }

        private _2DCoordinate GetNextCoordinate(int distance)
        {
            _2DCoordinate nextCoord = new _2DCoordinate(SelfCoordinate.Point[0], SelfCoordinate.Point[1]);

            switch (SelfDirection.Direction[0])
            {
                case 0:
                    nextCoord.Point[1] += distance;
                    break;
                case 90:
                    nextCoord.Point[0] += distance;
                    break;
                case 180:
                    nextCoord.Point[1] -= distance;
                    break;
                case 270:
                    nextCoord.Point[0] -= distance;
                    break;
                default:
                    throw new NotImplementedException();
            }

            return nextCoord;
        }
    }
}
