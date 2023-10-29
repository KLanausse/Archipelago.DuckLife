using System;
using System.Collections.Generic;
using System.Text;

namespace Archipelago.DuckLife
{
    internal class ArchipelagoMenu
    {
        public ArchipelagoBtn Btn;

        private void OnMouseDown()
        {
            Btn.MoveToMenuFromArchipelago();
        }
    }
}
