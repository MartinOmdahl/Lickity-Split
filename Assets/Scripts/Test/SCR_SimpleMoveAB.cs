using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Move object between point A and point B at specified speed

public class SCR_SimpleMoveAB : MonoBehaviour
{
    [Tooltip("Target positions, relative to object's position")]
    public Vector3 pointAOffset, pointBOffset;

    [Tooltip("Seconds it takes to move between points")]
    public float movementDuration = 1;

    [Tooltip("Seconds to pause after movement")]
    public float waitTime = 1;
    
    Vector3 startPos, pointA, pointB;
    bool movementStarted = false;
    MeshCollider mc;

    void Start()
    {
        startPos = transform.position;
        pointA = startPos + pointAOffset;
        pointB = startPos + pointBOffset;

        transform.position = pointA;
    }

    void Update()
    {
        if (!movementStarted && transform.position == pointB)
        {
            StartCoroutine(MoveToPoint(pointA));
        }

        if (!movementStarted && transform.position == pointA)
        {
            StartCoroutine(MoveToPoint(pointB));
        }
    }

    IEnumerator MoveToPoint(Vector3 targetPos)
    {
        movementStarted = true;

        float time = 0;
        while(time < movementDuration)
        {
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            transform.position = Vector3.MoveTowards(transform.position, targetPos, Vector3.Distance(pointA, pointB) / movementDuration * Time.deltaTime);
        }
        transform.position = targetPos;

        yield return new WaitForSeconds(waitTime);
        movementStarted = false;
    }

    private void OnDrawGizmos()
    {
        Vector3 gizmoAnchor;
        if (Application.isPlaying)
        {
            gizmoAnchor = startPos;
        }
        else
        {
            gizmoAnchor = transform.position;
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(gizmoAnchor + pointAOffset, gizmoAnchor + pointBOffset);
        Gizmos.DrawWireSphere(gizmoAnchor + pointAOffset, 0.2f);
        Gizmos.DrawWireSphere(gizmoAnchor + pointBOffset, 0.2f);
    }

    private void OnDrawGizmosSelected()
    {
        // DrawOffsetMesh is poorly optimized, so it's only done when object is selected

        Vector3 gizmoAnchor;
        if (Application.isPlaying)
        {
            gizmoAnchor = startPos;
        }
        else
        {
            gizmoAnchor = transform.position;
        }

        DrawOffsetMesh(pointAOffset, gizmoAnchor);
        DrawOffsetMesh(pointBOffset, gizmoAnchor);
    }

    private void DrawOffsetMesh(Vector3 offset, Vector3 anchor)
    {
        Gizmos.color = Color.black;

        var m = Gizmos.matrix;

        if(mc == null)
        {
            mc = GetComponentInChildren<MeshCollider>();
        }

        var g = new GameObject();
        var t = g.transform;

        t.position = anchor + mc.transform.localPosition;
        t.rotation = mc.transform.rotation;
        t.localScale = mc.transform.localScale;

        t.position += offset;

        Gizmos.matrix = t.localToWorldMatrix;

        Gizmos.DrawWireMesh(GetComponentInChildren<MeshCollider>().sharedMesh);
        DestroyImmediate(g);

        Gizmos.matrix = m;
    }

    // Mesh for gizmo must be updated manually
    [ContextMenu("Update Mesh")]
    void UpdateMesh()
    {
        mc = GetComponentInChildren<MeshCollider>();
    }
}