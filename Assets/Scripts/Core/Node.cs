using System.Collections.Generic;
using UnityEngine;

public abstract class Node : MonoBehaviour
{
    public int rotation = 0;
    public abstract Dictionary<int, Node> children { get; set; }
    public abstract Vector3 GetStartLocation { get; set; }
    public abstract Vector3 GetEndLocation { get; set; }
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
}
