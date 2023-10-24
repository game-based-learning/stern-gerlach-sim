using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SternGerlach
{
    [CreateAssetMenu(fileName = "InputData", menuName = "Custom/InputData")]
    public class UserInputData : ScriptableObject
    {
        public string input_data;
    }
}
