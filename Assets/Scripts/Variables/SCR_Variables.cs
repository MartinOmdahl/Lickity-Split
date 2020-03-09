using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default_Variables", menuName = "Variables/Game Variables")]
public class SCR_Variables : ScriptableObject
{
	[Header("World Settings")]
	public float gravity = -15f;


	[Header("Player movement")]
	public bool holdToRun = true;
	public float walkSpeed = 4;
	public float runSpeed = 8;

	public float playerTurnSpeed = 0.1f;
	public float Acceleration = 0.1f;

	public float jumpHeight = 1.5f;
	public float airJumpHeight = 1.5f;
    [Tooltip("Cooldown for performing mid-air jump after jump")]
    public float airJumpCooldown = 0.5f;
    [Tooltip("Length of time after leaving ground player can still jump")]
    public float coyoteTime = 0.3f;
	[Range(0,1)]
	public float airControlPercent = .5f;
    [Tooltip("Highest possible velocity during normal movement")]
    public float terminalVelocity = 20;

    [Tooltip("Max walkable ground angle.\n" +
        "0 is level ground, 90 is a wall")]
    [Range(0, 90)]
    public float maxGroundAngle = 45;
    [Tooltip("Max angle of ceiling that will cancel jump if player hits it with their head.")]
    [Range(0, 90)]
    public float maxHeadBonkAngle = 20;


    [Header("Player tongue")]
    public float maxTargetDistance = 5;
    [Tooltip("Max angle target can be, relative to player.\n" +
        "An angle of 0 is directly in front of player.")]
    [Range(0, 360)]
    public float maxTargetAngle = 90;
    public bool holdToTarget = true;
    public AnimationCurve tongueAttackCurve;
    [Tooltip("Length of tongue while swinging")]
    [Min(0)]
    public float swingDistance = 1;


    [Header("Player misc")]
    public int maxHealth = 3;


	[Header("Camera")]
	public float camSensitivity = 1;
	public Vector2 camDistanceMinMax = new Vector2(1, 7);
    public Vector2 camPitchMinMax = new Vector2(-15, 85);
    public float camTurnSpeed = 0.1f;
}
