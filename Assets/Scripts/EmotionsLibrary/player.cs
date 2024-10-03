using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;  // Add this to handle scene events

public class Player : MonoBehaviour {
    //! Variables
    //? Public
    [Header("Events")]
    public GameEvent onAchievementTrigger;

    //? Private
    [Header("Movement & Physics")]
    [SerializeField] private float mouseSensitivity = 3f;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float mass = 1f;
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float acceleration = 20f;
    [SerializeField] private float worldBottomBounds = -100f;
    [SerializeField] private Transform cameraTransform;

    private CharacterController controller;
    private Vector3 velocity;
    private Vector2 look;

    private (Vector3, Quaternion) initialPositionandRotation;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction escAction;

    private bool isCursorLocked = true;

    //! Methods
    //? Unity
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        AssignInputActions();

        // Subscribe to the scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        initialPositionandRotation = (transform.position, transform.rotation);
    }

    private void Update() {
        UpdateGravity();
        UpdateMovement();
        UpdateLook();
        CheckBounds();
        CheckEscInput();
    }

    //? Public  
    public void ShowCursor(bool show) {
        isCursorLocked = !show;
        Cursor.lockState = show ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = show;
    }

    public void Teleport(Vector3 position, Quaternion rotation) {
        transform.position = position;
        Physics.SyncTransforms();
        look.x = rotation.eulerAngles.y;
        look.y = rotation.eulerAngles.z;
        velocity = Vector3.zero;
    }

    //? Private
    private void CheckBounds() {
        if (transform.position.y < worldBottomBounds) {
            var (position, rotation) = initialPositionandRotation;
            Teleport(position, rotation);
        }
    }

    private void UpdateGravity() {
        var gravity = Physics.gravity * mass * Time.deltaTime;
        velocity.y = controller.isGrounded ? -1f : velocity.y + gravity.y;
    }

    private Vector3 GetMovementInput() {
        var moveInput = moveAction.ReadValue<Vector2>();

        var input = new Vector3();
        input += transform.forward * moveInput.y;
        input += transform.right * moveInput.x;
        input = Vector3.ClampMagnitude(input, 1f);
        input *= movementSpeed;

        return input;
    }

    private void UpdateMovement() {
        var input = GetMovementInput();
        Debug.Log("Input: " + input);

        var factor = acceleration * Time.deltaTime;
        velocity.x = Mathf.Lerp(velocity.x, input.x, factor);
        velocity.z = Mathf.Lerp(velocity.z, input.z, factor);

        var jumpInput = jumpAction.ReadValue<float>();
        if (jumpInput > 0 && controller.isGrounded) {
            velocity.y += jumpSpeed;
        }

        controller.Move(velocity * Time.deltaTime);
    }

    private void UpdateLook() {
        if (isCursorLocked) {
            var lookInput = lookAction.ReadValue<Vector2>();
            look.x += lookInput.x * mouseSensitivity;
            look.y += lookInput.y * mouseSensitivity;

            look.y = Mathf.Clamp(look.y, -89f, 89f);

            cameraTransform.localRotation = Quaternion.Euler(-look.y, 0, 0);
            transform.localRotation = Quaternion.Euler(0, look.x, 0);
        }
    }

    private void CheckEscInput() {
        if (escAction.triggered) {
            ToggleCursorLock();
        }
    }

    private void ToggleCursorLock() {
        isCursorLocked = !isCursorLocked;
        Cursor.lockState = isCursorLocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !isCursorLocked;
    }

    //? New methods to handle reinitialization

    // This method assigns/rebinds input actions
    private void AssignInputActions() {
        moveAction = playerInput.actions["move"];
        lookAction = playerInput.actions["look"];
        jumpAction = playerInput.actions["jump"];
        escAction = playerInput.actions["esc"];

        // Debugging output to ensure actions are assigned
        Debug.Log("Input Actions Assigned: Move, Look, Jump, Escape");
    }

    // This method gets called whenever a new scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        AssignInputActions();  // Reassign input actions after the scene loads
        Debug.Log("Scene Loaded: " + scene.name + ". Input Actions Reassigned.");
    }

    // Remember to unsubscribe when the object is destroyed to avoid memory leaks
    private void OnDestroy() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
