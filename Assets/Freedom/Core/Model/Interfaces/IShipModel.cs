using UnityEngine;
using Freedom.Core.View;

namespace Freedom.Core.Model
{
    public interface IShipModel : ITickable
    {
        void Setup (IShipView shipView);
        Vector3 GetPosition ();
        ShipFactory.ShipType GetShipType ();
        void SetDirection (Vector3 direction);
        void StopMovement ();
    }
}