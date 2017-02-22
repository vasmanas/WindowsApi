using System;
using WindowsApi.Core.Native;

namespace WindowsApi.Core.Devices
{
    public class DelegateKeyboardGlobalHook : KeyboardGlobalHook
    {
        public event Action<VirtualKeyCode> KeyUp;
        public event Action<VirtualKeyCode> KeyDown;

        public DelegateKeyboardGlobalHook() : base()
        {
        }

        public override void Dispose()
        {
            this.KeyUp = null;
            this.KeyDown = null;

            base.Dispose();
        }

        protected override void KeyboardKeyUp(VirtualKeyCode key)
        {
            this.KeyUp?.Invoke(key);
        }

        protected override void KeyboardKeyDown(VirtualKeyCode key)
        {
            this.KeyDown?.Invoke(key);
        }
    }
}
