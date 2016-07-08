using UnityEngine;
using Freedom.Core.Model;

namespace Freedom.Core.View
{
    public class ShipView : MonoBehaviour, IShipView
    {
        private ITickable tickableModel;

        public void Setup (ITickable tickableObject)
        {
            this.tickableModel = tickableObject;
        }

        private void Update ()
        {
            // tick model
            if (tickableModel != null)
            {
                tickableModel.Tick (Time.deltaTime);
            }
        }

        public void UpdateView (Vector3 position)
        {
            this.transform.position = position;
        }
    }
}