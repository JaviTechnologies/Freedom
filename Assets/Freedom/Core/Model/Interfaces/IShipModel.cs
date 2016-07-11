using UnityEngine;

namespace Freedom.Core.Model
{
    public interface IShipModel : ITickable
    {
        void SetDirection (Vector3 direction);
        void StopMovement ();
    }
}