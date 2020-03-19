using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_PlayerSoundTriggers : MonoBehaviour
{
    [Header("Footstep")]
    public AudioSource footstepSource;
    public Vector2 footstepPitchRange = Vector2.one;

    [Header("Tongue Attack")]
    public AudioSource tongueAttackSource;
    public Vector2 tongueAttackPitchRange = Vector2.one;

    public void PlayFootstepSound()
    {
        footstepSource.pitch = Random.Range(footstepPitchRange.x, footstepPitchRange.y);
        footstepSource.Play();
    }

    public void PlayTongueAttackSound()
    {
        tongueAttackSource.pitch = Random.Range(tongueAttackPitchRange.x, tongueAttackPitchRange.y);
        tongueAttackSource.Play();
    }
}
