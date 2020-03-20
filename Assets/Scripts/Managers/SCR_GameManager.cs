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

    public static SCR_GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.parent = null;
        }
        else
        {
            Destroy(gameObject);
        }

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
        // If Resume was pressed on pause menu, try to unpause game
        if (objectRefs.pauseMenu.ResumePressed)
        {
            objectRefs.pauseMenu.ResumePressed = false;
            PauseCheck();
        }
    }

    void PauseCheck()
    {
        print("pause check");

        // If not [in a cutscene] or other menu
        if (varManager.gamePaused && !varManager.gameOver)
        {
            varManager.gamePaused = false;

            // Deactivate pause menu
            objectRefs.pauseMenu.ClosePauseMenu();

            // Unfreeze time
            Time.timeScale = 1;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;

            // Enable player controls
            objectRefs.player.GetComponent<SCR_PlayerMovement>().enabled = true;
            objectRefs.player.GetComponent<SCR_Tongue>().enabled = true;
        }
        else if (!varManager.gameOver)
        {
            varManager.gamePaused = true;

            // Activate pause menu
            objectRefs.pauseMenu.OpenPauseMenu();

            // Freeze time
            Time.timeScale = 0;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;

            // Disable player controls
            objectRefs.player.GetComponent<SCR_PlayerMovement>().enabled = false;
            objectRefs.player.GetComponent<SCR_Tongue>().enabled = false;
        }
    }

    void InputActions()
    {
        controls.Player.Pause.performed += ctx => PauseCheck();
    }
}
