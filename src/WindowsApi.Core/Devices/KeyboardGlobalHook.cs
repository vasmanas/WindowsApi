using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using WindowsApi.Core.Native;

namespace WindowsApi.Core.Devices
{
    public abstract class KeyboardGlobalHook : IDisposable
    {
        private LowLevelProc _proc;
        private IntPtr _hookID = IntPtr.Zero;

        public KeyboardGlobalHook()
        {
            this._proc = this.HookCallback;
            this._hookID = this.SetHook(this._proc);
        }
        
        public virtual void Dispose()
        {
            WinApiMethods.UnhookWindowsHookEx(this._hookID);
        }

        private IntPtr SetHook(LowLevelProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return WinApiMethods.SetWindowsHookEx((int)HookType.WH_KEYBOARD_LL, proc, WinApiMethods.GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        protected abstract void KeyboardKeyUp(VirtualKeyCode key);

        protected abstract void KeyboardKeyDown(VirtualKeyCode key);

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                var hookStruct = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
                var vkc = (VirtualKeyCode)hookStruct.vkCode;

                //Debug.WriteLine("up   - {0}/{1}/{2}", KeyInterop.KeyFromVirtualKey((int)hookStruct.vkCode), hookStruct.vkCode, vkc);

                switch ((KeyboardMessages)wParam)
                {
                    case KeyboardMessages.WM_KEYDOWN:
                    case KeyboardMessages.WM_SYSKEYDOWN:
                        {
                            this.KeyboardKeyDown(vkc);

                            break;
                        }

                    case KeyboardMessages.WM_KEYUP:
                    case KeyboardMessages.WM_SYSKEYUP:
                        {
                            this.KeyboardKeyUp(vkc);

                            break;
                        }
                }
            }

            return WinApiMethods.CallNextHookEx(_hookID, nCode, wParam, lParam);
        }
    }
}