using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Loop
{
    public class StackItem : MonoBehaviour, IPoolable
    {

        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Rigidbody rigidbody;
        
        public void PrepareForActivate(Vector3 psition)
        {
            this.transform.SetParent(null);
            this.transform.position = psition;
            this.gameObject.SetActive(true);
        }

        public void EnableGravity()
        {
            rigidbody.useGravity = true;
            rigidbody.isKinematic = false;
        }
        
        public void PrepareForDeactivate(Transform orginalParent)
        {
            this.transform.SetParent(orginalParent);
            this.gameObject.SetActive(false);
        }

        public void SetColour(float hue)
        {
            meshRenderer.material.color = Color.HSVToRGB(hue, 1f, 1f);
        }

        public void SetColour(float hue, float value)
        {
            meshRenderer.material.color = Color.HSVToRGB(hue, 1f, value);
        }

        public void SetColour(Color color)
        {
            meshRenderer.material.color = color;
        }

        public Color GetColour()
        {
            return meshRenderer.material.color;
        }
    } 
}
