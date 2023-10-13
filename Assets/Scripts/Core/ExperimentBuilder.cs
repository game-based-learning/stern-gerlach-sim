using UnityEngine;

namespace SternGerlach.Assets.Scripts.Core
{
    internal class ExperimentBuilder
    {
        // SETUP INFO
        private Source source = null;
        private string id = "DEFAULT_EXPERIMENT_NAME";

        // MCQ (A: <Message>)
        private string moveForwardToMCQMessage = "DEFAULT_GO_TO_MCQ_MESSAGE";
        private MCQuestion mcq = new MCQuestion();
        private string questionText = "DEFAULT_QUESTION_TEXT";
        private bool voluntary = true;

        // PREDICTION
        private string predictionMessage = "DEFAULT_PREDICTION_MESSAGE";

        // EXECUTION
        public enum ExecutionType { MinParticles } // Add more types of execution types
        private Experiment.ExecutionType executionType = Experiment.ExecutionType.MinParticles;
        private int minParticles = 10;
        public ExperimentBuilder() {  }
        public Experiment Build() {
            GameObject parent = new GameObject(id);
            Experiment exp = parent.AddComponent<Experiment>();
            mcq.question = questionText;
            exp.id = id;
            exp.source = source;
            exp.mcq = mcq;
            exp.predictionMessage = predictionMessage;
            exp.executionType = executionType;
            exp.minParticles = minParticles;
            exp.voluntary = voluntary;
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
        public void SetVoluntaryMCQ(bool voluntary) {
            this.voluntary = voluntary;
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
        public void AddPredictionMessage(string message)
        {
            this.predictionMessage = message;
        }
        public void SetExecution(Experiment.ExecutionType type)
        {
            this.executionType = type;
        }
        public void SetMinParticles(int minParticles)
        {
            this.minParticles = minParticles;
        }
    }
}
