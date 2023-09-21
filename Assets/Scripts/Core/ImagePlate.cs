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
}
