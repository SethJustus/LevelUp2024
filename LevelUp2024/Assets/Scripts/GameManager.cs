using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Start()
    {
        // Set up a game manager singleton
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
