using UnityEngine;
using System.Collections.Generic;
using Freedom.Core.Model;

namespace Freedom.Core.View
{
    public class ShipViewFactory : MonoBehaviour
    {
        /// <summary>
        /// Ship prefab entry.
        /// Helper class to store ship prefab references
        /// </summary>
        [System.Serializable]
        public class ShipPrefabEntry
        {
            public ShipFactory.ShipType type;
            public GameObject prefab;
        }

        /// <summary>
        /// The ship prefabs.
        /// </summary>
        public List<ShipPrefabEntry> shipPrefabs = new List<ShipPrefabEntry> ();

        /// <summary>
        /// Creates a ship of the especified type.
        /// </summary>
        /// <returns>The ship.</returns>
        /// <param name="type">Type.</param>
        /// <param name="parent">Parent.</param>
        public IShipView CreateShip (ShipFactory.ShipType type, Transform parent)
        {
            // obtain the prefab by type
            GameObject prefab = GetPrefab (type);

            // check prefab
            if (prefab == null)
            {
                UnityEngine.Debug.LogError (string.Format("Prefab not found: {0}", type.ToString()));
                return null;
            }

            // create instance
            Transform shipTransform = GameObject.Instantiate<GameObject>(prefab).GetComponent<Transform>();

            // set parent
            shipTransform.SetParent(parent);

            // set defaul configuration
            shipTransform.localPosition = Vector3.zero;
            shipTransform.localScale = Vector3.one;
            shipTransform.localRotation = Quaternion.identity;

            // get ship view
            IShipView shipView = shipTransform.GetComponent<IShipView> ();

            return shipView;
        }

        /// <summary>
        /// Gets a ship prefab by type.
        /// </summary>
        /// <returns>The prefab.</returns>
        /// <param name="type">Type.</param>
        private GameObject GetPrefab (ShipFactory.ShipType type)
        {
            int count = shipPrefabs.Count;
            for (int i = 0; i < count; i++)
            {
                if (shipPrefabs [i].type == type)
                    return shipPrefabs [i].prefab;
            }

            return null;
        }
    }
}