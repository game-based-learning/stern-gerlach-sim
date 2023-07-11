using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SGMagnet : Node
{
    [SerializeField] List<Node> nodes;
    [SerializeField] GameObject entrance,exit;
    private Vector3 entranceLoc, exitLoc;
    public override List<Node> children { get => nodes; set => nodes = value; }
    public override Vector3 GetStartLocation { get => entranceLoc; set => entranceLoc = value; }
    public override Vector3 GetEndLocation { get => exitLoc; set => exitLoc = value; }
    public override int GetRotation(GameObject gameObject) {
        return Mathf.FloorToInt(transform.eulerAngles.x);
    } 
    public SGMagnet() {

    }
    void Start()
    {
        this.entranceLoc = entrance.transform.position;
        this.exitLoc = exit.transform.position;
    }

    // Update is called once per frame
}
