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
            if (factory.SilverAtomMode())
            {
                ImagePlate plate = factory.CreateImagePlate(loc);
                PlaceNode(plate);
            }
            else {
                List<ImagePlate> largePlate = factory.CreateLargeImagePlate(loc);
                PlaceLargePlateNodes(largePlate);
            }
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

            newNode.transform.parent = parent;
            Node node = parent.gameObject.GetComponent<SGMagnet>();

            // choose child
            int index = 0;
            if (node != null && selectedNode == node.children[1]) { index = 1; }

            // Accounts for special case (first node not placed yet)
            bool firstnode = false;
            if(node == null) {
                node = parent.gameObject.GetComponent<Source>();
                firstnode = true;
            }
            node.children[index] = newNode;
            if(!firstnode) {
                newNode.transform.rotation = parent.rotation;
                // perform appropriate rotation
                int direction = 1;
                if(index != 1) {
                    direction *= -1;
                }
                newNode.transform.Rotate(new Vector3(0, 0, direction * Globals.ANGLE_BETWEEN_NODES),Space.Self);
            }
            Destroy(selectedNode.gameObject);
        }
        void PlaceLargePlateNodes(List<ImagePlate> nodes)
        {
            Transform parent = selectedNode.transform.parent;
            Node sgMagnet = parent.gameObject.GetComponent<SGMagnet>();
            int count = 0;
            foreach (Node node in nodes) {
                node.transform.parent = parent;
                sgMagnet.children[count++] = node;
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
    }
}
