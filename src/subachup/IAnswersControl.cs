using System.Collections.Generic;
using subachup.utility;

namespace subachup
{
    public interface IAnswersControl
    {
        event Proc<IEnumerable<IQuizItem>> GaveAnAnswer;
        IEnumerable<IQuizItem> QuizItems { get; set; }
        void LoadContents();//IEnumerable<IQuizItem> choices);
        void ShowAnswerLocations(Utterance utterance);
        
    }
}