using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;

// General script for all tongue-interactable objects
public class SCR_TongueTarget : MonoBehaviour
{
    // On target types
    // Swing: Frog hangs and swings from target.
    // Eat: Frog instantly eats target.
    // Grab: Frog can grab target and pull it.
    // Deflect: Frog tongue immediately retracts after hitting target.
    public enum TargetType { Swing, Eat, Grab, Deflect}
    public TargetType targetType = TargetType.Swing;

    // General variables
    [Header("General variables")]
    public Vector3 targetIconOffset = new Vector3(0, 0.5f, 0);
    [HideInInspector]
    public bool isBeingAttacked;

    // Swing variables
    [Header("Swing variables")]
    [Tooltip("Whether player can start swing while grounded")]
    public bool canSwingFromGround = true;
    public bool startWithVelocity = true;
    [HideInInspector]
    public bool isBeingSwung;

    // Eat variables
    [Header("Eat variables")]
    [Tooltip("Whether player gets something after eating this object")]
    public bool HasReward;
    public enum RewardType { Coin, TempReward2, TempReward3}
    public RewardType rewardType = RewardType.Coin;
    public int rewardCount = 1;
    [HideInInspector]
    public bool isBeingEaten;

    // Grab variables
    [Header("Grab variables")]
    [Tooltip("Whether pulling on this object triggers an event.")]
    public bool eventOnPull;
    [Tooltip("How many seconds must object be pulled to trigger effect?")]
    public float timeToPull = 1;
    [HideInInspector]
    public bool isBeingPulled;
    [HideInInspector]
    public bool eventTriggered;


    private void OnEnable()
    {
        // Add reference to self to list of all tongue targets
        if (SCR_ObjectReferenceManager.Instance != null)
        {
            SCR_ObjectReferenceManager.Instance.tongueTargets.Add(this);
        }
    }

    private void Start()
    {
        // Scan through list of tongue targets. If self wasn't added yet, add self.
        bool foundSelf = false;
        foreach (var target in SCR_ObjectReferenceManager.Instance.tongueTargets)
        {
            if (target == this)
            {
                foundSelf = true;
                break;
            }
        }

        if (!foundSelf)
        {
            SCR_ObjectReferenceManager.Instance.tongueTargets.Add(this);
        }
    }

    private void OnDisable()
    {
        // Remove reference to self from list of all tongue targets
        if (SCR_ObjectReferenceManager.Instance != null)
        {
            SCR_ObjectReferenceManager.Instance.tongueTargets.Remove(this);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize target icon position
        Gizmos.DrawIcon(transform.position + transform.TransformDirection(targetIconOffset), "SPR_TargetTriangle128.png", false, Color.white);
    }
}

// Editor class to show different variables based on target type
//[CustomEditor(typeof(SCR_TongueTarget))]
//public class DynamicVariables : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        SCR_TongueTarget tongueTarget = target as SCR_TongueTarget;

//        tongueTarget.targetType = (SCR_TongueTarget.TargetType)EditorGUILayout.EnumPopup("Target Type", tongueTarget.targetType);

//        switch (tongueTarget.targetType)
//        {
//            case SCR_TongueTarget.TargetType.Swing:
//                tongueTarget.canSwingFromGround = EditorGUILayout.Toggle("Can Swing From Ground", tongueTarget.canSwingFromGround);
//                tongueTarget.startWithVelocity = EditorGUILayout.Toggle("Start With Velocity", tongueTarget.startWithVelocity);
//                break;

//            case SCR_TongueTarget.TargetType.Eat:
//                tongueTarget.HasReward = EditorGUILayout.Toggle("Has Reward", tongueTarget.HasReward);
//                if (tongueTarget.HasReward)
//                {
//                    tongueTarget.rewardType = (SCR_TongueTarget.RewardType)EditorGUILayout.EnumPopup("Reward Type", tongueTarget.rewardType);

//                    tongueTarget.rewardCount = EditorGUILayout.IntField("Count", tongueTarget.rewardCount);
//                }
//                break;

//            case SCR_TongueTarget.TargetType.Grab:
//                tongueTarget.eventOnPull = EditorGUILayout.Toggle("Event On Pull", tongueTarget.eventOnPull);

//                if (tongueTarget.eventOnPull)
//                {
//                    tongueTarget.timeToPull = EditorGUILayout.FloatField("Time To Pull", tongueTarget.timeToPull);
//                }

//                break;

//            case SCR_TongueTarget.TargetType.Deflect:

//                break;

//            default:
//                break;
//        }
//        EditorGUILayout.Space();

//        tongueTarget.targetIconOffset = EditorGUILayout.Vector3Field("Target Icon Offset", tongueTarget.targetIconOffset);
//    }
//}
