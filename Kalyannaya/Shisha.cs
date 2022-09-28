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