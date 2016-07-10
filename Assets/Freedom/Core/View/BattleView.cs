using UnityEngine;
using System.Collections.Generic;
using Freedom.Core.Controller;
using Freedom.Core.Model;
using UnityEngine.UI;

namespace Freedom.Core.View
{
    public class BattleView : MonoBehaviour, IBattleView
    {
        #region UI elements
        public ShipViewFactory shipViewFactory;
        public Transform levelContainer;

        [Header("Start Battle Dialog")]
        public GameObject startDialog;
        public Text levelLabel;
//        [Space(10)]

        [Header("Level Generator")]
        public LevelGenerator levelGenerator;
        #endregion
    	
    	void Start ()
        {
            GameController.Instance.InitBattle (this);
    	}
    	
    	void Update ()
        {
    	    
    	}

        #region IBattleView Implementation
        private System.Action startBattleListener;

        public void SetBattleStartListener (System.Action listener)
        {
            startBattleListener = listener;
        }

        public void ShowStartDialog (ILevelModel level)
        {
            // open dialog
            startDialog.SetActive(true);

            // update info
            levelLabel.text = string.Format ("Level: {0}", level.id.ToString());
        }

        public void SpawnShip (ShipFactory.ShipType type, System.Action<IShipView> callback)
        {
            IShipView shipView = shipViewFactory.CreateShip (type, levelContainer);

            callback (shipView);
        }

        public void StartLevel ()
        {
            levelGenerator.StartLevel ();
        }
        #endregion

        #region Event Handlers
        public void OnStartBattleEvent ()
        {
            // close dialog
            startDialog.SetActive(false);

            // message listeners
            if (startBattleListener != null)
                startBattleListener ();
        }
        #endregion
    }
}