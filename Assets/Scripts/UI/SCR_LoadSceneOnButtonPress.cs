using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SCR_LoadSceneOnButtonPress : MonoBehaviour
{
    #region Public variables
    public enum SceneLoadType { Name, Index };
    [Tooltip("Should scene index or scene name be used to load scene?")]
    public SceneLoadType loadSceneBy = SceneLoadType.Name;

    [Tooltip("Name of scene to load (only used if loadSceneBy is set to \"Name\")")]
    public string sceneName;
    [Tooltip("Index of scene to load (only used if loadSceneBy is set to \"Index\")")]
    public int sceneIndex;
    #endregion

    Gamepad gamepad;

    void Start()
    {
        gamepad = Gamepad.current;
    }


    void Update()
    {
        if (gamepad.buttonSouth.wasPressedThisFrame)
            LoadScene();
    }

    void LoadScene()
    {
        switch (loadSceneBy)
        {
            case SceneLoadType.Name:
                if (sceneName != "")
                    SceneManager.LoadScene(sceneName);
                else
                    print("Error: scene name not set");
                break;
            case SceneLoadType.Index:
                SceneManager.LoadScene(sceneIndex);
                break;
            default:
                break;
        }
    }
}
