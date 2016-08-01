using UnityEngine;
using Freedom.Core.Model;
using Freedom.Core.Model.Factories;
using Freedom.Core.Model.Interfaces;
using Freedom.Core.View.EnemyGeneratorModule;
using System.Collections.Generic;

namespace Freedom.Core.View.Interfaces
{
    public interface IBattleView
    {
        void SetBattleStartListener (System.Action listener);
        void SetInputEventListener (System.Action<Vector3> listener);
        void SetStopInputEventListener (System.Action listener);
        void ShowStartDialog (ILevelModel level);
        void SpawnPlayerShip (ShipFactory.ShipType shipType, System.Action<IShipView> callback);
        void SpawnGroupOfEnemies (ShipFactory.ShipType shipType, System.Action<IShipView[], Transform[]> callback);
        void SpawnBullet (BulletFactory.BulletType bulletType, System.Action<IBulletView> callback);
        void StartLevel ();
        void SetTickableModel (ITickable tickableModel);
    }
}