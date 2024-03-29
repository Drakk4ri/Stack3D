using Movement;
using Input;
using Points;
using UI;
using UnityEngine;
using System;

namespace Loop
{
    public class GameState : BaseState
    {
        private MovementSystem movementSystem;
        private StackInput stackInput;
        private StackItemPool pool;
        private LoweringSystem loweringSystem;
        private PointSystem pointSystem;
        private GameView gameView;
        private Action transitionToLoseState;

        
        public GameState(MovementSystem movementSystem, StackInput stackInput, StackItemPool pool, 
            LoweringSystem loweringSystem, PointSystem pointSystem, GameView gameView, Action transitionToLoseState)
        {
            this.movementSystem = movementSystem;
            this.stackInput = stackInput;
            this.pool = pool;
            this.loweringSystem = loweringSystem;
            this.pointSystem = pointSystem;
            this.gameView = gameView;
            this.transitionToLoseState = transitionToLoseState;
        }
        
        public void InitState()
        {
            gameView.ShowView();
            stackInput.OnTapAddListener(OnTap);
            movementSystem.GenerateNewItem(pool);
        }
        public void UpdateState()
        {
            movementSystem.UpdateMovement();
        }
        public void DisposeState()
        {
            gameView.HideView();
            stackInput.ClearListeners();
        }

        private void OnTap()
        {
            var data = movementSystem.StopItem();
            if(data.Result == StopResult.GameOver)
            {
                transitionToLoseState.Invoke();
                return;
            }


            pointSystem.ProcessStopResult(data.Result);
            gameView.UpdateCurrentScore(pointSystem.Points);
            loweringSystem.AddNewItem(data.stoppedItem);

            if (data.fallingItem != null)
            {
                loweringSystem.AddNewItem(data.fallingItem);
            }
            loweringSystem.LowerItem();
            movementSystem.GenerateNewItem(pool);
            var finalPos = 
                pointSystem.ProcessCombo(movementSystem.ItemCurrentlyInMovement, data.lastItems);
            movementSystem.UpdateSpawnPos(finalPos);
        }
    } 
}
