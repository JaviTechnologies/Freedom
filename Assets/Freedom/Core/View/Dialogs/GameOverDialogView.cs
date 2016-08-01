using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Freedom.Core.Model.Interfaces;

namespace Freedom.Core.View.Dialogs
{
    public class GameOverDialogView : MonoBehaviour
    {
        public Text levelInfo;
        public Text maxScoreInfo;
        public Text scoreInfo;

        private System.Action<int> buyLifesListener;

        public void Setup (IGamerModel gamer, int score, System.Action<int> buyLifesListener)
        {
            // save continue listener
            this.buyLifesListener = buyLifesListener;

            // update info
            levelInfo.text = string.Format ("Level: {0}", gamer.CurrentLevel.id.ToString ());

            // max score
            maxScoreInfo.text = string.Format("Max Score: {0}", gamer.MaxScore.ToString ());

            // score
            scoreInfo.text = string.Format("Final Score: {0}", score.ToString ());
        }

        public void BuyLifes (int quantity)
        {
            buyLifesListener (quantity);
        }
    }
}