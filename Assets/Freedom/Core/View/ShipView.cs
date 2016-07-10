using UnityEngine;
using Freedom.Core.Model;

namespace Freedom.Core.View
{
    public class ShipView : MonoBehaviour, IShipView
    {
        public void UpdateView (Vector3 position)
        {
            this.transform.position = position;
        }
    }
}