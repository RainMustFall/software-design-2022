using FluentAssertions;
using NUnit.Framework;
using Roguelike.Controllers;
using Roguelike.Core.Abstractions.Map;
using Roguelike.Map;
using Roguelike.Map.Cells;

namespace Roguelike_Tests.Controllers;

[TestFixture]
public class MapController_Tests
{
    private MapController mapController;
    private Map map;
    private readonly TestCell testCell = new(1, 1);

    [SetUp]
    public void TestSetup()
    {
        map = new Map(5, 5, false);
        mapController = new MapController(map);
        map.Cells[1, 1].PutCell(testCell);
    }

    [Test]
    public void Should_ReturnTrue_IfMoveIsPossible()
    {
        mapController.Move(testCell, 2, 2).Should().BeTrue();
        map.Cells[1, 1].Empty().Should().BeTrue();
        map.Cells[2, 2].Empty().Should().BeFalse();
    }

    [Test]
    public void Should_ReturnFalse_IfOutOfBounds()
    {
        mapController.Move(testCell, -1, 0).Should().BeFalse();
        map.Cells[1, 1].Empty().Should().BeFalse();
        map.Cells[2, 2].Empty().Should().BeTrue();
    }

    [Test]
    public void Should_ReturnFalse_WhenMovingIntoAWall()
    {
        map.Cells[2, 2].PutCell(new WallCell(2, 2));
        mapController.Move(testCell, -1, 0).Should().BeFalse();
        map.Cells[1, 1].Empty().Should().BeFalse();
    }

    private class TestCell : ICell
    {
        public TestCell(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }
        public char Render()
        {
            throw new System.NotImplementedException();
        }
    }
}