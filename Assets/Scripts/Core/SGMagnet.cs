using SternGerlach;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SGMagnet : Node
{
    [SerializeField] Dictionary<int,Node> nodes;
    [SerializeField] GameObject entrance,exit;
    private Vector3 entranceLoc, exitLoc;
    public override Dictionary<int,Node> children { get => nodes; set => nodes = value; }
    public override Vector3 GetStartLocation { get => entrance.transform.position; set => entranceLoc = value; }
    public override Vector3 GetEndLocation { get => exit.transform.position; set => exitLoc = value; }
    public override int GetRotation(GameObject gameObject) {
        return Mathf.FloorToInt(transform.eulerAngles.x);
    } 
    public SGMagnet() {

    }
    void Start() {
        if (nodes == null) {
            nodes = new Dictionary<int, Node>();
            if (transform.Find(Globals.TOP_EMPTY_NODE_NAME) == null)
            {
                nodes[0] = transform.Find(Globals.EMPTY_NODE_NAME).gameObject.GetComponent<Node>();
            }
            else {
                nodes[0] = transform.Find(Globals.TOP_EMPTY_NODE_NAME).gameObject.GetComponent<Node>();
                nodes[1] = transform.Find(Globals.BOTTOM_EMPTY_NODE_NAME).gameObject.GetComponent<Node>();
            }
        }

    }
}
