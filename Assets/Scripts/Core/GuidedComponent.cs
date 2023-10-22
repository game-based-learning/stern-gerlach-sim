using SternGerlach.Assets.Scripts.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SternGerlach
{
    public class GuidedComponent : MonoBehaviour
    {
        private Experiment exp;
        [SerializeField] public XMLDeserializer xml;
        [SerializeField] Source nbSource;
        private Source instSource;
        private void Update()
        {
            //Debug.Log("gc update");
            exp = xml.currExp;
            instSource = exp.source;
            //Debug.Log(instSource);
            //Debug.Log(nbSource);
            Debug.Log("complete?: "+ instSource.Equals(nbSource));
            
        }
    }
}
