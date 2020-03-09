using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_MovingSwingObject : MonoBehaviour
{
    [Tooltip("Position bug moves to when player is attached (relative to starting position)")]
    public Vector3 moveDestination;
    [Tooltip("Bug's top speed (in meters/second)")]
    public float flySpeed = 1;
    [Tooltip("How many seconds does it take bug to reach top speed?")]
    public float accelerationTime = 1;
    [Tooltip("How many seconds does bug wait between changing directions?")]
    public float waitTime = 1;

    Vector3 startingPosition;
    Vector3 currentPositionAnchor;
    float randomAnimationOffset;
    float sineTime;
    float movingForwardSpeed, movingBackSpeed;
    float waitTimer;

    SCR_TongueTarget targetComponent;

    void Awake()
    {
        targetComponent = GetComponent<SCR_TongueTarget>();


        randomAnimationOffset = Random.Range(-4, 4);
        currentPositionAnchor = transform.position;
        startingPosition = transform.position;

        // make Move Destination relative to transform.position
        moveDestination += transform.position;
    }

    void Update()
    {
        if (targetComponent.isBeingSwung)
        {
            movingBackSpeed = 0;

            if (currentPositionAnchor == moveDestination)
                FloatInPlace();
            else
                MoveToDestination();
        }
        else
        {
            movingForwardSpeed = 0;

            if (currentPositionAnchor == startingPosition)
                FloatInPlace();
            else
                MoveBack();
        }
    }

    void MoveToDestination()
    {
        if (movingForwardSpeed < flySpeed)
            movingForwardSpeed += flySpeed / accelerationTime * Time.deltaTime;
        else
            movingForwardSpeed = flySpeed;

        currentPositionAnchor = Vector3.MoveTowards(currentPositionAnchor, moveDestination, movingForwardSpeed * Time.deltaTime);

        transform.position = Vector3.Lerp(transform.position, currentPositionAnchor, 0.5f);
    }

    void MoveBack()
    {
        if (movingBackSpeed < flySpeed)
            movingBackSpeed += flySpeed / accelerationTime * Time.deltaTime;
        else
            movingBackSpeed = flySpeed;

        currentPositionAnchor = Vector3.MoveTowards(currentPositionAnchor, startingPosition, movingBackSpeed * Time.deltaTime);

        transform.position = Vector3.Lerp(transform.position, currentPositionAnchor, 0.5f);
    }

    void FloatInPlace()
    {
        float YPos = currentPositionAnchor.y + Mathf.Sin(Time.time * 4 + randomAnimationOffset) * 0.08f;
        transform.position = new Vector3(currentPositionAnchor.x, YPos, currentPositionAnchor.z);
    }

    private void OnDrawGizmosSelected()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        Vector3 gizmoPos;

        if (Application.isPlaying)
            gizmoPos = moveDestination;
        else
            gizmoPos = transform.position + moveDestination;


        Gizmos.DrawLine(transform.position, gizmoPos);
        Gizmos.DrawSphere(gizmoPos, 0.3f);
    }
}
