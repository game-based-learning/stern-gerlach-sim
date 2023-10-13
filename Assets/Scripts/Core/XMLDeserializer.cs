using SternGerlach.Assets.Scripts.Core;
using System.IO;
using System.Xml;
using UnityEngine;

namespace SternGerlach
{
    public class XMLDeserializer : MonoBehaviour
    {
        [SerializeField] TextAsset firstXmlFile;
        [SerializeField] GameObjectFactory factory;
        [SerializeField] NodeBuilder nodeBuilder;
        private TextAsset currFile;
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

            ExperimentBuilder experimentBuilder = new ExperimentBuilder();

            string experimentName;
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
                DisplayBadlyFormattedMessage();
            }
            experimentBuilder.SetID(experimentName);

            GameObject exp = new GameObject(experimentName);
            string type = source.Attributes["source"]?.Value;

            Source src = factory.CreateSource(type == "Macroscopic_Magnet");
            experimentBuilder.SetSource(src);

            if (source.ChildNodes.Count == 0) {
                DisplayBadlyFormattedMessage();
            }

            nodeBuilder.SelectNode(src.children[0]);
            ParseNBNode(source.ChildNodes[0]);
            /*            foreach (XmlNode attribute in node.Attributes)
                        {
                            switch (attribute.Name)
                            {
                                case:
                            }
                            Debug.Log(attribute.Value + attribute.Name);
                        }*/

            return experimentBuilder.Build();
        }
        // Parse a nodebuilder node
        void ParseNBNode(XmlNode node)
        {
            string type = node.Attributes["type"]?.Value;
            if (type == "" || type == null)
            {
                DisplayBadlyFormattedMessage();
            }
            switch (type) {
                case "SG_Magnet":
                    nodeBuilder.PlaceSGMagnet();
                    break;
                // Intentional fallthrough. NodeBuilder handles this
                case "Image_Plate":
                case "Large_Image_Plate":
                    nodeBuilder.PlaceImagePlate();
                    break;
            }
            nodeBuilder.Rotate(int.Parse(node.Attributes["angle"]?.Value));
            if (node.ChildNodes.Count == 0) {
                return;
            }
            for (int i = 0; i < node.ChildNodes.Count; i++) {
                nodeBuilder.SelectNode(nodeBuilder.selectedNode.children[i]);
                ParseNBNode(node.ChildNodes[i]);
            }
        }
        void DisplayBadlyFormattedMessage() {
            Debug.Log("Badly formatted xml file named " + currFile.name);
            return;
        }

    }
}
