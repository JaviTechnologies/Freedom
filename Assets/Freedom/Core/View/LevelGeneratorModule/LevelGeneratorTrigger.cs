using UnityEngine;
using System;
using System.Collections;

namespace Freedom.Core.View.LevelGeneratorModule
{
    public class LevelGeneratorTrigger : MonoBehaviour
    {
        public Action<Transform> OnLevelGeneratorTriggerEvent;

        void OnTriggerEnter (Collider other)
        {
            if (other.gameObject.tag == LevelGenerator.LEVEL_TRIGGER_TAG)
            {
                if (OnLevelGeneratorTriggerEvent != null)
                {
                    OnLevelGeneratorTriggerEvent (other.transform.parent);
                }
            }
        }
    }
}