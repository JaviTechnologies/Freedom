using System;
using UnityEngine;
using Freedom.Core.View;
using Freedom.Core.View.BulletGeneratorModule;

namespace Freedom.Core.View.BulletGeneratorModule
{
    public class BulletRecycleTrigger : MonoBehaviour
    {
        public Action<BulletView> OnRecycleBulletTriggerEvent;

        void OnTriggerEnter (Collider other)
        {
            if (other.gameObject.CompareTag (BulletViewPool.BULLET_TAG)) {
                if (OnRecycleBulletTriggerEvent != null) {
                    OnRecycleBulletTriggerEvent (other.GetComponent<BulletView> ());
                }
            }
        }
    }
}

