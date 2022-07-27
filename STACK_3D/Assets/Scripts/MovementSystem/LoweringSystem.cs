using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Loop;
using DG.Tweening;

namespace Movement
{
    public class LoweringSystem : MonoBehaviour
    {
        [SerializeField]
        private List<StackItem> gameplayItems;

        public StackItem InitItem => gameplayItems[0];
        float timeTolowerItem = 0.4f;
        float howMuchLowerItem = 0.271f;

        public void AddNewItem(StackItem item)
        {
            gameplayItems.Add(item);
        }


        public void LowerItem()
        {
            var downVector = Vector3.down; // to jest nowy Vector3 (0,-1,0) 
            foreach (var item in gameplayItems)
            {
                
                var finalPos = item.transform.position + downVector * howMuchLowerItem;
                item.transform.DOMove(finalPos, timeTolowerItem);
            }
        }

    }
}