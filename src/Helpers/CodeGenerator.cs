namespace UrlShortenerApi.Helpers
{
    public static class CodeGenerator
    {
        private static readonly Random _random = new Random();

        private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public static string GenerateShortCode(int length = 6)
        {
            return new string(
                Enumerable.Repeat(_chars, length).Select(s => s[_random.Next(s.Length)]).ToArray()
            );

        }
    }
}