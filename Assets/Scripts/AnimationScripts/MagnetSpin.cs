using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetSpin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 6.0f * 20f * Time.deltaTime, Space.Self);
        //transform.Rotate(0, 0, 6.0f * 20f * Time.deltaTime);
    }
}
