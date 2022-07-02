using Loop;
using UnityEngine;
using System.Collections.Generic;

namespace Movement
{

    public class MovementSystem
    {
        private const int NUMBER_OF_COMBO_ITEMS = 5;

        private StackItem itemCurrentlyInMovement;
        public StackItem ItemCurrentlyInMovement => itemCurrentlyInMovement;


        private StackItem lastItem;
        private StackItemPool pool;
        private StackItem[] lastItems = new StackItem[NUMBER_OF_COMBO_ITEMS];
        
        private Line currentlyActiveLine;

        private Line xAxis = new Line(new Vector3(12f, -8.356f, 0f), new Vector3(8f, -8.356f, 0f));
        private Line zAxis = new Line(new Vector3(10f, -8.356f, -2f), new Vector3(10f, -8.356f, 2f));

        private int counter = 0;
        private int comboCounter = 0;

        private float currentTime;
        private float startTime;
        private float duration = 3f;



        public MovementSystem(StackItem lastItem)
        {
            this.lastItem = lastItem;
            counter = Random.Range(0, 150);
            lastItem.SetColour((counter / 100f) % 1f, 0.8f);
        }


        public StopData StopItem()
        {
            var curr = itemCurrentlyInMovement;
            itemCurrentlyInMovement = null;

            var isXAxis = (counter - 1) % 2 == 0; // ? true : false;
            var distance = 0f;
            var limit = 0f;
            var direction = 0f;


            if (isXAxis)
            {
                distance = curr.transform.position.x - lastItem.transform.position.x;
                direction = Mathf.Sign(distance);
                distance = Mathf.Abs(distance);
                limit = lastItem.transform.localScale.x;
            }
            else
            {
                distance = curr.transform.position.z - lastItem.transform.position.z;
                direction = Mathf.Sign(distance);
                distance = Mathf.Abs(distance);
                limit = lastItem.transform.localScale.z;
            }

            var result = StopResult.GameOver;

            StackItem fallingItem = null;

            if (distance >= 0f && distance <= 0.1f)
            {
                result = StopResult.ComboPoint;
                curr.transform.position = new Vector3(lastItem.transform.position.x,
                    curr.transform.position.y, lastItem.transform.position.z);
            }
            else if (distance > 0.1f && distance <= limit)
            {
                result = StopResult.Point;
                if (isXAxis)
                {
                    fallingItem = CutItemOnX(curr, distance, direction);
                }
                else
                {
                    fallingItem = CutItemOnZ(curr, distance, direction);
                }
            }


            lastItem = curr;

            lastItems[comboCounter % NUMBER_OF_COMBO_ITEMS] = lastItem;
            comboCounter++;
            
            
            var data = new StopData(curr, fallingItem, result, lastItems);
            return data;
        }
         
        public void GenerateNewItem (StackItemPool pool)
        {

            this.pool = pool;
            currentlyActiveLine = counter++ % 2 == 0 ? xAxis : zAxis;
            itemCurrentlyInMovement = pool.GetFromPool(currentlyActiveLine.startPoint);

            itemCurrentlyInMovement.SetColour((counter / 100f) % 1f);

            if(!lastItem.CompareTag("First"))
                itemCurrentlyInMovement.transform.localScale = lastItem.transform.localScale;
            
            startTime = Time.time;
    
        }

        public void UpdateMovement()
        {
            if (itemCurrentlyInMovement == null)
                return;

            currentTime = (Time.time - startTime) / duration;

            itemCurrentlyInMovement.transform.position = currentlyActiveLine.GetPositionOnTheLine(currentTime);

            if (currentTime> 1f)
            {
                startTime = Time.time;
                currentlyActiveLine.Reversemovement();
            }
        }

        private StackItem CutItemOnX(StackItem item, float distance, float direction)
        {
            var newXScale = lastItem.transform.localScale.x - distance;
            var newXPosition = lastItem.transform.position.x + (distance / 2f) * direction;

            var fallingXItemScale = item.transform.localScale.x - newXScale;

            item.transform.position = new Vector3(newXPosition, item.transform.position.y, item.transform.position.z);
            item.transform.localScale = new Vector3(newXScale, item.transform.localScale.y, item.transform.localScale.z);

            var itemEdge = item.transform.position.x + (newXScale / 2f) * direction;
            var fallingXItemPosition = itemEdge + (fallingXItemScale / 2f) * direction;

            var fallingItem = pool.GetFromPool(new Vector3(fallingXItemPosition, item.transform.position.y, item.transform.position.z));
            fallingItem.transform.localScale = new Vector3 (fallingXItemScale, item.transform.localScale.y, item.transform.localScale.z);
            fallingItem.SetColour(item.GetColour());
            fallingItem.EnableGravity();

            zAxis.EditXValue(newXPosition);

            return fallingItem;
        }
        private StackItem CutItemOnZ(StackItem item, float distance, float direction)
        {
            float newZScale = lastItem.transform.localScale.z - distance;
            float newZPosition = lastItem.transform.position.z + (distance / 2f) * direction;

            var fallingZItemScale = item.transform.localScale.z - newZScale;

            item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, newZPosition);
            item.transform.localScale = new Vector3(item.transform.localScale.x, item.transform.localScale.y, newZScale);

            var itemEdge = item.transform.position.z + (newZScale / 2f) * direction;
            var fallingZItemPosition = itemEdge + (fallingZItemScale / 2f) * direction;

            var fallingItem = pool.GetFromPool(new Vector3(item.transform.position.x, item.transform.position.y, fallingZItemPosition));
            fallingItem.transform.localScale = new Vector3(item.transform.localScale.x, item.transform.localScale.y, fallingZItemScale);
            fallingItem.SetColour(item.GetColour());
            fallingItem.EnableGravity();

            xAxis.EditZValue(newZPosition);

            return fallingItem;
        }
    } 
}
