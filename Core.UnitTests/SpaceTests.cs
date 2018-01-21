using Xunit;

namespace CubicSnake.Core.UnitTests
{
    public class SpaceTests
    {
        [Fact]
        public void Constructor_InputParameters_SetsRightDimentions()
        {
            var space = new Space(1, 1, 1);
            
            Assert.Equal(space.Height, 1);
            Assert.Equal(space.Width, 1);
            Assert.Equal(space.Depth, 1);
        }

        [Fact]
        public void FitsInCells_CorrectPoint_ReturnsTrue()
        {
            var space = new Space(1, 1, 1);
            var point = new Point(0, 0, 0);
            
            Assert.True(space.FitsInCells(point));
        }
        
        [Fact]
        public void FitsInCells_UncorrectPoint_ReturnsFalse()
        {
            var space = new Space(1, 1, 1);
            var point = new Point(1, 1, 1);
            
            Assert.False(space.FitsInCells(point));
        }
        
        [Fact]
        public void Insert_CorrectPointInFreeCell_ReturnsTrue()
        {
            var space = new Space(1, 1, 1);
            var point = new Point(0, 0, 0);
            
            Assert.True(space.Insert(point));
        }
        
        [Fact]
        public void Insert_CorrectPointInUsedCell_ReturnsFalse()
        {
            var space = new Space(1, 1, 1);
            var point = new Point(0, 0, 0);

            space.Insert(point);
            
            Assert.False(space.Insert(point));
        }
        
        [Fact]
        public void Insert_UncorrectPoint_ReturnsFalse()
        {
            var space = new Space(1, 1, 1);
            var point = new Point(1, 1, 1);
            
            Assert.False(space.Insert(point));
        }
    }
}