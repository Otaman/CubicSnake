namespace CubicSnake.Core
{
    public interface I3DRotatable<T>
    {
        T Rotate(Segment axcis, int angle);
    }
}