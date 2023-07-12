using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SternGerlach
{
    public class ObjectClicker : MonoBehaviour
    {
        private InputAction click;
        private InputAction mPos;
        private InputAction lShift;
        [SerializeField] private Transform clickedObject;
        private Ray ray;
        private RaycastHit hit;
        [SerializeField] private PointManager pm;

        public void Initialize(InputAction c, InputAction p, InputAction ls)
        {
            c.Enable();
            p.Enable();
            ls.Enable();
            click = c;
            mPos = p;
            lShift = ls;
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
            float ls_val = lShift.ReadValue<float>();

            //Debug.Log("mouse pos: " + position);
            ray = Camera.main.ScreenPointToRay(position);

            if (Physics.Raycast(ray, out hit) )
            {
                if (lmb_val == 1 && ls_val == 0)
                {
                    Debug.Log(hit.collider.name);
                    pm.Focus(hit.collider.transform);
                }
            }
        }
    }
}
