using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float JitterAmount = 3f;
    public float HeadBobbingAmount = 1f;

    [SerializeField] private Transform CameraHolderTransform;
    [SerializeField] private Transform CameraSlotTransform;

    private Camera camera;

    private Vector3 currentPosition;
    private Vector3 targetPosition;

    private Vector3 currentRotation;
    private Vector3 targetRotation;

    private Vector3 randomOffset;
    private float speed;
    private float phase;
    private float footstepPhase;
    private float breathingPhase;

    private Vector2 direction = new Vector2();
    private Vector2 smoothedDirection = new Vector2();


    public void SetupComponents()
    {
        camera = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;

        randomOffset = new Vector3(Random.Range(0, 124), Random.Range(0, 124), Random.Range(0, 124));
    }

    public void ApplyCameraTransform()
    {
        CameraHolderTransform.position = currentPosition;

        // Apply camera jitering
        currentRotation = targetRotation + Mathf.Sqrt(speed) * JitterAmount * new Vector3(OctaveNoise(phase + randomOffset.x),
                                                               OctaveNoise(phase + randomOffset.y),
                                                               OctaveNoise(phase + randomOffset.z) * 2 - 1);

        // Apply head bobbing
        Vector3 rotationHeadBobbing = currentRotation + HeadBobbingAmount * Mathf.Sqrt(speed) * new Vector3(-Mathf.Abs(Mathf.Cos(footstepPhase)),
                                                                      Mathf.Sin(footstepPhase),
                                                                      0
                                                                      );

        Vector3 rotationBreathing = rotationHeadBobbing + Mathf.Sqrt(0.5f + speed) * 0.8f * new Vector3(Mathf.Sin(breathingPhase + 234.234f), 0, 0);

        Vector3 rotationSway = rotationBreathing + new Vector3(0, 0, -smoothedDirection.x * speed);

        Vector3 finalRotation = rotationSway;

        transform.localRotation = Quaternion.Euler(finalRotation);
    }

    public void SetPosition(Vector3 position)
    {
        targetPosition = position;

        // Smooth camera position
        currentPosition = Vector3.Lerp(currentPosition, targetPosition, Time.deltaTime * 15f);

        // Clamp camera position if to much lerping
        Vector3 positionDifference = CameraSlotTransform.position - currentPosition;
        positionDifference = Vector3.ClampMagnitude(positionDifference, 0.2f);

        currentPosition = CameraSlotTransform.position - positionDifference;


    }

    public void SetRotation(Vector3 rotation)
    {
        targetRotation = rotation;

    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
        smoothedDirection = Vector2.Lerp(smoothedDirection, direction, Time.deltaTime * 2f);
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetPhase(float phase)
    {
        this.phase = phase;
    }

    public void SetFootstepPhase(float phase)
    {
        this.footstepPhase = phase;
    }

    public void SetBreathingPhase(float phase)
    {
        this.breathingPhase = phase;
    }


    private float OctaveNoise(float input)
    {
        return Mathf.PerlinNoise(input, 14) + Mathf.PerlinNoise(input * 3, 43) * 0.2f + Mathf.PerlinNoise(input * 9, 23) * 0.05f;
    }

}