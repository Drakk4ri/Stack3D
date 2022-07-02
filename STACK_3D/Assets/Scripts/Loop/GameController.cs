using System;
using UnityEngine;
using UI;
using Input;
using Movement;
using Points;

namespace Loop
{

    public class GameController : MonoBehaviour
    {
        
        [SerializeField] 
        private MainMenuView mainMenuView;

        [SerializeField]
        private GameView gameView;

        [SerializeField] 
        private StackInput stackInput;
        
        [SerializeField] 
        private StackItemPool pool;

        [SerializeField]
        private LoweringSystem loweringSystem;
        
        private MovementSystem movementSystem;
        private PointSystem pointSystem;
        
        private MainMenuState mainMenuState;
        private GameState gameState;

        private Action transitionToGameState;
        
                
        private BaseState currentlyActiveState;
        
        private void Start()
        {
            transitionToGameState += () => ChangeState(gameState);

            movementSystem = new MovementSystem(loweringSystem.InitItem);

            pointSystem = new PointSystem();

            mainMenuState = new MainMenuState(transitionToGameState, mainMenuView);
                        gameState = new GameState(movementSystem, stackInput, pool, loweringSystem, pointSystem, gameView);

            ChangeState(gameState); //potem do zmainy jak zrobimy menu g³ówne
        }

        void Update()
        {
            currentlyActiveState?.UpdateState();
        }
        private void OnDestroy()
        {
            
        }

        private void ChangeState(BaseState newState)
        {
            currentlyActiveState?.DisposeState();
            currentlyActiveState = newState;
            currentlyActiveState?.InitState();
        }
    }

}