using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loop
{
    public interface BaseState
    {
        void InitState();
        void UpdateState();
        void DisposeState();

    }
}