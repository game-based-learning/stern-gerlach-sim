using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SternGerlach
{
    public class Pan : MonoBehaviour
    {
        private InputAction mousex;
        private InputAction leftmousebutton;
        private float lmb_val;
        private float mos_x;
        [SerializeField] private float speed;
        [SerializeField] private Transform cam;
        public void Initialize(InputAction x, InputAction lmb)
        {
            x.Enable();
            lmb.Enable();
            mousex = x;
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
                cam.Translate(Vector3.right * mos_x);
                //Debug.Log("Mouse x: " + mos_x);
            }
        }
    }
}
