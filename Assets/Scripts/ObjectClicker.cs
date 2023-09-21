
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
        //private Node selectedNode;
        private Ray ray;
        private RaycastHit hit;
        private Vector3 position = new Vector3();
        [SerializeField] private PointManager pm;
        [SerializeField] private UIUpdater UIManager;

        public void Initialize(InputAction c, InputAction p, InputAction ls)
        {
            c.Enable();
            p.Enable();
            ls.Enable();
            click = c;
            mPos = p;
            lShift = ls;
        }
        
        private void FocusCollider(Ray ray, float lmb_val, float ls_val)
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (lmb_val == 1 && ls_val == 0)
                {
                    //Debug.Log(hit.collider.name);
                    pm.Focus(hit.collider.transform);
                    var ntag = hit.collider.gameObject.tag;
                    if (ntag == "Node")
                    {
                        UIManager.PopupDialog(position);
                        UIManager.state = UIUpdater.States.UI_OPEN;
                    } else if (ntag == "Object")
                    {
                        UIManager.DeletePopup();
                        UIManager.state = UIUpdater.States.UI_OPEN;
                    }
                    if (hit.transform.TryGetComponent<Node>(out UIManager.builder.selectedNode))
                    {
                        Debug.Log("Selected: " + hit.transform.gameObject.name);
                    } else
                    {
                        UIManager.builder.selectedNode = null;
                    }
                }
            }
        }

        private void FocusUI(Ray ray, float lmb_val, float ls_val)
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (lmb_val == 1 && ls_val == 0)
                {
                    UIManager.Modify();
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 tmp = mPos.ReadValue<Vector2>();
            position.x = tmp.x;
            position.y = tmp.y;
            position.z = 0;

            float lmb_val = click.ReadValue<float>();
            float ls_val = lShift.ReadValue<float>();

            //Debug.Log("mouse pos: " + position);
            ray = Camera.main.ScreenPointToRay(position);

            //Debug.Log(UIManager.state);
            if(UIManager.state == UIUpdater.States.UI_CLOSED) {
                FocusCollider(ray, lmb_val, ls_val);
            }
            FocusUI(ray, lmb_val, ls_val);
        }
    }
}
