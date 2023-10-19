using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public abstract class Node : MonoBehaviour
{
    // objects where we change the material (only for instructions)
    [SerializeField] List<GameObject> instructionObjects;
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
                return false; // Child nodes are not equal, not equal.
            }
        }

        return true; // All child nodes are equal.
    }
}
