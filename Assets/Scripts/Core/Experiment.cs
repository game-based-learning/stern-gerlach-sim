using UnityEngine;

namespace SternGerlach.Assets.Scripts.Core
{
    internal class Experiment : MonoBehaviour
    {
        // SETUP INFO
        public Source source;
        public string id;
        // MCQ
        public MCQuestion mcq;
        public string moveForwardToMCQMessage;
        public char correctMCAnswer;
        // PREDICTION
        public string predictionMessage;
        // EXECUTION
        public enum ExecutionType { MinParticles } // Add more types of execution types
        public ExecutionType executionType = ExecutionType.MinParticles;
        public int minParticles = 10;
        public bool voluntary;
        void Awake() {
            DontDestroyOnLoad(this);
        }
        public string ToString() {
            string debugString = "Nodes: \n";
            debugString += NodeToString(source);
            debugString += "\nExperiment ID:" + id + "\n";
            debugString += mcq.question + "\n";
            foreach ((char, string) answer in mcq.GetAnswerChoices()) {
                debugString += answer.Item1 + ": " + answer.Item2 + "\n";
            }
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
