using System;
using UnityEngine;

namespace Freedom.Core.View.Interfaces
{
    public interface IBulletView
    {
        /// <summary>
        /// Updates the view.
        /// </summary>
        /// <param name="position">Position.</param>
        void UpdateView (Vector3 position);

        /// <summary>
        /// Sets the recycle listener.
        /// </summary>
        /// <param name="recycleListener">Recycle listener.</param>
        void SetRecycleListener (System.Action recycleListener);

        /// <summary>
        /// Sets the impact listener.
        /// </summary>
        /// <param name="impactListener">Impact listener.</param>
        void SetImpactListener (System.Action<BulletView> impactListener);
    }
}

