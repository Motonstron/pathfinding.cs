using NUnit.Framework;
using Pathfinding;
using Pathfinding.Finders;

namespace PathfindingTests
{
    [TestFixture]
    public class PathTest
    {
        private object[] _asserts;

        [SetUp]
        public void SetUp()
        {
            _asserts = new object[]
            {
                new object[] {0, 0, 1, 1, new []
                {
                    new [] {0, 0},
                    new [] {1, 0}
                }, 3},
                new object[] {1, 1, 4, 4, new []
                {
                    new []{0, 0, 0, 0, 0},
                    new []{1, 0, 1, 1, 0},
                    new []{1, 0, 1, 0, 0},
                    new []{0, 1, 0, 0, 0},
                    new []{0, 0, 1, 1, 0},
                    new []{0, 0, 1, 0, 0}
                }, 9},
                new object[] {0, 3, 3, 3, new []
                {
                    new []{0, 0, 0, 0, 0},
                    new []{0, 0, 1, 1, 0},
                    new []{0, 0, 1, 0, 0},
                    new []{0, 0, 1, 0, 0},
                    new []{1, 0, 1, 1, 0},
                    new []{0, 0, 0, 0, 0}
                }, 10},
                new object[] {4, 4, 19, 19, new []
                {
                    new []{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    new []{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    new []{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    new []{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    new []{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    new []{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    new []{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    new []{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    new []{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    new []{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    new []{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    new []{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    new []{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    new []{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    new []{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    new []{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    new []{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    new []{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    new []{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                    new []{0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
                }, 31}
            };
        }

        [TearDown]
        public void TearDown()
        {
            _asserts = null;
        }

        [Test]
        public void Test_SolvesMaze()
        {
            foreach (object[] assert in _asserts)
            {
                var startX = (int)assert[0];
                var startY = (int)assert[1];
                var endX = (int)assert[2];
                var endY = (int)assert[3];
                var matrix = assert[4] as int[][];
                var expectedLength = (int)assert[5];

                var grid = new Grid(matrix);
                var finder = new AStarPathFinder(false, true, DiagonalMovement.Never);
                var path = finder.FindPath(startX, startY, endX, endY, grid.Clone());

                var actualLength = path.Count;
                Assert.AreEqual(expectedLength, actualLength);
            }
        }
    }
}