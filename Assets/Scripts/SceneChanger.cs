using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace SternGerlach
{
    public class SceneChanger : MonoBehaviour
    {
        
        private InputAction spacebutton;
        public void Initialize(InputAction space)
        {
            space.Enable();
            spacebutton = space;
        }

        public void Update()
        {
            if (SceneManager.GetActiveScene().name != "AlphaBuild")
            {
                if (spacebutton.WasPressedThisFrame())
                {
                    Debug.Log("pressed!");
                    changeScene(Globals.SCENES[++Globals.CURRENT_SCENE_INDEX % 2]);
                }
            }
            
        }
        public void changeScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
