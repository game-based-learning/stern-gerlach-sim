using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SternGerlach
{
    public class Pan : MonoBehaviour
    {
        private InputAction mousex;
        private InputAction mousey;
        private InputAction leftmousebutton;
        private float lmb_val;
        private float mos_x;
        private float mos_y;
        [SerializeField] private float speed;
        [SerializeField] private Transform cam;
        public void Initialize(InputAction x, InputAction y, InputAction lmb)
        {
            x.Enable();
            y.Enable();
            lmb.Enable();
            mousex = x;
            mousey = y;
            leftmousebutton = lmb;
        }
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            lmb_val = leftmousebutton.ReadValue<float>();
            if (lmb_val == 1)
            {
                mos_x = mousex.ReadValue<float>() * Time.deltaTime * speed;
                mos_y = mousey.ReadValue<float>() * Time.deltaTime * speed;
                cam.Translate(new Vector3(mos_x, mos_y, 0));
                //Debug.Log("Mouse x: " + mos_x);
            }
        }
    }
}
