using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UI;
using Input;
using Points;

namespace Loop
{
    public class MainMenuState : BaseState
    {
        private StackInput stackInput;        
        private MainMenuView mainMenuView;
        private Action transicionToGameState;
        private PointSystem pointSystem;


        public MainMenuState(Action transitionToGameState, MainMenuView mainMenuView, StackInput stackInput, PointSystem pointSystem)
        {
            this.transicionToGameState = transitionToGameState;
            this.mainMenuView = mainMenuView;
            this.stackInput = stackInput;
            this.pointSystem = pointSystem;

        }
        public void InitState()
        {
            mainMenuView.ShowView();
            //stackInput.OnTapAddListener(StartGame);
            mainMenuView.UpdateBestSCore(pointSystem.BestScore);
            mainMenuView.UpdateLastScore(pointSystem.LastScore);

            mainMenuView.StartGameButtonAddListener(StartGame);
        }
        public void UpdateState()
        {
            
        }
        public void DisposeState()
        {
            stackInput.ClearListeners();
            mainMenuView.HideView();
        }
        
        private void StartGame()
        {
            transicionToGameState.Invoke();
        }

    } 
}
