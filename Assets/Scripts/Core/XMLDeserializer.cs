using SternGerlach.Assets.Scripts.Core;
using System.IO;
using System.Xml;
using UnityEditor;
using UnityEngine;

namespace SternGerlach
{
    public class XMLDeserializer : MonoBehaviour
    {
        [SerializeField] TextAsset firstXmlFile;
        [SerializeField] GameObjectFactory factory;
        [SerializeField] NodeBuilder nodeBuilder;
        string sourceFilePath = "/Scripts/Core/Experiments/";
        private TextAsset currFile;
        void Start()
        {
            ParseExperiment(firstXmlFile);
        }
        Experiment ParseExperiment(TextAsset xmlFile) {
            // Create XML reader settings
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;                         // Exclude comments

            this.currFile = xmlFile;
            string data = xmlFile.text;
            XmlDocument xmlDocument= new XmlDocument();
            xmlDocument.Load(XmlReader.Create(AssetDatabase.GetAssetPath(xmlFile), settings));
            string path = "//experiment/setup";
            XmlNodeList nodeList = xmlDocument.SelectNodes(path);

            ExperimentBuilder experimentBuilder = new ExperimentBuilder();

            string experimentName;
            XmlNode source = null;

            source = nodeList[0];

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
            Debug.Log("Children: " + source.HasChildNodes + "Amount: " + source.ChildNodes.Count);
            
            nodeBuilder.SelectNode(src.firstMagnet);
            Debug.Log(2);
            ParseNBNode(source.ChildNodes[0]);
            Debug.Log(3);
            /*            foreach (XmlNode attribute in node.Attributes)
                        {
                            switch (attribute.Name)
                            {
                                case:
                            }
                            Debug.Log(attribute.Value + attribute.Name);
                        }*/
            Experiment experiment = experimentBuilder.Build();
            experiment.ToString();
            return experiment;
        }
        // Parse a nodebuilder node
        void ParseNBNode(XmlNode node)
        {
            Debug.Log("Node: " + node);
            string type = node.Attributes["type"]?.Value;
            if (type == "" || type == null)
            {
                DisplayBadlyFormattedMessage();
            }

            Debug.Log("A");
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

            Debug.Log("B");
            nodeBuilder.Rotate(int.Parse(node.Attributes["angle"]?.Value));
            if (node.ChildNodes.Count == 0) {
                return;
            }

            Debug.Log("C");
            for (int i = 0; i < node.ChildNodes.Count; i++) {

                Debug.Log("The line below is where this blows up");
                nodeBuilder.SelectNode(nodeBuilder.selectedNode.children[i]);

                Debug.Log("E");
                ParseNBNode(node.ChildNodes[i]);
            }

            Debug.Log("D");
        }
        void DisplayBadlyFormattedMessage() {
            Debug.Log("Badly formatted xml file named " + currFile.name);
            return;
        }

    }
}
