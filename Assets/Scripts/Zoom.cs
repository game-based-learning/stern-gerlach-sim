using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SternGerlach
{
    public class Zoom : MonoBehaviour
    {
        private InputAction scroll;
        [SerializeField] private Transform cam;
        [SerializeField] private Transform fieldObj;
        [SerializeField] private float speed;
        private float scrl;
        // Start is called before the first frame update
        public void Initialize(InputAction s)
        {
            s.Enable();
            scroll = s;
        }
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            scrl = scroll.ReadValue<float>() * Time.deltaTime * speed;
            //Debug.Log(scrl);
            cam.Translate(Vector3.forward * scrl);
        }
    }
}
