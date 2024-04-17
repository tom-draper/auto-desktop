using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auto_desktop
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
