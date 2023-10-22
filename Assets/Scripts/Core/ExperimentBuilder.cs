using UnityEngine;

namespace SternGerlach.Assets.Scripts.Core
{
    internal class ExperimentBuilder
    {
        private GuidedComponent guidedComponent;
        // SETUP INFO
        private Source source;
        private string id = "DEFAULT_EXPERIMENT_NAME";

        // INSTRUCTION SETTINGS
        private float sizemodifier = 0f, xposmodifier = 0f, yposmodifier = 0f;

        // MCQ (A: <Message>)
        private MCQuestion mcq = new MCQuestion();
        private string questionText = "DEFAULT_QUESTION_TEXT";

        // PREDICTION
        private string predictionMessage = "DEFAULT_PREDICTION_MESSAGE";

        // EXECUTION
        private int minParticles = 10;
        private string moveForwardToMCQMessage = "DEFAULT_GO_TO_MCQ_MESSAGE";
        public ExperimentBuilder() {  }
        public Experiment Build() {
            GameObject parent = new GameObject(id);
            Experiment exp = parent.AddComponent<Experiment>();
            mcq.question = questionText;
            exp.id = id;
            exp.source = source;
            exp.mcq = mcq;
            exp.predictionMessage = predictionMessage;
            exp.minParticles = minParticles;
            exp.moveForwardToMCQMessage = moveForwardToMCQMessage;
            exp.guidedComponent = guidedComponent;
            exp.sizeModifier = sizemodifier;
            exp.xPosModifier = xposmodifier;
            exp.yPosModifier= yposmodifier;
            return exp;
        }
        public void SetGuidedComponent(GuidedComponent guidedComponent) {
            this.guidedComponent = guidedComponent;
        }
        public void SetQuestionText(string question) {
            this.questionText = question;
        }
        public void AddMCAnswer(char label, string answer, string pickedMessage, bool correctAnswer)
        {
            mcq.AddAnswer(label, answer, pickedMessage, correctAnswer);
        }
        public void SetMoveForwardToMCQMessage(string message) {
            this.moveForwardToMCQMessage = message;
        }
        public void SetSource(Source source)
        {
            this.source = source;
        }
        public void SetID(string id)
        {
            this.id = id;
        }
        public void SetPredictionMessage(string message)
        {
            this.predictionMessage = message;
        }
        public void SetMinParticles(int minParticles)
        {
            this.minParticles = minParticles;
        }
        //  sizemodifier="0" xposmodifier ="0" yposmodifier="0"
        public void SetInstructionModifier(string type, float modification) { 
            switch (type)
            {
                case "sizemodifier":
                    this.sizemodifier = modification;
                    break;
                case "xposmodifier":
                    this.xposmodifier = modification;
                    break;
                case "yposmodifier":
                    this.yposmodifier = modification;
                    break;
            }
        }
    }
}
