using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartianRobotsService.BaseClasses
{
    public interface IGrid<Coordinate, Direction> 
        where Coordinate : CoordinateBase
        where Direction : DirectionBase
    {
        List<CoordinateBase> GridShape { get; }
        int InitialParametersCount { get; }
        bool IsCoordinateWithin(Coordinate coordinate);
        void AddRobotOnPoint(IRobot robot);
        void RemoveRobotFromPoint(IRobot robot);
        bool IsCoordinateScented(Coordinate coordinate);
        void ScenteCoordinate(Coordinate coordinate);
        //factory method
        IRobot AcquireRobot(Coordinate coordinate, Direction direction = null);
    }
}
