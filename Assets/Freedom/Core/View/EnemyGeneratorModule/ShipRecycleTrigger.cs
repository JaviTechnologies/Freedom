using UnityEngine;
using System.Collections;
using System;

namespace Freedom.Core.View.EnemyGeneratorModule
{
    public class ShipRecycleTrigger : MonoBehaviour
    {
        public Action<ShipView> OnRecycleShipTriggerEvent;

        void OnTriggerEnter (Collider other)
        {
            if (other.gameObject.tag == ShipViewPool.ENEMY_SHIP_TAG) {
                if (OnRecycleShipTriggerEvent != null) {
                    OnRecycleShipTriggerEvent (other.transform.parent.GetComponent<ShipView> ());
                }
            }
        }
    }
}