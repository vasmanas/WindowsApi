using System;
using WindowsApi.Core.Native;

namespace WindowsApi.Core.Devices
{
    public class DelegateMouseGlobalHook : MouseGlobalHook
    {
        public event Action<int, int> MouseMove;
        public event Action<int, int> LeftMouseButtonDown;
        public event Action<int, int> LeftMouseButtonUp;
        public event Action<int, int> RightMouseButtonDown;
        public event Action<int, int> RightMouseButtonUp;
        public event Action<int> MouseWheelMove;

        public DelegateMouseGlobalHook() : base()
        {
        }

        public override void Dispose()
        {
            this.MouseMove = null;
            this.LeftMouseButtonDown = null;
            this.LeftMouseButtonUp = null;
            this.RightMouseButtonDown = null;
            this.RightMouseButtonUp = null;
            this.MouseWheelMove = null;

            base.Dispose();
        }

        protected override void Move(POINT pos)
        {
            this.MouseMove?.Invoke(pos.x, pos.y);
        }

        protected override void LeftButtonDown(POINT pos)
        {
            this.LeftMouseButtonDown?.Invoke(pos.x, pos.y);
        }

        protected override void LeftButtonUp(POINT pos)
        {
            this.LeftMouseButtonUp?.Invoke(pos.x, pos.y);
        }

        protected override void RightButtonDown(POINT pos)
        {
            this.RightMouseButtonDown?.Invoke(pos.x, pos.y);
        }

        protected override void RightButtonUp(POINT pos)
        {
            this.RightMouseButtonUp?.Invoke(pos.x, pos.y);
        }

        protected override void WheelMove(int delta)
        {
            this.MouseWheelMove?.Invoke(delta);
        }
    }
}
