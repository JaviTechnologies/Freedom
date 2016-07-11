using UnityEngine;
using System.Collections;

namespace Freedom.Core.View.InputModule
{
    public abstract class BattleInputAdapter : MonoBehaviour
    {
        /// <summary>
        /// The direction.
        /// </summary>
        protected Vector3 direction;

        /// <summary>
        /// Whether should check input.
        /// </summary>
        protected bool shouldCheckInput = false;

        /// <summary>
        /// The player ship.
        /// </summary>
        protected IShipView playerShip;

        /// <summary>
        /// The main camera.
        /// </summary>
        protected Camera mainCamera;

        /// <summary>
        /// Whether there is an input.
        /// </summary>
        protected bool isInputDown = false;

        /// <summary>
        /// The input event.
        /// </summary>
        public System.Action<Vector3> InputEvent;

        /// <summary>
        /// The stop input event.
        /// </summary>
        public System.Action StopInputEvent;

        private void Start ()
        {
            direction = Vector3.zero;
            mainCamera = Camera.main;
        }

        /// <summary>
        /// Setup the input adapter.
        /// </summary>
        /// <param name="playerShip">Player ship.</param>
        public void Setup (IShipView playerShip)
        {
            this.playerShip = playerShip;
        }

        /// <summary>
        /// Starts listening for input.
        /// </summary>
        public void StartInput ()
        {
            shouldCheckInput = true;
        }

        /// <summary>
        /// Triggers the input event.
        /// </summary>
        protected void TriggerInputEvent ()
        {
            if (InputEvent != null)
                InputEvent (direction.normalized);

            isInputDown = true;
        }

        /// <summary>
        /// Triggers the stop input event.
        /// </summary>
        protected void TriggerStopInputEvent ()
        {
            if (StopInputEvent != null)
                StopInputEvent ();

            isInputDown = false;
        }
    }
}