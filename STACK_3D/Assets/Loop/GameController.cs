using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

namespace Loop
{

    public class GameController : MonoBehaviour
    {
        
        [SerializeField] private MainMenuView mainMenuView;
        
        private MainMenuState mainMenuState;
        private GameState gameState;

        private Action transitionToGameState;
        
        private IBaseState currentlyActiveState;
        
        private void Start()
        {
            transitionToGameState += () => ChangeState(gameState);
            
            mainMenuState = new MainMenuState(transitionToGameState, mainMenuView);
            gameState = new GameState();


            ChangeState(mainMenuState);
        }

        void Update()
        {
            currentlyActiveState?.UpdateState();
        }
        private void OnDestroy()
        {
            
        }

        private void ChangeState(IBaseState newState)
        {
            currentlyActiveState?.DisposeState();
            currentlyActiveState = newState;
            currentlyActiveState?.InitState();
        }
    }

}