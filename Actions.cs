using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auto_desktop
{
    internal class Actions
    {
        public Actions()
        {
            List<Action> Actions = GetActions();
        }

        class Action(string ID, string Name, string Code)
        {
            string ID = ID;
            string Name = Name;
            string Code = Code;
        }

        private List<Action> GetActions()
        {
            return new List<Action> { Action("hi", "hello", "h") }
        }
    }
}
