using UnityEngine;
using Freedom.Core.Model;

namespace Freedom.Core.View
{
    public class ShipView : MonoBehaviour, IShipView
    {
        private Transform myTransform;

        private void Awake ()
        {
            myTransform = this.transform;
        }

        public void UpdateView (Vector3 position)
        {
            myTransform.position = position;
        }

        public Vector3 GetPosition ()
        {
            return myTransform.position;
        }
    }
}