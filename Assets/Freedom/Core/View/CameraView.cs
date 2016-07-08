using UnityEngine;
using System.Collections;

namespace Freedom.Core.View
{
    public class CameraView : MonoBehaviour
    {
        private Transform cameraTransform;

    	void Start ()
        {
            // save local reference to the transform
            cameraTransform = this.transform;
    	}
    	
    	void Update ()
        {
            // move the camera in order to follow player's ship

    	}
    }
}