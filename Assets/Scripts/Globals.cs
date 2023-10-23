using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SternGerlach
{
    public class Globals
    {
        public static List<int> POSSIBLE_MACROSCOPIC_ANGLES = new List<int>() { 0, 30, 60, 90, 120, 150, 180 };
        public static float PARTICLE_STEP_SPEED = 3f;
        public static string QUESTION_MARK_NAME = "QuestionMark";
        public static string ARROW_NAME = "Arrow";
        public static string MAGNET_NAME = "Magnet";
        public static float ANGLE_BETWEEN_NODES = 22.5f;
        public static string[] SCENES = { "MacroscopicNodeBuilder", "SilverAtomNodeBuilder" };
        public static int CURRENT_SCENE_INDEX = 0;
        internal static float PARTICLE_COOLDOWN = 0.15f;
        internal static string UP_CARET_NAME = "UpCaret";
        internal static string DOWN_CARET_NAME = "DownCaret";
        // Instruction camera defaults
        internal static float INST_CAM_INIT_X = 0f, INST_CAM_INIT_Y = 0.5f, INST_CAM_INIT_SIZE = 6.79f;
        internal static float INST_CAM_INIT_WIDTH = 0.5f, INST_CAM_INIT_HEIGHT = 0.5f;
    }
}
