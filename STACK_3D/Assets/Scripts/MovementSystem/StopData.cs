using Loop;

namespace Movement
{

    public struct StopData
        {
            public StackItem stoppedItem;
            public StackItem fallingItem;
            public StopResult Result;
            public StackItem[] lastItems;
            
        public StopData(StackItem stoppedItem, StackItem fallingItem, StopResult status, StackItem[] lastItems)
            {
                this.stoppedItem = stoppedItem;
                this.Result = status;
                this.fallingItem = fallingItem;
                this.lastItems = lastItems;
            }
        }
    
}