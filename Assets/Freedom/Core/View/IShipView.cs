using UnityEngine;
using Freedom.Core.Model;

namespace Freedom.Core.View
{
    public interface IShipView
    {
        void Setup (ITickable tickableObject);
        void UpdateView (Vector3 position);
    }
}