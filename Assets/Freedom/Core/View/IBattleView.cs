using Freedom.Core.Model;
using UnityEngine;

namespace Freedom.Core.View {
    public interface IBattleView {
        void SetBattleStartListener (System.Action listener);
        void SetInputEventListener (System.Action<Vector3> listener);
        void SetStopInputEventListener (System.Action listener);
        void ShowStartDialog (ILevelModel level);
        void SpawnShip (ShipFactory.ShipType type, System.Action<IShipView> callback);
        void StartLevel ();
        void SetTickableModel (ITickable tickableModel);
    }
}