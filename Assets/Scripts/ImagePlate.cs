using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImagePlate : Node
{    
    public override Vector3 GetStartLocation { get => base.transform.position; set => base.transform.position = value; }
    public override Vector3 GetEndLocation { get => base.transform.position; set => base.transform.position = value; }
    public override List<Node> children { get => null; set { } }
    [SerializeField] GameObject indicator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowIndicator() {
        this.indicator.SetActive(true);
    }
}
