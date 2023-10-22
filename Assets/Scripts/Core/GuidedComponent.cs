using SternGerlach.Assets.Scripts.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

namespace SternGerlach
{
    internal class GuidedComponent : MonoBehaviour
    {
        private Experiment exp;
        [SerializeField] XMLDeserializer xml;
        [SerializeField] Source nbSource;
        private Source instSource;
        public void TryEqual()
        {
            this.exp = xml.currExp;
            instSource = exp.source;
            Debug.Log(nbSource.Equals(instSource));
        }
        public void DebugTrees() 
        {
            this.exp = xml.currExp;
            instSource = exp.source;
            Debug.Log("Node builder tree:\n" + nbSource.ToString());
            Debug.Log("Instruction tree:\n" + instSource.ToString());
        }
    }
}
