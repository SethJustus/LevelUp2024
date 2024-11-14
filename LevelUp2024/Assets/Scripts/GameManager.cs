using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    #region Unity Methods
    void Start()
    {
        // Set up a game manager singleton
        if (Instance == null)
        {
            Instance = this;
        }
    }
    #endregion

    #region Methods
    public void OnPlayerDeath()
    {
        
        
    }
    #endregion
}
