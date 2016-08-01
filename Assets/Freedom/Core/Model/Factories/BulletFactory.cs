using UnityEngine;
using System;
using Freedom.Core.Model.Interfaces;
using Freedom.Core.Controller.Utils;

namespace Freedom.Core.Model.Factories
{
    public class BulletFactory
    {
        public enum BulletType
        {
            A, B
        }

        public static IBulletModel CreateBullet (BulletFactory.BulletType bulletType, Vector3 position, Vector3 direction)
        {
            IBulletModel bullet = new BulletModel (bulletType, position, direction);

            return bullet;
        }
    }
}

