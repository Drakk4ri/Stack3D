using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class StackInput : MonoBehaviour
    {
        private Action onTap;
        public void OnTapAddListener(Action callback)

        {
            onTap += callback;
        }        

        public void ClearListeners()
        {
            onTap = null;
        }

        public void  OnTap(InputAction.CallbackContext ctx)
        {
            if(ctx.action.WasPerformedThisFrame())
            {
                onTap?.Invoke();
            }

        }
    } 
}
