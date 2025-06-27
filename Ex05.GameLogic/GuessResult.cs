using Ex05.Enums;
using System.Collections.Generic;

namespace Ex05.GameLogic
{
    public struct GuessResult
    {
        public int Bulls { get; set; }
        public int Hits { get; set; }
        public List<eFeedbackOption> Feedback { get; set; }

        public GuessResult(int i_Bulls, int i_Hits, List<eFeedbackOption> i_Feedback)
        {
            Bulls = i_Bulls;
            Hits = i_Hits;
            Feedback = i_Feedback;
        }
    }
}
