using NUnit.Framework;
using Pathfinding;

namespace PathfindingTests
{
    [TestFixture]
    public class GridTest
    {
        private int[][] _matrix;

        [SetUp]
        public void SetUp()
        {
            _matrix = new[]
            {
                new [] {1, 0, 0 ,1},
                new [] {0, 1, 0 ,0},
                new [] {0, 1, 0 ,0},
                new [] {0, 0, 0 ,0},
                new [] {1, 0, 0 ,1}
            };
        }

        [TearDown]
        public void TearDown()
        {
            _matrix = null;
        }

        [Test]
        public void Test_GenerateGridWithoutMatrix()
        {
            const int columns = 10;
            const int rows = 10;

            var grid = new Grid(columns, rows);

            Assert.AreEqual(columns, grid.Columns);
            Assert.AreEqual(rows, grid.Rows);
        }

        [Test]
        public void Test_GenerateGridWithMatrix()
        {
            var columns = _matrix[0].Length;
            var rows = _matrix.Length;
            var grid = new Grid(columns, rows, _matrix);

            Assert.AreEqual(columns, grid.Columns);
            Assert.AreEqual(rows, grid.Rows);
        }

        [Test]
        public void Test_GenerateGridWithMatrixOnly()
        {
            var columns = _matrix[0].Length;
            var rows = _matrix.Length;
            var grid = new Grid(_matrix);

            Assert.AreEqual(columns, grid.Columns);
            Assert.AreEqual(rows, grid.Rows);
        }

        [Test]
        public void Test_AllNodesInitialisedAreWalkable()
        {
            const int columns = 10;
            const int rows = 10;

            var grid = new Grid(columns, rows);

            for (var i = 0; i < rows - 1; i++)
            {
                for (var j = 0; j < columns - 1; j++)
                {
                    Assert.IsTrue(grid.IsWalkableAt(i, j));
                }
            }
        }

        [Test]
        public void Test_SetNodeWalkableAttribute()
        {
            const int columns = 10;
            const int rows = 10;

            var grid = new Grid(columns, rows);

            Assert.IsTrue(grid.IsWalkableAt(0, 0));
            Assert.IsTrue(grid.IsWalkableAt(4, 6));

            grid.SetWalkableAt(4, 6, false);
            grid.SetWalkableAt(9, 9, false);

            Assert.IsFalse(grid.IsWalkableAt(4, 6));
            Assert.IsFalse(grid.IsWalkableAt(9, 9));

            grid.SetWalkableAt(9, 9, true);

            Assert.IsTrue(grid.IsWalkableAt(9, 9));
        }

        [Test]
        public void Test_ReturnCorrectAnswerForValidPositionQuery()
        {
            var columns = _matrix[0].Length;
            var rows = _matrix.Length;
            var grid = new Grid(columns, rows, _matrix);

            var asserts = new[]
            {
                new object[] {0, 0, true},
                new object[] {0, rows - 1, true},
                new object[] {columns - 1, 0, true},
                new object[] {columns - 1, rows - 1, true},
                new object[] {-1, -1, false},
                new object[] {0, -1, false},
                new object[] {-1, 0, false},
                new object[] {0, rows, false},
                new object[] {columns, 0, false},
                new object[] {columns, rows, false}
            };

            foreach (var assert in asserts)
            {
                Assert.AreEqual(grid.IsInside((int)assert[0], (int)assert[1]), assert[2]);
            }
        }
    }
}
