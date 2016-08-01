namespace Freedom.Core.Model.Interfaces
{
    public interface IGamerModel
    {
        /// <summary>
        /// Gets or sets the current level.
        /// </summary>
        /// <value>The current level.</value>
        ILevelModel CurrentLevel { get; set; }

        /// <summary>
        /// Gets or sets the max score.
        /// </summary>
        /// <value>The max score.</value>
        int MaxScore { get; set; }

        /// <summary>
        /// Saves gamer data to disk.
        /// </summary>
        void Save ();

        /// <summary>
        /// Loads gamer data from disk.
        /// returns true when loaded.
        /// </summary>
        bool Load ();
    }
}