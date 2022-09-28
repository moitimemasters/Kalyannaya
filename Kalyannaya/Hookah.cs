using System.Text.Json;

public class Hookah
{
    public static string CompanyName = "Туманные перспективы";
    public static float VIPExtraCharge = 1.4f;
    public string Address { get; init; }
    public uint CapacityOrdinary { get; private init; }
    public uint CapacityVip { get; private init; }
    public uint WorkloadOrdinary { get; set; }
    public uint WorkloadVip { get; set; }

    public OneTimePurchaseItemManager Shop { get; set; }
    public SessionItemManager Shishas { get; set; }

    public Hookah(string address)
    {
        Address = address;
        CapacityOrdinary = 0;
        CapacityVip = 0;
        WorkloadOrdinary = 0;
        WorkloadVip = 0;
        Shop = new OneTimePurchaseItemManager();
        Shishas = new SessionItemManager();
    }

    public Hookah
    (
        string adress,
        uint capacityOrdinary,
        uint capacityVip
    ) : this(adress)
    {
        CapacityOrdinary = capacityOrdinary;
        CapacityVip = capacityVip;
    }

    public Hookah
    (
        string adress,
        uint capacityOrdinary,
        uint capacityVip,
        OneTimePurchaseItemManager shop,
        SessionItemManager shishas
    ) : this(adress, capacityOrdinary, capacityVip)
    {
        CapacityOrdinary = capacityOrdinary;
        CapacityVip = capacityVip;
        Shop = shop;
        Shishas = shishas;
    }

    public Check? PurchaseShisha(Customer customer, Shisha shisha, DateTime sessionStart, DateTime sessionEnd, bool vip)
    {
        if (vip)
            if (WorkloadVip + 1 < CapacityVip)
                WorkloadVip++;
            else
                return null;
        else
            if (WorkloadOrdinary + 1 < CapacityOrdinary)
            WorkloadOrdinary++;
        else
            return null;
        var check = Shishas.Purchase(customer, shisha, sessionStart, sessionEnd);
        if (vip)
        {
            if (check is null) return null;
            check.Price *= VIPExtraCharge;
        }
        return check;
    }
    public Check? PurchaseItem(Customer customer, Item item)
    {
        if (item is Shisha) return null;
        return Shop.Purchase(customer, item);
    }
    public override string ToString()
        => JsonSerializer.Serialize(this);
}

