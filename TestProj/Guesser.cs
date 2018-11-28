using System;

namespace Guessing
{
    public class Guesser
    {
        // Used to record play history
        int[,,] _matrix;
        int lw1, lw2;
        int _sequentialLosesCounter;

        Random _rnd;

        /// <summary>
        /// Expectation which number will be choosen by Human
        /// </summary>
        public int Prediction { get; private set; }

        public int ScoreCpu = 0;
        public int ScoreHuman = 0;
        public int GamesCounter { get { return ScoreCpu + ScoreHuman; } }
        public float ScoreCpuInPercents { get { return ScoreCpu / ((float)GamesCounter / 100); } }

        private int _playerChoice;

        public Guesser()
        {
            _rnd = new Random(DateTime.Now.Millisecond);

            _matrix = new int[2, 2, 2] { { { 0, 0 }, { 0, 0 } }, { { 0, 0 }, { 0, 0 } } };

            // Random value as we have no data to get Prediction
            Prediction = SimulateCoinFlip();

            // Won or lost last play and play before last
            lw2 = lw1 = 0;
        }

        public bool IsWinnerIsHuman(int playerChoice)
        {
            _playerChoice = playerChoice;

            return IsWinnerIsHuman();
        }

        private bool IsWinnerIsHuman()
        {
            return (_playerChoice != Prediction);
        }

        public void CalcNextPrediction()
        {
            if (IsWinnerIsHuman())
            {
                ScoreHuman++;
                _sequentialLosesCounter++;
            }
            else
            {
                ScoreCpu++;
                _sequentialLosesCounter = 0;
            }

            WriteLatestScoresToMatrix();
            CalculatePrediction();
        }

        private void WriteLatestScoresToMatrix()
        {
            _matrix[lw2, lw1, 1] = (_matrix[lw2, lw1, 0] == _playerChoice ? 1 : 0);
            _matrix[lw2, lw1, 0] = _playerChoice;

            lw2 = lw1;
            lw1 = _playerChoice;
        }

        /// <summary>
        /// If lost more than twice in present state, chose random strategy;  Otherwise, keep same strategy
        /// </summary>
        private void CalculatePrediction()
        {
            Prediction = (_matrix[lw2, lw1, 1] == 1 && _sequentialLosesCounter < 2)
                         ? _matrix[lw2, lw1, 0]
                         : SimulateCoinFlip();
        }

        private int SimulateCoinFlip()
        {
            return _rnd.Next(0, 2);
        }
    }
}
