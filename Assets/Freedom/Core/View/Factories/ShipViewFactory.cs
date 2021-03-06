﻿using UnityEngine;
using System.Collections.Generic;
using Freedom.Core.Model;
using Freedom.Core.Model.Factories;
using Freedom.Core.View.EnemyGeneratorModule;
using Freedom.Core.View.Interfaces;

namespace Freedom.Core.View.Factories
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
        /// Creates a ship of the specified type at the specified position.
        /// </summary>
        /// <returns>The ship.</returns>
        /// <param name="type">Type.</param>
        /// <param name="parent">Parent.</param>
        public IShipView CreateShip (ShipFactory.ShipType shipType, Transform parent, Vector3 position)
        {
            // obtain the prefab by type
            GameObject prefab = GetPrefab (shipType);

            // check prefab
            if (prefab == null) {
                UnityEngine.Debug.LogError (string.Format ("Prefab not found: {0}", shipType.ToString ()));
                return null;
            }

            // create new ship view instance
            IShipView shipView = GameObject.Instantiate<GameObject> (prefab).GetComponent<ShipView> ();

            // get transform
            Transform shipTransform = ((ShipView)shipView).transform;

            // set parent
            shipTransform.SetParent(parent);

            // set defaul configuration
            shipTransform.position = position;
            shipTransform.localScale = Vector3.one;
            shipTransform.localRotation = Quaternion.identity;

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