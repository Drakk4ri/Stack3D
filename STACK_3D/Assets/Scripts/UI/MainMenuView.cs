using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class MainMenuView : BaseView
    {
        [SerializeField] private TextMeshProUGUI bestScore;
        [SerializeField] private TextMeshProUGUI lastScore;


        public void UpdateBestSCore(int score)
        {
            this.bestScore.text = score.ToString();

        }

        public void UpdateLastScore()
        {
            this.lastScore.text = lastScore.ToString();
        }

    } 
}
