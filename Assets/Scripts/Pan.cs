using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SternGerlach
{
    public class Pan : MonoBehaviour
    {
        private InputAction mousex;
        private InputAction mousey;
        private InputAction leftmousebutton;
        private InputAction leftshift;
        private float lmb_val;
        private float ls_val;
        private float mos_x;
        private float mos_y;
        [SerializeField] private float speed;
        [SerializeField] private Transform cam;
        public void Initialize(InputAction x, InputAction y, InputAction lmb, InputAction ls)
        {
            x.Enable();
            y.Enable();
            lmb.Enable();
            ls.Enable();
            mousex = x;
            mousey = y;
            leftmousebutton = lmb;
            leftshift = ls;
        }
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            ls_val = leftshift.ReadValue<float>();
            lmb_val = leftmousebutton.ReadValue<float>();
            if (lmb_val + ls_val == 2)
            {
                mos_x = mousex.ReadValue<float>() * Time.deltaTime * speed;
                mos_y = mousey.ReadValue<float>() * Time.deltaTime * speed;
                cam.Translate(new Vector3(mos_x, mos_y, 0));
                //Debug.Log("Mouse x: " + mos_x);
            }
        }
    }
}
