using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonController : MonoBehaviour
{

    #region Variables

    // ====================== PARAMETERS ======================

    [Header("Camera Settings")]
    [SerializeField] private Vector2 CameraSensitivity;
    [SerializeField] private Vector2 CameraSmoothingAmount;
    [Range(0f, 1f)][SerializeField] private float CameraJitterSpeed;
    [Range(0.5f, 2f)][SerializeField] private float FootstepSpeed;

    [Header("Movement Speed")]
    [SerializeField] private float WalkSpeed = 1f;
    [SerializeField] private float SprintSpeed = 2f;
    [SerializeField] private float CrouchSpeed = 0.5f;
    [Range(0f, 1f)][SerializeField] private float BackwardsSpeedMultiplier = 0.5f;
    [Range(0f, 1f)][SerializeField] private float SidewaysSpeedMultiplier = 0.75f;
    [Range(0f, 1f)][SerializeField] private float AirSpeedMultiplier = 0.5f;

    [Space, Header("Sprint Settings")]
    [SerializeField] private float SprintTransitionDuration = 1f;
    [SerializeField] private AnimationCurve SprintTransitionCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    [Space, Header("Crouch Settings")]
    [Range(0.4f, 0.8f)][SerializeField] private float CrouchHeightMultiplier = 0.5f;
    [SerializeField] private float CrouchTransitionDuration = 1f;
    [SerializeField] private AnimationCurve CrouchTransitionCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    [Space, Header("Jump Settings")]
    [SerializeField] private float MinJumpHeight = 0.5f;
    [SerializeField] private float MaxJumpHeight = 1f;
    [SerializeField] private int NumJumps = 1;
    [SerializeField] private bool AutoJump = false;

    [Space, Header("Slope Settings")]
    [SerializeField] private float MaxSlopeAngle = 45;

    [Space, Header("Stair Settings")]
    [SerializeField] private float MaxStepSize = 0.5f;

    [Space, Header("Landing Settings")]
    [Range(0.05f, 0.5f)][SerializeField] private float MinLandAmount = 0.1f;
    [Range(0.2f, 0.9f)][SerializeField] private float MaxLandAmount = 0.6f;
    [SerializeField] private float landTimer = 0.5f;
    [SerializeField] private float landDuration = 1f;
    [SerializeField] private AnimationCurve landCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    [Space, Header("Ground Settings")]
    [SerializeField] private LayerMask GroundLayer;
    [Range(0f, 1f)][SerializeField] private float RayLength = 0.1f;

    [Space, Header("Wall Settings")]
    [SerializeField] private LayerMask WallLayers;


    // ===================== COMPONENT REF =====================

    [Space, Header("Component References")]
    [SerializeField] private Transform PlayerTransform;
    [SerializeField] private Rigidbody PlayerRigidBody;

    [SerializeField] private Transform CameraSlotTransform;
    [SerializeField] private Transform CameraHolderTransform;
    [SerializeField] private Transform CameraTransform;
    [SerializeField] private Camera PlayerCamera;
    [SerializeField] private CameraController CameraController;

    //[SerializeField] private PlayerAudioController PlayerAudioController;


    // ===================== PLAYER INPUT =====================

    private DesktopInput playerControls;

    // Camera
    private Vector2 cameraInput;


    // Movement
    private Vector2 movementInput;

    private bool isMoving;
    private bool sprintClicked;
    private bool sprintReleased;
    private bool sprintPerformed;
    private bool crouchClicked;
    private bool crouchReleased;
    private bool crouchPerformed;

    private bool jumpClicked;



    // =================== INTERNAL VALUES ====================

    public enum MovementState
    {
        Standing,
        Walking,
        Sprinting,
        Crouching,
        InAir
    }

    private MovementState movementState;

    private Vector2 smoothedMovementInput;

    private Vector3 currentRotation;
    private Vector3 targetRotation;

    private Vector3 moveDirection;
    private Vector3 smoothedMoveDirection;

    private Vector3 moveVector;

    private float currentSpeed;
    private float targetSpeed;

    private bool isGrounded;
    private bool previouslyGrounded;
    private bool isRoofed;
    private bool wallInFront;

    private float defaultHeight;
    private float sprintHeight;
    private float crouchHeight;
    private float currentHeight;

    private float phase;
    private float footstepPhase;
    private float breathingSpeed;
    private float breathingPhase;

    private bool footstepTriggered;
    private int currentFootstepSide;

    private bool breathingTriggered;
    private bool breathingSide;

    #endregion

    #region Methods

    // ====================== BUILD IN ======================

    private void Awake()
    {
        InitializeVariables();
        SetupInput();
        CameraController.SetupComponents();

        //PlayerAudioController.SetupComponents();
    }

    private void Start()
    {

    }

    private void Update()
    {
        // Handle Input
        HandleInput();

        CheckIfGrounded();
        CheckIfRoofed();
        CheckFrontWall();
        CheckForStep();

        ProcessInput();

        // Check Available Movement Options

        // Calculate Movement
        UpdateMovementDirection();
        UpdateMovementSpeed();
        HandleLanding();


        // Handle Crouching, Jumping

        // Update Camera
        UpdateCamera();
        ApplyCameraTransform();

        UpdateAudio();

        previouslyGrounded = isGrounded;
    }

    private void FixedUpdate()
    {
        UpdatePhase();
        UpdateMovement();
    }

    // ====================== INITIALIZATION ======================

    private void InitializeVariables()
    {
        movementState = MovementState.Standing;

        smoothedMovementInput = new Vector2(0, 0);

        currentRotation = transform.eulerAngles;
        targetRotation = currentRotation;

        moveDirection = new Vector2(0, 0);
        smoothedMoveDirection = new Vector2(0, 0);

        moveVector = new Vector2(0, 0);

        currentSpeed = 0;
        targetSpeed = 0;

        isGrounded = false;
        previouslyGrounded = false;

        isRoofed = false;

        wallInFront = false;

        defaultHeight = 1;
        sprintHeight = 0.9f;
        crouchHeight = 0.5f;
        currentHeight = 1;

        currentFootstepSide = 1;
    }


    // ====================== INPUT HANDLING ======================

    private void SetupInput()
    {
        playerControls = new DesktopInput();
        playerControls.Enable();

        playerControls.Player.Run.started += StartSprinting;
        playerControls.Player.Run.canceled += StopSprinting;

        playerControls.Player.Crouch.started += StartCrouching;
        playerControls.Player.Crouch.canceled += HandleStopCrouching;

        playerControls.Player.Jump.performed += Perform_Jump;

        playerControls.Player.ChangeSpatializer.performed += SwitchSpatializer;

    }

    private void OnDisable()
    {
        playerControls.Player.Run.started -= StartSprinting;
        playerControls.Player.Run.canceled -= StopSprinting;

        playerControls.Player.Crouch.started -= StartCrouching;
        playerControls.Player.Crouch.canceled -= HandleStopCrouching;

        playerControls.Player.Jump.performed -= Perform_Jump;

        playerControls.Player.ChangeSpatializer.performed -= SwitchSpatializer;
    }


    private void HandleInput()
    {
        cameraInput = playerControls.Player.Look.ReadValue<Vector2>();

        movementInput = playerControls.Player.Move.ReadValue<Vector2>();
        isMoving = movementInput.x != 0 || movementInput.y != 0;
    }

    private void ProcessInput()
    {
        targetRotation.x -= cameraInput.y * CameraSensitivity.y;
        targetRotation.y += cameraInput.x * CameraSensitivity.x;
        targetRotation.x = Mathf.Clamp(targetRotation.x, -90, 90);

        currentRotation = Vector3.Lerp(currentRotation, targetRotation, Time.deltaTime * 50f);

        if (isMoving)
        {
            if (sprintPerformed && CanSprint()) movementState = MovementState.Sprinting;
            else if (movementState != MovementState.Crouching) movementState = MovementState.Walking;
        }
        else
        {
            if (movementState != MovementState.Crouching) movementState = MovementState.Standing;
        }
    }

    void UpdatePhase()
    {
        breathingSpeed = Mathf.Lerp(breathingSpeed, (0.7f + currentSpeed * 2f), Time.fixedDeltaTime * 0.4f);
        breathingPhase += breathingSpeed * Time.fixedDeltaTime;
        if (wallInFront || !isGrounded || !isMoving)
        {
            footstepPhase = 0;
            return;
        }
        phase += currentSpeed * 1.5f * Time.fixedDeltaTime * CameraJitterSpeed;
        footstepPhase += currentSpeed * 3 * Time.fixedDeltaTime * FootstepSpeed;

    }


    // ====================== EVENTS ======================

    private void StartSprinting(InputAction.CallbackContext context)
    {
        if (wallInFront) return;
        sprintPerformed = true;
    }

    private void StopSprinting(InputAction.CallbackContext context)
    {
        sprintPerformed = false;
    }


    private void StartCrouching(InputAction.CallbackContext context)
    {
        if (!isGrounded) return;
        crouchPerformed = true;
        movementState = MovementState.Crouching;
        transform.localScale = Vector3.one * crouchHeight;
        currentHeight = crouchHeight;
    }

    private void HandleStopCrouching(InputAction.CallbackContext context)
    {
        crouchPerformed = false;
        if (isRoofed) return;
        StopCrouching();
    }

    private void StopCrouching()
    {
        movementState = MovementState.Walking;
        transform.localScale = Vector3.one * defaultHeight;
        currentHeight = defaultHeight;
    }

    private void Perform_Jump(InputAction.CallbackContext context)
    {
        if (movementState == MovementState.Crouching) return;
        HandleJump();
    }


    // ====================== CHECKS ======================

    private void CheckIfGrounded()
    {
        RaycastHit hit;
        isGrounded = Physics.SphereCast(PlayerTransform.position + Vector3.up * 0.3f, 0.5f, Vector3.down, out hit, currentHeight / 1f + 0.5f, GroundLayer);
    }

    private void CheckIfRoofed()
    {
        RaycastHit hit;
        isRoofed = Physics.SphereCast(PlayerTransform.position + Vector3.down * 0.1f, 0.5f, Vector3.up, out hit, currentHeight / 1f + (defaultHeight - crouchHeight), GroundLayer);
        if (!isRoofed && !crouchPerformed) StopCrouching();
    }

    private void CheckFrontWall()
    {
        RaycastHit hit;
        wallInFront = Physics.SphereCast(PlayerTransform.position - transform.forward * 0.1f, 0.5f, transform.forward, out hit, 1, GroundLayer);
    }

    private void CheckForStep()
    {
        RaycastHit hitBottom;
        RaycastHit hitMid;
        RaycastHit hitTop;
        bool checkBottom = Physics.Raycast(PlayerTransform.position - Vector3.up * (currentHeight - 0.05f), moveDirection, out hitBottom, 0.5f, GroundLayer);
        bool checkMid = Physics.Raycast(PlayerTransform.position - Vector3.up * (currentHeight - 0.1f), moveDirection, out hitMid, 0.5f, GroundLayer);
        bool checkTop = Physics.Raycast(PlayerTransform.position - Vector3.up * (currentHeight - 0.6f), moveDirection, out hitTop, 0.5f, GroundLayer);

        if (checkBottom && !checkTop && Mathf.Abs(hitBottom.distance - hitMid.distance) < 0.05f) transform.position += moveDirection * 0.2f + transform.up * 0.4f;
    }

    private bool CanSprint()
    {
        return true;
    }

    private bool CanJump()
    {
        return true;
    }

    private bool CanCrouch()
    {
        return true;
    }

    private bool CanUnCrouch()
    {
        return true;
    }


    // ====================== MOVEMENT ======================

    private void UpdateMovementDirection()
    {
        moveDirection = transform.forward * movementInput.y + transform.right * movementInput.x * SidewaysSpeedMultiplier;

        smoothedMoveDirection = Vector3.Lerp(smoothedMoveDirection, moveDirection, Time.deltaTime * 10f);

        transform.localRotation = Quaternion.Euler(0, currentRotation.y, 0);
    }

    private void UpdateMovementSpeed()
    {
        switch (movementState)
        {
            case MovementState.Standing:
                targetSpeed = 0;
                break;
            case MovementState.Walking:
                targetSpeed = WalkSpeed;
                break;
            case MovementState.Sprinting:
                targetSpeed = SprintSpeed;
                break;
            case MovementState.Crouching:
                targetSpeed = CrouchSpeed;
                break;

        }

        if (movementInput.y < 0) targetSpeed *= BackwardsSpeedMultiplier;

        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * 5f);
    }

    private void UpdateMovement()
    {
        float forceAmount = currentSpeed * 1000 * AirSpeedMultiplier;
        PlayerRigidBody.AddForce(smoothedMoveDirection * forceAmount, ForceMode.Force);

        Vector3 clampedVelocity = new Vector3(PlayerRigidBody.velocity.x, 0, PlayerRigidBody.velocity.z);
        clampedVelocity = Vector3.ClampMagnitude(clampedVelocity, currentSpeed);
        clampedVelocity.y = PlayerRigidBody.velocity.y;

        PlayerRigidBody.velocity = clampedVelocity;
    }

    // ====================== SPRINTING ======================

    // Start Sprinting

    // Sprinting

    // End Sprinting


    // ====================== CROUCHING ======================

    // Start Crouching

    // Crouching

    // End Crouching


    // ====================== JUMPING ======================

    private void HandleJump()
    {
        if (!isGrounded) return;

        PlayerRigidBody.AddForce(transform.up * 65f * Mathf.Sqrt(MaxJumpHeight), ForceMode.Impulse);

        PlayJump();
    }

    // Start Landing
    private void HandleLanding()
    {
        if (!previouslyGrounded && isGrounded)
        {
            PlayLanding(1);
        }
    }

    // Landing Routine

    // ====================== SMOOTHING ======================

    private void SmoothInput()
    {

    }

    private void SmoothDirection()
    {

    }

    private void SmoothMovement()
    {

    }

    // ====================== CAMERA ======================

    private void UpdateCamera()
    {
        CameraController.SetPosition(CameraSlotTransform.position);
        CameraController.SetRotation(currentRotation);
        CameraController.SetDirection(movementInput);
        CameraController.SetSpeed(currentSpeed);
        CameraController.SetPhase(phase);
        CameraController.SetFootstepPhase(footstepPhase);
        CameraController.SetBreathingPhase(breathingPhase);
    }

    private void ApplyCameraTransform()
    {
        CameraController.ApplyCameraTransform();
    }


    // ====================== AUDIO ======================

    private void UpdateAudio()
    {
        float footstepState = Mathf.Sin(footstepPhase * 2);
        if (footstepState > 0 && !footstepTriggered && !jumpClicked)
        {
            footstepTriggered = true;
            Invoke("PlayFootstep", Random.Range(0, 0.05f));
            currentFootstepSide *= -1;
        }

        if (footstepState < 0) footstepTriggered = false;


        float breathingState = Mathf.Sin(breathingPhase * 2);
        if (breathingState > 0 && !breathingTriggered)
        {
            breathingTriggered = true;
            //PlayerAudioController.PlayBreathing(breathingSide, breathingSpeed * 0.1f);
            breathingSide = !breathingSide;
        }

        if (breathingState < 0) breathingTriggered = false;
    }

    private void PlayFootstep()
    {
        //PlayerAudioController.PlayFootstep(currentFootstepSide, movementState);

    }

    private void PlayJump()
    {
        //PlayerAudioController.PlayJump();
    }

    private void PlayLanding(float intensity)
    {
        //PlayerAudioController.PlayLanding(intensity);
    }

    #endregion


    private void SwitchSpatializer(InputAction.CallbackContext context)
    {
        //GameManager.Instance.SwitchSpatializer();
        //AudioComparisonManager comp = FindObjectOfType<AudioComparisonManager>();
        //if (comp != null) comp.SwitchSpatializer();
    }

}