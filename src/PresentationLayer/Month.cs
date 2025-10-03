namespace PresentationLayer;

internal record Month
{
    public int Value { get; init; }

    public Month(int value)
    {
        if (value <= 12 && value >= 0)
        {
            Value = value;
        }
        else
        {
            throw new ArgumentException("Value must be between 0 and 12");
        }
    }
}