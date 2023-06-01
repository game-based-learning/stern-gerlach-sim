using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SternGerlach
{
    public class CameraRotation : MonoBehaviour
    {
        private InputAction mouse;
        [SerializeField] private Transform cam;
        [SerializeField] private Transform fieldObj;

        private float mos_x;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            mos_x = Mouse.current.position.ReadValue().x;
            cam.Rotate(Vector3.up, mos_x*Time.deltaTime, Space.World);
            //Debug.Log(mos_x);
        }
    }
}
