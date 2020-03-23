using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_PlayerSystem : MonoBehaviour
{
    public SCR_Variables variables;
    public Transform tongueParent;
    [Tooltip("The joint Tongue Parent should follow")]
    public Transform tongueParentJoint;

    SCR_ControllerRumble rumble;
    SCR_ObjectReferenceManager objectRefs;
    SCR_VarManager varManager;

    void Start()
    {
        rumble = GetComponent<SCR_ControllerRumble>();
        varManager = SCR_VarManager.Instance;
        objectRefs = SCR_ObjectReferenceManager.Instance;

        objectRefs.player = gameObject;
        varManager.currentHealth = variables.maxHealth;
        varManager.playerGravityScale = 1;

        tongueParent.SetParent(tongueParentJoint);

        // From åpendag (move this elsewhere?)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        objectRefs.player = null;
    }

    void Update()
    {
        if(varManager.currentHealth <= 0 && !varManager.gameOver)
        {
            StartCoroutine(Die());
        }

        if (objectRefs.levelSettings.useKillY)
            CheckForKillY();
    }

    /// <summary>
    /// Check if player is below KillY altitude and kill them if they are.
    /// </summary>
    void CheckForKillY()
    {
        if(transform.position.y < objectRefs.levelSettings.killY && !varManager.gameOver)
        {
            varManager.currentHealth = 0;
        }
    }

    IEnumerator Die()
    {
        varManager.gameOver = true;
        // Disable player behavior
        // Play animation

        // Play death rumble
        rumble.StartRumble(10, 0.3f, 0.3f, 0.75f);

        // Wait until end of animation
        yield return new WaitForSeconds(1.5f);

        //freeze time
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0;

        print("YOU DIED");
    }
}
