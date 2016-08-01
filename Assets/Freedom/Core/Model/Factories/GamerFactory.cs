using Freedom.Core.Model.Interfaces;

namespace Freedom.Core.Model.Factories
{
    /// <summary>
    /// Gamer factory.
    /// </summary>
    public class GamerFactory
    {
        /// <summary>
        /// Creates the gamer.
        /// This should be taken from the server.
        /// </summary>
        /// <returns>The gamer.</returns>
        public static IGamerModel CreateGamer ()
        {
            IGamerModel gamer = new GamerModel ();

            // asign first level by default
            gamer.CurrentLevel = LevelFactory.CreateLevelModel (1);

            return gamer;
        }
    }
}