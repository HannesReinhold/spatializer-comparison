using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState InitialGameState = GameState.Explore;

    private static GameManager instance;

    public static GameManager Instance 
    { get 
        { 
            if(instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance; 
        } 
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }


        InitializeGame();
    }

    private void Start()
    {

    }

    private void Update()
    {

    }

    private void InitializeGame()
    {
        Debug.Log("Initialize Game");
    }

    
}

public enum GameState
{
    Explore,
    Evaluate,
    GuessDirection
}
