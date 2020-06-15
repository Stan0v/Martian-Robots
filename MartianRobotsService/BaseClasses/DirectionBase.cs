using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartianRobotsService.BaseClasses
{
    public abstract class DirectionBase
    {
        public List<uint> Direction { get; protected set; }

        public virtual string ToOrientation()
        {
            return string.Join(' ', Direction);
        }
    }
}
