using Freedom.Core.Model;
using Freedom.Core.View;

namespace Freedom.Core.Controller {
    public class GameController {
        private enum State {
            NONE,
            BATTLE
        }

        /// <summary>
        /// The instance reference.
        /// </summary>
        private static GameController instance = null;

        /// <summary>
        /// Gets the instance.
        /// It creates if it doesn't exists.
        /// </summary>
        /// <value>The instance.</value>
        public static GameController Instance {
            get { 
                if (instance == null) {
                    UnityEngine.Debug.Log ("Creating new GameController instance.");
                    instance = new GameController ();
                }

                return instance;
            }
        }

        /// <summary>
        /// The gamer.
        /// </summary>
        private IGamerModel gamer;

        private BattleController battleController;

        private State currentState = State.NONE;

        /// <summary>
        /// Initializes a new instance of the <see cref="Freedom.Core.Controller.GameController"/> class.
        /// </summary>
        /// <param name="gamer">Gamer.</param>
        private GameController () {
            // Create a new gamer
            this.gamer = GamerFactory.CreateGamer();
        }

        /// <summary>
        /// Inits the battle.
        /// This creates a battle controller using the given battle view.
        /// </summary>
        /// <param name="battleView">Battle view.</param>
        public void InitBattle (IBattleView battleView) {
            // Create a new battle controller
            battleController = new BattleController (gamer.CurrentLevel, battleView);

            // prepare battle
            battleController.PrepareBattle ();

            // Update game state
            currentState = State.BATTLE;
        }
    }
}