using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Freedom.Core.View.Utils
{
    public class GameObjectPool<T> : MonoBehaviour where T : MonoBehaviour
    {
        private Dictionary<string,List<T>> poolDictionary;

        private void Start ()
        {
            poolDictionary = new Dictionary<string, List<T>> ();
        }

        public T GetObject(string objectType)
        {
            T item = null;
            if (poolDictionary.Count > 0)
            {
//                if (poolDictionary.ContainsKey (objectType) && poolDictionary [objectType].Count > 0)
//                {
//                    // get item from pool
//                    item = poolDictionary [objectType][poolDictionary [objectType].Count - 1];
//                    poolDictionary [objectType].RemoveAt (poolDictionary [objectType].Count - 1);
//
//                    // activate item
//                    item.gameObject.SetActive (true);
//
//                    // unparent
//                    item.transform.SetParent (null);
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

        public void PoolObject (string objectType, T item)
        {
            // desactivate item
            item.gameObject.SetActive(false);

            // change parent
            item.transform.SetParent(this.transform);

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