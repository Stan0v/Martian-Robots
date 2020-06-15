using MartianRobotsService.BaseClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace MartianRobotsService.Models
{
    public class _2DCoordinate : CoordinateBase
    {
        private _2DCoordinate() 
        {
            
        }

        public _2DCoordinate(int x, int y)
        {
            Point = new List<int>(2) { x , y };
        }
    }

    public class _2DCoordinateEqualityComparer : IEqualityComparer<_2DCoordinate>
    {
        public bool Equals(_2DCoordinate x, _2DCoordinate y)
        {
            return x.Point[0] == y.Point[0] && x.Point[1] == y.Point[1];
        }

        public int GetHashCode([DisallowNull] _2DCoordinate obj)
        {
            unchecked
            {
                int hash = 17;

                hash = hash * 23 + obj.Point[0].GetHashCode();
                hash = hash * 23 + obj.Point[1].GetHashCode();
                return hash;
            }
        }
    }
}
