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
        // Start is called before the first frame update

        private void Awake()
        {
            controlScheme = new PlayerControl();
            rotationController.Initialize(controlScheme.Game.MouseX, controlScheme.Game.RMB);
            zoomController.Initialize(controlScheme.Game.Zoom);
        }
    }
}
