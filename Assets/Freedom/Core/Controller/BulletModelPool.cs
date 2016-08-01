using System;
using Freedom.Core.Controller.Utils;
using Freedom.Core.Model.Interfaces;
using Freedom.Core.Model.Factories;

namespace Freedom.Core.Controller
{
    public class BulletModelPool : ObjectPool<BulletFactory.BulletType, IBulletModel>
    {

    }
}

