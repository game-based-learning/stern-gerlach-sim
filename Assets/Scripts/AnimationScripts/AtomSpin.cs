using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomSpin : MonoBehaviour
{
    [SerializeField] GameObject rings;
    private Transform ringTransform;
    // Start is called before the first frame update
    void Start()
    {
        this.ringTransform = rings.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,3.0f*20f*Time.deltaTime,0);
        transform.Rotate(0,0,6.0f*20f*Time.deltaTime);
    }
}
