using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SternGerlach
{
    public class NodeBuilder : MonoBehaviour
    {
        private Node selectedNode;
        [SerializeField] GameObjectFactory factory;
        internal void PlaceImagePlate()
        {
            if (!CanPlace()) { return; }
            Debug.Log("Place Image Plate");
            Vector3 loc = selectedNode.transform.position;
            ImagePlate plate = factory.CreateImagePlate(loc);
            PlaceNode(plate);
        }

        internal void PlaceSGMagnet()
        {
            if (!CanPlace()) { return; }
            Debug.Log("Place SG Magnet");
            Vector3 loc = selectedNode.transform.position;
            SGMagnet plate = factory.CreateSGMagnet(loc);
            PlaceNode(plate);
        }
        void PlaceNode(Node newNode) {
            Transform parent = selectedNode.transform.parent;
            Debug.Log(parent);
            int index = 0;
            switch (selectedNode.gameObject.name)
            {
                case "EmptyNodeBottom":
                    index = 1;
                    break;
                // intentional fallthrough
                case "EmptyNodeTop":
                case "EmptyNode":
                    index = 0;
                    break;
            }
            newNode.transform.parent = parent;
            Node node = parent.gameObject.GetComponent<SGMagnet>();
            // Accounts for special case (first node not placed yet)
            bool firstnode = false;
            if(node == null) {
                node = parent.gameObject.GetComponent<Source>();
                firstnode = true;
            }
            node.children[index] = newNode;
            if(!firstnode) {
                newNode.transform.rotation = parent.rotation;
                // we know the parent is an SG magnet so find magnet child
                Transform sgChildTransform = parent.transform.Find(Globals.MAGNET_NAME).gameObject.transform;
                // perform appropriate rotation
                int direction = 1;
                if(index != 1) {
                    direction *= -1;
                }
                newNode.transform.Rotate(new Vector3(0, 0, direction * Globals.ANGLE_BETWEEN_NODES),Space.Self);
            }
            Destroy(selectedNode.gameObject);
        }
        bool NodeSelected() {
            return selectedNode != null;
        }
        bool CanPlace() {
            if(!factory.CanUpdateSetup()) { 
                Debug.Log("Cannot update setup while particles are actively traversing."); 
                return false; 
            }
            return NodeSelected() && selectedNode.gameObject.name != "FirstMagnet";
        }
        internal void SelectNode(Ray ray)
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.TryGetComponent<Node>(out selectedNode)) {
                    Debug.Log("Selected: " + hit.transform.gameObject.name);
                }
            }
        }
        internal void RotateLeft()
        {
            if (!ShouldRotate()) { return; }
            Debug.Log("Rotate Left");
            selectedNode.transform.Rotate(new Vector3(-15,0,0),Space.Self);
        }
        internal void RotateRight()
        {
            if (!ShouldRotate()) { return; }
            Debug.Log("Rotate Right");
            selectedNode.transform.Rotate(new Vector3(15, 0, 0), Space.Self);
        }
        private bool ShouldRotate() {
            return factory.SilverAtomMode() && NodeSelected() && selectedNode is SGMagnet;
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
