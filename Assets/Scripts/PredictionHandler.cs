using SternGerlach.Assets.Scripts.Core;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace SternGerlach
{
    public class PredictionHandler : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] UIUpdater updater;
        [SerializeField] GuidedComponent gc;
        private VisualElement root;
        private MCQuestion multiplechoice;
        private string prediction;
        private InputAction mbutton;
        private bool shown = true;

        public UserInputData uid;

        public void Initialize(InputAction m)
        {
            m.Enable();
            mbutton = m;
        }
        void Start()
        {
            root = updater.root;
            var p = root.Q<TextField>("prediction");
            p.RegisterCallback<ChangeEvent<string>>(OnTextFieldValueChanged);

            var m = root.Q<RadioButtonGroup>("mcq");
            m.RegisterCallback<ChangeEvent<int>>(OnMCQValueChanged);
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
            /*if (mbutton.WasPressedThisFrame())
            {
                //pos = updater.PredictionToggle(pos); 
                shown = updater.PredictionToggle(shown);
            }*/
        }
        private void OnMCQValueChanged(ChangeEvent<int> e)
        {
            uid.mcq_choice = (UserInputData.choices)e.newValue;
        }
        private void OnTextFieldValueChanged(ChangeEvent<string> e)
        {
            uid.input_data = e.newValue;
        }

        public string Check()
        {
            
            var msg = "";
            switch (uid.mcq_choice)
            {
                case UserInputData.choices.A:
                    msg = multiplechoice.GetPickedMessage('A').TrimStart();
                    uid.correct = multiplechoice.IsCorrect('A');
                    Debug.Log(msg + uid.correct);
                    break;
                case UserInputData.choices.B:
                    msg = multiplechoice.GetPickedMessage('B').TrimStart();
                    uid.correct = multiplechoice.IsCorrect('B');
                    break;
                case UserInputData.choices.C:
                    msg = multiplechoice.GetPickedMessage('C').TrimStart();
                    uid.correct = multiplechoice.IsCorrect('C');
                    break;
                case UserInputData.choices.D:
                    msg = multiplechoice.GetPickedMessage('D').TrimStart();
                    uid.correct = multiplechoice.IsCorrect('D');
                    break;
                default:
                    break;
            }
            return msg;
        }
    }
}
