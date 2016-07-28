using UnityEngine;
using Freedom.Core.View;

namespace Freedom.Core.Model
{
    public interface IShipModel : ITickable
    {
        void Init (ShipFactory.ShipType type, Vector3 position);
        void Setup (IShipView shipView);
        ShipModel.ShipState State { get; }
        Vector3 GetPosition ();
        ShipFactory.ShipType GetShipType ();
        void SetDirection (Vector3 direction);
        void StopMovement ();
    }
}