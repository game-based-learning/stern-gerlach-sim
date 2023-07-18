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
                case "EmptyNodeTop":
                    index = 0;
                    break;
            }
            newNode.transform.parent = parent;
            SGMagnet node = parent.gameObject.GetComponent<SGMagnet>();
            Debug.Log(index);
            Debug.Log(node);
            node.children[index] = newNode;
            newNode.transform.rotation = parent.rotation;
            float angle = Vector3.SignedAngle(parent.gameObject.transform.position, newNode.transform.position, parent.gameObject.transform.up);
            newNode.transform.Rotate(new Vector3(0, 0, -angle),Space.Self);

            Destroy(selectedNode.gameObject);
        }
        bool NodeSelected() {
            return selectedNode != null;
        }
        bool CanPlace() {
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
            if (!NodeSelected() || !(selectedNode is SGMagnet)) { return; }
            Debug.Log("Rotate Left");
            selectedNode.transform.Rotate(new Vector3(-15,0,0),Space.Self);
        }
        internal void RotateRight()
        {
            if (!NodeSelected() || !(selectedNode is SGMagnet)) { return; }
            Debug.Log("Rotate Right");
            selectedNode.transform.Rotate(new Vector3(15, 0, 0), Space.Self);
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
