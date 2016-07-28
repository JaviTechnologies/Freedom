using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Freedom.Core.Controller.Utils
{
    public class ObjectPool<T>
    {
        /// <summary>
        /// The pool dictionary.
        /// </summary>
        private Dictionary<string,List<T>> poolDictionary = new Dictionary<string, List<T>> ();

        public T GetObject(string objectType)
        {
            T item = default(T);
            if (poolDictionary.Count > 0)
            {
                // Debug pool
//                foreach (KeyValuePair<string, List<T>> entry in poolDictionary)
//                {
//                    UnityEngine.Debug.Log (string.Format("CLASSES SHIP MODEL... ID: {0}, COUNT: {1}", entry.Key, entry.Value.Count));
//                }
                List<T> list;
                if (poolDictionary.TryGetValue (objectType, out list))
                {
                    if (list.Count > 0)
                    {
                        // get item from pool
                        item = list[list.Count - 1];
                        list.RemoveAt (list.Count - 1);
                    }
                }
            }

            return item;
        }

        public void PoolObject (string objectType, T item)
        {
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