using Xunit;

namespace CubicSnake.Core.UnitTests
{
    public class FigureFactoryTests
    {
        [Fact]
        public void GenerateFromSegmentLengthsSequence_TwoSegmentSequense_GenerateFigureByXYPlot()
        {
            var figure1 = new FigureFactory().GenerateFromSegmentLengthsSequence(new[] {4, 3});
            var figure2 = new Figure(new []
            {
                new Segment(new Point(0, 0, 0), new Point(3, 0, 0)),
                new Segment(new Point(3, 0, 0), new Point(3, 2, 0))
            });
            
            Assert.Equal(figure1, figure2);
        }
    }
}