using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Freedom.Core.Model.Interfaces;

namespace Freedom.Core.View.Dialogs
{
    public class PauseDialogView : MonoBehaviour
    {
        public Text levelInfo;
        public Text scoreInfo;
        public Text lifesInfo;

        private System.Action continueListener;

        public void Setup (ILevelModel level, int score, int lifes, System.Action continueListener)
        {
            // save continue listener
            this.continueListener = continueListener;

            // update info
            levelInfo.text = string.Format ("Current Level: {0}", level.id.ToString ());

            // score
            scoreInfo.text = string.Format("Current Score: {0}", score.ToString ());

            // lifes
            lifesInfo.text = string.Format("Remaining Lifes: {0}", lifes.ToString());
        }

        public void ContinuePlaying ()
        {
            continueListener ();
        }
    }
}