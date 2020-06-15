using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartianRobotsService.BaseClasses
{
    public interface IRobot
    {
        bool isLost { get; }
        CoordinateBase SelfCoordinate { get; }
        DirectionBase SelfDirection { get; }

        void Move(int distance);
        void Turn(uint degrees);
        string GetState();
    }
}
