using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Archipelago.DuckLifeRetroPack
{
    internal class ArchipelagoBackBtn : ArchipelagoBtn
    {

        private void OnMouseDown()
        {
            this.MoveToMenuFromArchipelago();
        }
    }
}
