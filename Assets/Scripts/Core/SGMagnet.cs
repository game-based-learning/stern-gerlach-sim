using SternGerlach;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SGMagnet : Node
{
    [SerializeField] List<Node> nodes;
    [SerializeField] GameObject entrance,exit;
    [SerializeField] TextMeshPro textCount = null;
    private Vector3 entranceLoc, exitLoc;
    private Dictionary<int,Node> childs = new Dictionary<int,Node>();
    public override Dictionary<int,Node> children { get => childs; set => childs = value; }
    public override Vector3 GetStartLocation { get => entrance.transform.position; set => entranceLoc = value; }
    public override Vector3 GetEndLocation { get => exit.transform.position; set => exitLoc = value; }
    public SGMagnet() { }
    void Awake() {
        if (childs == null)
        {
            childs = new Dictionary<int,Node>();
        }
        else {
            int count = 0;
            foreach (Node node in nodes)
            {
                childs[count++] = node;
            }
        }
    }
}
