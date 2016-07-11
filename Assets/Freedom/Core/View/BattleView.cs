using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Freedom.Core.Controller;
using Freedom.Core.Model;
using Freedom.Core.View.LevelGeneratorModule;
using Freedom.Core.View.InputModule;

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

        private ITickable battleController;
        private BattleInputAdapter battleInputAdapter;

        private void Awake ()
        {
            #if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IPHONE)
            battleInputAdapter = this.gameObject.AddComponent<MobileBattleInputAdapter>();
            #else
            battleInputAdapter = this.gameObject.AddComponent<PCBattleInputAdapter>();
            #endif
        }

    	void Start ()
        {
            GameController.Instance.InitBattle (this);
    	}
    	
    	void Update ()
        {
            battleController.Tick (Time.deltaTime);
    	}

        #region IBattleView Implementation
        private System.Action startBattleListener;
        private System.Action<Vector3> inputEventListener;
        private System.Action stopInputEventListener;

        public void SetTickableModel (ITickable tickableModel)
        {
            battleController = tickableModel;
        }

        public void SetBattleStartListener (System.Action listener)
        {
            startBattleListener = listener;
        }

        public void SetInputEventListener (System.Action<Vector3> listener)
        {
            inputEventListener = listener;
        }

        public void SetStopInputEventListener (System.Action listener)
        {
            stopInputEventListener = listener;
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

            battleInputAdapter.Setup (shipView);

            callback (shipView);
        }

        public void StartLevel ()
        {
            levelGenerator.StartLevel ();

            battleInputAdapter.InputEvent = OnInputEvent;
            battleInputAdapter.StopInputEvent = OnStopInputEvent;
            battleInputAdapter.StartInput ();
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

        private void OnInputEvent (Vector3 movementDirection)
        {
            if (inputEventListener != null)
                inputEventListener (movementDirection);
        }

        private void OnStopInputEvent ()
        {
            if (stopInputEventListener != null)
                stopInputEventListener ();
        }
        #endregion
    }
}