namespace auto_desktop.Classes
{
    internal class Actions
    {
        private static readonly List<Action> _Actions = InitialiseActions();

        public class Action(string name, string? code)
        {
            public readonly string Name = name;
            public readonly string? Code = code;
        }

        private static List<Action> InitialiseActions()
        {
            List<Action> actions = [];
            actions.AddRange(GetDelayActions());
            actions.AddRange(GetLowerCaseLetterActions());
            actions.AddRange(GetUpperCaseLetterActions());
            actions.AddRange(GetNumberActions());
            actions.AddRange(GetUtilityActions());
            actions.AddRange(GetFunctionActions());
            actions.AddRange(GetSymbolActions());
            actions.AddRange(GetMouseActions());

            return actions;
        }

        private static List<Action> GetDelayActions()
        {
            return [
                new Action("Wait 1 millisecond", null),
                new Action("Wait 1 second", null),
                new Action("Wait 1 minute", null),
                new Action("Wait 1 hour", null),
            ];
        }

        private static List<Action> GetLowerCaseLetterActions()
        {
            List<Action> lowerCaseActions = [];
            for (int i = 97; i < 123; i++)
            {
                char c = (char)i;
                lowerCaseActions.Add(new Action(c.ToString(), c.ToString()));
            }
            return lowerCaseActions;
        }

        private static List<Action> GetUpperCaseLetterActions()
        {
            List<Action> upperCaseActions = [];
            for (int i = 65; i < 91; i++)
            {
                char c = (char)i;
                upperCaseActions.Add(new Action(c.ToString(), c.ToString()));
            }
            return upperCaseActions;
        }

        private static List<Action> GetNumberActions()
        {
            List<Action> numberActions = [];
            for (int i = 48; i < 58; i++)
            {
                char c = (char)i;
                numberActions.Add(new Action(c.ToString(), c.ToString()));
            }
            return numberActions;
        }

        private static List<Action> GetFunctionActions()
        {
            List<Action> functionActions = [];
            for (int i = 1; i < 13; i++)
            {
                functionActions.Add(new Action($"F{i}", $"{{F{i}}}"));
            }
            return functionActions;
        }

        private static List<Action> GetUtilityActions()
        {
            return [
                new Action("Space", " "),
                new Action("Enter", "{ENTER}"),
                new Action("Up", "{UP}"),
                new Action("Down", "{DOWN}"),
                new Action("Left", "{LEFT}"),
                new Action("Right", "{RIGHT}"),
                new Action("Tab", "{TAB}"),
                new Action("Backspace", "{BACKSPACE}"),
                new Action("Delete", "{DELETE}"),
                new Action("Home", "{HOME}"),
                new Action("End", "{END}"),
                new Action("Page Up", "{PGUP}"),
                new Action("Page Down", "{PGDN}"),
                new Action("Escape", "{ESC}"),
            ];
        }

        private static List<Action> GetSymbolActions()
        {
            return [
                new Action("!", "!"),
                new Action("@", "@"),
                new Action("#", "#"),
                new Action("$", "$"),
                new Action("%", "%"),
                new Action("^", "^"),
                new Action("&", "&"),
                new Action("*", "*"),
                new Action("(", "("),
                new Action(")", ")"),
                new Action("-", "-"),
                new Action("_", "_"),
                new Action("=", "="),
                new Action("+", "+"),
                new Action("[", "["),
                new Action("{", "{"),
                new Action("]", "]"),
                new Action("}", "}"),
                new Action(";", ";"),
                new Action(":", ":"),
                new Action("'", "'"),
                new Action("\"", "\""),
                new Action(",", ","),
                new Action("<", "<"),
                new Action(".", "."),
                new Action(">", ">"),
                new Action("/", "/"),
                new Action("?", "?"),
                new Action("\\", "\\"),
                new Action("|", "|"),
                new Action("`", "`"),
                new Action("~", "~"),
            ];
        }

        private static List<Action> GetMouseActions()
        {
            return [
                new Action("Left click", null),
                new Action("Right click", null),
                new Action("Middle click", null),
                new Action("Scroll up", null),
                new Action("Scroll down", null),
                new Action("Mouse 1px left", null),
                new Action("Mouse 1px right", null),
                new Action("Mouse 1px up", null),
                new Action("Mouse 1px down", null),
            ];
        }

        public static bool IsMouseAction(string actionName)
        {
            return GetMouseActions().Any(action => action.Name == actionName);
        }

        public static bool IsDelayAction(string actionName)
        {
            return GetDelayActions().Any(action => action.Name == actionName);
        }

        public static List<Action> GetActions()
        {
            return _Actions;
        }

        public static Action GetAction(string name)
        {
            return _Actions.FirstOrDefault(action => action.Name == name);
        }
    }
}
