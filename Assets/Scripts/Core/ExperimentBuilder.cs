using UnityEngine;

namespace SternGerlach.Assets.Scripts.Core
{
    internal class ExperimentBuilder
    {
        // SETUP INFO
        private Source source = null;
        private string id = "DEFAULT_EXPERIMENT_NAME";

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
            return exp;
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
    }
}
