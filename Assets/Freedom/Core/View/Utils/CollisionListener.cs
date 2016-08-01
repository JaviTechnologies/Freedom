using UnityEngine;
using System.Collections;

namespace Freedom.Core.View.Utils
{
    public class CollisionListener<T> : MonoBehaviour
    {
        public System.Action<T> OnCollisionDetected;
        public string objectTag;

        private void OnTriggerEnter (Collider other)
        {
            if (other.CompareTag (objectTag)) {
                if (OnCollisionDetected != null) {
                    OnCollisionDetected (other.GetComponent<T> ());
                }
            }
        }
    }
}