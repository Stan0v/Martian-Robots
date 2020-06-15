using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartianRobotsService.Configuration
{
    public class SettingsOptions
    {

        /// <summary>
        /// Maximum coordinate size for the grid
        /// </summary>
        public int MaxCoordinates { get; set; }

        /// <summary>
        /// Maximum length limit of the robot commands
        /// </summary>
        public int MaxRobotCommandLength { get; set; }

    }
}
