using System.Runtime.InteropServices;
namespace auto_desktop.Classes
{
    internal class Actions
    {

        private static Dictionary<string, Action<int>> InitialiseActions()
        {
            return MouseActions.Concat(UtilityActions).Concat(DelayActions).Concat(FunctionActions).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        private static Action<int> DelayLambda(int milliseconds)
        {
            return (multiplier) => { Thread.Sleep(milliseconds * multiplier); };
        }

        private static Action<int> KeyEventLambda(string code)
        {
            return (_) => { SendKeys.SendWait(code); };
        }

        private static Action<int> MouseMovementLambda(int x, int y)
        {
            return (multiplier) => { Cursor.Position = new Point(Cursor.Position.X + (x * multiplier), Cursor.Position.Y + (y * multiplier)); };
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        private const int MOUSEEVENTF_MIDDLEDOWN = 0x20;
        private const int MOUSEEVENTF_MIDDLEUP = 0x40;


        private static readonly Dictionary<string, Action<int>> DelayActions = NewDelayActions();

        private static Dictionary<string, Action<int>> NewDelayActions()
        {
            return new Dictionary<string, Action<int>>
            {
                {"Wait 1 millisecond", DelayLambda(1)},
                {"Wait 1 second", DelayLambda(1000)},
                {"Wait 1 minute", DelayLambda(1000*60)},
                {"Wait 1 hour", DelayLambda(1000 * 60 * 60)}
            };
        }

        private static readonly Dictionary<string, Action<int>> FunctionActions = NewFunctionActions();

        private static Dictionary<string, Action<int>> NewFunctionActions()
        {
            Dictionary<string, Action<int>> functionActions = [];
            for (int i = 1; i < 13; i++)
                functionActions.Add($"F{i}", KeyEventLambda($"{{F{i}}}"));
            return functionActions;
        }

        private static readonly Dictionary<string, Action<int>> UtilityActions = NewUtilityActions();

        private static Dictionary<string, Action<int>> NewUtilityActions()
        {
            return new Dictionary<string, Action<int>>
            {
                {"Space", KeyEventLambda(" ")},
                {"Enter", KeyEventLambda("{ENTER}")},
                {"Up", KeyEventLambda("{UP}")},
                {"Down", KeyEventLambda("{DOWN}")},
                {"Left", KeyEventLambda("{LEFT}")},
                {"Right", KeyEventLambda("{RIGHT}")},
                {"Tab", KeyEventLambda("{TAB}")},
                {"Backspace", KeyEventLambda("{BACKSPACE}")},
                {"Delete", KeyEventLambda("{DELETE}")},
                {"Home", KeyEventLambda("{HOME}")},
                {"End", KeyEventLambda("{END}")},
                {"Page Up", KeyEventLambda("{PGUP}")},
                {"Page Down", KeyEventLambda("{PGDN}")},
                {"Escape", KeyEventLambda("{ESC}")},
            };
        }

        private static readonly Dictionary<string, Action<int>> MouseActions = NewMouseActions();

        private static Dictionary<string, Action<int>> NewMouseActions()
        {
            return NewMouseClickActions().Concat(NewMouseMovementActions()).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        private static readonly Dictionary<string, Action<int>> MouseMovementActions = NewMouseMovementActions();

        private static Dictionary<string, Action<int>> NewMouseMovementActions()
        {
            return new Dictionary<string, Action<int>>
            {
                {"Mouse 1px left", MouseMovementLambda(-1, 0)},
                {"Mouse 1px right", MouseMovementLambda(1, 0)},
                {"Mouse 1px up", MouseMovementLambda(0, -1)},
                {"Mouse 1px down", MouseMovementLambda(0, 1)},
            };
        }

        private static readonly Dictionary<string, Action<int>> MouseClickActions = NewMouseClickActions();

        private static Dictionary<string, Action<int>> NewMouseClickActions()
        {
            return new Dictionary<string, Action<int>>
            {
                {"Left click", (_) => {
                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                }},
                {"Right click", (_) => {
                    mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                    mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                }},
                {"Middle click", (_) => {
                    mouse_event(MOUSEEVENTF_MIDDLEDOWN, 0, 0, 0, 0);
                    mouse_event(MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
                }},
            };
        }

        private static readonly Dictionary<string, Action<int>> MouseScrollActions = NewMouseScrollActions();

        private static Dictionary<string, Action<int>> NewMouseScrollActions()
        {
            return new Dictionary<string, Action<int>>
            {
                {"Scroll up", (_) => {
                    mouse_event(0x0800, 0, 0, 120, 0);
                }},
                {"Scroll down", (_) => {
                    mouse_event(0x0800, 0, 0, -120, 0);
                }},
            };
        }

        public static bool IsAction(string actionName)
        {
            return _Actions.ContainsKey(actionName);
        }

        public static bool IsMouseAction(string actionName)
        {
            return MouseActions.ContainsKey(actionName);
        }

        public static bool IsMouseMovementAction(string actionName)
        {
            return MouseMovementActions.ContainsKey(actionName);
        }

        public static bool IsDelayAction(string actionName)
        {
            return DelayActions.ContainsKey(actionName);
        }

        private static readonly Dictionary<string, Action<int>> _Actions = InitialiseActions();

        public static Dictionary<string, Action<int>> GetActions()
        {
            return _Actions;
        }

        public static Action<int>? GetAction(string actionName)
        {
            _Actions.TryGetValue(actionName, out Action<int>? value);
            return value;
        }

        public static void InvokeAction(string actionName, int multiplier)
        {
            Action<int>? action = GetAction(actionName);
            action?.Invoke(multiplier);
        }

        public static void InvokeAction(string actionName)
        {
            Action<int>? action = GetAction(actionName);
            action?.Invoke(1);
        }

        public static bool IsMultipliableAction(string actionName)
        {
            return IsMouseMovementAction(actionName) || IsDelayAction(actionName);
        }

    }
}
