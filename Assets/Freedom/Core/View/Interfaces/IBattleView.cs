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
        /// <summary>
        /// Sets the battle start listener.
        /// </summary>
        /// <param name="listener">Listener.</param>
        void SetBattleStartListener (System.Action listener);

        /// <summary>
        /// Sets the input event listener.
        /// </summary>
        /// <param name="listener">Listener.</param>
        void SetInputEventListener (System.Action<Vector3> listener);

        /// <summary>
        /// Sets the stop input event listener.
        /// </summary>
        /// <param name="listener">Listener.</param>
        void SetStopInputEventListener (System.Action listener);

        /// <summary>
        /// Sets the pause battle listener.
        /// </summary>
        /// <param name="listener">Listener.</param>
        void SetPauseBattleListener (System.Action listener);

        /// <summary>
        /// Shows the start dialog.
        /// </summary>
        /// <param name="level">Level.</param>
        void ShowStartDialog (IGamerModel gamer);

        /// <summary>
        /// Spawns the player ship.
        /// </summary>
        /// <param name="shipType">Ship type.</param>
        /// <param name="callback">Callback.</param>
        void SpawnPlayerShip (ShipFactory.ShipType shipType, System.Action<IShipView> callback);

        /// <summary>
        /// Spawns the group of enemies.
        /// </summary>
        /// <param name="shipType">Ship type.</param>
        /// <param name="callback">Callback.</param>
        void SpawnGroupOfEnemies (ShipFactory.ShipType shipType, System.Action<IShipView[], Transform[]> callback);

        /// <summary>
        /// Spawns the bullet.
        /// </summary>
        /// <param name="bulletType">Bullet type.</param>
        /// <param name="callback">Callback.</param>
        void SpawnBullet (BulletFactory.BulletType bulletType, System.Action<IBulletView> callback);

        /// <summary>
        /// Starts the level.
        /// </summary>
        void StartLevel ();

        /// <summary>
        /// Sets the tickable model.
        /// </summary>
        /// <param name="tickableModel">Tickable model.</param>
        void SetTickableModel (ITickable tickableModel);

        /// <summary>
        /// Updates the lifes.
        /// </summary>
        /// <param name="lifes">Lifes.</param>
        void UpdateLifes (int lifes);

        /// <summary>
        /// Updates the score.
        /// </summary>
        /// <param name="score">Score.</param>
        void UpdateScore (int score);

        /// <summary>
        /// Handles the battle pause.
        /// </summary>
        /// <param name="gamer">Gamer.</param>
        /// <param name="score">Score.</param>
        /// <param name="lifes">Lifes.</param>
        /// <param name="continueListener">Continue listener.</param>
        void HandleBattlePause (IGamerModel gamer, int score, int lifes, System.Action continueListener);

        /// <summary>
        /// Handles the battle resume.
        /// </summary>
        void HandleBattleResume ();

        /// <summary>
        /// Handles the game over.
        /// </summary>
        /// <param name="level">Level.</param>
        /// <param name="score">Score.</param>
        /// <param name="buyLifesListener">Buy lifes listener.</param>
        void HandleGameOver (IGamerModel gamer, int score, System.Action<int> buyLifesListener);

        /// <summary>
        /// Handles the continue after game over.
        /// </summary>
        void HandleContinueAfterGameOver ();
    }
}