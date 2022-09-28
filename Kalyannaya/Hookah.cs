public record Session
{
    public DateTime Start;
    public DateTime End;
    public Customer Customer;
    public uint TableNumber;
    public Shisha Shisha;
}

public class Hookah
{
    public static string CompanyName = "Туманные перспективы";
    public string Adress { get; init; }
    public uint CapacityOrdinary { get; private init; }
    public uint CapacityVip { get; private init; }
    public uint WorkloadOrdinary { get; set; }
    public uint WorkloadVip { get; set; }
    Dictionary<Shisha, uint?> Prices { get; set; }
    Dictionary<Shisha, uint> ShishaAmount { get; set; }
    Dictionary<Shisha, uint> ShishaAvailability { get; set; }
    List<Session> Sessions { get; set; }

    public Hookah(string adress)
    {
        Adress = adress;
        CapacityOrdinary = 0;
        CapacityVip = 0;
        WorkloadOrdinary = 0;
        WorkloadVip = 0;
        Prices = new Dictionary<Shisha, uint?>();
        ShishaAmount = new Dictionary<Shisha, uint>();
        ShishaAvailability = new Dictionary<Shisha, uint>();
    }

    public Hookah(string adress, uint capacityOrdinary, uint capacityVip) : this(adress)
    {
        CapacityOrdinary = capacityOrdinary;
        CapacityVip = capacityVip;
    }

    public Hookah(
        string adress,
        uint capacityOrdinary,
        uint capacityVip,
        Dictionary<Shisha, uint?> prices,
        Dictionary<Shisha, uint> shishaAmount,
        Dictionary<Shisha, uint> shishaAvailability) : this(adress, capacityOrdinary, capacityVip)
    {
        CapacityOrdinary = capacityOrdinary;
        CapacityVip = capacityVip;

        if (
            new List<Shisha>(prices.Keys) == new List<Shisha>(shishaAmount.Keys)
            &&
            new List<Shisha>(shishaAmount.Keys) == new List<Shisha>(shishaAvailability.Keys)
        )
        {
            Prices = prices;
            ShishaAmount = shishaAmount;
            ShishaAvailability = shishaAvailability;
        }
        else
        {
            throw new Exception("У тебя не совпадают ключи");
        }
    }
    public bool AddSession(Session session)
    {
        var shisha = session.Shisha;
        if (ShishaAvailability.ContainsKey(shisha))
        {
            if (ShishaAvailability[shisha] > 0)
            {
                Sessions.Add(session);
                ShishaAvailability[shisha]--;
                return true;
            }
            return false;

        }
        return false;
    }
    public bool SetPrice(Shisha shisha, uint price)
    {
        if (!Prices.ContainsKey(shisha)) return false;
        Prices[shisha] = price;
        return true;
    }
    public void AddShisha(Shisha shisha, uint amount)
    {
        if (!ShishaAmount.ContainsKey(shisha))
        {
            ShishaAmount[shisha] = 0;
            ShishaAvailability[shisha] = 0;
            Prices[shisha] = null;
        }
        ShishaAmount[shisha] += amount;
        ShishaAvailability[shisha] += amount;
    }
}

