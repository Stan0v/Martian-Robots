using MartianRobotsService.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartianRobotsService.Models
{
    public class RectangularGrid : IGrid<_2DCoordinate, _2DDirection>
    {
        private Dictionary<_2DCoordinate, List<IRobot>> _robotsOnGrid;
        private Dictionary<_2DCoordinate, bool> _coordinatesScented;
        public int InitialParametersCount { get; private set; }
        public List<CoordinateBase> GridShape { get; private set; }

        public RectangularGrid(int x, int y)
        {
            InitialParametersCount = 2;
            GridShape = new List<CoordinateBase> { new _2DCoordinate(0, 0), new _2DCoordinate(x, y) };
            _robotsOnGrid = new Dictionary<_2DCoordinate, List<IRobot>>( new _2DCoordinateEqualityComparer());
            _coordinatesScented = new Dictionary<_2DCoordinate, bool>( new _2DCoordinateEqualityComparer());
        }

        public bool IsCoordinateWithin(_2DCoordinate coordinate)
        {
            return coordinate.Point[0] >= GridShape[0].Point[0] && coordinate.Point[0] <= GridShape[1].Point[0] &&
                    coordinate.Point[1] >= GridShape[0].Point[1] && coordinate.Point[1] <= GridShape[1].Point[1];
        }

        public void AddRobotOnPoint(IRobot robot)
        {
            if (IsCoordinateWithin(robot.SelfCoordinate as _2DCoordinate))
            {
                if (!_robotsOnGrid.ContainsKey(robot.SelfCoordinate as _2DCoordinate))
                    _robotsOnGrid[robot.SelfCoordinate as _2DCoordinate] = new List<IRobot>();

                _robotsOnGrid[robot.SelfCoordinate as _2DCoordinate].Add(robot);
            }
        }

        public void RemoveRobotFromPoint(IRobot robot)
        {
            if (IsCoordinateWithin(robot.SelfCoordinate as _2DCoordinate))
            {
                if (_robotsOnGrid.ContainsKey(robot.SelfCoordinate as _2DCoordinate) && 
                    _robotsOnGrid[robot.SelfCoordinate as _2DCoordinate].Count > 0)
                    _robotsOnGrid[robot.SelfCoordinate as _2DCoordinate].Remove(robot);
            }
        }

        public bool IsCoordinateScented(_2DCoordinate coordinate)
        {
            if (coordinate != null && _coordinatesScented.ContainsKey(coordinate))
                return true;

            return false;
        }

        public void ScenteCoordinate(_2DCoordinate coordinate)
        {
            if(coordinate != null && IsCoordinateWithin(coordinate))
                _coordinatesScented[coordinate] = true;
        }

        //if direction is null, then assume we are trying to get existing robot
        public IRobot AcquireRobot(_2DCoordinate coordinate, _2DDirection direction = null)
        {
            IRobot robot = null;

            if (direction == null && _robotsOnGrid.ContainsKey(coordinate) && _robotsOnGrid[coordinate].Count > 0)
                robot = _robotsOnGrid[coordinate].FirstOrDefault(x => !x.isLost);

            if (robot == null && direction != null)
                robot = new SimpleMartianRobot(coordinate, direction, this);

            return robot;
        }
    }
}
