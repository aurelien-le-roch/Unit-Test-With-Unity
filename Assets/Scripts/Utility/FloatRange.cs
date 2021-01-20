public struct FloatRange
{
    private readonly float _min;
    private readonly float _max;

    public FloatRange(float min, float max)
    {
        _min = min;
        _max = max;
    }

    public bool InRange(float value) => value >= _min && value <= _max;
}