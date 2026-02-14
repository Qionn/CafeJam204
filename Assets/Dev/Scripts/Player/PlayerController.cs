using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Transform m_FollowTarget;
    [SerializeField] private float m_Sensitivity = 1f;
    [SerializeField] private float m_MinVerticalAngle = -40f;
    [SerializeField] private float m_MaxVerticalAngle = 70f;

    // Input related
    private PlayerInput m_PlayerInput;
    private InputAction m_LookMoveAction;
    private InputAction m_LookAction;

    // Camera related
    private Vector2 m_LookInput;

    // =========== Unity Functions ==========

    private void Awake()
    {
        m_PlayerInput = GetComponent<PlayerInput>();

        m_LookMoveAction = m_PlayerInput.actions["LookMove"];
        m_LookAction = m_PlayerInput.actions["Look"];

        // Disable looking by default
        m_LookMoveAction.Disable();
    }
    
    private void OnEnable()
    {
        m_LookMoveAction.performed += OnLookMove;
        m_LookMoveAction.canceled += OnLookMove;

        m_LookAction.performed += OnLookStarted;
        m_LookAction.canceled += OnLookStopped;
    }

    private void OnDisable()
    {
        m_LookMoveAction.performed -= OnLookMove;
        m_LookMoveAction.canceled -= OnLookMove;

        m_LookAction.performed -= OnLookStarted;
        m_LookAction.canceled -= OnLookStopped;
    }

    private void LateUpdate()
    {
        // Rotate camera on lateupdate to ensure we received
        // the input updates this frame.
        RotateFollowTarget();
    }

    // ============ Input Events ============

    private void OnLookMove(InputAction.CallbackContext ctx)
    {
        m_LookInput = ctx.ReadValue<Vector2>();
    }

    private void OnLookStarted(InputAction.CallbackContext ctx)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        m_LookMoveAction.Enable();
    }

    private void OnLookStopped(InputAction.CallbackContext ctx)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        m_LookMoveAction.Disable();
    }

    // ========= Internal functions =========

    private void RotateFollowTarget()
    {
        Vector3 angles = m_FollowTarget.localEulerAngles;

        // Convert to a signed angle [0;360] -> [-180;180]
        float pitch = angles.x > 180.0f ? angles.x - 360.0f : angles.x;
        float yaw = angles.y;

        yaw += m_LookInput.x * m_Sensitivity;
        pitch -= m_LookInput.y * m_Sensitivity;

        pitch = Mathf.Clamp(pitch, m_MinVerticalAngle, m_MaxVerticalAngle);

        m_FollowTarget.localRotation = Quaternion.Euler(pitch, yaw, 0);
    }
}
