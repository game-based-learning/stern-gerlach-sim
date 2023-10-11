using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SternGerlach.Source;

namespace SternGerlach.Assets.Scripts.Core
{
    internal class Experiment
    {
        // SETUP INFO
        private Source source = null;
        private string id = "DEFAULT_EXPERIMENT_NAME";

        // MCQ
        private Dictionary<char, string> mcq = new Dictionary<char, string>();
        private char correctMCAnswer = '\0';

        // PREDICTION
        private string predictionMessage = "DEFAULT_PREDICTION_MESSAGE";

        // EXECUTION
        public enum ExecutionType { MinParticles } // Add more types of execution types
        private ExecutionType executionType = ExecutionType.MinParticles;
        private int minParticles = 10;

    }
}
