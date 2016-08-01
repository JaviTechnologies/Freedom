using UnityEngine;
using System.Collections;
using Freedom.Core.View.Utils;
using Freedom.Core.Model.Factories;

namespace Freedom.Core.View.BulletGeneratorModule
{
    public class BulletViewPool : GameObjectPool<BulletFactory.BulletType,BulletView>
    {
        public const string BULLET_TAG = "Bullet";

        public BulletRecycleTrigger bulletRecycleTrigger;

        private void OnEnable ()
        {
            bulletRecycleTrigger.OnRecycleBulletTriggerEvent = OnRecycleTriggerEventHandler;
        }

        private void OnDisable ()
        {
            bulletRecycleTrigger.OnRecycleBulletTriggerEvent = null;
        }

        private void OnRecycleTriggerEventHandler (BulletView bulletView)
        {
            StartCoroutine (RecycleBullet(bulletView));
        }

        private IEnumerator RecycleBullet (BulletView bulletView)
        {
            yield return new WaitForEndOfFrame ();

            bulletView.Recycle ();

            yield return new WaitForEndOfFrame ();

            this.PoolObject (bulletView.BulletType, bulletView);

            yield return 0;
        }
    }
}