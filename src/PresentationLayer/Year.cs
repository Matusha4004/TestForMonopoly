namespace PresentationLayer;

internal record Year
{
    public int Value { get; init; }

    public Year(int value)
    {
        if (value > 0 && value <= 9999)
        {
            Value = value;
        }
        else
        {
            throw new ArgumentException("Value must be between 0 and 9999");
        }
    }
}