using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Freedom.Core.View.Utils
{
    public class GameObjectPool<T> : MonoBehaviour where T : MonoBehaviour
    {
        /// <summary>
        /// The pool container.
        /// </summary>
        public Transform poolContainer;

        /// <summary>
        /// The pool dictionary.
        /// </summary>
        private Dictionary<string,List<T>> poolDictionary;

        private void Start ()
        {
            poolDictionary = new Dictionary<string, List<T>> ();

            if (poolContainer == null)
                poolContainer = transform;
        }

        /// <summary>
        /// Gets the object of type objectType.
        /// </summary>
        /// <returns>The object.</returns>
        /// <param name="objectType">Object type.</param>
        public T GetObject(string objectType)
        {
            T item = null;
            if (poolDictionary.Count > 0)
            {
                // Debug pool
//                foreach (KeyValuePair<string, List<T>> entry in poolDictionary)
//                {
//                    UnityEngine.Debug.Log (string.Format("ID: {0}, COUNT: {1}", entry.Key, entry.Value.Count));
//                }
                List<T> list;
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
        public void PoolObject (string objectType, T item)
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
                poolDictionary.Add (objectType, new List<T>(){item});
            }
        }
    }
}