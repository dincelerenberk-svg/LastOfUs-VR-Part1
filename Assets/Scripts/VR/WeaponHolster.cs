using UnityEngine;
using System.Collections;

namespace LastOfUsVR
{
    public class WeaponHolster : MonoBehaviour
    {
        [Header("Holster References")]
        [SerializeField] private Transform holsterPoint;
        [SerializeField] private Transform backAnchor;
        [SerializeField] private Transform weaponAnchor;

        [Header("Weapon State")]
        [SerializeField] private GameObject equippedWeapon;
        [SerializeField] private bool isDrawn;

        [Header("Animation")]
        [SerializeField] private float drawSpeed = 10f;

        public bool IsDrawn => isDrawn;

        private void Reset()
        {
            if (holsterPoint == null)
                holsterPoint = transform;
        }

        public void DrawWeapon()
        {
            if (equippedWeapon == null || isDrawn)
                return;

            StartCoroutine(AnimateWeaponTo(weaponAnchor.position, weaponAnchor.rotation, true));
        }

        public void StoreWeapon()
        {
            if (equippedWeapon == null || !isDrawn)
                return;

            StartCoroutine(AnimateWeaponTo(backAnchor != null ? backAnchor.position : holsterPoint.position,
                backAnchor != null ? backAnchor.rotation : holsterPoint.rotation,
                false));
        }

        public void SetWeapon(GameObject weapon)
        {
            equippedWeapon = weapon;
            if (equippedWeapon != null)
            {
                equippedWeapon.transform.SetParent(holsterPoint != null ? holsterPoint : transform, false);
                equippedWeapon.transform.localPosition = Vector3.zero;
                equippedWeapon.transform.localRotation = Quaternion.identity;
            }
        }

        private IEnumerator AnimateWeaponTo(Vector3 targetPosition, Quaternion targetRotation, bool drawState)
        {
            if (equippedWeapon == null)
                yield break;

            while (Vector3.Distance(equippedWeapon.transform.position, targetPosition) > 0.02f)
            {
                equippedWeapon.transform.position = Vector3.Lerp(
                    equippedWeapon.transform.position,
                    targetPosition,
                    Time.deltaTime * drawSpeed);

                equippedWeapon.transform.rotation = Quaternion.Slerp(
                    equippedWeapon.transform.rotation,
                    targetRotation,
                    Time.deltaTime * drawSpeed);

                yield return null;
            }

            equippedWeapon.transform.position = targetPosition;
            equippedWeapon.transform.rotation = targetRotation;
            isDrawn = drawState;
        }
    }
}
