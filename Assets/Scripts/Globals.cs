using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SternGerlach
{
    public class Globals : MonoBehaviour
    {
        public static List<int> POSSIBLE_MACROSCOPIC_ANGLES = new List<int>() { 0, 30, 60, 90, 120, 150, 180 };
        public static float PARTICLE_STEP_SPEED = 3f;
        public static string QUESTION_MARK_NAME = "QuestionMark";
        public static string ARROW_NAME = "Arrow";
        public static string MAGNET_NAME = "Magnet";
        public static float ANGLE_BETWEEN_NODES = 22.5f;
    }
}
