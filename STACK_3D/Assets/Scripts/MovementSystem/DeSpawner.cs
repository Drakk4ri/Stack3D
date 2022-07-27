using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Loop;

namespace Movement
{
    public class DeSpawner : MonoBehaviour
    {
        [SerializeField] private StackItemPool pool;

        private void OnTriggerEnter(Collider other)
        {
            var item = other.GetComponent<StackItem>();
            if (item != null)
            {
                pool.ReturnToPool(item);
            }
        }


    }
}