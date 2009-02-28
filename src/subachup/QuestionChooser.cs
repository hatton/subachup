using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Subachup
{
    /// <summary>
    /// This class handles giving a sequence questions, based on the student's past performance.
    /// </summary>
    public class QuestionChooser
    {
        public IQuizItem CurrentQuizItem { get; set; }
        private readonly List<IQuizItem> _quizItems;

        /// <summary>
        /// 0-10.  How "focussed" vs. random the quizing should be.  That is, how much should it drill on past errors.
        /// </summary>
        public double FocusAmount
        {
            get; set;
        }

        protected int _indexOfCurrentQuestion;


        public QuestionChooser(List<IQuizItem> quizItems)
        {
            _quizItems = quizItems;
        }

        protected int[] RandomizeVisitOrder(int[] input)
        {
            int[] output = new int[input.Length];
            for (int j = 0; j < output.Length; j++)
            {
                output[j] = -1;
            }

            Random rnd = new Random();
            int count = 0;

            while (count < output.Length)
            {
                int destination = rnd.Next(0, output.Length);

                if (output[destination] == -1)
                {
                    output[destination] = count;
                    count++;
                }
            }
            return output;
        }

        public IQuizItem PickNextQuizItem()
        {
            Random rnd = new Random();
            Debug.Assert((FocusAmount >= 0) && (FocusAmount <= 10));
            double howRandom = (10 - FocusAmount); //0 to 10
            int best = -1;
            double lowScore = 100000;
            int[] scores = MakeScoresArray();
            int[] visit = RandomizeVisitOrder(scores);
            for (int i = 0; i < visit.Length; i++)
            {
                int index = visit[i];

                double randomizer = 0.5 - rnd.NextDouble(); // centered around 0
                randomizer = randomizer * howRandom; //spread out around 0 (-5 to 5?)
                double score = randomizer + (scores[index]);

                //enhance need something to favor those we haven't even tested
                if (index == _indexOfCurrentQuestion)
                    score = 100; //very unlikely, but won't break if only one item in the quiz
                if (score < lowScore)
                {
                    lowScore = score;
                    best = index;
                }
            }
            CurrentQuizItem = _quizItems[best];
            return CurrentQuizItem;
        }


        public int[] MakeScoresArray()
        {
            int[] scores = new int[_quizItems.Count];
            for (int i = 0; i < _quizItems.Count    ; i++)
            {
                scores[i] = _quizItems[i].Score;
            }
            return scores;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>true if it was the right answer</returns>
        public bool GaveAnswer(IQuizItem answer)
        {
            CurrentQuizItem.LastQuizzedDate = DateTime.Now;
            if (answer == CurrentQuizItem)
            {
                ++CurrentQuizItem.Score;
                ++CurrentQuizItem.CorrectWithoutMistakesCount;
                //_readyForNext = true;
                return true;
            }
            else
            {
               CurrentQuizItem.CorrectWithoutMistakesCount = 0;
                CurrentQuizItem.Score = -2; //-=2;//you'll get one back when you click it right
                //also decrement the score of the one we mistakenly clicked on
                answer.CorrectWithoutMistakesCount = 0;
                answer.Score = -1;
                return false;
            }
        }
    }
}
