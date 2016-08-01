using System;
using UnityEngine;

namespace Freedom.Core.View.Interfaces
{
    public interface IBulletView
    {
        /// <summary>
        /// Setup the bullet.
        /// </summary>
        /// <param name="damage">Damage.</param>
        void Setup(float damage);

        /// <summary>
        /// Gets the damage.
        /// </summary>
        /// <value>The damage.</value>
        float Damage { get; }

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
    }
}

