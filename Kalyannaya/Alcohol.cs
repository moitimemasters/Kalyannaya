public class Alcohol : Item
{
    uint _percentage;
    public uint Percentage
    {
        get => _percentage; private set
        {
            if (value > 100) throw new ArgumentException("Странный у тебя алкоголь");
            _percentage = value;
        }
    }
    public Alcohol(string name, uint article, uint percentage) : base(name, article)
    {
        Percentage = percentage;
    }
}