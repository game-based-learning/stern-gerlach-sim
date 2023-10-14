using SternGerlach.Assets.Scripts.Core;
using System.IO;
using System.Xml;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

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
            
            nodeBuilder.SelectNode(src.firstMagnet);
            ParseNBNode(source.ChildNodes[0]);

            Experiment experiment = experimentBuilder.Build();
            Debug.Log(experiment.ToString());
            return experiment;
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
                default:
                    DisplayBadlyFormattedMessage();
                    break;
            }

            nodeBuilder.Rotate(int.Parse(node.Attributes["angle"]?.Value));
            if (node.ChildNodes == null || node.ChildNodes.Count == 0 || nodeBuilder.selectedNode.children == null || nodeBuilder.selectedNode.children.Count == 0)
            {
                return;
            }
            Node parentNode = nodeBuilder.selectedNode;
            for (int i = 0; i < node.ChildNodes.Count; i++) {
                nodeBuilder.SelectNode(parentNode.children[i]);
                ParseNBNode(node.ChildNodes[i]);
            }
        }
        void DisplayBadlyFormattedMessage() {
            Debug.Log("Badly formatted xml file named " + currFile.name);
            return;
        }

    }
}
