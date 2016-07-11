using UnityEngine;
using System.Collections;

namespace Freedom.Core.View.InputModule
{
    /// <summary>
    /// Mobile battle input adapter.
    /// </summary>
    public class MobileBattleInputAdapter : BattleInputAdapter
    {
        private void Update ()
        {
            if (!shouldCheckInput)
                return;

            // reset direction
            direction.x = direction.z = 0;

            // check input
            if (Input.touchCount > 0)
            {
                // check only one touch
                Touch touch = Input.touches [0];

                // calculate direction the ship should move
                Vector3 shipPosition = mainCamera.WorldToScreenPoint (playerShip.GetPosition ());
                direction.x = touch.position.x - shipPosition.x;
                direction.z = touch.position.y - shipPosition.y;
            }

            // trigger event
            if (direction.x != 0 && direction.z != 0)
                TriggerInputEvent ();
            else if (isInputDown)
                TriggerStopInputEvent ();
        }
    }
}