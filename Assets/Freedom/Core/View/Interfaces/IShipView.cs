using UnityEngine;
using Freedom.Core.Model;
using Freedom.Core.Model.Interfaces;

namespace Freedom.Core.View.Interfaces
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
        /// Gets the gun position.
        /// </summary>
        /// <value>The gun position.</value>
        Vector3 GunPosition { get; }

        /// <summary>
        /// Sets the bullet impact listener.
        /// </summary>
        /// <param name="bulletImpactListener">Bullet impact listener.</param>
        void SetBulletImpactListener (System.Action bulletImpactListener);

        /// <summary>
        /// Sets the recycle listener.
        /// </summary>
        /// <param name="recycleListener">Recycle listener.</param>
        void SetRecycleListener (System.Action recycleListener);

        /// <summary>
        /// Sets the destroyed listener.
        /// </summary>
        /// <param name="destroyedListener">Destroyed listener.</param>
        void SetDestroyedListener (System.Action<ShipView> destroyedListener);

        /// <summary>
        /// Handles the visual representation of the death.
        /// </summary>
        /// <param name="callback">Callback.</param>
        void Die (System.Action callback);

        /// <summary>
        /// Handles the invincible mode.
        /// </summary>
        /// <param name="duration">Duration.</param>
        void HandleInvincibleMode (float duration);
    }
}