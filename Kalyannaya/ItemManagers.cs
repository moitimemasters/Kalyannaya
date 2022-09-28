using System.Text.Json;

public record Session
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public Customer Customer { get; set; }
    public Item Item { get; set; }
    public override string ToString()
        => JsonSerializer.Serialize(this);
}

public record Check
{
    public Customer Customer { get; set; }
    public Item Item { get; set; }
    public DateTime Date { get; set; }
    public float Price { get; set; }
    public override string ToString()
        => JsonSerializer.Serialize(this);
}


public class SessionItemManager : OneTimePurchaseItemManager
{
    public List<Session> Sessions { get; set; }
    public SessionItemManager() : base()
    {
        Sessions = new List<Session>();
    }
    public SessionItemManager(
        Dictionary<ulong, float?> prices,
        Dictionary<ulong, uint> itemAmount,
        Dictionary<ulong, uint> itemAvailability)
        : base(prices, itemAmount, itemAvailability)
    {
        Sessions = new List<Session>();
    }
    public Check? Purchase(Customer customer, Item item, DateTime sessionStart, DateTime sessionEnd)
    {
        if (ItemAvailability.ContainsKey(item.Article))
        {
            if (ItemAvailability[item.Article] > 0)
            {
                var session = new Session()
                {
                    Customer = customer,
                    Start = sessionStart,
                    End = sessionEnd,
                    Item = item,
                };
                Sessions.Add(session);
                ItemAvailability[item.Article]--;
                return new Check()
                {
                    Customer = customer,
                    Price = Prices[item.Article].Value,
                    Date = DateTime.Now,
                    Item = item,
                };
            }
            return null;

        }
        return null;
    }
    public void UpdateSessions()
    {
        for (var i = Sessions.Count - 1; i >= 0; --i)
        {
            var session = Sessions[i];
            if (session.End < DateTime.Now)
            {
                ItemAvailability[session.Item.Article] += 1;
                Sessions.RemoveAt(i);
            }
        }
    }
}

public class OneTimePurchaseItemManager
{
    public Dictionary<ulong, float?> Prices { get; set; }
    public Dictionary<ulong, uint> ItemAmount { get; set; }
    public Dictionary<ulong, uint> ItemAvailability { get; set; }
    public OneTimePurchaseItemManager()
    {
        Prices = new Dictionary<ulong, float?>();
        ItemAmount = new Dictionary<ulong, uint>();
        ItemAvailability = new Dictionary<ulong, uint>();
    }


    public OneTimePurchaseItemManager(
        Dictionary<ulong, float?> prices,
        Dictionary<ulong, uint> itemAmount,
        Dictionary<ulong, uint> itemAvailability
    )
    {
        if (
           new List<ulong>(prices.Keys) == new List<ulong>(itemAmount.Keys)
           &&
           new List<ulong>(itemAmount.Keys) == new List<ulong>(itemAvailability.Keys)
        )
        {
            Prices = prices;
            ItemAmount = itemAmount;
            ItemAvailability = itemAvailability;
            return;
        }
        throw new Exception("У тебя не совпадают ключи");
    }

    public bool SetPrice(Item item, float price)
    {
        if (!Prices.ContainsKey(item.Article)) return false;
        Prices[item.Article] = price;
        return true;
    }
    public void AddItem(Item item, uint amount)
    {
        if (ItemAmount.ContainsKey(item.Article) is false)
        {
            ItemAmount[item.Article] = 0;
            ItemAvailability[item.Article] = 0;
            Prices[item.Article] = null;
        }
        ItemAmount[item.Article] += amount;
        ItemAvailability[item.Article] += amount;
    }

    public Check? Purchase(Customer customer, Item item)
    {
        if (ItemAvailability.ContainsKey(item.Article))
        {
            if (ItemAvailability[item.Article] > 0)
            {
                ItemAvailability[item.Article]--;
                return new Check()
                {
                    Customer = customer,
                    Item = item,
                    Date = DateTime.Now,
                    Price = Prices[item.Article].Value,
                };
            }
            return null;
        }
        return null;
    }
    public override string ToString()
        => JsonSerializer.Serialize(this);
}