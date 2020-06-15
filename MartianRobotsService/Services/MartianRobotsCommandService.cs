using Grpc.Core;
using MartianRobotsService.BaseClasses;
using MartianRobotsService.Configuration;
using MartianRobotsService.Models;
using MartianRobotsService.Protos;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MartianRobotsService
{
    public class MartianRobotsCommandService : MartianRobotsCommand.MartianRobotsCommandBase
    {
        private readonly ILogger<MartianRobotsCommandService> _logger;
        private readonly SettingsOptions _settingsOption;
        public MartianRobotsCommandService(ILogger<MartianRobotsCommandService> logger,
                             IOptions<SettingsOptions> settingsOption)
        {
            _logger = logger;
            _settingsOption = settingsOption.Value;
        }

        //unary commands are Stubs for now as redundancy according wit task specifications. Could be implemented later
        public override Task<CommandResult> GridInitialize(GridInitParameters command, ServerCallContext context)        
        {
            //return Task.FromResult(GridInitialize(command));
            return Task.FromResult(new CommandResult { Result = "Stub" });
        }

        private IGrid<CoordinateBase, DirectionBase> GridInitialize(string command)//(GridInitParameters command)
        {
            var coords = command.Split(' ');

            //TODO compare grid.InitialParametersCount and coords.Count and create grid respectively in the future
            int x = 0, y = 0;

            if (coords.Count() != 2 || !Int32.TryParse(coords[0], out x) || !Int32.TryParse(coords[1], out y))
                return null;

            if (x > _settingsOption.MaxCoordinates || y > _settingsOption.MaxCoordinates)
                return null;

            return (IGrid<CoordinateBase, DirectionBase>)new RectangularGrid(x, y);
        }

        public override Task<CommandResult> RobotActivate(RobotInitParameters command, ServerCallContext context)
        {
            //return Task.FromResult(RobotActivate(command));
            return Task.FromResult(new CommandResult { Result = "Stub" });
        }

        private IRobot RobotActivate(/*RobotInitParameters command*/ string coordinates, IGrid<CoordinateBase, DirectionBase> grid)
        {
            var position = coordinates.Split(' ');

            int x = 0, y = 0;
            DirectionBase direction = null;

            if (position.Count() != 3 || !Int32.TryParse(position[0], out x) || !Int32.TryParse(position[1], out y))
            {
                return null;
            }

            switch(position[2])
            {
                case "N":
                    direction = new _2DDirection(0);
                    break;
                case "S":
                    direction = new _2DDirection(180);
                    break;
                case "E":
                    direction = new _2DDirection(90);
                    break;
                case "W":
                    direction = new _2DDirection(270);
                    break;
                default:
                    return null;
            }

            return grid.AcquireRobot(new _2DCoordinate(x, y), direction);
        }

        public override Task<CommandResult> RobotMove(RobotMoveInstruction command, ServerCallContext context)
        {
            //return Task.FromResult(RobotMove(command));
            return Task.FromResult(new CommandResult { Result = "Stub" });
        }

        private bool RobotMove(/*RobotMoveInstruction command*/ string instruction, IRobot robot)
        {
            if(!string.IsNullOrEmpty(instruction) && instruction.Length == 1)
            {
                switch (instruction)
                {
                    case "F":
                        robot.Move(1);
                        return true;
                    default:
                        return false;
                }
            }

            return false;
        }

        public override Task<CommandResult> RobotTurn(RobotTurnInstruction command, ServerCallContext context)
        {
            //return Task.FromResult(RobotTurn(command));
            return Task.FromResult(new CommandResult { Result = "Stub" });
        }

        private bool RobotTurn(/*RobotTurnInstruction command*/ string instruction, IRobot robot)
        {
            if (!string.IsNullOrEmpty(instruction) && instruction.Length == 1)
            {
                switch (instruction)
                {
                    case "L":
                        robot.Turn(270);
                        return true;
                    case "R":
                        robot.Turn(90);
                        return true;
                    default:
                        return false;
                }
            }

            return false;
        }

        //GRPC message size limit setting is 4Mb by default, we should remember that in case of huge batch
        public override Task<CommandResult> BatchCommand(BatchInstructions command, ServerCallContext context)
        {
            return Task.FromResult(BatchCommand(command));
        }

        private CommandResult BatchCommand(BatchInstructions command)
        {
            StringBuilder res = new StringBuilder();
            IGrid<CoordinateBase, DirectionBase> grid = null;
            IRobot robot = null;

            //TODO check for consistency robot initialization with next command for it
            //should not be a problem if overall command format is right even if there are local errors in single command
            foreach (var myString in command.Batch.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                if(Regex.IsMatch(myString, @"^[a-zA-Z]+$"))
                {
                    //skip or cut? for consistency skip was choosen
                    if(myString.Length <= _settingsOption.MaxRobotCommandLength && robot != null)
                    {
                        //TODO Refactor for incapsulated choosing command type
                        for(int i = 0; i < myString.Length; ++i)
                        {
                            if (myString[i] == 'L' || myString[i] == 'R')
                                if (!RobotTurn(myString[i].ToString(), robot))
                                    break;
                                else
                                    if (!RobotMove(myString[i].ToString(), robot))
                                        break;
                        }

                        res.AppendLine(robot.ToString());
                    }
                }
                else
                {
                    if (myString.Length == 2)
                        grid = GridInitialize(myString);

                    if (myString.Length == 3 && grid != null)
                        robot = RobotActivate(myString, grid);
                }
            }

            return new CommandResult { Result = res.ToString() };
        }
    }
}
