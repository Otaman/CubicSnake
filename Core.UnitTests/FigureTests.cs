using Xunit;

namespace CubicSnake.Core.UnitTests
{
    public class FigureTests
    {
        [Fact]
        public void Equals_TwoSameFigures_IsEquals()
        {
            var figure1 = new Figure(new []{new Segment(new Point(0, 0, 0), new Point(1, 0, 0))});
            var figure2 = new Figure(new []{new Segment(new Point(0, 0, 0), new Point(1, 0, 0))});
            
            Assert.Equal(figure1, figure2);
        }
        
        [Fact]
        public void FitInSpace_OneSegmentFigure_FitInSpace()
        {
            var space = new Space(1, 2, 1);
            var figure = new Figure(new []{new Segment(new Point(0, 0, 0), new Point(1, 0, 0))});
            
            Assert.True(figure.FitInSpace(space));
        }
        
        [Fact]
        public void FitInSpace_ThreeSegmentFigure_FitsIn221Space()
        {
            var space = new Space(2, 2, 1);
            var figure = new Figure(new []
            {
                new Segment(new Point(0, 0, 0), new Point(1, 0, 0)),
                new Segment(new Point(1, 0, 0), new Point(1, 1, 0)),
                new Segment(new Point(1, 1, 0), new Point(0, 1, 0))
            });
            
            Assert.True(figure.FitInSpace(space));
        }
        
        [Fact]
        public void FitInSpace_ThreeSegmentNotFittedFigure_FitsIn221Space()
        {
            var space = new Space(2, 2, 1);
            var figure = new Figure(new []
            {
                new Segment(new Point(0, 0, 0), new Point(1, 0, 0)),
                new Segment(new Point(1, 0, 0), new Point(1, 1, 0)),
                new Segment(new Point(1, 1, 0), new Point(1, 1, 1))
            });
            
            Assert.True(figure.FitInSpace(space));
        }
        
        [Fact]
        public void FitInSpace_ThreeSegmentNotFittedFigure_FitsIn331Space()
        {
            var space = new Space(3, 3, 1);
            var figure = new Figure(new []
            {
                new Segment(new Point(0, 0, 0), new Point(2, 0, 0)),
                new Segment(new Point(2, 0, 0), new Point(2, 0, 2)),
                new Segment(new Point(2, 0, 2), new Point(2, -2, 2)),
                new Segment(new Point(2, -2, 2), new Point(3, -2, 2))
            });
            
            var result = figure.FitInSpace(space);
            
            Assert.True(result);
        }
    }
}