using UnityEngine;
using Freedom.Core.Model;

namespace Freedom.Core.View
{
    public class ShipView : MonoBehaviour, IShipView
    {
        public ShipFactory.ShipType shipType;

        private Transform myTransform;
        private System.Action recycleListener;

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

        public void SetRecycleListener (System.Action recycleListener)
        {
            this.recycleListener = recycleListener;
        }

        public string ShipType { get { return shipType.ToString (); } }

        public void Recycle ()
        {
            if (recycleListener != null)
                recycleListener ();

            // reset
            this.recycleListener = null;
        }
    }
}