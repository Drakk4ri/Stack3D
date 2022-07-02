using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Movement;
using UnityEngine.UI;
using Loop;
using DG.Tweening;

namespace Points
{
    public class PointSystem
    {
        private int points;
        private int comboPoints;

        private Action onComboCompleted;

        public int Points => points;

        
        public void ProcessStopResult(StopResult result)
        {
            switch (result)
            {
                case StopResult.Point:
                    ClearComboPoints();
                    AddPoint();
                    break;
                case StopResult.ComboPoint:
                    AddPoint();
                    AddComboPoint();
                    break;

            }

        }

        public void ProcessCombo(StackItem currentItem, StackItem[] lastItems)
        {
            Debug.Log(comboPoints);
            if (comboPoints != 5)
                return;
            Debug.Log("po combo");

            foreach (var item in lastItems)
            {
                RescaleItem(item);
            }

            RescaleItem(currentItem);
            onComboCompleted?.Invoke();
            ClearComboPoints();

        }

        private void RescaleItem(StackItem item)
        {
            var possibleXScale = item.transform.localScale.x * 1.4f;
            possibleXScale = Mathf.Clamp(possibleXScale, 0, 1f); //Clamp01(possibleXScale); - to samo tylko skrót.

            var possibleZScale = item.transform.localScale.z * 1.4f;
            possibleZScale = Mathf.Clamp(possibleZScale, 0, 1f);

            item.transform.DOScale(new Vector3(possibleXScale, 0.27099f, possibleZScale), 0.1f);
            Debug.Log("dupa");
        }

        private void AddPoint()
        {
            points += 1;
        }

        private void AddComboPoint()
        {
            comboPoints += 1;

        }

        private void ClearComboPoints()
        {
            comboPoints = 0;
        }

        

    }
}