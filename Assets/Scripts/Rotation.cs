using UnityEngine;
using UnityEngine.InputSystem;

namespace SternGerlach
{
    public class Rotation : MonoBehaviour
    {
        private InputAction mousex;
        private InputAction mousey;
        private InputAction rightmousebutton;
        [SerializeField] private Transform cam;
        [SerializeField] private Transform fieldObj;
        [SerializeField] private float speed;
        private float mos_x;
        private float mos_y;
        private float rmb_val;
        // Start is called before the first frame update
        
        public void Initialize(InputAction x, InputAction y, InputAction rmb)
        {
            x.Enable();
            y.Enable();
            rmb.Enable();
            mousex = x;
            mousey = y;
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
                mos_y = mousey.ReadValue<float>() * Time.deltaTime * speed;
                cam.RotateAround(fieldObj.position, fieldObj.right, mos_y);
                cam.RotateAround(fieldObj.position, Vector3.up, mos_x);
                Debug.Log("Mouse x: " + mos_x);
            }

        }
    }
}
