using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Loop
{
    public class MainMenuState : IBaseState
    {
        private Action transicionToGameState;
        
        public MainMenuState(Action transitionToGameState)
        {
            this.transicionToGameState = transitionToGameState;
        }
        public void InitState()
        {
     
        }
        public void UpdateState()
        {
            Debug.Log("Dupa");
            if(Input.GetKeyUp(KeyCode.Space))
            {
                transicionToGameState.Invoke();
            }
        }
        public void DisposeState()
        {
            
        }
    } 
}
