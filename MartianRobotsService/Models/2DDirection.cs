using MartianRobotsService.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartianRobotsService.Models
{
    public class _2DDirection : DirectionBase
    {
        private _2DDirection()
        {
        }

        public _2DDirection(uint direction) : base()
        {
            Direction = new List<uint>(1) { direction % 360 };
        }

        public override string ToOrientation()
        {
            string orientation;

            switch (Direction[0]) 
            {
                case 0:
                    orientation = "N";
                    break;
                case 90:
                    orientation = "E";
                    break;
                case 180:
                    orientation = "S";
                    break;
                case 270:
                    orientation = "W";
                    break;
                default:
                    throw new NotImplementedException();
            }

            return orientation;
        }
    }
}
