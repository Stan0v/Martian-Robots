using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartianRobotsService.BaseClasses
{
    public abstract class CoordinateBase
    {
        public List<int> Point { get; protected set; }
        public override string ToString()
        {
            return string.Join(' ', Point);
        }
    }
}
