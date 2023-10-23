using SternGerlach.Assets.Scripts.Core;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SternGerlach
{
    public class PredictionHandler : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] UIUpdater updater;
        [SerializeField] GuidedComponent gc;
        private MCQuestion multiplechoice;
        private string prediction;
        private InputAction mbutton;
        private bool shown = true;
        public void Initialize(InputAction m)
        {
            m.Enable();
            mbutton = m;
        }
        void Start()
        {
            /*Debug.Log(gc);
            Debug.Log(gc.xml);
            Debug.Log(gc.xml.currExp);
            Debug.Log(gc.xml.currExp.mcq);*/
            multiplechoice = gc.xml.currExp.mcq;
            prediction = gc.xml.currExp.predictionMessage;

            //Debug.Log(gc.xml.currExp.ToString());
            updater.MCQInit(multiplechoice);
            updater.PredQInit(prediction);
        }

        // Update is called once per frame
        void Update()
        {
            if (mbutton.WasPressedThisFrame())
            {
                //pos = updater.PredictionToggle(pos); 
                shown = updater.PredictionToggle(shown);
            }
        }
    }
}
