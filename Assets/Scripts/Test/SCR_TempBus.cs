using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Temp script to show off bus beetle

public class SCR_TempBus : MonoBehaviour
{
    public Vector3 targetPos;

    SCR_TongueTarget targetComponent;

    bool go;
    void Awake()
    {
        targetComponent = GetComponent<SCR_TongueTarget>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            go = true;
        }

        if (go)
        {

            transform.position = Vector3.MoveTowards(transform.position, targetPos, 5 * Time.deltaTime);
        }
        else
        {

        }
    }


    [ContextMenu("Set target to current position")]
    void SetTarget()
    {
        targetPos = transform.position;
    }
}
