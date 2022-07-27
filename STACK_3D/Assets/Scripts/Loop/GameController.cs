using System;
using UnityEngine;
using UI;
using Input;
using Movement;
using Points;
using Data;

namespace Loop
{

    public class GameController : MonoBehaviour
    {
        
        [SerializeField] 
        private MainMenuView mainMenuView;

        [SerializeField]
        private GameView gameView;

        [SerializeField]
        private LoseView loseView;

        [SerializeField] 
        private StackInput stackInput;
        
        [SerializeField] 
        private StackItemPool pool;

        [SerializeField]
        private LoweringSystem loweringSystem;
        
        private MovementSystem movementSystem;
        private PointSystem pointSystem;
        private SaveSystem saveSystem;

        private MainMenuState mainMenuState;
        private GameState gameState;
        private LoseState loseState;

        private Action transitionToGameState;
        private Action transitionToLoseState;
        
                
        private BaseState currentlyActiveState;
        
        private void Start()
        {
            transitionToGameState += () => ChangeState(gameState);
            transitionToLoseState += () => ChangeState(loseState);

            saveSystem = new SaveSystem();
            saveSystem.Load();

            movementSystem = new MovementSystem(loweringSystem.InitItem);
            pointSystem = new PointSystem(saveSystem.LoadedData);


            mainMenuState = new MainMenuState(transitionToGameState, mainMenuView, stackInput, pointSystem);
            gameState = new GameState(movementSystem, stackInput, pool, loweringSystem, pointSystem, gameView, transitionToLoseState);
            loseState = new LoseState(loseView, pointSystem, saveSystem);


            ChangeState(mainMenuState);
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