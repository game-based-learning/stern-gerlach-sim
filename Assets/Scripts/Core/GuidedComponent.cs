using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SternGerlach
{
    public class GuidedComponent : MonoBehaviour
    {
        void Start()
        {
            Scene s = SceneManager.GetActiveScene();
            SceneManager.LoadScene("LegoInstructions", LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("LegoInstructions"));
        }
    }
}
