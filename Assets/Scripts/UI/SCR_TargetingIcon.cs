using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_TargetingIcon : MonoBehaviour
{
    public Transform lockIcon;

    SCR_ObjectReferenceManager objectRefs;
    SCR_Tongue playerTongue;
    SCR_TongueTarget target;
    SpriteRenderer lockIconSprite;

    bool targetHasBeenNull = true;
    float iconAngleOffset;

    void Start()
    {
        objectRefs = SCR_ObjectReferenceManager.Instance;
        objectRefs.targetIcon = this;
        lockIconSprite = lockIcon.GetComponent<SpriteRenderer>();

        transform.SetParent(null);

        // Make icon start invisible
        transform.localScale = Vector3.zero;
        lockIcon.localScale = Vector3.zero;
    }

    void FixedUpdate()
    {
        // Destroy self if player stops existing
        if (objectRefs.player == null)
            Destroy(gameObject);
        else
        {
            if(playerTongue == null)
                playerTongue = objectRefs.player.GetComponent<SCR_Tongue>();

            // Rotate icon to always face camera
            transform.LookAt(objectRefs.playerCamera.transform, objectRefs.playerCamera.transform.up);


            target = playerTongue.currentTarget;
            if (target != null)
            {
                Targeting();
            }
            else
            {
                NotTargeting();
            }

            // Rotate icon 45 degrees if locked on target
            if (playerTongue.lockedOnTarget && target != null)
            {
                iconAngleOffset = Mathf.Lerp(iconAngleOffset, -45, 0.6f);
                lockIcon.localScale = Vector3.Lerp(lockIcon.localScale, Vector3.one, 0.6f);
                lockIconSprite.color = new Color(lockIconSprite.color.r, lockIconSprite.color.g, lockIconSprite.color.b, 1);
            }
            else
            {
                iconAngleOffset = Mathf.Lerp(iconAngleOffset, 0, 0.6f);
                lockIcon.localScale = Vector3.Lerp(lockIcon.localScale, new Vector3(1.5f, 1.5f, 1.5f), 0.6f);
                lockIconSprite.color = new Color(lockIconSprite.color.r, lockIconSprite.color.g, lockIconSprite.color.b, 0);
            }
            transform.localEulerAngles += new Vector3(0, 0, iconAngleOffset);
        }
    }

    void Targeting()
    {
        // Grow icon to normal size
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, 0.7f);

        Vector3 targetPos = target.transform.position;
        if (targetHasBeenNull)
        {
            // If NOT moving directly from the previous target, set position instantly
            transform.position = targetPos;
            targetHasBeenNull = false;
        }
        else
        {
            // If moving directly from the previous target, set position with a lerp
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.7f);
        }
    }

    void NotTargeting()
    {
        // Shrink icon
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, 0.7f);

        // If scale is close to zero, set scale to zero
        if (transform.localScale.x < 0.01f)
        {
            transform.localScale = Vector3.zero;
            targetHasBeenNull = true;
        }
    }
}
