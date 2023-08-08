using System.Collections.Generic;
using UnityEngine;

namespace SternGerlach
{
    public class NodeBuilder : MonoBehaviour
    {
        public Node selectedNode;
        [SerializeField] GameObjectFactory factory;
        internal void PlaceImagePlate()
        {
            if (!CanPlace()) { return; }
            if (!factory.SilverAtomMode() && selectedNode.transform.parent.GetComponent<Node>() is Source) { return; }

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

            //selectedNode = null; //added code
        }

        internal void PlaceSGMagnet()
        {
            if (!CanPlace()) { return; }
            // only allow one sg magnet for macroscopic mode
            if (!factory.SilverAtomMode() && selectedNode.transform.parent.GetComponent<Node>() is SGMagnet) { return; }
            Debug.Log("Place SG Magnet");
            Vector3 loc = selectedNode.transform.position;
            SGMagnet plate = factory.CreateSGMagnet(loc);
            PlaceNode(plate);
            //selectedNode = null; //added code
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
            selectedNode = newNode;
        }
        void PlaceLargePlateNodes(List<ImagePlate> nodes)
        {
            Transform parent = selectedNode.transform.parent;
            Node sgMagnet = parent.gameObject.GetComponent<SGMagnet>();
            int count = 0;
            foreach (Node node in nodes) {
                sgMagnet.children[count++] = node;
            }

            nodes[0].transform.parent.transform.parent = sgMagnet.transform;
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
        internal void DeleteNode()
        {
            if (!factory.CanUpdateSetup()) { Debug.Log("Cannot update setup while particles are actively traversing."); return; }
            if (selectedNode == null) { Debug.Log("No node was selected."); return; }
            if (selectedNode is Source) { Debug.Log("Cannot delete source."); return; }
            if (selectedNode is EmptyNode) { Debug.Log("Cannot delete a node that is already empty."); return; }
            Node parent = selectedNode.transform.parent.GetComponent<Node>();
            // Special case for large image plate
            int index = 0;
            if (!factory.SilverAtomMode() && selectedNode is ImagePlate) {
                parent = selectedNode.transform.parent.transform.parent.GetComponent<SGMagnet>();
                parent.children = new Dictionary<int, Node>();
                EmptyNode node = factory.CreateEmptyNode(selectedNode.transform.parent.transform.position);
                node.transform.parent = parent.transform;
                parent.children[index] = node;
                Destroy(selectedNode.transform.parent.gameObject);
            }
            else {
                for (int i = 0; i < parent.children.Count; i++) {
                    if (parent.children[i] == selectedNode) {
                        index = i;
                        break;
                    }
                }
                EmptyNode node = factory.CreateEmptyNode(selectedNode.transform.position);
                node.transform.parent = parent.transform;
                parent.children[index] = node;
                Destroy(selectedNode.gameObject);
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
            return factory.SilverAtomMode() && NodeSelected() && factory.CanUpdateSetup() && selectedNode is SGMagnet;
        }
    }
}
