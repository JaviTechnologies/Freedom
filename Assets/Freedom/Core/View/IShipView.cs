using UnityEngine;
using Freedom.Core.Model;

namespace Freedom.Core.View
{
    public interface IShipView
    {
        /// <summary>
        /// Updates the view.
        /// </summary>
        /// <param name="position">Position.</param>
        void UpdateView (Vector3 position);

        /// <summary>
        /// Gets the position.
        /// </summary>
        /// <returns>The position.</returns>
        Vector3 GetPosition ();

        /// <summary>
        /// Sets the recycle listener.
        /// </summary>
        /// <param name="recycleListener">Recycle listener.</param>
        void SetRecycleListener (System.Action recycleListener);
    }
}