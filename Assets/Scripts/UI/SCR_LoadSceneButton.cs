using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Script is for simple UI interaction that should just load a scene.
public class SCR_LoadSceneButton : MonoBehaviour
{
    #region Public variables
    public enum SceneLoadType { Name, Index};
    [Tooltip("Should scene index or scene name be used to load scene?")]
    public SceneLoadType loadSceneBy = SceneLoadType.Name;

    [Tooltip("Name of scene to load (only used if loadSceneBy is set to \"Name\")")]
    public string sceneName;
    [Tooltip("Index of scene to load (only used if loadSceneBy is set to \"Index\")")]
    public int sceneIndex;
    #endregion

    /// <summary>
    /// Load the specified scene.
    /// Function is called by button on UI canvas.
    /// </summary>
    public void LoadScene()
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
