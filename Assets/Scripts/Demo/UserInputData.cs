using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SternGerlach
{
    [CreateAssetMenu(fileName = "InputData", menuName = "Custom/InputData")]
    public class UserInputData : ScriptableObject
    {
        public string input_data;

        public enum choices
        {
            A,
            B,
            C,
            D
        }
        public choices mcq_choice;
        public bool correct = false;
    }
}
