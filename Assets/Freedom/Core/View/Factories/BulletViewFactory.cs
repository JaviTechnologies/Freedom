using UnityEngine;
using System.Collections;
using Freedom.Core.View.BulletGeneratorModule;
using Freedom.Core.Model.Factories;
using System.Collections.Generic;
using Freedom.Core.View.Interfaces;

namespace Freedom.Core.View.Factories
{
    public class BulletViewFactory : MonoBehaviour
    {
        /// <summary>
        /// Bullet prefab entry.
        /// Helper class to store bullet prefabs
        /// </summary>
        [System.Serializable]
        public class BulletPrefabEntry
        {
            public BulletFactory.BulletType type;
            public GameObject prefab;
        }

        /// <summary>
        /// The bullet prefabs.
        /// </summary>
        public List<BulletPrefabEntry> bulletPrefabs = new List<BulletPrefabEntry> ();

        /// <summary>
        /// Creates the bullet view.
        /// </summary>
        /// <returns>The bullet.</returns>
        /// <param name="bulletType">Bullet type.</param>
        /// <param name="parent">Parent.</param>
        /// <param name="position">Position.</param>
        public IBulletView CreateBullet (BulletFactory.BulletType bulletType, Transform parent, Vector3 position)
        {
            // obtain the prefab by type
            GameObject prefab = GetPrefab (bulletType);

            // check prefab
            if (prefab == null) {
                UnityEngine.Debug.LogError (string.Format ("Prefab not found: {0}", bulletType.ToString ()));
                return null;
            }

            // create new ship view instance
            IBulletView bulletView = GameObject.Instantiate<GameObject> (prefab).GetComponent<BulletView> ();

            // get transform
            Transform bulletTransform = ((BulletView)bulletView).transform;

            // set parent
            bulletTransform.SetParent(parent);

            // set defaul configuration
            bulletTransform.position = position;
//            bulletTransform.localScale = Vector3.one;
            bulletTransform.localRotation = Quaternion.identity;

            return bulletView;
        }

        /// <summary>
        /// Gets the bullet prefab.
        /// </summary>
        /// <returns>The prefab.</returns>
        /// <param name="type">Type.</param>
        private GameObject GetPrefab (BulletFactory.BulletType type)
        {
            int count = bulletPrefabs.Count;
            for (int i = 0; i < count; i++)
            {
                if (bulletPrefabs [i].type == type)
                    return bulletPrefabs [i].prefab;
            }

            return null;
        }
    }
}