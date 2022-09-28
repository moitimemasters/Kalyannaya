using System.Text.Json;

public class Item
{
    public string Name { get; set; }
    public ulong Article { get; init; }
    public Item(string name, ulong article)
    {
        Name = name;
        Article = article;
    }

    public override string ToString()
    => JsonSerializer.Serialize(this);
}