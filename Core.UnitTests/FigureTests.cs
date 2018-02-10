using System;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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
        
        [Fact]
        public void FitInSpace_MishasFigure_FitsIn444Space()
        {
            var space = new Space(4, 4, 4);
            int[] segmentLengths = {4,2,4,2,2,2,2,3,2,2,2,2,2,3,2,4,2,3,3,4,2,3,2,2,2,2,2,2,2,2,2,4,2,4,2,4,4,4,3};
            segmentLengths = segmentLengths.Reverse().ToArray();
            var figure = new FigureFactory().GenerateFromSegmentLengthsSequence(segmentLengths);
            figure.Translate(new Point(1, 0, 0));
            
            var sw = new Stopwatch();
            sw.Start();
            
            var result = figure.FitInSpace(space);

            var miliseconds = sw.ElapsedMilliseconds;
            var valid = figure.IsValid();

            var spaceIsFull = true;
            for (int i = 0; i < space.Cells.GetLength(0); i++)
            {
                for (int j = 0; j < space.Cells.GetLength(1); j++)
                {
                    for (int k = 0; k < space.Cells.GetLength(2); k++)
                    {
                        spaceIsFull = spaceIsFull && space.Cells[i, j, k];
                    }
                }
            }
            
            var maxPossibleCombinations = BigInteger.Pow(new BigInteger(4), segmentLengths.Length) * 4 * 3;
            double efectiveness = Math.Exp(BigInteger.Log(figure.Iterations) - BigInteger.Log(maxPossibleCombinations));

            var sb = new StringBuilder();
            sb.AppendLine("Figure:");
            for (int i = 0; i < figure.Segments.Length; i++)
            {
                sb.AppendLine(figure.Segments[i].ToString());
            }

            var json = JsonConvert.SerializeObject(figure, new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            sb.AppendLine(json);
            
            Debug.WriteLine($"Space is full: {spaceIsFull}");
            Debug.WriteLine($"Figure is valid: {valid}");
            Debug.WriteLine($"Figure fits in space: {result}");
            Debug.WriteLine($"Total single segment rotations: {figure.SingleSegmentRotations}");
            Debug.WriteLine($"Max depth of recursive rotations: {figure.Depth}");
            Debug.WriteLine($"Count of FitSegmentsInSpace method calls: {figure.Iterations}");
            Debug.WriteLine($"Max count of possible FitSegmentsInSpace method calls: {maxPossibleCombinations}");
            Debug.WriteLine($"Optimisation (best = 1.0): {1 - efectiveness}");
            Debug.WriteLine($"Elapsed ms: {miliseconds}");
            Debug.WriteLine("---------------");
            Debug.WriteLine(sb.ToString());
            
            Assert.True(result);
        }
    }
}