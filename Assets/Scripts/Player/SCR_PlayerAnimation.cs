using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_PlayerAnimation : MonoBehaviour
{
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }

    public Animator anim;
    public MeshRenderer eyesRenderer;

    public Texture eyesForward, eyesLeft, eyesRight;

    [Header("Runtime variables")]
    public bool tongueOut;

    InputControls controls;
    SCR_PlayerV3 movement;
    SCR_Tongue tongue;

    private void Awake()
    {
        controls = new InputControls();
        movement = GetComponentInParent<SCR_PlayerV3>();
        tongue = GetComponentInParent<SCR_Tongue>();
    }

    void Update()
    {
        // Set animator bools
        anim.SetBool("Walking", controls.Player.Movement.ReadValue<Vector2>().magnitude > 0.1f);
        anim.SetBool("TouchingGround", movement.touchingGround);
        anim.SetBool("TongueOut", tongueOut);
        //print("Touching ground: " + movement.touchingGround + ", Walking: " + (controls.Player.Movement.ReadValue<Vector2>().magnitude > 0.05f) + ", Tongue out: " + tongueOut);

        // Set animator values
        anim.SetFloat("WalkSpeed", 1.4f * controls.Player.Movement.ReadValue<Vector2>().magnitude);

        EyeAnimation();
    }

    void EyeAnimation()
    {
        // Eye animation is completely independent from animator. It works by swapping out frog's eye texture so it's looking roughly at current target.

        if (tongue.currentTarget != null)
        {
            // Get direction between player and target (ignoring height difference)
            Vector3 targetDirection = (tongue.currentTarget.transform.position - new Vector3(transform.position.x, tongue.currentTarget.transform.position.y, transform.position.z)).normalized;

            // Get angle of target direction relative to player's rotation
            float angleToTarget = Vector3.Angle(transform.forward, targetDirection);

            if (angleToTarget > 30)
            {
                // Check if object is to the left or right of player
                if (transform.InverseTransformDirection(targetDirection).x > 0)
                {
                    eyesRenderer.material.mainTexture = eyesRight;
                }
                else
                {
                    eyesRenderer.material.mainTexture = eyesLeft;
                }
            }
            else
            {
                eyesRenderer.material.mainTexture = eyesForward;
            }
        }
        else if (eyesRenderer.material.mainTexture != eyesForward)
        {
            eyesRenderer.material.mainTexture = eyesForward;
        }
    }

    public void OverrideMovementAnims()
    {
        // Function is called by animation event.
        // Activate the "Override Walk" bool. Walk animation won't play until it's re-activated.

        anim.SetBool("OverrideWalk", true);
    }
    public void StopOverrideMovementAnims()
    {
        // Function is called by animation event.
        // Re-activate normal movement animations

        anim.SetBool("OverrideWalk", false);
    }
    
}
