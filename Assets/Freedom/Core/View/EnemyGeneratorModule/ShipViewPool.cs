using UnityEngine;
using System.Collections;
using Freedom.Core.View.Utils;
using System.Collections.Generic;
using Freedom.Core.Model.Factories;

namespace Freedom.Core.View.EnemyGeneratorModule
{
    public class ShipViewPool : GameObjectPool<ShipFactory.ShipType,ShipView>
    {
        public const string ENEMY_SHIP_TAG = "EnemyShip";

        public ShipRecycleTrigger shipRecycleTrigger;

        private List<ShipView> shipsToRecycle = new List<ShipView> ();
        private Coroutine recycleCoroutine;

        private void OnEnable ()
        {
            shipRecycleTrigger.OnRecycleShipTriggerEvent = OnRecycleTriggerEventHandler;
        }

        private void OnDisable ()
        {
            shipRecycleTrigger.OnRecycleShipTriggerEvent = null;
        }

        private void OnRecycleTriggerEventHandler (ShipView shipView)
        {
            shipsToRecycle.Add (shipView);

            if (recycleCoroutine == null) {
                recycleCoroutine = StartCoroutine (RecycleShips ());
            }
        }

        private IEnumerator RecycleShips ()
        {
            ShipView shipView;
            int index = shipsToRecycle.Count - 1;
            while (shipsToRecycle.Count > 0) {
                shipView = shipsToRecycle [index];
                shipsToRecycle.RemoveAt (index);

                yield return 0;

                shipView.Recycle ();

                yield return 0;

                this.PoolObject (shipView.shipType, shipView);

                index = shipsToRecycle.Count - 1;
            }

            recycleCoroutine = null;
        }
    }
}