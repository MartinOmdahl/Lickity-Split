using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_MovePlatformByLever : MonoBehaviour
{
    [Tooltip("Position when activated by lever, relative to starting position.")]
    public Vector3 activatedPosition;

    [Min(0)]
    [Tooltip("move speed in meters/second")]
    public float moveSpeed = 1;

    public SCR_Lever lever;

    Vector3 startingPosition;

    void Awake()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        if (lever == null)
            print("Error: no lever assigned!\n" +
                "You must assign a lever to this object.");

        if (lever.activated)
        {
            MoveToPosition(startingPosition + activatedPosition);
        }
        else
        {
            MoveToPosition(startingPosition);
        }
    }

    void MoveToPosition(Vector3 targetPos)
    {
        if(transform.position != targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        }
    }
}
