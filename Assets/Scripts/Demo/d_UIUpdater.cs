using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SternGerlach
{
    public class d_UIUpdater : UIUpdater
    {
        // Start is called before the first frame update
        public override void GuidedModeButtonPressed()
        {
            scenechanger.changeScene("d_SilverAtomNodeBuilder");
        }
    }
}
