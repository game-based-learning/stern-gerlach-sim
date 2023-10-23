using SternGerlach;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public abstract class Node : MonoBehaviour
{
    // objects where we change the material (only for instructions)
    [SerializeField] public List<GameObject> instructionObjects;
    public int rotation = 0;
    public abstract Dictionary<int, Node> children { get; set; }
    public abstract Vector3 GetStartLocation { get; set; }
    public abstract Vector3 GetEndLocation { get; set; }
    public Vector3 GetAbsoluteRotation() { return transform.eulerAngles; }
    public virtual int GetRotation() { return rotation; }
    // Precondition: newRot is in [-180, 180]
    public void SetRotation(int newRot) { this.rotation = newRot; }
    // Increase or Decrease Rotation
    public void AdjustRotation(int degrees)
    {
        this.rotation += degrees;
        // Wrap the rotation value within [-180, 180] range
        if (rotation > 180)
        {
            rotation -= 360; // Wrap to the negative range
        }
        else if (rotation < -180)
        {
            rotation += 360; // Wrap to the positive range
        }
        if (children == null) {
            return;
        }
        foreach (KeyValuePair<int, Node> child in children) { 
            child.Value.AdjustRotation(degrees);
        }
    }
    public void UpdateInstructionColoring(Node instructionSource)
    {
        if (Equals(instructionSource))
        {
            Debug.Log("Trees are equal.");
            ApplyMaterialToTree(instructionSource, GameManager.Instance.correct);
            return;
        }
        Debug.Log("Trees are not equal.");
        ColorMismatchedNodes(instructionSource, this);
    }

    void ColorMismatchedNodes(Node node1, Node node2)
    {
        if (!node1.EqualsNodeOnly(node2))
        {
            if (!NodesHaveCorrectRotation(node1, node2))
            {
                // Nodes match, but rotation is wrong, so color them yellow.
                ApplyMaterialToTree(node1, GameManager.Instance.wrongAngle);
            }
            else
            {
                // Nodes don't match, so color them and their descendants red.
                ApplyMaterialToTree(node1, GameManager.Instance.wrongNode);
            }
        }
        else
        {
            // Nodes match, so color them green.
            ApplyMaterialToTree(node1, GameManager.Instance.correct);
        }

        if (node1.children == null || node2.children == null)
        {
            return;
        }

        // Traverse the children and recursively check for mismatches.
        foreach (var kvp in node1.children)
        {
            if (node2.children.TryGetValue(kvp.Key, out Node otherChild))
            {
                ColorMismatchedNodes(kvp.Value, otherChild);
            }
            else
            {
                // Child is missing in the second tree, color it red.
                ApplyMaterialToTree(kvp.Value, GameManager.Instance.wrongNode);
            }
        }
    }

    bool NodesHaveCorrectRotation(Node node1, Node node2)
    {
        return node1.GetRotation() == node2.GetRotation();
    }
    void ApplyMaterialToTree(Node node, Material mat) {
        // apply material to node
        foreach(GameObject gameObj in node.instructionObjects) {
            gameObj.GetComponent<Renderer>().material = mat;
        }
        if (node.children == null || node.children.Count == 0) {
            return;
        }
        for (int i = 0; i < node.children.Count; i++) {
            node.ApplyMaterialToTree(node.children[i], mat);
        }
    }
    public override bool Equals(object other)
    {
        if (other == null || GetType() != other.GetType()) { 
            return false; 
        }
        Node otherNode = (Node) other;
        if (GetRotation() != otherNode.GetRotation()) {
            return false;
        }
        if (!ChildrenEqual(otherNode)) {
            return false;
        }
        return true;
    }
    private bool EqualsNodeOnly(Node other) {
        if (other == null || GetType() != other.GetType())
        {
            return false;
        }
        Node otherNode = (Node)other;
        if (GetRotation() != otherNode.GetRotation())
        {
            return false;
        }
        return true;
    }
    private bool ChildrenEqual(Node otherNode) {
        if (children == null && otherNode.children == null)
        {
            return true; // Both have no children, so they are equal.
        }

        if (children == null || otherNode.children == null)
        {
            return false; // One has children and the other doesn't, not equal.
        }

        if (children.Count != otherNode.children.Count)
        {
            return false; // Different number of children, not equal.
        }

        foreach (var kvp in children)
        {
            if (!otherNode.children.TryGetValue(kvp.Key, out Node otherChild))
            {
                return false; // A child with the same key is missing, not equal.
            }

            if (!kvp.Value.Equals(otherChild))
            {
                Debug.Log("i:" + kvp.Value);
                Debug.Log("nb:" + otherChild);
                return false; // Child nodes are not equal, not equal.
            }
        }

        return true; // All child nodes are equal.
    }
    public override string ToString()
    {
        string str ="I am a " + this.GetType() + "with rotation" + this.GetRotation();
        if (children != null && children.Count != 0)
        {
            str += "And my children are: ";
            for (int i = 0; i < children.Count; i++)
            {
                str += children[i].ToString();
            }
        }
        else {
            str += "And I have no children.";
        }
        return str;
    }
}
