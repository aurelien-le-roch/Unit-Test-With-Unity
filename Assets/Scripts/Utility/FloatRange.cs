public struct FloatRange
{
    public readonly float Min;
    public readonly float Max;

    public FloatRange(float min, float max)
    {
        Min = min;
        Max = max;
    }

    public bool InRange(float value) => value >= Min && value <= Max;
}