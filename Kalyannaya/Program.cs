using System.Text.Json;

class Program
{
    public static void Main()
    {
        var shisha1 = new Shisha("Первый Бонк", 11231213, Material.Латунь);
        var shisha2 = new Shisha("Поцелуй звезды", 1154132, Material.Нержавейка);
        var shisha3 = new Shisha("Синее знамя", 3251123, Material.Undefined);
        var Whiskey = new Alcohol("Jack Daniel's, 0.5", 452562345, 40);
        var Oleg = new Customer("Oleeeeg", "Olegov", 228, Sex.SigmaMale);
        var hookah = new Hookah("Ulitsa Puskina, Dom Kolotushkina", 10, 10);
        hookah.Shishas.AddItem(shisha1, 5);
        hookah.Shishas.SetPrice(shisha1, 1000);
        hookah.Shishas.AddItem(shisha2, 10);
        hookah.Shishas.SetPrice(shisha2, 1200);
        hookah.Shishas.AddItem(shisha3, 15);
        hookah.Shishas.SetPrice(shisha3, 1400);
        hookah.Shop.AddItem(Whiskey, 100);
        hookah.Shop.SetPrice(Whiskey, 104.23f);
        _ = hookah.PurchaseShisha(Oleg, shisha1, DateTime.MinValue, DateTime.MinValue, true);
        _ = hookah.PurchaseShisha(Oleg, shisha1, DateTime.MinValue, DateTime.MinValue, true);
        _ = hookah.PurchaseShisha(Oleg, shisha1, DateTime.Now, DateTime.Now, true);
        //Console.WriteLine(hookah);
        Console.WriteLine(JsonExtensions.ToJson<Hookah>(hookah));
        File.WriteAllText("result.json", hookah.ToJson());
    }
}
