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

        [Fact]
        public void Rotate_SegmentOnYOneRightByXAxcis_ReturnsSegmentOnZMinusOne()
        {
            var axcisSegment = new Segment(new Point(0,0,0), new Point(1, 0, 0));
            var inputSegment = new Segment(new Point(0,0,0), new Point(0, 1, 0));

            var resultSegment = inputSegment.Rotate(axcisSegment, 90);
            
            Assert.True(resultSegment.Head == new Point(0, 0, 1) && resultSegment.Tail == new Point(0,0,0));
        }
    }
}