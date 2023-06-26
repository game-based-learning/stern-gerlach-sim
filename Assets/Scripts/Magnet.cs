using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : Node
{
    [SerializeField] List<Node> nodes;
    [SerializeField] GameObject entrance,exit;
    private Vector3 entranceLoc, exitLoc;
    public override List<Node> children { get => nodes; set => nodes = value; }
    public override Vector3 GetStartLocation { get => entranceLoc; set => entranceLoc = value; }
    public override Vector3 GetEndLocation { get => exitLoc; set => exitLoc = value; }
    public Magnet() {

    }
    void Start()
    {
        this.entranceLoc = entrance.transform.position;
        this.exitLoc = exit.transform.position;
    }

    // Update is called once per frame
}
