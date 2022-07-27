using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

namespace UI
{
    public class LoseView : BaseView
    {
        [SerializeField] private TextMeshProUGUI score;
        [SerializeField] private Button goToMainMenuButton;
        [SerializeField] private Button restartGameButton;

        
        
        public void UpdateScore(int currentScore)
        {
            score.SetText($"YOUR SCORE WAS: {currentScore.ToString()}"); //$"score: 
        }

        public void RestartButtonAddListener(Action callback)
        {
            goToMainMenuButton.onClick.AddListener(callback.Invoke);
        }

        public void RestrtGameButtonAddListener(Action callback)
        {
            restartGameButton.onClick.AddListener(callback.Invoke);
        }
    }

}