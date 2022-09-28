public class Item
{
    public string Name { get; set; }
    public ulong Article { get; init; }
    public Item(string name, ulong article)
    {
        Name = name;
        article = Article;
    }
}
public enum Material
{
    Undefined = 0,
    Латунь = 1,
    Медь = 2,
    Нержавейка = 3,
    Стекло = 4,
}

public class Shisha : Item
{
    public Material Material { get; init; }
    public Shisha(string name, ulong article, Material material) : base(name, article)
    {
        Material = material;
    }
}

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
    public Alcohol(string name, uint article) : base(name, article)
    {

    }
}

