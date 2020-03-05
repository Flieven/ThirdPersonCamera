using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGizmo : MonoBehaviour
{
    [SerializeField] private Color GizmoColor = Color.white;

    [SerializeField] private float GizmoSize = 0.5f;

    private void OnDrawGizmos()
    {
        Gizmos.color = GizmoColor;
        Gizmos.DrawWireSphere(transform.position, GizmoSize);
    }
}
