using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionGuessingManager : MonoBehaviour
{
    public MenuManager menuManagerRef;

    public CountDown countDown;
    public GameObject Score;

    private double startingTime;
    private double guessTime;

    private DirectionGuessingData data;
    private int currentGameIndex = 0;

    private Transform cam;

    public Transform AudioSourcePosition;

    public MenuFollower followMenu;

    private void OnEnable()
    {
        countDown.gameObject.SetActive(false);
        cam = FindObjectOfType<Camera>().transform; 
    }



    public void StartGame()
    {
        countDown.gameObject.SetActive(true);
        Score.SetActive(false);
        countDown.StartCountdown();
          
        data = new DirectionGuessingData(currentGameIndex);
        currentGameIndex++;


        Invoke("PlayAudioSource", 3);
        Invoke("SelectGuess", 6);
    }

    public void SetMenuManager(MenuManager man)
    {
        menuManagerRef = man;
    }

    public void PlayAudioSource()
    {
        Debug.Log("Play Source");
        SpawnAtRandomPosition();
        startingTime = Time.time;
    }

    private void SpawnAtRandomPosition()
    {
        AudioSourcePosition.position = cam.position + new Vector3(Random.Range(-5,5), Random.Range(-1,1), Random.Range(-5,5));
    }

    public void SelectGuess()
    {
        Debug.Log("Guess");
        guessTime = Time.time;
        double dif = guessTime - startingTime;

        data.timeToGuessDirection = dif;
        Vector3 direction = (AudioSourcePosition.position-cam.position).normalized;
        Vector3 guessedDirection = cam.forward;

        data.timeToGuessDirection = dif;
        data.sourceDirection = direction;
        data.guessedDirection = guessedDirection;
        data.azimuthDifference = GetAzimuth();
        data.elevationDifference = GetElevation();
        GameManager.Instance.dataManager.currentSessionData.directionGuessingResults.Add(data);
        GameManager.Instance.dataManager.SaveSession();

        Debug.Log("Azimuth: "+GetAzimuth());
        Debug.Log("Elevation: " + GetElevation());

        countDown.gameObject.SetActive(false);
        Score.SetActive(true);

        followMenu.Follow();
    }

    private float GetAzimuth()
    {
        Vector3 direction = (AudioSourcePosition.position - cam.position).normalized;
        Vector3 d = new Vector3(Vector3.Dot(direction, cam.right), Vector3.Dot(direction, cam.up), Vector3.Dot(direction, cam.forward));
        return Mathf.Atan2(d.x, d.z) / Mathf.PI;
    }

    private float GetElevation()
    {
        Vector3 direction = (AudioSourcePosition.position - cam.position).normalized;
        Vector3 d = new Vector3(Vector3.Dot(direction, cam.right), Vector3.Dot(direction, cam.up), Vector3.Dot(direction, cam.forward));

        Vector3 proj = new Vector3(d.x, 0, d.z).normalized;
        float elevation = Mathf.Acos(Vector3.Dot(d, proj)) / Mathf.PI;
        if (d.z < 0) elevation = 1 - elevation;
        if (d.x < 0 || d.y < 0) elevation = -elevation;
        return elevation;
    }

    public void OnBackClick()
    {
        menuManagerRef.SetMenu(MenuState.Closed);
    }

    public void OnAgainClick()
    {
        StartGame();
    }
}
