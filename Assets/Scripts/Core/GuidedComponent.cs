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
        [SerializeField] Camera instructionCamera;
        private Source instSource;
        // initial camera settings
        private float initX, initY, initSize, width, height;
        public void Start()
        {
            this.initSize = instructionCamera.orthographicSize;
            this.initX = instructionCamera.rect.x;
            this.initY = instructionCamera.rect.y;
            this.width = instructionCamera.rect.width;
            this.height = instructionCamera.rect.height;
        }
        public void SetCameraSettings(float sizeModifier, float xModifier, float yModifier)
        {
            instructionCamera.orthographicSize = sizeModifier + initSize;
            instructionCamera.rect = new Rect(xModifier + this.initX, yModifier + this.initY, this.width, this.height);
        }
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
