using MartianRobotsService.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobotsTests.ModelsTests
{
    class SimpleMartianRobotTests
    {
        private _2DCoordinate _coordinate;
        private SimpleMartianRobot _robot;
        private RectangularGrid _grid;

        [SetUp]
        public void Setup()
        {
            _coordinate = new _2DCoordinate(4, 1);
            _grid = new RectangularGrid(4, 7);
            _robot = new SimpleMartianRobot(_coordinate, new _2DDirection(90), _grid);
        }

        [Test]
        public void IsRobotLost()
        {
            // Arrange

            // Act
            _robot.Move(1);
            var result = _robot.Grid.IsCoordinateScented(_coordinate);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsRobotMoveAroundNearTheEdgeAndNotLost()
        {
            // Arrange
            _robot.Turn(90);
            _robot.Move(1);
            _robot.Turn(90);
            _robot.Move(1);
            _robot.Turn(90);
            _robot.Move(1);
            _robot.Turn(90);
            _robot.Move(1);

            // Act
            var result = _robot.Grid.IsCoordinateScented(_coordinate);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void RobotNotLostAfterPreviousLostSamePoint()
        {
            // Arrange
            var robot = new SimpleMartianRobot(_coordinate, new _2DDirection(90), _grid);

            // Act
            _robot.Move(1);
            robot.Move(5);

            // Assert
            Assert.IsFalse(robot.isLost);
        }
    }
}
