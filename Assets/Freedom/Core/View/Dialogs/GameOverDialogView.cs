using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Freedom.Core.Model.Interfaces;

namespace Freedom.Core.View.Dialogs
{
    public class GameOverDialogView : MonoBehaviour
    {
        public Text levelInfo;
        public Text scoreInfo;

        private System.Action<int> buyLifesListener;

        public void Setup (ILevelModel level, int score, System.Action<int> buyLifesListener)
        {
            // save continue listener
            this.buyLifesListener = buyLifesListener;

            // update info
            levelInfo.text = string.Format ("Level: {0}", level.id.ToString ());

            // score
            scoreInfo.text = string.Format("Final Score: {0}", score.ToString ());
        }

        public void BuyLifes (int quantity)
        {
            buyLifesListener (quantity);
        }
    }
}