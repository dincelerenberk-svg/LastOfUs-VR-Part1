using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace LastOfUsVR
{
    public class VRHandController : MonoBehaviour
    {
        [Header("Hand References")]
        [SerializeField] private Transform handRoot;
        [SerializeField] private XRBaseControllerInteractor controllerInteractor;

        [Header("Grab Settings")]
        [SerializeField] private float grabRadius = 0.12f;
        [SerializeField] private LayerMask grabLayerMask;

        [Header("Smoothing")]
        [SerializeField] private float positionLerpSpeed = 20f;
        [SerializeField] private float rotationLerpSpeed = 20f;

        private Collider[] nearbyColliders = new Collider[8];

        private void Reset()
        {
            handRoot = transform;
            controllerInteractor = GetComponent<XRBaseControllerInteractor>();
        }

        private void Update()
        {
            if (handRoot == null)
                return;

            if (controllerInteractor == null)
            {
                SyncHandVisuals();
                return;
            }

            SyncHandVisuals();
        }

        private void SyncHandVisuals()
        {
            if (handRoot == null)
                return;

            Vector3 targetPosition = transform.position;
            Quaternion targetRotation = transform.rotation;

            handRoot.position = Vector3.Lerp(handRoot.position, targetPosition, Time.deltaTime * positionLerpSpeed);
            handRoot.rotation = Quaternion.Slerp(handRoot.rotation, targetRotation, Time.deltaTime * rotationLerpSpeed);
        }

        public void AttemptGrab()
        {
            if (controllerInteractor == null)
                return;

            int overlapCount = Physics.OverlapSphereNonAlloc(transform.position, grabRadius, nearbyColliders, grabLayerMask);
            if (overlapCount <= 0)
                return;

            Collider nearest = GetNearestCollider();
            if (nearest == null)
                return;

            XRGrabInteractable grabInteractable = nearest.GetComponentInParent<XRGrabInteractable>();
            if (grabInteractable == null)
                return;

            if (grabInteractable.isSelected)
                return;

            controllerInteractor.Select(grabInteractable);
        }

        private Collider GetNearestCollider()
        {
            Collider best = null;
            float bestDistance = float.MaxValue;

            foreach (var collider in nearbyColliders)
            {
                if (collider == null)
                    continue;

                float dist = Vector3.Distance(transform.position, collider.transform.position);
                if (dist < bestDistance)
                {
                    bestDistance = dist;
                    best = collider;
                }
            }

            return best;
        }
    }
}
