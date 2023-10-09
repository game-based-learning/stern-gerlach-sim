using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

namespace SternGerlach
{
    public class XMLDeserializer : MonoBehaviour
    {
        [SerializeField] TextAsset firstXmlFile;
        // Start is called before the first frame update
        void Start()
        {
            Parse(firstXmlFile);    
        }
        void Parse(TextAsset xmlFile) { 
            string data = xmlFile.text;
            XmlDocument xmlDocument= new XmlDocument();
            xmlDocument.Load(new StringReader(data));
            string path = "//experiment/setup";
            XmlNodeList nodeList = xmlDocument.SelectNodes(path);
            foreach (XmlNode node in nodeList)
            {
                Debug.Log(node.FirstChild.Name);
            }
        }


        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
