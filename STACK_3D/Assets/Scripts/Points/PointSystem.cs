using System;
using UnityEngine;
using Movement;
using Loop;
using DG.Tweening;
using Data;

namespace Points
{
    public class PointSystem
    {
        private int points;
        private int comboPoints;

        private int bestScore;
        private int lastScore;

        private Action onComboCompleted;

        public int Points => points;
        public int BestScore => bestScore;
        public int LastScore => lastScore;

        Vector3 center = new Vector3(10f, -11f, 0); 

        public PointSystem(PlayerData loadedData)
        {
            bestScore = loadedData.bestScore;
            lastScore = loadedData.lastScore;
        }

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

        public Vector3 ProcessCombo(StackItem currentItem, StackItem[] lastItems)
        {

            Vector3 finalPos = Vector3.zero;
            if (comboPoints != 5)
                return finalPos;
            

            foreach (var item in lastItems)
            {
                finalPos = RescaleItem(item);
            }

            RescaleItem(currentItem);
            currentItem.StartComboSound();
            onComboCompleted?.Invoke();
            ClearComboPoints();

            return finalPos;
        }

        private Vector3 RescaleItem(StackItem item)
        {

            var beforeScaleX = item.transform.localScale.x;
            var beforeScaleZ = item.transform.localScale.z;

            var possibleXScale = item.transform.localScale.x * 1.4f;
            possibleXScale = Mathf.Clamp01(possibleXScale);

            var possibleZScale = item.transform.localScale.z * 1.4f;
            possibleZScale = Mathf.Clamp01(possibleZScale);

            var deltaX = (possibleXScale - beforeScaleX) / 2f;
            var deltaZ = (possibleZScale - beforeScaleZ) / 2f;

            var finalPos = MoveItem(item, deltaZ, deltaX, true);

            item.transform
                .DOScale(new Vector3(possibleXScale,
                        item.transform.localScale.y, possibleZScale),
                    0.1f).OnComplete(() => MoveItem(item, deltaZ, deltaX, false));

            return finalPos;


        }

        private Vector3 MoveItem(StackItem item, float deltaZ, float deltaX, bool dry)
        {
            Debug.Log("dupa");
            
            var xDir = Mathf.Sign(center.x - item.transform.position.x);
            var zDir = Mathf.Sign(center.z - item.transform.position.z);

            var finalX = item.transform.position.x + deltaX * xDir;
            var finalZ = item.transform.position.z + deltaZ * zDir;

            var finalPos = new Vector3(finalX, item.transform.position.y, finalZ);

            Debug.Log(finalPos);

            if (!dry)
                item.transform.DOMove(finalPos, 0.1f); // tu po zmianie 
            //item.transform.position = new Vector3(finalX, item.transform.position.y, finalZ);

            return finalPos;

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

        public void OnLose()
        {
            if (points > bestScore)
                bestScore = points;
            lastScore = points;
        }
        

    }
}