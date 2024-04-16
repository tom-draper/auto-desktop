using System;
using System.Collections.Generic;
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
            return [
                new Action("Enter", "{ENTER}"),
                new Action("Up", "{UP}"),
                new Action("Down", "{DOWN}"),
                new Action("Left", "{LEFT}"),
                new Action("Right", "{RIGHT}"),
                new Action("Tab", "{TAB}"),
                new Action("Wait 1 millisecond", null),
                new Action("Wait 1 second", null),
                new Action("Wait 1 minute", null),
                new Action("Wait 1 hour", null),
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
