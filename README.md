# WindowsApi
A set of classes and methods for windows native libraries API.

# Usage

In WinApiMethods.cs there are API methods used by this library.

In WinKeyboardState.cs few methods for keyboard keys state cheking.

In Folder "Devices" basic global Keyboard and Mouse hooks, just create a new class, add actions and ready to go:

```
var hook = new Devices.DelegateMouseGlobalHook();

hook.MouseMove +=
    (x, y) =>
    {
        System.Diagnostics.Debug.WriteLine("{0}:{1}", x, y);
        System.Console.WriteLine("{0}:{1}", x, y);
    };
```