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
        [SerializeField] NodeBuilder nb;
        [SerializeField] public XMLDeserializer xml;
        [SerializeField] Source nbSource;
        [SerializeField] Camera instructionCamera;
        [SerializeField] TextMeshProUGUI completeText;
        [SerializeField] UIUpdater updater;
        private Source instSource;

        public UserInputData uid;

        private bool hasPredictionBeenShown = false;
        private bool hasMCQBeenShown = false;
        private bool startSwitch = true;
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
            Debug.Log("atomcount: " + CollapsedAtomCount());
            if(TryEqual() && !hasPredictionBeenShown)
            {
                updater.ShowPrediction();
                hasPredictionBeenShown = true;
            }
            if (TryEqual() && CollapsedAtomCount() == xml.currExp.minParticles && !hasMCQBeenShown) 
            { 
                updater.ShowMCQ(); 
                hasMCQBeenShown = true;
            }
            Debug.Log("uid"+uid.correct);
            if (hasMCQBeenShown && uid.correct && startSwitch)
            {
                Debug.Log("entering here");
                startSwitch = false;
                StartCoroutine(moveon());
            }
        }
        IEnumerator moveon()
        {
            yield return new WaitForSeconds(3);
            Debug.Log("resetting scene");
        }
        private int CollapsedAtomCount()
        {
            int atoms = 0;
            foreach (var plate in nb.current_plates)
            {
                atoms += plate.collapseCount;
            }
            return atoms;
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
