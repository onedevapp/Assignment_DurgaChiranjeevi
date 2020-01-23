using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneDevApp
{
    public class PoolObjects : MonoBehaviour
    {
        public string tagNameForParent;
        public bool active; //is this pool object active or not?
        private  GameObject parent;

        void Start()
        {
            if(!string.IsNullOrEmpty(tagNameForParent))
                parent = GameObject.FindWithTag(tagNameForParent);
        }

        // Disables a pool object.
        public void DisablePoolObject()
        {
            this.active = false;
            this.gameObject.SetActive(false);

            if(parent)
                this.transform.SetParent(parent.transform);
        }

        // Enables a pool object.
        public void ActivatePoolObject()
        {
            this.active = true;
            this.gameObject.SetActive(true);
        }
    }
}
