using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SCR_GameManager : MonoBehaviour
{
    #region Enable & Disable
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
    #endregion

    InputControls controls;
    SCR_VarManager varManager;
    SCR_ObjectReferenceManager objectRefs;

    private void Awake()
    {
        controls = new InputControls();
        InputActions();
    }

    private void Start()
    {
        varManager = SCR_VarManager.Instance;
        objectRefs = SCR_ObjectReferenceManager.Instance;

        // Disable menu controls
        controls.Menu.Disable();
    }

    void Update()
    {
        
    }

    void PauseCheck()
    {
        print("pause check");

        // If not [in a cutscene] or other menu
        if (varManager.gamePaused && !varManager.gameOver)
        {
            varManager.gamePaused = false;

            // Remove pause HUD
            objectRefs.pauseMenu.alpha = 0;
            objectRefs.pauseMenu.interactable = false;
            objectRefs.pauseMenu.blocksRaycasts = false;

            // Unfreeze time
            Time.timeScale = 1;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;

            // Enable player controls
            objectRefs.player.GetComponent<SCR_PlayerV3>().enabled = true;
            objectRefs.player.GetComponent<SCR_Tongue>().enabled = true;
        }
        else if (!varManager.gameOver)
        {
            varManager.gamePaused = true;

            // Bring up pause hud
            objectRefs.pauseMenu.alpha = 1;
            objectRefs.pauseMenu.interactable = true;
            objectRefs.pauseMenu.blocksRaycasts = true;

            // Freeze time
            Time.timeScale = 0;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;

            // Disable player controls
            objectRefs.player.GetComponent<SCR_PlayerV3>().enabled = false;
            objectRefs.player.GetComponent<SCR_Tongue>().enabled = false;
        }
    }

    void InputActions()
    {
        controls.Player.Pause.performed += ctx => PauseCheck();
    }
}
