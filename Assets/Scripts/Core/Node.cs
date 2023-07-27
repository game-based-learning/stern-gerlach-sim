using System.Collections.Generic;
using UnityEngine;

public abstract class Node : MonoBehaviour
{
    public abstract Dictionary<int, Node> children { get; set; }
    public abstract Vector3 GetStartLocation { get; set; }
    public abstract Vector3 GetEndLocation { get; set; }
    public abstract int GetRotation(GameObject gameObj);
}
