using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UI;

namespace Loop
{
    public class MainMenuState : BaseState
    {
        private Action transicionToGameState;
        private MainMenuView mainMenuView;
        
        public MainMenuState(Action transitionToGameState, MainMenuView mainMenuView)
        {
            this.transicionToGameState = transitionToGameState;
            this.mainMenuView = mainMenuView;
        }
        public void InitState()
        {
            mainMenuView.ShowView();
        }
        public void UpdateState()
        {
            
        }
        public void DisposeState()
        {
            mainMenuView.HideView();
        }
    } 
}
