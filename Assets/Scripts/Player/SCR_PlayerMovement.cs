using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_PlayerMovement : MonoBehaviour
{
	#region Enable & Disable
	private void OnEnable()
	{
        // Enable input
		controls.Enable();

        // Enable external velocity
        externalVelocity.gameObject.SetActive(true);
    }
	private void OnDisable()
    {
        // Disable input
        controls.Disable();

        // Disable external velocity
        if (externalVelocity != null)
            externalVelocity.gameObject.SetActive(false);
    }
    #endregion

    #region References
    public SCR_Variables variables;
    public Rigidbody externalVelocity;
    public PhysicMaterial noFrictionMat, highFrictionMat;
    public Transform playerMesh;
    InputControls controls;
    Rigidbody rb;
	Transform camT;
    CapsuleCollider movementColl;
    ConstantForce gravity;
    SCR_VarManager varManager;
    #endregion

    #region Public variables
    [Tooltip("Layer mask used for ground detection")]
    public LayerMask groundMask;
    [Header("Runtime variables")]
    public bool overrideNormalMovement = false;
    public bool overrideJump = false;
    public bool overrideRotation = false;
    public bool overrideGroundDetect = false;
    public bool touchingGround;
    public bool canMidairJump = true;
    public bool jumping;
    #endregion

    #region Local Variables
    bool running;
	float currentSpeed;
    Vector3 moveDirection;
	float moveRotationVelocity, moveSpeedVelocity;
	float rotationVelocity, speedVelocity;
	float targetRotation;
    float offGroundTime;
    float jumpCooldown;
    Vector3 groundNormal;
    bool bonkedOnCeiling;
    #endregion

    #region Debug variables
    [ContextMenu("Show or hide ground normal")]
    void GroundNormalSwitch()
    {
        showGroundNormal = !showGroundNormal;
    }
    [HideInInspector]
    public bool showGroundNormal;

    #endregion

    private void Awake()
	{
		//REF:
        // Whoa change this please
		camT = GameObject.FindGameObjectWithTag("PlayerCam").transform;
		controls = new InputControls();
		rb = GetComponent<Rigidbody>();
        movementColl = GetComponent<CapsuleCollider>();
        gravity = externalVelocity.GetComponent<ConstantForce>();

		//Functions:
		InputActions();

        // unparent external velocity object
        externalVelocity.transform.SetParent(null);

        // Set default gravity
        gravity.force = Vector3.down * variables.gravityForce;
	}

    private void Start()
    {
        varManager = SCR_VarManager.Instance;
    }

    private void FixedUpdate()
	{
        //Functions

        if (!overrideRotation)
        {
            NormalRotation();
        }

        if (!overrideNormalMovement)
        {
            if (touchingGround)
            {
                GroundMovement();
            }
            else
            {
                AirMovement();
            }
        }

        ManageExternalVelocity();
        GroundDetect();
    }

    #region Horizontal movement

    /// <summary>
    /// Horizontal movement while touching ground
    /// </summary>
    void GroundMovement()
    {
        /* OK, so this is gonna need some explanation
         * This movement system originally worked by setting player's rotation, then moving them forward.
         * I've changed this to make movement independent of player's rotation. But the code still works mostly the same:
         * by setting a rotation and then moving player along the forward vector of that rotation. 
         */

        // Get player directional input:
        Vector2 input = controls.Player.Movement.ReadValue<Vector2>();

        // Find rotation of direction player should move in (it's weird, but truuuuuuuuust me)
        float targetMoveRotation = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + camT.eulerAngles.y;

        float targetSpeed;
        // If Input then set Target Rotation &Smoothly Rotate in Degrees:
        if (input.magnitude > 0.1f)
        {
            // moveDirection = Vector3.up * Mathf.SmoothDampAngle(moveDirection.y, targetMoveRotation, ref moveRotationVelocity, variables.playerTurnSpeed);

            moveDirection = Vector3.up * targetMoveRotation;

            //targetSpeed = (running ? variables.runSpeed : variables.walkSpeed) * input.magnitude;
            targetSpeed = variables.walkSpeed * input.magnitude;
        }
        else
        {
            targetSpeed = 0;

            rb.velocity = Vector3.zero;
        }

        //Check if Running & Set Speed Accordingly:
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref moveSpeedVelocity, variables.accelerationTime);

        // Find the right vector of player's move direction
        Vector3 rightVector = Vector3.Cross(Vector3.up, Quaternion.Euler(moveDirection) * Vector3.forward);

        // Align move direction to ground normal.
        // This is done by crossing ground angle with the right vector of player's move direction, resulting in a vector that is perpendicular to both.
        Vector3 velocity = Vector3.Cross(groundNormal, -rightVector) * currentSpeed;

        rb.velocity = velocity;
    }

    /// <summary>
    /// Old movement function is only kept around temporarily
    /// </summary>
    void GroundMovementOld()
    {
        /* OK, so this is gonna need some explanation
         * This movement system originally worked by setting player's rotation, then moving them forward.
         * I've changed this to make movement independent of player's rotation. But the code still works mostly the same:
         * by setting a rotation and then moving player along the forward vector of that rotation. 
         */

        // Get player directional input:
        Vector2 input = controls.Player.Movement.ReadValue<Vector2>();

        // Find rotation of direction player should move in (it's weird, but truuuuuuuuust me)
        float targetMoveRotation = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + camT.eulerAngles.y;

        float targetSpeed;
        // If Input then set Target Rotation &Smoothly Rotate in Degrees:
        if (input.magnitude > 0.1f)
        {
            // moveDirection = Vector3.up * Mathf.SmoothDampAngle(moveDirection.y, targetMoveRotation, ref moveRotationVelocity, variables.playerTurnSpeed);

            // Align move direction to ground normal
            moveDirection = Vector3.up * targetMoveRotation;

            //targetSpeed = (running ? variables.runSpeed : variables.walkSpeed) * input.magnitude;
            targetSpeed = variables.walkSpeed * input.magnitude;
        }
        else
        {
            targetSpeed = 0;

            rb.velocity = Vector3.zero;
        }

        //Check if Running & Set Speed Accordingly:
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref moveSpeedVelocity, variables.accelerationTime);

        //Move the Player:
        Vector3 velocity = (Quaternion.Euler(moveDirection) * Vector3.forward) * currentSpeed;
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);

        // Should be updated to move along ground normal
        // Player mesh should also be rotated to fit ground normal.
    }

    /// <summary>
    /// Horizontal movement while not touching ground
    /// </summary>
    void AirMovement()
    {
        //Get player directional input:
        Vector2 input = controls.Player.Movement.ReadValue<Vector2>();

        // Find rotation of direction player should move in (it's weird, but truuuuuuuuust me)
        float targetMoveRotation = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + camT.eulerAngles.y;

        float targetSpeed;
        //If Input then set Target Rotation &Smoothly Rotate in Degrees:
        if (input.magnitude > 0.1f)
        {
            //moveDirection = Vector3.up * Mathf.SmoothDampAngle(moveDirection.y, targetMoveRotation, ref moveRotationVelocity, variables.playerTurnSpeed);
            moveDirection = Vector3.up * targetMoveRotation;

            //targetSpeed = (running ? variables.runSpeed : variables.walkSpeed) * input.magnitude;
            targetSpeed = variables.walkSpeed * input.magnitude;
        }
        else
        {
            targetSpeed = 0;
        }

        //Check if Running & Set Speed Accordingly:
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref moveSpeedVelocity, variables.accelerationTime);

        //Move the Player:
        Vector3 internalVelocity = (Quaternion.Euler(moveDirection) * Vector3.forward) * currentSpeed * variables.airControlPercent;

        // If player bonks head on ceiling during jump, stop jump velocity.
        if(bonkedOnCeiling
            && externalVelocity.velocity.y > 0)
        {
            externalVelocity.velocity = new Vector3(externalVelocity.velocity.x, 0, externalVelocity.velocity.z);
        }

        // Sum external and internal forces to determine velocity
        rb.velocity = new Vector3(internalVelocity.x, 0, internalVelocity.z) + externalVelocity.velocity;

        externalVelocity.velocity = Vector3.Lerp(externalVelocity.velocity, new Vector3(0, externalVelocity.velocity.y, 0), 0.05f);
    }
    
    void ManageExternalVelocity()
    {
        // Keep external velocity object at world origin
        externalVelocity.transform.position = Vector3.zero;

        if (touchingGround)
        {
            // Align gravity to ground normal when grounded
            gravity.force = -groundNormal * variables.gravityForce * varManager.playerGravityScale;

            // Clamp gravity force below terminal velocity
            if (externalVelocity.velocity.magnitude > variables.terminalVelocity)
                externalVelocity.velocity = externalVelocity.velocity.normalized * variables.terminalVelocity;

            varManager.playerGravityScale = 1;
        }
        else
        {
            // Reset gravity direction in air
            gravity.force = Vector3.down * variables.gravityForce * varManager.playerGravityScale;

            // Clamp fall speed below terminal velocity
            float clampedYVelocity = Mathf.Clamp(externalVelocity.velocity.y, -variables.terminalVelocity, Mathf.Infinity);
            externalVelocity.velocity = new Vector3(externalVelocity.velocity.x, clampedYVelocity, externalVelocity.velocity.z);

            // Lerp gravity scale towards 1. This is used during swing when gravity is temporarily reduced
            if (varManager.playerGravityScale != 1)
                varManager.playerGravityScale = Mathf.Lerp(varManager.playerGravityScale, 1, 2 * Time.fixedDeltaTime);
        }

        // Visualize external velocity
        Debug.DrawLine(transform.position, transform.position + externalVelocity.velocity * 1);
    }

    #endregion

    #region Jumping

    /// <summary>
    /// This function runs on jump input.
    /// It checks what kind of jump should be performed, if any.
    /// </summary>
    void JumpCheck()
    {
        if ((touchingGround || offGroundTime < variables.coyoteTime)
            && !jumping
            && !overrideJump)
        {
            StartCoroutine(GroundJump());
            StartCoroutine(AirJumpCooldown());
        }
        else if (canMidairJump && !overrideJump)
        {
            StartCoroutine(MidairJump(jumpCooldown));
            canMidairJump = false;
        }
    }

    /// <summary>
    /// Normal jump (from ground)
    /// </summary>
    /// <returns></returns>
    IEnumerator GroundJump()
    {
        jumping = true;

        // Change everything about this later, please
        externalVelocity.velocity = new Vector3(rb.velocity.x * variables.airControlPercent, variables.jumpHeight, rb.velocity.z * variables.airControlPercent);

        // Stop checking for ground collision for a few frames after jump
        overrideGroundDetect = true;
        touchingGround = false;

        Debug.DrawLine(transform.position, transform.position + Vector3.up, Color.red, 0.1f);

        // Start checking for ground collision again
        yield return new WaitForSeconds(0.1f);
        overrideGroundDetect = false;
    }

    /// <summary>
    /// This coroutine starts when player jumps from ground.
    /// It handles the cooldown between jumping and being able to double jump.
    /// </summary>
    /// <returns></returns>
    IEnumerator AirJumpCooldown()
    {
        jumpCooldown = variables.airJumpCooldown;
        while (jumpCooldown > 0)
        {
            yield return null;
            jumpCooldown -= Time.deltaTime;
        }
        jumpCooldown = 0;
    }

    /// <summary>
    /// Mid-air jump
    /// </summary>
    /// <param name="buffer">Input buffering</param>
    /// <returns></returns>
    IEnumerator MidairJump(float buffer)
    {
        // If button was pressed before cooldown ended, buffer the jump
        yield return new WaitForSeconds(buffer);

        if (!overrideNormalMovement && !touchingGround)
        {
            // oh god this hurts to look at
            //rb.velocity = new Vector3(rb.velocity.x, 4f, rb.velocity.z);
            externalVelocity.velocity = new Vector3(rb.velocity.x * variables.airControlPercent, variables.airJumpHeight, rb.velocity.z * variables.airControlPercent);

            Debug.DrawLine(transform.position, transform.position + Vector3.up / 2, Color.blue, 0.1f);
        }
        yield return null;
    }

    #endregion

    #region Ground Detection

    /// <summary>
    /// This function runs in FixedUpdate.
    /// Half of the ground detection process happens here; the other half happens in OnCollisionStay.
    /// </summary>
    void GroundDetect()
    {
        if (!touchingGround && offGroundTime <= 0)
        {
            // Run "leaving ground" function on first frame of not touching ground
            OnLeavingGround();
        }

        // Keep track of how many frames player has been in air
        if (!touchingGround)
            offGroundTime += Time.fixedDeltaTime;

        // Set touchingGround to false before checking for ground collision.
        // If OnCollisionStay finds ground, this gets overwritten as true.
        // This works because FixedUpdate runs before OnCollisionStay each frame.
        touchingGround = false;

        // Set bonkedOnCeiling to false. This is used to check if a jump should be canceled because player hit something.
        bonkedOnCeiling = false;

        // Check if player should be snapped down to ground
        if (offGroundTime <= 0 && !overrideGroundDetect)
            StartCoroutine(SnapToGround());
    }

    /// <summary>
    /// This function runs each time player lands on ground after being in the air.
    /// </summary>
    void OnLanding()
    {
        moveDirection = transform.eulerAngles;

        // Set fall speed to zero
        externalVelocity.velocity = Vector3.zero;

        // Give player high friction physic material (possibly a terrible, terrible solution...)
        //movementColl.material = highFrictionMat;
    }

    /// <summary>
    /// This function runs once when player leaves the ground.
    /// </summary>
    void OnLeavingGround()
    {
        // If falling, set fall speed to zero
        if (externalVelocity.velocity.y < 0)
            externalVelocity.velocity = Vector3.zero;

        // [expand this to add velocity when leaving moving platforms]

    }

    /// <summary>
    /// This function runs after OnCollisionStay has checked for ground collision.
    /// If collision didn't find ground, this checks if player is still *almost* touching ground.
    /// If yes, it snaps player down to ground.
    /// This makes movement on irregular and convex surfaces much more stable.
    /// </summary>
    IEnumerator SnapToGround()
    {
        // Delay raycast until after OnCollisionStay has run
        yield return new WaitForFixedUpdate();

        Vector3 rayDirection = Vector3.Lerp(-groundNormal, Vector3.down, 0.5f);

        if (!touchingGround 
            && Physics.Raycast(transform.position, rayDirection, out RaycastHit groundHit, variables.groundSnapDistance, groundMask, QueryTriggerInteraction.Ignore))
        {
            float contactAngle = Vector3.Angle(Vector3.up, groundHit.normal);

            if (contactAngle < variables.maxGroundAngle)
            {
                Debug.DrawLine(transform.position, groundHit.point, Color.red, 10);

                transform.position = groundHit.point;
                touchingGround = true;
            }
        }
    }

    /// <summary>
    ///  OnCollisionStay is used for ground detection.
    ///  We check if player's capsule collider is making contact with a surface flat enough to stand on.
    /// </summary>
    private void OnCollisionStay(Collision collision)
    {
        // Go through each contact point and check for an appropriate ground angle.
        foreach (var contact in collision.contacts)
        {
            float contactAngle = Vector3.Angle(Vector3.up, contact.normal);

            // If contact is an appropriate angle, player is touching ground
            if (contactAngle < variables.maxGroundAngle && !overrideGroundDetect)
            {
                if (offGroundTime > 0)
                    OnLanding();

                touchingGround = true;
                offGroundTime = 0;
                jumping = false;
                canMidairJump = true;

                // Setting ground normal based on collision alone is too inaccurate and causes a lot of jitter.
                // To combat this, we use 4 additional raycasts with slightly different offsets, and use the average of their normals.
                Vector3[] offsets = { Vector3.forward, Vector3.back, Vector3.right, Vector3.left };
                Vector3 averageNormal = contact.normal;

                foreach (var offset in offsets)
                {
                    if (Physics.Raycast(transform.position + (offset * 0.01f), Vector3.down, out RaycastHit groundHit, variables.groundSnapDistance, groundMask, QueryTriggerInteraction.Ignore))
                    {
                        averageNormal += groundHit.normal;
                    }

                }
                groundNormal = averageNormal.normalized;
            }
        }
    }

    /// <summary>
    /// OnCollisionEnter is used to:
    /// - child player to moving platforms (so movement will be smoother on these platforms)
    /// - check if player has collided with a ceiling. This will cancel any upward velocity from a jump.
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        // Attach player to moving platform
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(collision.transform);
        }

        // Go through each contact point and check if player has bonked head on ceiling.
        foreach (var contact in collision.contacts)
        {
            float contactAngle = Vector3.Angle(Vector3.down, contact.normal);

            // If contact is an appropriate angle, player has bonked on ceiling
            if (contactAngle < variables.maxHeadBonkAngle)
            {
                bonkedOnCeiling = true;
            }
        }
    }

    /// <summary>
    /// OnCollisionExit is used to unchild player from moving platforms when contact ends.
    /// </summary>
    private void OnCollisionExit(Collision collision)
    {
        // Detach player from moving platform
        if (collision.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(null);
            transform.localScale = Vector3.one;
        }
    }

    #endregion

    #region Rotation

    /// <summary>
    /// Set player's rotation during normal movement.
    /// Rotation works the same on ground and in air, because aiming tongue needs to feel consistent.
    /// </summary>
    void NormalRotation()
    {
        Quaternion prevFrameMeshRotation = playerMesh.rotation;

        //Get player directional input:
        Vector2 input = controls.Player.Movement.ReadValue<Vector2>();

        //If Input then set Target Rotation & Smoothly Rotate in Degrees:
        if (input.magnitude > 0.1f)
        {
            targetRotation = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + camT.eulerAngles.y;
        }

        float turnSpeed = variables.playerTurnSpeed;

        // reduce turn speed in air 
        // (This has been deprecated as player needs to turn quickly to aim tongue)
        //if (touchingGround)
        //    turnSpeed = variables.playerTurnSpeed;
        //else
        //    turnSpeed = variables.playerTurnSpeed * 3f;

        transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity, turnSpeed);

        // Rotate player mesh along ground normal (only mesh, player collider is unaffected)
        if (touchingGround)
        {
            Quaternion targetMeshRotation = Quaternion.FromToRotation(Vector3.up, groundNormal) * transform.rotation;
            playerMesh.rotation = Quaternion.Slerp(prevFrameMeshRotation, targetMeshRotation, 30 * Time.fixedDeltaTime);
        }
        else
        {
            // If not touching ground, rotate back to normal rotation (slowly)
            playerMesh.localRotation = Quaternion.Euler(playerMesh.localEulerAngles.x, 0, playerMesh.localEulerAngles.z);
            playerMesh.localRotation = Quaternion.Slerp(playerMesh.localRotation, Quaternion.Euler(0, 0, 0), 6 * Time.fixedDeltaTime);
        }
    }

    #endregion

    #region Input actions

    void InputActions()
	{
		//Set Jump:
		controls.Player.Jump.performed += ctx => JumpCheck();

		//Check and Set Running:
		if (variables.holdToRun)
			controls.Player.HoldtoRun.performed += ctx => running = !running;
		else
			controls.Player.PresstoRun.performed += ctx => running = !running;
	}

    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;

        // Show ground angle with a line
        if (touchingGround && showGroundNormal)
        {
            Gizmos.DrawLine(transform.position, transform.position + groundNormal);
            Gizmos.DrawWireSphere(transform.position, 0.05f);
        }
    }
}
