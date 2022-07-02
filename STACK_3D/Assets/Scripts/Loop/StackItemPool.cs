
using UnityEngine;

namespace Loop
{
    public class StackItemPool : Pool<StackItem>
    {       
        private void Start()
        {
           InitializePool(size);
        }

    } 
}
