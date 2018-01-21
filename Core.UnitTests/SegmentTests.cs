using System;
using Xunit;

namespace CubicSnake.Core.UnitTests
{
    public class SegmentTests
    {
        [Fact]
        public void Constructor_EqualsPoints_ThrowException()
        {
            var point1 = new Point(1,1,1);
            var point2 = new Point(1,1,1);

            Assert.Throws<ArgumentException>(() => { new Segment(point1, point2); });
        }
    }
}