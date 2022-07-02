using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UI
{
    public class GameView : BaseView
    {
        [SerializeField] private TextMeshProUGUI currentScore;

        public void UpdateCurrentScore(int score)
        {
            this.currentScore.text = score.ToString();
        }
    }
}