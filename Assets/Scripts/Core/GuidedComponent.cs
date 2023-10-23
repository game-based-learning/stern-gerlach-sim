using SternGerlach.Assets.Scripts.Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        [SerializeField] TextMeshProUGUI completeText;
        [SerializeField] UIUpdater updater;
        private Source instSource;
        // initial camera settings
        private float initX = Globals.INST_CAM_INIT_X, initY = Globals.INST_CAM_INIT_Y, initSize = Globals.INST_CAM_INIT_SIZE;
        private float width = Globals.INST_CAM_INIT_WIDTH, height = Globals.INST_CAM_INIT_HEIGHT;

        public void SetCameraSettings(float sizeModifier, float xModifier, float yModifier)
        {
            instructionCamera.orthographicSize = sizeModifier + initSize;
            instructionCamera.rect = new Rect(xModifier + this.initX, yModifier + this.initY, this.width, this.height);
        }
        void Update()
        {
            if (TryEqual()) { updater.ShowMCQ(); }
            
        }
        public bool TryEqual()
        {
            this.exp = xml.currExp;
            instSource = exp.source;
            bool eq = nbSource.Equals(instSource);
            nbSource.UpdateInstructionColoring(instSource);
            if (eq)
            {
                completeText.text = "Complete setup!";
            }
            else
            {
                completeText.text = "Incomplete setup.";
            }
            return eq;
        }
        public void DebugTrees() 
        {
            this.exp = xml.currExp;
            instSource = exp.source;
            Debug.Log("Node builder tree:\n" + nbSource.ToString());
            Debug.Log("Instruction tree:\n" + instSource.ToString());
            exp = xml.currExp;
            instSource = exp.source;
        }
    }
}
