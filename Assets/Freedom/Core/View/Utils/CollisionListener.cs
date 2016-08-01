using UnityEngine;
using System.Collections;

namespace Freedom.Core.View.Utils
{
    public class CollisionListener<T> : MonoBehaviour
    {
        public System.Action<T> OnCollisionDetected;
        public string objectTag;

        void OnTriggerEnter (Collider other)
        {
            if (other.CompareTag (objectTag)) {
//                UnityEngine.Debug.LogError (string.Format("SHIP: {0}, impacted by BULLET: {1}", transform.parent.name, other.name));
                if (OnCollisionDetected != null) {
                    OnCollisionDetected (other.GetComponent<T> ());
                }
            }
        }
    }
}