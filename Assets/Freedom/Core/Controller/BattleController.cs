using UnityEngine;
using Freedom.Core.Model;
using Freedom.Core.Model.Factories;
using Freedom.Core.Model.Interfaces;
using Freedom.Core.View;
using System.Collections.Generic;
using Freedom.Core.Controller.Utils;
using Freedom.Core.View.Interfaces;

namespace Freedom.Core.Controller
{
    public class BattleController : ITickable
    {
        private enum BattleState
        {
            NONE,
            RUNNING,
            PAUSED
        }

        private IBattleView battleViewHandler;
        private ILevelModel currentLevel;
        private ShipModelPool shipModelPool;
        private BulletModelPool bulletModelPool;
        private IShipModel playerShip;
        private List<IShipModel> enemyShips;
        private List<IBulletModel> bullets;
        private BattleState battleState;

        private const string DEFAUL_BULLET_TYPE = "bullet";

        public BattleController (ILevelModel level, IBattleView battleView)
        {
            this.currentLevel = level;
            this.battleViewHandler = battleView;

            shipModelPool = new ShipModelPool ();
            bulletModelPool = new BulletModelPool ();

            this.enemyShips = new List<IShipModel> ();
            this.bullets = new List<IBulletModel> ();

            // setup view
            battleViewHandler.SetTickableModel (this);

            // set listeners
            battleViewHandler.SetBattleStartListener (StartBattle);
            battleViewHandler.SetInputEventListener (InputEvent);
            battleViewHandler.SetStopInputEventListener (StopInputEvent);

            battleState = BattleState.NONE;
        }

        public void PrepareBattle ()
        {
            battleViewHandler.ShowStartDialog (currentLevel);
        }

        private void StartBattle ()
        {
            // spwan player's ship
            battleViewHandler.SpawnPlayerShip (
                ShipFactory.ShipType.A,
                (IShipView shipView) => {
                    // create player's ship
                    playerShip = ShipFactory.CreateShip (ShipFactory.ShipType.A, Vector3.zero);

                    // setup ship
                    playerShip.SetBulletListener (OnBulletShooted);
                    playerShip.SetViewHandler (shipView);

                    battleViewHandler.StartLevel ();

                    UnityEngine.Debug.Log ("StartBattle");
                    battleState = BattleState.RUNNING;
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
            if (battleState != BattleState.RUNNING)
                return;

            // tick player's ship
            if (playerShip != null) {
                playerShip.Tick (deltaTime);
            }

            // tick enemy ships
            for (int i = 0; i < enemyShips.Count; i++) {
                enemyShips [i].Tick (deltaTime);

                if (enemyShips [i].State == ShipModel.ShipState.RECYCLE) {
                    RecycleShip (enemyShips [i]);
                }
            }

            // tick bullets
            for (int i = 0; i < bullets.Count; i++) {
                bullets [i].Tick (deltaTime);

                if (bullets [i].State == BulletModel.BulletState.RECYCLED) {
                    RecycleBullet (bullets [i]);
                }
            }

            CheckShouldSpawnEnemies (deltaTime);
        }

        private void RecycleShip (IShipModel ship)
        {
            enemyShips.Remove (ship);
            shipModelPool.PoolObject (ship.GetShipType (), ship);
        }

        private void RecycleBullet (IBulletModel bullet)
        {
            bullets.Remove (bullet);
            bulletModelPool.PoolObject (bullet.BulletType, bullet);
        }

        private float timeToCheckSpwanEnemies = 0;
        private float timeBetweenSpawns = 5;

        private void CheckShouldSpawnEnemies (float deltaTime)
        {
            timeToCheckSpwanEnemies -= deltaTime;

            if (timeToCheckSpwanEnemies < 0) {
                // reset time
                timeToCheckSpwanEnemies += timeBetweenSpawns;

                // spawn
                SpawnEnemies ();
            }
        }

        private void SpawnEnemies ()
        {
            // Random ship type
            ShipFactory.ShipType shipType = (ShipFactory.ShipType)Random.Range (1, 3);

            battleViewHandler.SpawnGroupOfEnemies (
                shipType,
                (IShipView[] shipViews, Transform[] spots) => {
                    IShipModel ship;
                    for (int i = 0; i < shipViews.Length; i++) {
                        // Get Ship from Pool or create one
                        ship = shipModelPool.GetObject (shipType);
                        if (ship == null) {
                            ship = ShipFactory.CreateShip (shipType, spots [i].position);
                        } else {
                            ship.Setup (shipType, spots [i].position);
                        }

                        // direction
                        ship.SetDirection (-spots [i].up);
                        
                        // setup ship
                        ship.SetViewHandler (shipViews [i]);

                        // bullet listener
                        ship.SetBulletListener (OnBulletShooted);

                        // add enemy ship
                        this.enemyShips.Add (ship);
                    }
                }
            );
        }

        private void OnBulletShooted (BulletFactory.BulletType bulletType, Vector3 position, Vector3 direction)
        {
            IBulletModel bullet = bulletModelPool.GetObject (bulletType);

            if (bullet == null) {
                bullet = BulletFactory.CreateBullet (bulletType, position, direction);
            } else {
                bullet.Init (bulletType, position, direction);
            }

            battleViewHandler.SpawnBullet (
                bulletType,
                (IBulletView bulletView) => {
                    bullet.SetBulletViewHandler(bulletView);
                }
            );

            // add bullet to active bullets
            bullets.Add (bullet);
        }
    }
}