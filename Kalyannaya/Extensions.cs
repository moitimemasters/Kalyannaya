public static class ObjectExtension
{
    public static class ToJson<T>(this <T> x)
    {
        return JsonSerializer.Serialize(x);
    }
}