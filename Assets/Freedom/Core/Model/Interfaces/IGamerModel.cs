namespace Freedom.Core.Model.Interfaces
{
    public interface IGamerModel
    {
        /// <summary>
        /// Gets or sets the current level.
        /// </summary>
        /// <value>The current level.</value>
        ILevelModel CurrentLevel { get; set; }
    }
}