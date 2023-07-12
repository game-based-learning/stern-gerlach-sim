using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SternGerlach
{
    public class PointManager : MonoBehaviour
    {
        [SerializeField] private Transform[] points;
        [SerializeField] private Transform cam;
        private bool rotating;
        private Transform focusObject;
        // Start is called before the first frame update
        public void Focus(Transform t)
        {
            if(points.Contains(t))
            {
                rotating= true;
                focusObject= t.Find("viewpoint");
            }
        }

        IEnumerator Transition(Transform final, float totalTime = 1.0f)
        {
            Quaternion initialR = cam.rotation;
            Vector3 initialP= cam.position;
            Quaternion endR = final.rotation;
            Vector3 endP= final.position;

            float timePassed = 0.0f;
            while(timePassed < totalTime && rotating)
            {
                cam.rotation = Quaternion.Slerp(initialR, endR, timePassed/totalTime);
                cam.position = Vector3.Slerp(initialP, endP, timePassed / totalTime);
                timePassed += Time.deltaTime;
                yield return null;
            }
            cam.rotation = endR;
            cam.position = endP;
            rotating= false;
        }
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (rotating)
            {
                StartCoroutine(Transition(focusObject));
            }
        }
    }
}
