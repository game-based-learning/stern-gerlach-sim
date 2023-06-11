using UnityEngine;
using UnityEngine.InputSystem;

namespace SternGerlach
{
    public class Rotation : MonoBehaviour
    {
        private InputAction mousex;
        private InputAction rightmousebutton;
        [SerializeField] private Transform cam;
        [SerializeField] private Transform fieldObj;
        [SerializeField] private float speed;
        private float mos_x;
        private float rmb_val;
        // Start is called before the first frame update
        
        public void Initialize(InputAction x, InputAction rmb)
        {
            x.Enable();
            rmb.Enable();
            mousex = x;
            rightmousebutton= rmb;
        }
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            rmb_val = rightmousebutton.ReadValue<float>();
            if (rmb_val == 1)
            {
                mos_x = mousex.ReadValue<float>() * Time.deltaTime * speed;
                cam.RotateAround(fieldObj.position, Vector3.up, mos_x);
                Debug.Log("Mouse x: " + mos_x);
            }

        }
    }
}
