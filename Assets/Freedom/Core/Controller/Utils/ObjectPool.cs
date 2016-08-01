using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Freedom.Core.Controller.Utils
{
    /// <summary>
    /// Generic object pool.
    /// </summary>
    public class ObjectPool<U, V>
    {
        /// <summary>
        /// The pool dictionary.
        /// </summary>
        private Dictionary<U,List<V>> poolDictionary = new Dictionary<U, List<V>> ();

        /// <summary>
        /// Gets the object.
        /// </summary>
        /// <returns>The object.</returns>
        /// <param name="objectType">Object type.</param>
        public V GetObject(U objectType)
        {
            V item = default(V);
            if (poolDictionary.Count > 0)
            {
                List<V> list;
                if (poolDictionary.TryGetValue (objectType, out list))
                {
                    if (list.Count > 0)
                    {
                        // get item from pool
                        item = list[list.Count - 1];

                        // remove it from pool
                        list.RemoveAt (list.Count - 1);
                    }
                }
            }

            return item;
        }

        /// <summary>
        /// Pools the object.
        /// </summary>
        /// <param name="objectType">Object type.</param>
        /// <param name="item">Item.</param>
        public void PoolObject (U objectType, V item)
        {
            if (poolDictionary.ContainsKey (objectType))
            {
                // add item to an existing pool
                poolDictionary [objectType].Add (item);
            }
            else
            {
                // add item in a new list
                poolDictionary.Add (objectType, new List<V>(){item});
            }
        }
    }
}