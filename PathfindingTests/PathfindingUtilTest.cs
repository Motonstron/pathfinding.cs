using System.Collections.Generic;
using NUnit.Framework;
using Pathfinding;
using Pathfinding.Utils;

namespace PathfindingTests
{
    [TestFixture]
    public class PathfindingUtilTest
    {
        private NodeComparer _comparer;

        [SetUp]
        public void SetUp()
        {
            _comparer = new NodeComparer();   
        }

        [TearDown]
        public void TearDown()
        {
            _comparer = null;
        }

        [Test]
        public void Test_ReturnCorrectInterpolatedPath()
        {
            var actualPath = PathfindingUtils.Interpolate(0, 1, 0, 4);
            var expectedPath = new List<INode>
            {
                new Node(0, 1),
                new Node(0, 2),
                new Node(0, 3),
                new Node(0, 4)
            };

            CollectionAssert.AreEqual(expectedPath, actualPath, _comparer);
        }

        [Test]
        public void Test_ExpandPathReturnsEmptyIfGivenEmpty()
        {
            var actualPath = PathfindingUtils.ExpandPath(new List<INode>());
            CollectionAssert.IsEmpty(actualPath);
        }

        [Test]
        public void Test_ReturnCorrectExpandedPath()
        {
            var originalNodes = new List<INode>
            {
                new Node(0, 1),
                new Node(0, 4)
            };

            var expectedPath = new List<INode>
            {
                new Node(0, 1),
                new Node(0, 2),
                new Node(0, 3),
                new Node(0, 4)
            };

            var actualPath = PathfindingUtils.ExpandPath(originalNodes);
            CollectionAssert.AreEqual(expectedPath, actualPath, _comparer);

            originalNodes = new List<INode>
            {
                new Node(0, 1),
                new Node(0, 4),
                new Node(2, 6)
            };

            expectedPath = new List<INode>
            {
                new Node(0, 1),
                new Node(0, 2),
                new Node(0, 3),
                new Node(0, 4),
                new Node(1, 5),
                new Node(2, 6)
            };

            actualPath = PathfindingUtils.ExpandPath(originalNodes);
            CollectionAssert.AreEqual(expectedPath, actualPath, _comparer);
        }

        [Test]
        public void Test_CompressPathReturnsEmptyIfGivenEmpty()
        {
            var actualPath = PathfindingUtils.CompressPath(new List<INode>());
            CollectionAssert.IsEmpty(actualPath);
        }

        [Test]
        public void Test_ReturnCorrectCompressedPath()
        {
            var originalNodes = new List<INode>
            {
                new Node(0, 1),
                new Node(0, 2),
                new Node(0, 3),
                new Node(0, 4)
            };

            var expectedPath = new List<INode>
            {
                new Node(0, 1),
                new Node(0, 4)
            };

            var actualPath = PathfindingUtils.CompressPath(originalNodes);
            CollectionAssert.AreEqual(expectedPath, actualPath, _comparer);

            originalNodes = new List<INode>
            {
                new Node(0, 1),
                new Node(0, 2),
                new Node(0, 3),
                new Node(0, 4),
                new Node(1, 5),
                new Node(2, 6)
            };

            expectedPath = new List<INode>
            {
                new Node(0, 1),
                new Node(0, 4),
                new Node(2, 6)
            };

            actualPath = PathfindingUtils.CompressPath(originalNodes);
            CollectionAssert.AreEqual(expectedPath, actualPath, _comparer);
        }
    }
}