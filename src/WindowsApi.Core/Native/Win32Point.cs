using System;
using System.Runtime.InteropServices;

namespace WindowsApi.Core.Native
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Win32Point
    {
        public Int32 X;
        public Int32 Y;
    };
}
