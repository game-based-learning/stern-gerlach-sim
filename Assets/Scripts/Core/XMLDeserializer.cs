using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Assertions;
using static SternGerlach.Source;

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
            string experimentName;
            SourceType sourceType = SourceType.MacroscopicMagnet;
            XmlNode source = null;
            foreach (XmlNode node in nodeList)
            {
                source = node;
            }
            if (source == null) {
                DisplayBadlyFormattedMessage(xmlFile);
            }
            experimentName = source.Attributes["id"]?.Value;
            if (experimentName == "" || experimentName == null) {
                experimentName = "Default_Experiment_Name";
            }
            string type = source.Attributes["source"]?.Value;
            if (type == "Silver_Atom") {
                sourceType = SourceType.SilverAtom;
            }
            else if (type == "Macroscopic_Magnet") {
                sourceType = SourceType.MacroscopicMagnet;
            }
            Debug.Log("Experiment ID:" + experimentName);
            Debug.Log("Source Type:" + sourceType.ToString());

            if (source.ChildNodes.Count == 0) {
                DisplayBadlyFormattedMessage(xmlFile);
            }
            ParseNBNode(source.ChildNodes[0]);
/*            foreach (XmlNode attribute in node.Attributes)
            {
                switch (attribute.Name)
                {
                    case:
                }
                Debug.Log(attribute.Value + attribute.Name);
            }*/
        }
        void DisplayBadlyFormattedMessage(TextAsset file) {
            Debug.Log("Badly formatted xml file named " + file.name);
            return;
        }
        // Parse a nodebuilder node
        Node ParseNBNode(XmlNode node) {
            Debug.Log(node.Name);
            return null;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
