using Freedom.Core.Model;

namespace Freedom.Core.View {
    public interface IBattleView {
        void SetBattleStartListener (System.Action listener);
        void ShowStartDialog (ILevelModel level);
        void SpawnShip (ShipFactory.ShipType type, System.Action<IShipView> callback);
    }
}