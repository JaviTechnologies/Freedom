using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Freedom.Core.Model.Interfaces;

namespace Freedom.Core.View.Dialogs
{
    public class StartBattleDialogView : MonoBehaviour
    {
        public Text levelLabel;
        public Text maxScoreLabel;

        private System.Action startListener;

        public void Setup (IGamerModel gamer, System.Action startListener)
        {
            // set listener
            this.startListener = startListener;

            // level
            levelLabel.text = string.Format ("Level: {0}", gamer.CurrentLevel.id.ToString ());

            // max score
            maxScoreLabel.text = string.Format ("Max Score: {0}", gamer.MaxScore.ToString());
        }

        public void OnStartBattleEvent ()
        {
            startListener ();
        }
    }
}