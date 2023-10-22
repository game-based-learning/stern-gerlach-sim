using UnityEngine;

namespace SternGerlach.Assets.Scripts.Core
{
    public class Experiment : MonoBehaviour
    {
        // SETUP INFO
        public Source source;
        public string id;
        // MCQ
        public MCQuestion mcq;
        public char correctMCAnswer;
        // PREDICTION
        public string predictionMessage;
        // EXECUTION
        public int minParticles = 10;
        public string moveForwardToMCQMessage;
        // CAMERA SETTINGS
        public float sizeModifier = 0f, yPosModifier = 0f, xPosModifier = 0f;
        void Awake() {
            DontDestroyOnLoad(this);
        }
        public string ToString() {
            string debugString = "Nodes: \n";
            debugString += NodeToString(source);
            debugString += "\nExperiment ID:" + id + "\n";
            debugString += mcq.question + "\n";
            foreach ((char, string) answer in mcq.GetAnswerChoices()) {
                debugString += answer.Item1 + ": " + answer.Item2 + " " + mcq.IsCorrect(answer.Item1) + "\n";
            }
            debugString += "PredictionMessage: " + predictionMessage;
            debugString += "\nMin Particles: " + minParticles;
            debugString += "\nMove Forward Message: " + moveForwardToMCQMessage;
            debugString += "\nInstruction settings:";
            return debugString;
        }
        private string NodeToString(Node node) { 
            string nodeToStr = "Node: " + node.GetType().Name + " Rotation: " + node.GetRotation();
            if (node.children == null || node.children.Count == 0) {
                return nodeToStr;
            }
            for (int i = 0; i < node.children.Count; i++) {
                nodeToStr += "\n" + NodeToString(node.children[i]);        
            }
            return nodeToStr;
        }
    }
}
