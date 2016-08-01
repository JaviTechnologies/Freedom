using UnityEngine;
using System.Collections;
using Freedom.Core.Controller.Utils;
using Freedom.Core.Model;
using Freedom.Core.Model.Interfaces;
using Freedom.Core.Model.Factories;

namespace Freedom.Core.Controller
{
    public class ShipModelPool : ObjectPool<ShipFactory.ShipType,IShipModel>
    {
        
    }
}