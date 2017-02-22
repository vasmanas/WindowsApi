namespace WindowsApi.Core.Extensions
{
    public static class IntExtensions
    {
        public static short HighWord(this uint number)
        {
            return ((short)(number >> 16));
        }

        public static short LowWord(this uint number)
        {
            return ((short)(number & 0xffff));
        }
    }
}
