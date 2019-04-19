using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[System.Serializable]
public enum GameState { NullState, MainMenu, Game }

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Allows a reference to GameManager

    public int Health = 3;              // Health
    public int Lives = 3;               // Lives

    void Awake()
    {
        // Keeps GameManager working
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }


    private void Update()
    {
        // Keeps health at max
        if (Health >= 4)
        {
            Health = 3;
        }
    }
}