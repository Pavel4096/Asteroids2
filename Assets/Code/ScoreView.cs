using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids2
{
    internal sealed class ScoreView : MonoBehaviour, IScoreView
    {
        [SerializeField] private Text scoreText;

        private IPlayerViewModel playerViewModel;

        public void Init(IPlayerViewModel _playerViewModel)
        {
            playerViewModel = _playerViewModel;
            playerViewModel.ScoreChanged += UpdateScore;
        }

        public void UpdateScore(int newScore)
        {
            scoreText.text = newScore.ToString();
        }

        ~ScoreView()
        {
            playerViewModel.ScoreChanged -= UpdateScore;
        }
    }

}
