using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_VarManager : MonoBehaviour
{
    #region Singleton
    public static SCR_VarManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    [Header("Player")]
    public int currentHealth;

    [Header("Collectibles")]
    public int currentSeeds;
    // technically, we don't need this... could use CollectedGoldNuts.count instead
    public int currentGoldNuts;
    public List<int> CollectedGoldNuts = new List<int>();

    [Header("Gameplay")]
    public bool gamePaused = false;
    public bool gameOver = false;
}
