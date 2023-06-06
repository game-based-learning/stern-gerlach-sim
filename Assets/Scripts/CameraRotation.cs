using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using SternGerlach.Input;
using UnityEditor;

namespace SternGerlach
{
    public class CameraRotation : MonoBehaviour
    {
        private InputAction mousex;
        [SerializeField] private Transform cam;
        [SerializeField] private Transform fieldObj;
        [SerializeField] private float speed;
        private float mos_x;
        // Start is called before the first frame update
        
        public void Initialize(InputAction x)
        {
            x.Enable();
            mousex = x;
        }
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            mos_x = mousex.ReadValue<float>() * Time.deltaTime * speed;
            cam.RotateAround(fieldObj.position, Vector3.up, mos_x);
            //Debug.Log(mos_x);
        }
    }
}
