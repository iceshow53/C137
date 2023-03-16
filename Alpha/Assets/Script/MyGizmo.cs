using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGizmo : MonoBehaviour
{
    public Color color = Color.red;

    private void OnDrawGizmos()
    {
        // Gizmos의 색 변경
        Gizmos.color = color;

        // Gizmos의 모양 변경
        Gizmos.DrawSphere(this.transform.position, 0.2f);
    }
}
