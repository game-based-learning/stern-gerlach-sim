using SternGerlach.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SternGerlach
{
    public class InputManager : MonoBehaviour
    {
        private PlayerControl controlScheme;
        [SerializeField] private CameraRotation mouseController;
        // Start is called before the first frame update

        private void Awake()
        {
            controlScheme = new PlayerControl();
            mouseController.Initialize(controlScheme.Game.Mouse);
        }
    }
}
