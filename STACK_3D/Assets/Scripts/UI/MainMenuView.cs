using System.Collections;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuView : BaseView
    {
        [SerializeField] private TextMeshProUGUI bestScore;
        [SerializeField] private TextMeshProUGUI lastScore;
        [SerializeField] private Button startButton;


        public void UpdateBestSCore(int score)
        {
            this.bestScore.text = score.ToString();

        }

        public void UpdateLastScore(int lastScore)
        {
            this.lastScore.text = lastScore.ToString();
        }

        public void StartGameButtonAddListener(Action callback)
        {
            startButton.onClick.AddListener(callback.Invoke);
        }

    } 
}
