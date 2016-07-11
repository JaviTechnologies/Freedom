using UnityEngine;
using System.Collections;

namespace Freedom.Core.View.InputModule
{
    /// <summary>
    /// PC battle input adapter.
    /// </summary>
    public class PCBattleInputAdapter : BattleInputAdapter
    {
        private void Update ()
        {
            if (!shouldCheckInput)
                return;
            
            // reset direction
            direction.x = direction.z = 0;

            // check axis
            if (Input.GetKey(KeyCode.LeftArrow))
                direction.x = -1;

            if (Input.GetKey (KeyCode.RightArrow))
                direction.x = 1;

            if (Input.GetKey (KeyCode.DownArrow))
                direction.z = -1;

            if (Input.GetKey(KeyCode.UpArrow))
                direction.z = 1;

            // trigger event
            if (direction.x != 0 || direction.z != 0)
                TriggerInputEvent ();
            else if (isInputDown)
                TriggerStopInputEvent ();
        }
    }
}