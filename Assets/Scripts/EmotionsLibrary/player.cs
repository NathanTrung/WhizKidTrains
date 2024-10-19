// #define UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EmotionsLibrary.Player
{
    public class Player : MonoBehaviour
    {
        #region Private Serialize Fields

        [Header("Events")]
        public GameEvent onAchievementTrigger;
        [SerializeField] private EmotionsMenuHandler menuHandler;

        [Header("Movement & Physics")]
        [SerializeField] private float mouseSensitivity = 3f;
        [SerializeField] private float movementSpeed = 5f;
        [SerializeField] private float mass = 1f;
        [SerializeField] private float jumpSpeed = 5f;
        [SerializeField] private float acceleration = 20f;
        [SerializeField] private float worldBottomBounds = -100f;
        [SerializeField] private Transform cameraTransform;

        #endregion
        #region Private Fields

        private CharacterController controller;
        private Vector3 velocity;
        private Vector2 look;

        private (Vector3, Quaternion) initialPositionandRotation;

        private PlayerInput playerInput;
        private InputAction moveAction;
        private InputAction lookAction;
        private InputAction jumpAction;
        private InputAction escAction;

        private bool isPaused = false;

        #endregion

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            playerInput = GetComponent<PlayerInput>();
            moveAction = playerInput.actions["move"];
            lookAction = playerInput.actions["look"];
            jumpAction = playerInput.actions["jump"];
            escAction = playerInput.actions["esc"];
        }

        private void Start()
        {
#if UNITY_EDITOR
            var gameWindow = EditorWindow
                .GetWindow(typeof(EditorWindow).Assembly.GetType("UnityEditor.GameView"));
            gameWindow.Focus();
            gameWindow.SendEvent(new Event 
            {
                button = 0,
                clickCount = 1,
                type = EventType.MouseDown,
                mousePosition = gameWindow.rootVisualElement.contentRect.center
            });
#endif
            Cursor.lockState = CursorLockMode.Locked;
            initialPositionandRotation = (transform.position, transform.rotation);
        }

        private void Update()
        {
            UpdateGravity();
            UpdateMovement();
            UpdateLook();
            CheckBounds();
            HandlePause();
        }

        public void Teleport(Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            Physics.SyncTransforms();
            look.x = rotation.eulerAngles.y;
            look.y = rotation.eulerAngles.z;
            velocity = Vector3.zero;
        }

        private void CheckBounds()
        {
            if (transform.position.y < worldBottomBounds)
            {
                var (position, rotation) = initialPositionandRotation;
                Teleport(position, rotation);
            }
        }

        #region Movements
        private void UpdateGravity()
        {
            var gravity = Physics.gravity * mass * Time.deltaTime;
            velocity.y = controller.isGrounded ? -1f : velocity.y + gravity.y;
        }

        private void UpdateLook()
        {
            var lookInput = lookAction.ReadValue<Vector2>();
            look.x += lookInput.x * mouseSensitivity;
            look.y += lookInput.y * mouseSensitivity;

            look.y = Mathf.Clamp(look.y, -89f, 89f);

            cameraTransform.localRotation = Quaternion.Euler(-look.y, 0, 0);
            transform.localRotation = Quaternion.Euler(0, look.x, 0);
        }

        private void UpdateMovement()
        {
            var input = GetMovementInput();

            var factor = acceleration * Time.deltaTime;
            velocity.x = Mathf.Lerp(velocity.x, input.x, factor);
            velocity.z = Mathf.Lerp(velocity.z, input.z, factor);

            var jumpInput = jumpAction.ReadValue<float>();
            if (jumpInput > 0 && controller.isGrounded)
            {
                velocity.y += jumpSpeed;
            }

            controller.Move(velocity * Time.deltaTime);
        }

        private Vector3 GetMovementInput()
        {
            var moveInput = moveAction.ReadValue<Vector2>();

            var input = new Vector3();
            input += transform.forward * moveInput.y;
            input += transform.right * moveInput.x;
            input = Vector3.ClampMagnitude(input, 1f);
            input *= movementSpeed;

            return input;
        }

        #endregion

        private void HandlePause()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;

            if (isPaused)
            {
                isPaused = false;
                menuHandler.Pause();
            }
            else
            {
                isPaused = true;
                menuHandler.Unpause();
            }
        }
    }
}

