using System.Runtime.InteropServices;

namespace WindowsApi.Core.Native
{
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int x;
        public int y;
    }
}
