using UnityEngine;
using Freedom.Core.View;
using Freedom.Core.Model.Factories;
using Freedom.Core.View.Interfaces;

namespace Freedom.Core.Model.Interfaces
{
    public interface IShipModel : ITickable
    {
        /// <summary>
        /// Sets up the ship using the specified type and position.
        /// </summary>
        /// <param name="type">Type.</param>
        /// <param name="position">Position.</param>
        void Setup (ShipFactory.ShipType type, Vector3 position);

        /// <summary>
        /// Sets the view handler.
        /// </summary>
        /// <param name="shipView">Ship view.</param>
        void SetViewHandler (IShipView shipView);

        /// <summary>
        /// Sets the bullet listener.
        /// </summary>
        /// <param name="listener">Listener.</param>
        void SetBulletListener(System.Action<BulletFactory.BulletType, Vector3, Vector3> listener);

        /// <summary>
        /// Gets the ship's state.
        /// </summary>
        /// <value>The state.</value>
        ShipModel.ShipState State { get; }

        /// <summary>
        /// Gets the position of the ship.
        /// </summary>
        /// <returns>The position.</returns>
        Vector3 GetPosition ();

        /// <summary>
        /// Gets the type of the ship.
        /// </summary>
        /// <returns>The ship type.</returns>
        ShipFactory.ShipType GetShipType ();

        /// <summary>
        /// Sets the direction.
        /// </summary>
        /// <param name="direction">Direction.</param>
        void SetDirection (Vector3 direction);

        /// <summary>
        /// Stops the movement.
        /// </summary>
        void StopMovement ();
    }
}