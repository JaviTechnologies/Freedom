using UnityEngine;
using System.Collections;

public class GizmosDebug : MonoBehaviour {
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.2f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position + transform.up, 0.1f);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + transform.forward, 0.1f);
    }
}
