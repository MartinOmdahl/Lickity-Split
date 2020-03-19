using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_PlayerSystem : MonoBehaviour
{
    public SCR_Variables variables;
    public Transform tongueParent;
    [Tooltip("The joint Tongue Parent should follow")]
    public Transform tongueParentJoint;

    SCR_ObjectReferenceManager objectRefs;
    SCR_VarManager varManager;

    void Start()
    {
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
    }

    IEnumerator Die()
    {
        varManager.gameOver = true;
        // Disable player behavior
        // Play animation

        // Wait until end of animation
        yield return null;

        //freeze time
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0;

        print("YOU DIED");
    }
}
