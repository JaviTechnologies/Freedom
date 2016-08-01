using UnityEngine;
using System.Collections;
using Freedom.Core.View.Utils;
using Freedom.Core.Model.Factories;

namespace Freedom.Core.View.BulletGeneratorModule
{
    public class BulletViewPool : GameObjectPool<BulletFactory.BulletType,BulletView>
    {
    }
}