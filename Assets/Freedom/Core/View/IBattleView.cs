using Freedom.Core.Model;
using UnityEngine;
using Freedom.Core.View.EnemySpawnModule;
using System.Collections.Generic;

namespace Freedom.Core.View
{
    public interface IBattleView
    {
        void SetBattleStartListener (System.Action listener);
        void SetInputEventListener (System.Action<Vector3> listener);
        void SetStopInputEventListener (System.Action listener);
        void ShowStartDialog (ILevelModel level);
        void SpawnShip (ShipFactory.ShipType shipType, System.Action<IShipView> callback);
        void SpawnGroupOfEnemies (ShipFactory.ShipType shipType, System.Action<IShipView[], Transform[]> callback);
        void StartLevel ();
        void SetTickableModel (ITickable tickableModel);
    }
}