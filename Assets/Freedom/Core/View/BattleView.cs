using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Freedom.Core.Controller;
using Freedom.Core.Model;
using Freedom.Core.Model.Factories;
using Freedom.Core.Model.Interfaces;
using Freedom.Core.View.LevelGeneratorModule;
using Freedom.Core.View.InputModule;
using Freedom.Core.View.EnemyGeneratorModule;
using Freedom.Core.View.Interfaces;
using Freedom.Core.View.Factories;
using Freedom.Core.View.BulletGeneratorModule;
using Freedom.Core.View.Dialogs;

namespace Freedom.Core.View
{
    public class BattleView : MonoBehaviour, IBattleView
    {
        /// <summary>
        /// The ship view factory.
        /// </summary>
        public ShipViewFactory shipViewFactory;

        /// <summary>
        /// The ship view pool.
        /// </summary>
        public ShipViewPool shipViewPool;

        /// <summary>
        /// The bullet view factory.
        /// </summary>
        public BulletViewFactory bulletViewFactory;

        /// <summary>
        /// The bullet pool.
        /// </summary>
        public BulletViewPool bulletViewPool;

        /// <summary>
        /// The level container.
        /// </summary>
        public Transform levelContainer;

        #region UI elements

        [Header ("HUD")]
        public Text scoreText;
        public Text lifesText;

        [Header ("Level Generator")]
        public LevelGenerator levelGenerator;

        [Header ("Enemy Spawner")]
        public EnemySpawnSpotsView enemySpawnSpotsView;

        [Header ("Start Battle Dialog")]
        public StartBattleDialogView startBattleDialog;

        [Header ("Pause Dialog")]
        public PauseDialogView pauseDialog;

        [Header ("GameOver Dialog")]
        public GameOverDialogView gameOverDialogView;

        #endregion

        private ITickable battleController;
        private ILevelModel currentLevel;
        private BattleInputAdapter battleInputAdapter;

        private void Awake ()
        {
            #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
            battleInputAdapter = this.gameObject.AddComponent<MobileBattleInputAdapter>();
            #else
            battleInputAdapter = this.gameObject.AddComponent<PCBattleInputAdapter> ();
            #endif
        }

        void Start ()
        {
            GameController.Instance.InitBattle (this);
        }

        void Update ()
        {
            battleController.Tick (Time.deltaTime);
        }

        #region IBattleView Implementation

        private System.Action startBattleListener;
        private System.Action<Vector3> inputEventListener;
        private System.Action stopInputEventListener;
        private System.Action pauseBattleListener;

        public void SetTickableModel (ITickable tickableModel)
        {
            battleController = tickableModel;
        }

        public void SetBattleStartListener (System.Action listener)
        {
            startBattleListener = listener;
        }

        public void SetInputEventListener (System.Action<Vector3> listener)
        {
            inputEventListener = listener;
        }

        public void SetStopInputEventListener (System.Action listener)
        {
            stopInputEventListener = listener;
        }

        public void SetPauseBattleListener (System.Action listener)
        {
            this.pauseBattleListener = listener;
        }

        public void ShowStartDialog (ILevelModel level)
        {
            // open dialog
            startBattleDialog.gameObject.SetActive (true);

            // update info
            startBattleDialog.Setup(level, OnStartBattleEvent);
        }

        public void SpawnGroupOfEnemies (ShipFactory.ShipType shipType, System.Action<IShipView[], Transform[]> callback)
        {
            Transform[] spawnPoints = enemySpawnSpotsView.GetSpawnPoints ();

            IShipView[] createdShips = new IShipView[spawnPoints.Length];
            for (int i = 0; i < spawnPoints.Length; i++) {
                // get ship view
                createdShips [i] = GetShipView (shipType, spawnPoints [i].position);

                // add listeners
                createdShips [i].SetDestroyedListener (OnShipDestroyed);
            }

            callback (createdShips, spawnPoints);
        }

        public void SpawnPlayerShip (ShipFactory.ShipType shipType, System.Action<IShipView> callback)
        {
            // get ship view
            IShipView shipView = GetShipView (shipType, Vector3.zero);

            // add listeners
            shipView.SetDestroyedListener (OnShipDestroyed);

            // configure input adapter in order to controll player's ship
            battleInputAdapter.Setup (shipView);

            // done
            callback (shipView);
        }

        public void SpawnBullet (BulletFactory.BulletType bulletType, System.Action<IBulletView> callback)
        {
            IBulletView bulletView = GetBulletView (bulletType, Vector3.zero);

            bulletView.SetImpactListener (OnBulletImpacted);

            callback (bulletView);
        }

        public void StartLevel ()
        {
            levelGenerator.StartLevel ();

            battleInputAdapter.InputEvent = OnInputEvent;
            battleInputAdapter.StopInputEvent = OnStopInputEvent;
            battleInputAdapter.StartInput ();
        }

        public void UpdateLifes (int lifes)
        {
            lifesText.text = lifes.ToString ();
        }

        public void UpdateScore (int score)
        {
            scoreText.text = score.ToString ();
        }

        public void HandleBattlePause (ILevelModel level, int score, int lifes, System.Action continueListener)
        {
            Time.timeScale = 0;
            pauseDialog.gameObject.SetActive (true);
            pauseDialog.Setup (level, score, lifes, continueListener);
        }

        public void HandleBattleResume ()
        {
            pauseDialog.gameObject.SetActive (false);
            Time.timeScale = 1;
        }

        public void HandleGameOver (ILevelModel level, int score, System.Action<int> buyLifesListener)
        {
            Time.timeScale = 0;

            gameOverDialogView.gameObject.SetActive (true);

            gameOverDialogView.Setup (level, score, buyLifesListener);
        }

        public void HandleContinueAfterGameOver ()
        {
            gameOverDialogView.gameObject.SetActive (false);
            Time.timeScale = 1;
        }

        #endregion

        private IShipView GetShipView (ShipFactory.ShipType shipType, Vector3 position)
        {
            IShipView shipView = null;

            // Get ship view from the pool
            ShipView ship = shipViewPool.GetObject (shipType);

            if (ship == null) {
                // create a new ship view
                shipView = shipViewFactory.CreateShip (shipType, levelContainer, position);
            } else {
                ship.transform.SetParent (levelContainer);
                shipView = ship;
            }

            return shipView;
        }

        private IBulletView GetBulletView (BulletFactory.BulletType bulletType, Vector3 position)
        {
            IBulletView bulletView = null;

            // Get bullet view from the pool
            BulletView bullet = bulletViewPool.GetObject (bulletType);

            if (bullet == null) {
                // create a new bullet view
                bulletView = bulletViewFactory.CreateBullet (bulletType, levelContainer, position);
            } else {
                bullet.transform.SetParent (levelContainer);
                bulletView = bullet;
            }

            return bulletView;
        }

        #region Event Handlers

        public void OnStartBattleEvent ()
        {
            // close dialog
            startBattleDialog.gameObject.SetActive (false);

            // message listeners
            if (startBattleListener != null)
                startBattleListener ();
        }

        private void OnInputEvent (Vector3 movementDirection)
        {
            if (inputEventListener != null)
                inputEventListener (movementDirection);
        }

        private void OnStopInputEvent ()
        {
            if (stopInputEventListener != null)
                stopInputEventListener ();
        }

        private void OnShipDestroyed (ShipView shipView)
        {
            // pool ship view
            shipViewPool.PoolObject (shipView.shipType, (ShipView)shipView);
        }

        private void OnBulletImpacted (BulletView bulletView)
        {
            bulletView.Recycle ();
            bulletViewPool.PoolObject (bulletView.BulletType, bulletView);
        }

        public void OnPauseButtonPressed ()
        {
            pauseBattleListener ();
        }

        #endregion
    }
}