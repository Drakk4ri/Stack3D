
using System;
using UI;
using Points;
using UnityEngine.SceneManagement;
using Data;

namespace Loop
{
    public class LoseState : BaseState
    {
        private LoseView loseView;
        private PointSystem pointSystem;
        private SaveSystem saveSystem;

        public LoseState(LoseView loseView, PointSystem pointSystem, SaveSystem saveSystem)
        {
            this.loseView = loseView;
            this.pointSystem = pointSystem;
            this.saveSystem = saveSystem;
        }
        

        public void InitState()
        {
            pointSystem.OnLose();
            loseView.UpdateScore(pointSystem.Points);
            loseView.RestartButtonAddListener(RestartLevel);

            saveSystem.LoadedData.bestScore = pointSystem.BestScore;
            saveSystem.LoadedData.lastScore = pointSystem.LastScore;
            saveSystem.Save();
            loseView.ShowView();
        }
        public void UpdateState()
        {
            
        }
        public void DisposeState()
        {
           loseView.HideView();
        }

        private void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //to wrzuca do g³ównego menu
        }

        private void GoToMainMeny()
        {
            //przekierowanie do meu g³ównego - ma zrobiæ to co robi przycisk start
        }
    }
}