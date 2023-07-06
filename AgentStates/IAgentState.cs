using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SternGerlach
{
    public interface IAgentState
    {
        void DisplayAngle();
        void HideAngle();
        void CollapseState();
        bool ShouldCollapseState();
    }
}
