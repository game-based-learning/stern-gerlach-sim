using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SternGerlach.Assets.Scripts.Core.Experiment;

namespace SternGerlach.Assets.Scripts.Core
{
    internal class ExperimentBuilder
    {
        // SETUP INFO
        private Source source = null;
        private string id = "DEFAULT_EXPERIMENT_NAME";

        // MCQ
        private Dictionary<char, (string,bool)> mcq = new Dictionary<char, (string, bool)>();
        private char correctMCAnswer = '\0';

        // PREDICTION
        private string predictionMessage = "DEFAULT_PREDICTION_MESSAGE";

        // EXECUTION
        public enum ExecutionType { MinParticles } // Add more types of execution types
        private ExecutionType executionType = ExecutionType.MinParticles;
        private int minParticles = 10;

        public ExperimentBuilder() { 

        }
        public Experiment GetExperiment() {
            return new Experiment();
        }
        public void AddMCAnswer(char label, string answer, bool correctAnswer)
        {
            mcq.Add(label, (answer, correctAnswer));
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
        public void SetExecutionType(ExecutionType type)
        {
            this.executionType = type;
        }
        public void SetMinParticles(int minParticles)
        {
            this.minParticles = minParticles;
        }
    }
}
