using Xunit;

namespace CubicSnake.Core.UnitTests
{
    public class PointTests
    {
        [Fact]
        public void Construtor_InputParameters_IsEqualToCoordinates()
        {
            var point = new Point(1, 2, 3);
            
            Assert.Equal(point.X, 1);
            Assert.Equal(point.Y, 2);
            Assert.Equal(point.Z, 3);
        }

        [Fact]
        public void Equals_TwoSamePoints_IsEquals()
        {
            var point1 = new Point(1, 1, 1);
            var point2 = new Point(1, 1, 1);
            
            Assert.Equal(point1, point2);
        }
        
        [Fact]
        public void Equals_TwoDifferentPoints_IsNotEquals()
        {
            var point1 = new Point(1, 1, 1);
            var point2 = new Point(2, 2, 2);
            
            Assert.NotEqual(point1, point2);
        }
        
        [Fact]
        public void EqualsOperator_TwoSamePoints_ReturnsTrue()
        {
            var point1 = new Point(1, 1, 1);
            var point2 = new Point(1, 1, 1);
            
            Assert.True(point1 == point2);
        }
        
        [Fact]
        public void EqualsOperator_TwoDifferentPoints_ReturnsFalse()
        {
            var point1 = new Point(1, 1, 1);
            var point2 = new Point(2, 2, 2);
            
            Assert.True(point1 != point2);
        }
    }
}