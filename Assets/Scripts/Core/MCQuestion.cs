using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SternGerlach.Assets.Scripts.Core
{
    public class MCQuestion
    {
        // Kind of messy to have a nested tuple but this could be refactored
        private Dictionary<char, ((string, bool), string)> mcq = new Dictionary<char, ((string, bool),string)>();
        public string question = "\0";
        public int GetNumberOfAnswers() { 
            return mcq.Count;
        }
        public void AddAnswer(char label, string answerText, string pickedText, bool isCorrect) {
            mcq.Add(label, ((answerText, isCorrect), pickedText)); 
        }
        public bool IsCorrect(char label) {
            return mcq[label].Item1.Item2;
        }
        // Get message that should be displayed when this answer is chosen
        public string GetPickedMessage(char label) {
            return mcq[label].Item2;
        }
        // Gets a list of answer labels and their corresponding answer text; i.e.
        // A It flies up!
        // B It flies down!
        // C The sun will explode :O
        public List<(char, string)> GetAnswerChoices() {
            List<(char,string)> answerChoices = new List<(char, string)>();
            foreach (char c in mcq.Keys) {
                answerChoices.Add((c, mcq[c].Item1.Item1));
            }
            return answerChoices;
        }
    }
}
