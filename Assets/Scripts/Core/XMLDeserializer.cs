using SternGerlach.Assets.Scripts.Core;
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
        private TextAsset currFile;
        // Start is called before the first frame update
        void Start()
        {
            ParseExperiment(firstXmlFile);    
        }
        Experiment ParseExperiment(TextAsset xmlFile) {
            this.currFile = xmlFile;
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
                DisplayBadlyFormattedMessage();
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
                DisplayBadlyFormattedMessage();
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
        void DisplayBadlyFormattedMessage() {
            Debug.Log("Badly formatted xml file named " + currFile.name);
            return;
        }
        // Parse a nodebuilder node
        Node ParseNBNode(XmlNode node) {
            Debug.Log(node.Name);
            string type = node.Attributes["type"]?.Value;
            if (type == "" || type == null)
            {
                DisplayBadlyFormattedMessage();
            }
            if (type == "SG_Magnet")
            {
                // create sg magnet
            }
            else if (type == "Image_Plate")
            {
                // create image plate
            }
            else if (type == "Large_Image_Plate") { 
                // create large image plate
            }
            return null;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
