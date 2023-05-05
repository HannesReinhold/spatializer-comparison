using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
    public bool IsVR;
    public bool CanSpawnPlayer;

    public GameState InitialGameState = GameState.Explore;

    // Prefabs
    public GameObject PlayerVRPrefab;
    public GameObject PlayerNonVRPrefab;

    [HideInInspector] public Transform PlayerSpawn;

    [HideInInspector] public GUIManager GuiManager;


    public DataManager dataManager;


    private XRIDefaultInputActions actions;



    public bool hasCompletedIntroduction;



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
            dataManager = new DataManager();

            GuiManager = FindObjectOfType<GUIManager>();




            
        }

        InitializeGame();
        StartNewSession();
    }

    private void Start()
    {
        GuiManager = FindObjectOfType<GUIManager>();

        Canvas[] list = Resources.FindObjectsOfTypeAll<Canvas>();
        foreach (Canvas c in list)
        {
            c.worldCamera = FindObjectOfType<Camera>();
        }

    }

    private void OnEnable()
    {
        actions = new XRIDefaultInputActions();

    }


    void Guess(InputAction.CallbackContext context)
    {
        Application.Quit();
    }

    void ResetMenu(InputAction.CallbackContext context)
    {

    }

    private void Update()
    {
        float val = actions.XRIRightHandInteraction.Guess.ReadValue<float>();
        if (val != 0) LoadScene("Freeroam");


    }

    public void StartNewSession()
    {
        dataManager.InitializeSession();
    }

    private void InitializeGame()
    {
        Debug.Log("Initialize Game");

        // Spawn player
        PreparePlayerSpawn();
        if(CanSpawnPlayer) SpawnPlayer();
    }



    private void PreparePlayerSpawn()
    {
        PlayerSpawnPoint spawn = FindObjectOfType<PlayerSpawnPoint>();

        //if no spawn exists, create a new one
        if (spawn == null)
        {
            // try to find the height of the terrain
            RaycastHit hit;
            Vector3 foundPosition = Vector3.zero;
            if (Physics.Raycast(Vector3.up * 100, Vector3.down, out hit, Mathf.Infinity))
            {
                foundPosition = hit.point + Vector3.up;
            }
            PlayerSpawn = new GameObject().transform;
            PlayerSpawn.position = foundPosition;
        }
        else
        {
            PlayerSpawn = spawn.transform;
        }
    }


    private void SpawnPlayer()
    {
        if (IsVR)
        {
            GameObject PlayerVR = Instantiate(PlayerVRPrefab);
            PlayerVR.transform.position = PlayerSpawn.position;
        }
        else
        {
            GameObject PlayerNonVR = Instantiate(PlayerNonVRPrefab);
            PlayerNonVR.transform.position = PlayerSpawn.position;
        }

    }


    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }


}

public enum GameState
{
    Explore,
    Evaluate,
    GuessDirection
}
