
using UnityEngine;

namespace Loop
{
    public interface IPoolable
    {
        void PrepareForActivate(Vector3 psition);
        void PrepareForDeactivate(Transform orginalParent);
    } 
}
