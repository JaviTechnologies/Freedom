using UnityEngine;
using System.Collections;
using Freedom.Core.View.Utils;

namespace Freedom.Core.View.EnemyGeneratorModule
{
    public class ShipViewPool : GameObjectPool<ShipView>
    {
        public const string ENEMY_SHIP_TAG = "EnemyShip";

        public ShipRecycleTrigger ShipRecycleTrigger;

        private void OnEnable ()
        {
            ShipRecycleTrigger.OnRecycleShipTriggerEvent = OnRecycleTriggerEventHandler;
        }

        private void OnDisable ()
        {
            ShipRecycleTrigger.OnRecycleShipTriggerEvent = null;
        }

        private void OnRecycleTriggerEventHandler (ShipView shipView)
        {
            StartCoroutine (RecycleShip(shipView));
        }

        private IEnumerator RecycleShip (ShipView shipView)
        {
            shipView.Recycle ();

            this.PoolObject (shipView.ShipType, shipView);

            yield return 0;
        }
    }
}