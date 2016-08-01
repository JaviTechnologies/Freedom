using System;
using UnityEngine;
using Freedom.Core.View.Interfaces;
using Freedom.Core.Model.Factories;

namespace Freedom.Core.Model.Interfaces
{
    public interface IBulletModel : ITickable
    {
        /// <summary>
        /// Gets the type of the bullet.
        /// </summary>
        /// <value>The type of the bullet.</value>
        BulletFactory.BulletType BulletType { get; }

        /// <summary>
        /// Inits the specified bulletType, position and direction.
        /// </summary>
        /// <param name="bulletType">Bullet type.</param>
        /// <param name="position">Position.</param>
        /// <param name="direction">Direction.</param>
        void Init (BulletFactory.BulletType bulletType, Vector3 position, Vector3 direction);

        /// <summary>
        /// Gets the state of the bullet.
        /// </summary>
        /// <value>The state.</value>
        BulletModel.BulletState State { get; }

        /// <summary>
        /// Sets the bullet view handler.
        /// </summary>
        /// <param name="bulletView">Bullet view.</param>
        void SetBulletViewHandler(IBulletView bulletView);
    }
}

