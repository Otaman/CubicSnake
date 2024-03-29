﻿using System;
using System.Linq;

namespace CubicSnake.Core
{
    public class Figure : IEquatable<Figure>, I3DTranslatable<Figure>
    {
        public Figure(Segment[] segments)
        {
            Segments = segments;
        }

        public Segment[] Segments { get; }

        public bool FitInSpace(Space space)
        {
            var previousSegment = new Segment(new Point(0, 0, 0), new Point(0, 0, 1));
            
            var tailOfFirstSegment = Segments[0].Tail;
            space.Insert(tailOfFirstSegment);

            return FitSegmentsInSpace(space, previousSegment, 0);
        }

        public long Iterations = 0;
        public long SingleSegmentRotations = 0;
        public int Depth = 0;

        private bool FitSegmentsInSpace(Space space, Segment previousSegment, int indexOfCurrentSegment)
        {
            Iterations++;
            Depth = Math.Max(Depth, indexOfCurrentSegment);
            
            if (indexOfCurrentSegment >= Segments.Length)
                return true;

            var segment = Segments[indexOfCurrentSegment];
            var nextSegmentsDeferredRotations = 0;
            for (int rotation = 0; rotation < 4; rotation++)
            {
                SingleSegmentRotations++;
                
                if (FitSegmentInSpace(space, segment))
                {
                    for (; nextSegmentsDeferredRotations > 0; nextSegmentsDeferredRotations--)
                    {
                        RotateNextSegments(previousSegment, indexOfCurrentSegment);
                    }

                    if (FitSegmentsInSpace(space, segment, indexOfCurrentSegment + 1))
                    {
                        Segments[indexOfCurrentSegment] = segment;
                        return true;
                    }
                    
                    //if segments after can not fit in the space,
                    //then clear current segment from space and do next rotation

                    RemoveSegmentFromSpace(space, segment);
                }

                segment = segment.Rotate(previousSegment, 90);
                nextSegmentsDeferredRotations++;
            }

            nextSegmentsDeferredRotations = nextSegmentsDeferredRotations % 4;
            for (; nextSegmentsDeferredRotations > 0; nextSegmentsDeferredRotations--)
            {
                RotateNextSegments(previousSegment, indexOfCurrentSegment);
            }
            
            return false;
        }

        private void RemoveSegmentFromSpace(Space space, Segment segment)
        {
            //from head to tail
            var points = segment.GetPoints();

            for (int i = 0; i < points.Length - 1; i++) // without tail point
            {
                space.Remove(points[i]);
            }
        }

        private void RotateNextSegments(Segment previousSegment, int indexOfCurrentSegment)
        {
            for (int i = indexOfCurrentSegment + 1; i < Segments.Length; i++)
            {
                Segments[i] = Segments[i].Rotate(previousSegment, 90);
            }
        }

        private bool FitSegmentInSpace(Space space, Segment segment)
        {
            //from head to tail
            var points = segment.GetPoints();

            for (int i = 0; i < points.Length - 1; i++) // without tail point
            {
                if (space.Insert(points[i]))
                {
                    continue; // try insert next
                }

                //rollback
                i--;
                for (; i >= 0; i--)
                {
                    space.Remove(points[i]);
                }

                return false;
            }

            return true;
        }

        public bool Equals(Figure other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Segments.SequenceEqual(other.Segments);
        }

        public Figure Translate(Point point)
        {
            for (int i = 0; i < Segments.Length; i++)
            {
                Segments[i] = Segments[i].Translate(point);
            }

            return this;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Figure) obj);
        }

        public override int GetHashCode()
        {
            return (Segments != null ? Segments.GetHashCode() : 0);
        }

        public bool IsValid()
        {
            for (int i = 0; i < Segments.Length - 1; i++)
            {
                var currentSegment = Segments[i];
                var nextSegment = Segments[i+1];

                if (currentSegment.Head != nextSegment.Tail)
                {
                    return false;
                }
            }

            return true;
        }
    }
}