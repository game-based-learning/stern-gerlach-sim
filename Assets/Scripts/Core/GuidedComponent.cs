using SternGerlach.Assets.Scripts.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

namespace SternGerlach
{
    public class GuidedComponent : MonoBehaviour
    {
        private Experiment exp;
        [SerializeField] public XMLDeserializer xml;
        [SerializeField] Source nbSource;
        [SerializeField] Camera instructionCamera;
        private Source instSource;
        // initial camera settings
        //private float initX = instructionCamera.rect.x, initY = instructionCamera.rect.y, initSize = instructionCamera.orthographicSize;
        //private float width = instructionCamera.rect.width, height = instructionCamera.rect.height;

        public void SetCameraSettings(float sizeModifier, float xModifier, float yModifier)
        {
            //instructionCamera.orthographicSize = sizeModifier + initSize;
            //instructionCamera.rect = new Rect(xModifier + this.initX, yModifier + this.initY, this.width, this.height);
        }
        void Update()
        {
            this.exp = xml.currExp;
            instSource = exp.source;
            nbSource.UpdateInstructionColoring(instSource);
        }
        public void TryEqual()
        {
            this.exp = xml.currExp;
            instSource = exp.source;
            Debug.Log(nbSource.Equals(instSource));
            nbSource.UpdateInstructionColoring(instSource);
        }
        public void DebugTrees() 
        {
            this.exp = xml.currExp;
            instSource = exp.source;
            Debug.Log("Node builder tree:\n" + nbSource.ToString());
            Debug.Log("Instruction tree:\n" + instSource.ToString());
            //Debug.Log("gc update");
            exp = xml.currExp;
            instSource = exp.source;
            //Debug.Log(instSource);
            //Debug.Log(nbSource);
            //Debug.Log("complete?: "+ instSource.Equals(nbSource));
        }
    }
}
