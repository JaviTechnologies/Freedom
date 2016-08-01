using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Freedom.Core.View.Utils
{
    public class GameObjectPool<U,V> : MonoBehaviour where V : MonoBehaviour
    {
        /// <summary>
        /// The pool container.
        /// </summary>
        public Transform poolContainer;

        /// <summary>
        /// The pool dictionary.
        /// </summary>
        private Dictionary<U,List<V>> poolDictionary;

        private void Start ()
        {
            poolDictionary = new Dictionary<U, List<V>> ();

            if (poolContainer == null)
                poolContainer = transform;
        }

        /// <summary>
        /// Gets the object of type objectType.
        /// </summary>
        /// <returns>The object.</returns>
        /// <param name="objectType">Object type.</param>
        public V GetObject(U objectType)
        {
            V item = null;
            if (poolDictionary.Count > 0)
            {
                List<V> list;
                if (poolDictionary.TryGetValue (objectType, out list))
                {
                    if (list.Count > 0)
                    {
                        // get item from pool
                        item = list[list.Count - 1];
                        list.RemoveAt (list.Count - 1);

                        // activate item
                        item.gameObject.SetActive (true);

                        // unparent
                        item.transform.SetParent (null);
                    }
                }
            }

            return item;
        }

        /// <summary>
        /// Pools the object of type objectType.
        /// </summary>
        /// <param name="objectType">Object type.</param>
        /// <param name="item">Item.</param>
        public void PoolObject (U objectType, V item)
        {
            // desactivate item
            item.gameObject.SetActive(false);

            // change parent
            item.transform.SetParent(poolContainer);

            // pool item
            if (poolDictionary.ContainsKey (objectType))
            {
                poolDictionary [objectType].Add (item);
            }
            else
            {
                poolDictionary.Add (objectType, new List<V>(){item});
            }
        }
    }
}