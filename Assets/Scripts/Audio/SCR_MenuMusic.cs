using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Make object a singleton, and persist through scene loads. Destroy object if told to do so.
public class SCR_MenuMusic : MonoBehaviour
{
    #region Singleton
    public static SCR_MenuMusic instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public void StopPlaying()
    {
        instance = null;
        Destroy(gameObject);
    }
}
