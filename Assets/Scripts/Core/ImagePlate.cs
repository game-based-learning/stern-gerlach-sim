using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ImagePlate : Node
{    
    public override Vector3 GetStartLocation { get => base.transform.position; set => base.transform.position = value; }
    public override Vector3 GetEndLocation { get => base.transform.position; set => base.transform.position = value; }
    public override Dictionary<int, Node> children { get => null; set { } }
    [SerializeField] GameObject indicator;
    public int collapseCount = 0; //added code
    [SerializeField] public TextMeshPro textCount; //added code
    public void ShowIndicator() {
        this.indicator.SetActive(true);
    }
    void FixedUpdate()
    {
        textCount.transform.LookAt(Camera.main.transform);
        textCount.transform.Rotate(0f, 180f, 0f);
    }
}
