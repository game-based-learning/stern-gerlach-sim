using UnityEngine;
using UnityEngine.InputSystem;

namespace SternGerlach
{
    public class PredictionHandler : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] UIUpdater updater;
        private InputAction mbutton;
        private float pos = 0;
        public void Initialize(InputAction m)
        {
            m.Enable();
            mbutton = m;
        }
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (mbutton.WasPressedThisFrame())
            {
                pos = updater.PredictionToggle(pos); 
            }
        }
    }
}
