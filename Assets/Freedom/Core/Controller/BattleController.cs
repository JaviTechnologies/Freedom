using UnityEngine;
using Freedom.Core.Model;
using Freedom.Core.View;
using System.Collections.Generic;

namespace Freedom.Core.Controller
{
    public class BattleController : ITickable
    {
        private IBattleView battleViewHandler;
        private ShipFactory shipFactory;
        private ILevelModel currentLevel;
        private IShipModel playerShip;
        private List<IShipModel> enemyShips;

        public BattleController (ILevelModel level, IBattleView battleView)
        {
            this.currentLevel = level;
            this.battleViewHandler = battleView;

            this.enemyShips = new List<IShipModel> ();

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
                    playerShip = ShipFactory.CreateShip (ShipFactory.ShipType.A, Vector3.zero);

                    // setup ship
                    playerShip.Setup (shipView);

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
            // tick player's ship
            if (playerShip != null)
                playerShip.Tick (deltaTime);

            // tick enemy ships
            for (int i = 0; i < enemyShips.Count; i++)
            {
                enemyShips [i].Tick (deltaTime);
            }

            CheckShouldSpawnEnemies (deltaTime);
        }

        private void SpawnEnemies ()
        {
            // Random ship type
            ShipFactory.ShipType shipType = (ShipFactory.ShipType) Random.Range(1, 2);

            battleViewHandler.SpawnGroupOfEnemies (
                shipType,
                (IShipView[] shipViews, Transform[] spots) =>
                {
                    IShipModel ship;
                    for (int i = 0; i < shipViews.Length; i++)
                    {
                        // create player's ship
                        ship = ShipFactory.CreateShip (shipType, spots[i].position, -spots[i].up);

                        // setup ship
                        ship.Setup (shipViews[i]);

                        // add enemy ship
                        this.enemyShips.Add(ship);
                    }
                }
            );
        }

        private float timeToCheckSpwanEnemies = 0;
        private float timeBetweenSpawns = 5;
        private void CheckShouldSpawnEnemies (float deltaTime)
        {
            timeToCheckSpwanEnemies -= deltaTime;

            if (timeToCheckSpwanEnemies < 0)
            {
                // reset time
                timeToCheckSpwanEnemies += timeBetweenSpawns;

                // spawn
                SpawnEnemies ();
            }
        }
    }
}