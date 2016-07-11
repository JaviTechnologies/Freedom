using Freedom.Core.Model;
using UnityEngine;
using Freedom.Core.View;

namespace Freedom.Core.Controller
{
    public class BattleController : ITickable
    {
        private IBattleView battleViewHandler;
        private IShipModel playerShip;
        private ShipFactory shipFactory;
        private ILevelModel currentLevel;

        public BattleController (ILevelModel level, IBattleView battleView)
        {
            this.currentLevel = level;
            this.battleViewHandler = battleView;

            // setup view
            battleViewHandler.SetTickableModel(this);

            // set listeners
            battleViewHandler.SetBattleStartListener (StartBattle);
            battleViewHandler.SetInputEventListener (InputEvent);
            battleViewHandler.SetStopInputEventListener (StopInputEvent);
        }

        public void PrepareBattle ()
        {
            battleViewHandler.ShowStartDialog (currentLevel);
        }

        private void StartBattle ()
        {
            // spwan player's ship
            battleViewHandler.SpawnShip (
                ShipFactory.ShipType.A,
                (IShipView shipView) => {
                    // create player's ship
                    playerShip = ShipFactory.CreateShip (ShipFactory.ShipType.A, Vector3.zero, shipView);

                    battleViewHandler.StartLevel ();
                });
        }

        private void InputEvent (Vector3 movementDirection)
        {
            playerShip.SetDirection (movementDirection);
        }

        private void StopInputEvent ()
        {
            playerShip.StopMovement ();
        }

        public void Tick (float deltaTime)
        {
            if (playerShip != null)
                playerShip.Tick (deltaTime);
        }
    }
}