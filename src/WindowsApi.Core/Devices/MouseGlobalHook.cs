using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using WindowsApi.Core.Extensions;
using WindowsApi.Core.Native;

namespace WindowsApi.Core.Devices
{
    public abstract class MouseGlobalHook : IDisposable
    {
        private LowLevelProc _proc;
        private IntPtr _hookID = IntPtr.Zero;

        public MouseGlobalHook()
        {
            this._proc = this.HookCallback;
            this._hookID = this.SetHook(this._proc);
        }

        public virtual void Dispose()
        {
            WinApiMethods.UnhookWindowsHookEx(this._hookID);
        }

        protected abstract void Move(POINT pos);

        protected abstract void LeftButtonDown(POINT pos);

        protected abstract void LeftButtonUp(POINT pos);

        protected abstract void RightButtonDown(POINT pos);

        protected abstract void RightButtonUp(POINT pos);

        protected abstract void WheelMove(int delta);

        private IntPtr SetHook(LowLevelProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return WinApiMethods.SetWindowsHookEx((int)HookType.WH_MOUSE_LL, proc, WinApiMethods.GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                var hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));

                switch ((MouseMessages)wParam)
                {
                    case MouseMessages.WM_MOUSEMOVE:
                        {
                            this.Move(hookStruct.pt);

                            break;
                        }

                    case MouseMessages.WM_LBUTTONDOWN:
                        {
                            this.LeftButtonDown(hookStruct.pt);

                            break;
                        }

                    case MouseMessages.WM_LBUTTONUP:
                        {
                            this.LeftButtonUp(hookStruct.pt);

                            break;
                        }

                    case MouseMessages.WM_RBUTTONDOWN:
                        {
                            this.RightButtonDown(hookStruct.pt);

                            break;
                        }

                    case MouseMessages.WM_RBUTTONUP:
                        {
                            this.RightButtonUp(hookStruct.pt);

                            break;
                        }

                    case MouseMessages.WM_MOUSEWHEEL:
                        {
                            // delta > 0 - wheel up
                            // delta < 0 - wheel down
                            this.WheelMove(hookStruct.mouseData.HighWord());

                            break;
                        }
                }
            }

            return WinApiMethods.CallNextHookEx(_hookID, nCode, wParam, lParam);
        }
    }
}
