using System.Collections.Generic;
using UnityEngine;

public abstract class Node : MonoBehaviour
{
    public abstract List<Node> children { get; set; }
    public abstract Vector3 GetStartLocation { get; set; }
    public abstract Vector3 GetEndLocation { get; set; }
}
