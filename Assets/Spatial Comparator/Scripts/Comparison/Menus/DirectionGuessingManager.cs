using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    public SpatializerSwitcher SourceSwitcher;

    public MenuFollower followMenu;

    private XRIDefaultInputActions actions;

    public LineRenderer GuessedLine;
    public LineRenderer ActualLine;

    private Vector3 guessedPosition;
    private int currentSpatializer = 0;

    private void OnEnable()
    {
        countDown.gameObject.SetActive(false);
        followMenu.Follow();

    }


    void Guess(InputAction.CallbackContext context)
    {
        SelectGuess();
    }

    void ResetMenu(InputAction.CallbackContext context)
    {
        followMenu.Follow();
    }



    public void StartGame()
    {
        cam = FindObjectOfType<Camera>().transform;
        countDown.gameObject.SetActive(true);
        Score.SetActive(false);
        countDown.StartCountdown();
          
        data = new DirectionGuessingData(currentGameIndex);
        currentGameIndex++;

        GuessedLine.SetPositions(new Vector3[] {});
        ActualLine.SetPositions(new Vector3[] {});

        Invoke("PlayAudioSource", 3);
        //Invoke("SelectGuess", 6);
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
        Vector3 dir = Random.insideUnitCircle.normalized;
        dir = new Vector3(dir.x*5, dir.y*1, dir.z*5);
        AudioSourcePosition.position = cam.position + dir;
        AudioSourcePosition.gameObject.SetActive(true);
        SourceSwitcher.SetSource(currentSpatializer);
        currentSpatializer = (currentSpatializer + 1) % 4;
    }

    public void SelectGuess()
    {
        data = new DirectionGuessingData(currentGameIndex);
        currentGameIndex++;
        guessTime = Time.time;
        double dif = guessTime - startingTime;

        data.timeToGuessDirection = dif;
        Vector3 direction = (AudioSourcePosition.position-cam.position).normalized;
        Vector3 guessedDirection = cam.forward;

        data.spatializerID = currentSpatializer;
        data.timeToGuessDirection = dif;
        data.sourceDirection = direction;
        data.guessedDirection = guessedDirection;
        data.azimuthDifference = GetAzimuth();
        data.elevationDifference = GetElevation();
        GameManager.Instance.dataManager.currentSessionData.directionGuessingResults.Add(data);
        GameManager.Instance.dataManager.SaveSession();

        guessedPosition = cam.position;
        GuessedLine.SetPositions(new Vector3[]{ guessedPosition, guessedPosition+cam.forward*2});
        ActualLine.SetPositions(new Vector3[] { guessedPosition, AudioSourcePosition.position });

        Debug.Log("Azimuth: "+GetAzimuth());
        Debug.Log("Elevation: " + GetElevation());

        countDown.gameObject.SetActive(false);
        Score.SetActive(true);

        followMenu.Follow();
        AudioSourcePosition.gameObject.SetActive(false);
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
        if (menuManagerRef != null)
            menuManagerRef.SetMenu(MenuState.Closed);
        else
            GameManager.Instance.LoadScene("MainHub");
    }

    public void OnAgainClick()
    {
        StartGame();
    }
}
