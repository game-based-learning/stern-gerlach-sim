using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SternGerlach
{
    public class Globals : MonoBehaviour
    {
        public static List<int> POSSIBLE_MACROSCOPIC_ANGLES = new List<int>() { 0, 30, 60, 90, 120, 150, 180 };
        public static float PARTICLE_STEP_SPEED = 3f;
    }
}
