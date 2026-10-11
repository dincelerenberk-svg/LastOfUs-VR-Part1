using UnityEngine;
using UnityEngine.InputSystem;

namespace LastOfUsVR
{
    public class VRPlayerRig : MonoBehaviour
    {
        [Header("Rig Setup")]
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Transform cameraRigRoot;
        [SerializeField] private Transform headAnchor;

        [Header("Movement")]
        [SerializeField] private InputActionReference moveAction;
        [SerializeField] private float moveSpeed = 2.5f;
        [SerializeField] private float turnSpeed = 60f;

        [Header("Body")]
        [SerializeField] private float minHeight = 1.5f;
        [SerializeField] private float maxHeight = 1.9f;

        private Vector3 moveDirection;

        private void Reset()
        {
            characterController = GetComponent<CharacterController>();
            cameraRigRoot = transform;
            if (Camera.main != null)
            {
                headAnchor = Camera.main.transform;
            }
        }

        private void Update()
        {
            if (characterController == null)
                return;

            UpdateHeight();
            UpdateMovement();
        }

        private void UpdateHeight()
        {
            if (headAnchor == null)
                return;

            float height = Mathf.Clamp(headAnchor.localPosition.y, minHeight, maxHeight);
            characterController.height = height;
            Vector3 center = new Vector3(0f, height * 0.5f, 0f);
            characterController.center = center;
        }

        private void UpdateMovement()
        {
            if (moveAction == null)
                return;

            Vector2 moveInput = moveAction.action.ReadValue<Vector2>();
            Vector3 forward = Camera.main.transform.forward;
            Vector3 right = Camera.main.transform.right;

            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            Vector3 desiredMove = (forward * moveInput.y + right * moveInput.x) * moveSpeed;
            characterController.Move(desiredMove * Time.deltaTime);

            if (moveInput.sqrMagnitude > 0.01f)
            {
                float targetAngle = Mathf.Atan2(moveInput.x, moveInput.y) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);
            }
        }
    }
}
