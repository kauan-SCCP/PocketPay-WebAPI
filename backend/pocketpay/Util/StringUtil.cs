public class StringUtil
{
    private static Random random = new Random();
    public static string generateRandom(int size)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, size)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}