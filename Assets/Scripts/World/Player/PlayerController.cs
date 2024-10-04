using System.Collections.Generic;
using UnityEngine;

namespace WhizKid.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Camera playerCamera;
        private float rotationX = 0;
        private float lookSpeed = 2f;
        private float lookXLimit = 45f;

        private enum ControlMode
        {
            /// <summary>
            /// Up moves the character forward, left and right turn the character gradually and down moves the character backwards
            /// </summary>
            Tank,
            /// <summary>
            /// Character freely moves in the chosen direction from the perspective of the camera
            /// </summary>
            Direct
        }

        #region Private Serialized Fields

        [SerializeField] private float m_moveSpeed = 2;
        [SerializeField] private float m_turnSpeed = 200;
        [SerializeField] private float m_jumpForce = 4;

        [SerializeField] private Animator m_animator;
        [SerializeField] private Rigidbody m_rigidBody;
        [SerializeField] private GameObject pauseMenu;

        [SerializeField] private ControlMode m_controlMode = ControlMode.Tank;

        #endregion
        #region Private Fields

        private float m_currentV = 0;
        private float m_currentH = 0;

        private readonly float m_interpolation = 10;
        private readonly float m_walkScale = 0.33f;
        private readonly float m_backwardsWalkScale = 0.16f;
        private readonly float m_backwardRunScale = 0.66f;

        private bool m_wasGrounded;
        private Vector3 m_currentDirection = Vector3.zero;

        [Header("Jump Settings")]
        [SerializeField] private float m_jumpTimeStamp = 0;
        [SerializeField] private float m_minJumpInterval = 0.25f;
        private bool m_jumpInput = false;

        private bool m_isGrounded;
        private List<Collider> m_collisions = new List<Collider>();

        #endregion
        #region MonoBehaviour CallBacks

        private void Awake()
        {
            if (!m_animator) { m_animator = gameObject.GetComponent<Animator>(); }
            if (!m_rigidBody) { m_rigidBody = gameObject.GetComponent<Rigidbody>(); }
            Cursor.visible = true;

            // Additional Rigidbody setup to prevent bouncing
            if (m_rigidBody != null)
            {
                // Freeze rotation to prevent unintended physics-based rotation
                m_rigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

                // Optionally, adjust drag values here if not set via Inspector
                // m_rigidBody.drag = 1f;
                // m_rigidBody.angularDrag = 1f;

                // Set collision detection mode to Continuous for better collision handling
                m_rigidBody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            }
        }

        private void Start()
        {
            if (pauseMenu == null) pauseMenu = GameObject.Find("PauseMenuManager");
        }

        private void Update()
        {
            if (pauseMenu.GetComponent<PauseMenu>().isPaused)
            {
                // handle pause state
                return;
            }

            if (!m_jumpInput && Input.GetKey(KeyCode.Space))
            {
                m_jumpInput = true;
            }
            CameraMovement();
        }

        private void FixedUpdate()
        {
            m_animator.SetBool("Grounded", m_isGrounded);

            switch (m_controlMode)
            {
                case ControlMode.Direct:
                    DirectUpdate();
                    break;

                case ControlMode.Tank:
                    TankUpdate();
                    break;

                default:
                    Debug.LogError("Unsupported state");
                    break;
            }

            // Optional: Clamp the Rigidbody's velocity to prevent excessive speeds
            if (m_rigidBody != null)
            {
                Vector3 clampedVelocity = m_rigidBody.velocity;
                clampedVelocity = Vector3.ClampMagnitude(clampedVelocity, 10f); // Adjust max speed as needed
                m_rigidBody.velocity = clampedVelocity;
            }

            m_wasGrounded = m_isGrounded;
            m_jumpInput = false;
        }

        #endregion
        #region MonoBehaviour Collision Callbacks

        private void OnCollisionEnter(Collision collision)
        {
            ContactPoint[] contactPoints = collision.contacts;
            bool validSurfaceNormal = false;

            for (int i = 0; i < contactPoints.Length; i++)
            {
                if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
                {
                    if (!m_collisions.Contains(collision.collider))
                    {
                        m_collisions.Add(collision.collider);
                    }
                    validSurfaceNormal = true;
                }
            }

            if (validSurfaceNormal)
            {
                m_isGrounded = true;

                // Mitigate bouncing by resetting vertical velocity upon landing
                if (m_rigidBody != null)
                {
                    Vector3 velocity = m_rigidBody.velocity;
                    velocity.y = 0;
                    m_rigidBody.velocity = velocity;
                }
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            ContactPoint[] contactPoints = collision.contacts;
            bool validSurfaceNormal = false;
            for (int i = 0; i < contactPoints.Length; i++)
            {
                if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
                {
                    validSurfaceNormal = true;
                    break;
                }
            }

            if (validSurfaceNormal)
            {
                m_isGrounded = true;
                if (!m_collisions.Contains(collision.collider))
                {
                    m_collisions.Add(collision.collider);
                }
            }
            else
            {
                if (m_collisions.Contains(collision.collider))
                {
                    m_collisions.Remove(collision.collider);
                }
                if (m_collisions.Count == 0) { m_isGrounded = false; }
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (m_collisions.Contains(collision.collider))
            {
                m_collisions.Remove(collision.collider);
            }
            if (m_collisions.Count == 0) { m_isGrounded = false; }
        }

        #endregion
        #region Custom methods

        private void CameraMovement()
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
        private void TankUpdate()
        {
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");

            bool walk = Input.GetKey(KeyCode.LeftShift);

            if (v < 0)
            {
                if (walk) { v *= m_backwardsWalkScale; }
                else { v *= m_backwardRunScale; }
            }
            else if (walk)
            {
                v *= m_walkScale;
            }

            m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
            m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

            Vector3 movement = transform.forward * m_currentV * m_moveSpeed * Time.deltaTime;
            transform.position += movement;
            transform.Rotate(0, m_currentH * m_turnSpeed * Time.deltaTime, 0);

            m_animator.SetFloat("MoveSpeed", m_currentV);

            JumpingAndLanding();
        }

        private void DirectUpdate()
        {
            float v = Input.GetAxis("Vertical");
            float h = Input.GetAxis("Horizontal");

            Transform camera = Camera.main.transform;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                v *= m_walkScale;
                h *= m_walkScale;
            }

            m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
            m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

            Vector3 direction = camera.forward * m_currentV + camera.right * m_currentH;

            float directionLength = direction.magnitude;
            direction.y = 0;
            direction = direction.normalized * directionLength;

            if (direction != Vector3.zero)
            {
                m_currentDirection = Vector3.Slerp(m_currentDirection, direction, Time.deltaTime * m_interpolation);

                transform.rotation = Quaternion.LookRotation(m_currentDirection);
                transform.position += m_currentDirection * m_moveSpeed * Time.deltaTime;

                m_animator.SetFloat("MoveSpeed", direction.magnitude);
            }

            JumpingAndLanding();
        }

        private void JumpingAndLanding()
        {
            bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;

            if (jumpCooldownOver && m_isGrounded && m_jumpInput)
            {
                m_jumpTimeStamp = Time.time;
                m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
                m_isGrounded = false; // Prevent multiple jumps
            }

            if (!m_wasGrounded && m_isGrounded)
            {
                m_animator.SetTrigger("Land");
            }

            if (!m_isGrounded && m_wasGrounded)
            {
                m_animator.SetTrigger("Jump");
            }
        }

        #endregion
    }
}
