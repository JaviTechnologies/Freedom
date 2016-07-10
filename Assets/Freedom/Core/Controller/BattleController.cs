using Freedom.Core.Model;
using UnityEngine;
using Freedom.Core.View;

namespace Freedom.Core.Controller
{
    public class BattleController
    {
        private IBattleView battleViewHandler;
        private IShipModel playerShip;
        private ShipFactory shipFactory;
        private ILevelModel currentLevel;

        public BattleController (ILevelModel level, IBattleView battleView) {
            this.currentLevel = level;
            this.battleViewHandler = battleView;

            // set listeners
            battleViewHandler.SetBattleStartListener (StartBattle);
        }

        public void PrepareBattle () {
            battleViewHandler.ShowStartDialog (currentLevel);
        }

        public void StartBattle () {
            // spwan player's ship
            battleViewHandler.SpawnShip (
                ShipFactory.ShipType.A,
                (IShipView shipView) => {
                    // create player's ship
                    playerShip = ShipFactory.CreateShip (ShipFactory.ShipType.A, Vector3.zero, shipView);

                    battleViewHandler.StartLevel ();
                });
        }

        public void Tick (float deltaTime) {
            playerShip.Tick (deltaTime);
        }
    }
}