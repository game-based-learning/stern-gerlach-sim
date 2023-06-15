using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SternGerlach
{
    public class ObjectClicker : MonoBehaviour
    {
        private InputAction click;
        private InputAction mPos;
        [SerializeField] private Transform clickedObject;
        private Ray ray;
        private RaycastHit hit;
        //[SerializeField] private Camera cam;

        public void Initialize(InputAction c, InputAction p)
        {
            c.Enable();
            p.Enable();
            click = c;
            mPos = p;
        }
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 tmp = mPos.ReadValue<Vector2>();
            Vector3 position = new Vector3(tmp.x, tmp.y, 0);

            float lmb_val = click.ReadValue<float>();

            //Debug.Log("mouse pos: " + position);
            ray = Camera.main.ScreenPointToRay(position);

            if (Physics.Raycast(ray, out hit) )
            {
                if (lmb_val == 1)
                {
                    Debug.Log(hit.collider.name);
                }
            }
        }
    }
}
