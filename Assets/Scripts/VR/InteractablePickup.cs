using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace LastOfUsVR
{
    [RequireComponent(typeof(XRGrabInteractable))]
    public class InteractablePickup : MonoBehaviour
    {
        [Header("Interaction")]
        [SerializeField] private string itemName = "Item";
        [SerializeField] private bool isWeapon = false;
        [SerializeField] private bool canBeStored = true;

        [Header("Visual State")]
        [SerializeField] private Color highlightColor = new Color(0.3f, 1f, 0.8f, 1f);

        private Renderer itemRenderer;
        private Color originalColor;
        private XRGrabInteractable grabInteractable;

        private void Awake()
        {
            grabInteractable = GetComponent<XRGrabInteractable>();
            itemRenderer = GetComponentInChildren<Renderer>();
            if (itemRenderer != null)
                originalColor = itemRenderer.material.color;
        }

        private void OnEnable()
        {
            if (grabInteractable != null)
            {
                grabInteractable.hoverEntered.AddListener(OnHoverEntered);
                grabInteractable.hoverExited.AddListener(OnHoverExited);
            }
        }

        private void OnDisable()
        {
            if (grabInteractable != null)
            {
                grabInteractable.hoverEntered.RemoveListener(OnHoverEntered);
                grabInteractable.hoverExited.RemoveListener(OnHoverExited);
            }
        }

        private void OnHoverEntered(HoverEnterEventArgs args)
        {
            if (itemRenderer != null)
                itemRenderer.material.color = highlightColor;
        }

        private void OnHoverExited(HoverExitEventArgs args)
        {
            if (itemRenderer != null)
                itemRenderer.material.color = originalColor;
        }

        public string GetItemName() => itemName;
        public bool IsWeapon => isWeapon;
        public bool CanBeStored => canBeStored;
    }
}
