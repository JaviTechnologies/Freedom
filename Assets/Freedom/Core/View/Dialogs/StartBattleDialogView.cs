using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Freedom.Core.Model.Interfaces;

namespace Freedom.Core.View.Dialogs
{
    public class StartBattleDialogView : MonoBehaviour
    {
        public Text levelLabel;

        private System.Action startListener;

        public void Setup (ILevelModel level, System.Action startListener)
        {
            // set listener
            this.startListener = startListener;

            // update text
            levelLabel.text = string.Format ("Level: {0}", level.id.ToString ());
        }

        public void OnStartBattleEvent ()
        {
            startListener ();
        }
    }
}