using SternGerlach.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SternGerlach
{
    public class InputManager : MonoBehaviour
    {
        private PlayerControl controlScheme;
        [SerializeField] private Rotation rotationController;
        [SerializeField] private Zoom zoomController;
        [SerializeField] private Pan panController;
        [SerializeField] private ObjectClicker clickController;
        [SerializeField] private SceneChanger sceneManager;
        [SerializeField] private UIUpdater updater;
        [SerializeField] private PredictionHandler predictionHandler;
        // Start is called before the first frame update

        private void Awake()
        {
            controlScheme = new PlayerControl();
            rotationController.Initialize(
                controlScheme.Game.MouseX, 
                controlScheme.Game.MouseY, 
                controlScheme.Game.RMB);
            zoomController.Initialize(
                controlScheme.Game.Zoom);
            panController.Initialize(
                controlScheme.Game.MouseX, 
                controlScheme.Game.MouseY, 
                controlScheme.Game.LMB,
                controlScheme.Game.LShift);
            clickController.Initialize(
                controlScheme.Game.LMB,
                controlScheme.UI.MousePosition,
                controlScheme.Game.LShift);
            sceneManager.Initialize(
                controlScheme.UI.Space);
            updater.Initialize(
                controlScheme.UI.UIToggle);
            predictionHandler.Initialize(
                controlScheme.UI.MCQPrediction);
        }
    }
}
