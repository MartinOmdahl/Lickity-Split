using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;


public class SCR_DebugMenu : MonoBehaviour
{
    #region Public variables

    public List<KeyCode> debugMenuShortcut = new List<KeyCode>();
    public KeyCode closeMenuButton;
    [Tooltip("How long should shortcut be held?")]
    public float shortcutHoldTime = 1;
    [Tooltip("Prefab used for setting player's position")]
    public GameObject playerPositionObject;

    [Header("Menu items")]
    public CanvasGroup debugMenu;
    public Button collectedStarsButton;
    public TextMeshProUGUI collectedStarsText;
    public Button collectedCoinsButton;
    public TextMeshProUGUI collectedCoinsText, incrementby10Text;
    public Button loadSceneButton;
    public TextMeshProUGUI loadSceneText, currentSceneText;
    #endregion

    #region Private variables
    float secondsHeld;
    bool debugMenuOpen = false;
    int sceneToLoad;
    int currentSceneIndex;
    int prevFrameXInput;
    #endregion

    #region References
    SCR_VarManager varManager;
    SCR_ObjectReferenceManager objectRefs;
    #endregion

    void Start()
    {
        varManager = SCR_VarManager.Instance;
        objectRefs = SCR_ObjectReferenceManager.Instance;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // make sure debug menu is closed when at beginning of play
        CloseDebugMenu();
    }

    void Update()
    {
        if (debugMenuOpen)
            MenuUpdate();
        else
            CheckForOpenMenu();

        // Remember this frame's X input so we can check for change next frame.
        // It's important that we do this at the END of Update!
        prevFrameXInput = (int)Input.GetAxisRaw("Horizontal");
    }

    #region Opening & closing debug menu

    void CheckForOpenMenu()
    {
        // If holding debug shortcut: increase "seconds held" timer. 
        // Else: reset timer.
        secondsHeld += Time.deltaTime;
        foreach (var shortcutKey in debugMenuShortcut)
        {
            if (!Input.GetKey(shortcutKey))
                secondsHeld = 0;
        }

        // Open menu if shortcut has been held long enough
        if (secondsHeld >= shortcutHoldTime)
        {
            OpenDebugMenu();
        }
    }

    void OpenDebugMenu()
    {
        debugMenuOpen = true;

        // Activate debug menu on canvas
        debugMenu.alpha = 1;
        debugMenu.interactable = true;
        debugMenu.blocksRaycasts = true;

        // Reset debug menu selections
        collectedStarsButton.Select();
        sceneToLoad = currentSceneIndex;

        // Freeze time
        Time.timeScale = 0;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;

        // Disable player controls
        if (objectRefs.player != null)
        {
            objectRefs.player.GetComponent<SCR_PlayerV3>().enabled = false;
            objectRefs.player.GetComponent<SCR_Tongue>().enabled = false;
        }

        print("Debug menu activated");
    }

    void CloseDebugMenu()
    {
        debugMenuOpen = false;

        // Deactivate debug menu on canvas
        debugMenu.alpha = 0;
        debugMenu.interactable = false;
        debugMenu.blocksRaycasts = false;

        // Unfreeze time
        Time.timeScale = 1;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;

        // Enable player controls
        if (objectRefs.player != null)
        {
            objectRefs.player.GetComponent<SCR_PlayerV3>().enabled = true;
            objectRefs.player.GetComponent<SCR_Tongue>().enabled = true;
        }

        print("Debug menu closed");
    }

    #endregion

    #region Menu & button functionality

    void MenuUpdate()
    {
        // Check for close menu input
        if (Input.GetKeyDown(closeMenuButton))
            CloseDebugMenu();

        // Collected gold nuts button
        collectedStarsText.SetText(varManager.currentGoldNuts.ToString());
        int goldNutIncrement = Input.GetKey(KeyCode.JoystickButton2) ? 10 : 1;
        if (EventSystem.current.currentSelectedGameObject == collectedStarsButton.gameObject)
        {
            // Increment / decrement current stars on joystick left / right
            if (prevFrameXInput == 0 && Input.GetAxisRaw("Horizontal") == -1)
                varManager.currentGoldNuts -= goldNutIncrement;

            if (prevFrameXInput == 0 && Input.GetAxisRaw("Horizontal") == 1)
                varManager.currentGoldNuts += goldNutIncrement;

            if (varManager.currentGoldNuts < 0)
                varManager.currentGoldNuts = 0;
        }

        // Collected seeds button
        collectedCoinsText.SetText(varManager.currentSeeds.ToString());
        int seedIncrement = Input.GetKey(KeyCode.JoystickButton2) ? 10 : 1;
        if (EventSystem.current.currentSelectedGameObject == collectedCoinsButton.gameObject)
        {
            // Increment / decrement current stars on joystick left / right
            if (prevFrameXInput == 0 && Input.GetAxisRaw("Horizontal") == -1)
                varManager.currentSeeds -= seedIncrement;

            if (prevFrameXInput == 0 && Input.GetAxisRaw("Horizontal") == 1)
                varManager.currentSeeds += seedIncrement;

            if (varManager.currentSeeds < 0)
                varManager.currentSeeds = 0;

            incrementby10Text.enabled = true;
        }
        else
        {
            incrementby10Text.enabled = false;
        }

        // Load scene button

        loadSceneText.SetText(sceneToLoad.ToString());
        if (EventSystem.current.currentSelectedGameObject == loadSceneButton.gameObject)
        {
            // Increment / decrement scene value on joystick left / right
            if (prevFrameXInput == 0 && Input.GetAxisRaw("Horizontal") == -1 && sceneToLoad > 0)
                sceneToLoad--;

            if (prevFrameXInput == 0 && Input.GetAxisRaw("Horizontal") == 1 && sceneToLoad < SceneManager.sceneCountInBuildSettings - 1)
                sceneToLoad++;

            // Get name of selected scene
            string scenePath = SceneUtility.GetScenePathByBuildIndex(sceneToLoad);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            // Trim prefix from name
            if (sceneName.Substring(0, 4) == "SCE_")
                sceneName = sceneName.Substring(4);

            // Set "selected scene" text
            currentSceneText.enabled = true;
            currentSceneText.SetText(sceneName);
            if (sceneToLoad == currentSceneIndex)
                currentSceneText.text += "\n(current scene)";

            if (Input.GetButtonDown("Submit") && SceneManager.GetSceneByBuildIndex(sceneToLoad) != null)
            {
                print("Loaded scene index " + sceneToLoad);
                SceneManager.LoadScene(sceneToLoad);
            }
        }
        else
        {
            currentSceneText.enabled = false;
        }
    }

    public void ResetCollectibles()
    {
        // Function called by button in menu

        // Empty list of collected gold nuts
        while (varManager != null && varManager.CollectedGoldNuts.Count > 0)
        {
            varManager.CollectedGoldNuts.RemoveAt(0);
        }

        // Reset number of seeds and gold nuts
        varManager.currentGoldNuts = 0;
        varManager.currentSeeds = 0;

        print("All collectibles have been reset");
    }
    public void RebootGame()
    {
        // Function called by button in menu

        Destroy(varManager.gameObject);
        SceneManager.LoadScene(0);
        print("Game has been reset");
    }

    public void SetPlayerPosition()
    {
        // Function called by button in menu

        if (objectRefs.player != null)
        {
            Instantiate(playerPositionObject, objectRefs.player.transform.position, playerPositionObject.transform.rotation);
            Destroy(objectRefs.player);
            CloseDebugMenu();
        }
        else
        {
            print("Error: Couldn't set player position because no player exists in scene");
        }
    }

    #endregion
}
