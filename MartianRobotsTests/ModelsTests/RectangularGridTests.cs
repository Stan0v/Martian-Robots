using MartianRobotsService.BaseClasses;
using MartianRobotsService.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MartianRobotsTests.ModelsTests
{
    class RectangularGridTests
    {
        private RectangularGrid _grid;

        [SetUp]
        public void Setup()
        {
            _grid = new RectangularGrid(4, 7);            
        }

        [Test]
        public void IsCoordinateWithinTest()
        {
            // Arrange
            var coordinate = new _2DCoordinate(3, 5);

            // Act
            var result = _grid.IsCoordinateWithin(coordinate);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsCoordinateOutTest()
        {
            // Arrange
            var coordinate = new _2DCoordinate(5, 1);

            // Act
            var result = _grid.IsCoordinateWithin(coordinate);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void IsRobotAddedOnPoint()
        {
            // Arrange
            var coordinate = new _2DCoordinate(3, 5);
            var robot = new SimpleMartianRobot(coordinate, new _2DDirection(0), _grid);

            // Act
            _grid.AddRobotOnPoint(robot);
            var robotSet = _grid.AcquireRobot(coordinate);

            // Assert
            Assert.AreEqual(robot, robotSet);
        }

        [Test]
        public void IsRobotNotAddedOnPoint()
        {
            // Arrange
            var coordinate = new _2DCoordinate(5, 1);
            var robot = new SimpleMartianRobot(coordinate, new _2DDirection(0), _grid);

            // Act
            _grid.AddRobotOnPoint(robot);
            var robotSet = _grid.AcquireRobot(coordinate);

            // Assert
            Assert.IsNull(robotSet);
        }

        [Test]
        public void IsRobotRemovedPoint()
        {
            // Arrange
            var coordinate = new _2DCoordinate(3, 5);
            var robot = new SimpleMartianRobot(coordinate, new _2DDirection(0), _grid);

            // Act
            _grid.AddRobotOnPoint(robot);
            var robotSet = _grid.AcquireRobot(coordinate);
            _grid.RemoveRobotFromPoint(robot);
            var robotDel = _grid.AcquireRobot(coordinate);

            // Assert
            Assert.AreNotEqual(robotDel, robotSet);
        }

        [Test]
        public void IsCoordinateScent()
        {
            // Arrange
            var coordinate = new _2DCoordinate(3, 5);

            // Act
            _grid.ScenteCoordinate(coordinate);
            var result = _grid.IsCoordinateScented(coordinate);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void IsCoordinateNotScent()
        {
            // Arrange
            var coordinate = new _2DCoordinate(5, 1);

            // Act
            _grid.ScenteCoordinate(coordinate);
            var result = _grid.IsCoordinateScented(coordinate);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
