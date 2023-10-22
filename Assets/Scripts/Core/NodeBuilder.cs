using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using Vector3 = UnityEngine.Vector3;

namespace SternGerlach
{
    public class NodeBuilder : MonoBehaviour
    {
        public Node selectedNode;
        [SerializeField] GameObjectFactory factory;
        [SerializeField] GameObject COR;
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
        internal Vector3 FindCenterOfRotation() {
            Source source = factory.GetSource();
            List<Vector3> nodeLocations = GetChildrenLocations(source);
            // Initialize maxima to an arbitrary tiny value
            float xMaxima = Int32.MinValue, yMaxima = Int32.MinValue, zMaxima = Int32.MinValue;
            // Initialize minima to an arbitrary huge value
            float xMinima = Int32.MaxValue, yMinima = Int32.MaxValue, zMinima = Int32.MaxValue;
            foreach (Vector3 position in nodeLocations) {
                if (position.x > xMaxima) { xMaxima = position.x; }
                if (position.x < xMinima) { xMinima = position.x; }
                if (position.y > yMaxima) { yMaxima = position.y; }
                if (position.y < yMinima) { yMinima = position.y; }
                if (position.z > zMaxima) { zMaxima = position.z; }
                if (position.z < zMinima) { zMinima = position.z; }
            }
            return new Vector3((xMaxima+xMinima)/2, (yMaxima+yMinima)/2, (zMaxima+zMinima)/2);
        }
        private List<Vector3> GetChildrenLocations(Node node) {
            if (node.children == null || node.children.Count == 0) { 
                return new List<Vector3>();
            }
            List<Vector3> locations = new List<Vector3>();
            locations.Add(node.transform.position);
            for (int i = 0; i < node.children.Count; i++) {
                foreach (Vector3 position in GetChildrenLocations(node.children[i])) {
                    locations.Add(position);
                }
                return locations;
            }
            return locations;
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
            if (node == null)
            {
                node = parent.gameObject.GetComponent<Source>();
                firstnode = true;
                ((Source)node).SetFirstNode(newNode);
            }
            else {
                node.children[index] = newNode;
            }
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
            this.COR.transform.position = FindCenterOfRotation();
            this.selectedNode = newNode;
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
            this.COR.transform.position = FindCenterOfRotation();
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
        public void SelectNode(Node node) {
            this.selectedNode = node;
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
            this.COR.transform.position = FindCenterOfRotation();
        }
        // Only used for loading from file. Use RotateLeft and RotateRight for normal rotation!
        internal void Rotate(int rot) {
            if (!ShouldRotate()) { return; }
            if (rot % 15 != 0) {
                Debug.Log("Error: You cannot use angles that are not divisible by 15 in your XML file.");
                return;
            }
            selectedNode.transform.Rotate(new Vector3(rot, 0, 0), Space.Self);
            selectedNode.AdjustRotation(rot);
        }
        internal void RotateLeft()
        {
            if (!ShouldRotate()) { return; }
            Debug.Log("Rotate Left");
            selectedNode.transform.Rotate(new Vector3(-15,0,0),Space.Self);
            selectedNode.AdjustRotation(-15);
        }
        internal void RotateRight()
        {
            if (!ShouldRotate()) { return; }
            Debug.Log("Rotate Right");
            selectedNode.transform.Rotate(new Vector3(15, 0, 0), Space.Self);
            selectedNode.AdjustRotation(15);
        }
        private bool ShouldRotate() {
            return factory.SilverAtomMode() && NodeSelected() && factory.CanUpdateSetup() && selectedNode is SGMagnet;
        }
    }
}
