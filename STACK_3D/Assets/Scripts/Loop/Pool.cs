using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Loop
{
    public class Pool<TItem> : MonoBehaviour 
        where TItem : MonoBehaviour, IPoolable


    {
        private Queue<TItem> pooledObjects = new Queue<TItem>();   

        [SerializeField] private TItem orginalPrefab;
        
        [SerializeField] protected int size;

        //[SerializeField] private Transform cubeSpawnPos;


        public void InitializePool(int size)
        {
            this.size = size;
            for (int i = 0; i < size; ++i)
               {
                var obj = Instantiate(orginalPrefab);
                obj.PrepareForDeactivate(transform);
                pooledObjects.Enqueue(obj);
               }
        }

        public TItem GetFromPool(Vector3 position)
        {
            if (pooledObjects.Count > 0)
            {
                var obj = pooledObjects.Dequeue();
                obj.PrepareForActivate(position);
                return obj;
            }
            else
            {
                var obj = Instantiate(orginalPrefab);
                obj.PrepareForActivate(position);
                return obj;
            }
        }

        public void ReturnToPool(TItem item)
        {
            if (pooledObjects.Count <= size)
            {
                item.PrepareForDeactivate(transform);
                pooledObjects.Enqueue(item);

            }
            else
            {
                Destroy(item.gameObject);
            }
        }

    } 
}
