// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Sword : MonoBehaviour
// {
//     [SerializeField] private Transform weaponCollider;

//     private PlayerControls playerControls;
//     private Animator myAnimator;
//     private PlayerController playerController;
//     private ActiveWeapon activeWeapon;

//     private void Awake() {
//         playerController = GetComponentInParent<PlayerController>();
//         activeWeapon = GetComponentInParent<ActiveWeapon>();
//         myAnimator = GetComponent<Animator>();
//         playerControls = new PlayerControls();
//     }

//     private void OnEnable() {
//         playerControls.Enable();
//     }


//     void Start(){
//         weaponCollider.gameObject.SetActive(false);

//         playerControls.Combat.Attack.started += _ => Attack();
//     }

//     private void Update() {
//         MouseFollowWithOffset();
//     }

//     private void MouseFollowWithOffset() {
//         Vector3 mousePos = Input.mousePosition;
//         Vector3 playerPos = Camera.main.WorldToScreenPoint(playerController.transform.position);

//         Vector3 offset = mousePos - playerPos;

//         float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

//         if (offset.x < 0) {
//             //activeWeapon.transform.rotation = Quaternion.Euler(0, 180, -angle);
//             activeWeapon.transform.rotation = Quaternion.Euler(0, 180, 180 - angle);
//             weaponCollider.transform.rotation = Quaternion.Euler(0, 180, 180 - angle);
//         } else {
//             activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
//             weaponCollider.transform.rotation = Quaternion.Euler(0, 0, angle);
//         }
//     }


//     // private void Attack() {
//     //     // myAnimator.SetTrigger("Attack");
//     //     // StartCoroutine(EnableColliderForDuration(0.2f));
//     //     if (gameObject.activeSelf) { // Memastikan objek Sword aktif
//     //         myAnimator.SetTrigger("Attack");
//     //         StartCoroutine(EnableColliderForDuration(0.2f));
//     //     }
//     // }
//     private void Attack() {
//         // Check if the object is still valid and active
//         if (this == null || !gameObject.activeInHierarchy) {
//             return;
//         }

//         myAnimator.SetTrigger("Attack");
//         StartCoroutine(EnableColliderForDuration(0.2f));
//     }

//     private IEnumerator EnableColliderForDuration(float duration)
//     {
//         // Enable weapon collider
//         weaponCollider.gameObject.SetActive(true);

//         // Wait for duration
//         yield return new WaitForSeconds(duration);

//         // Disable weapon collider
//         weaponCollider.gameObject.SetActive(false);
//     }
// }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private Transform weaponCollider;

    private PlayerControls playerControls;
    private Animator myAnimator;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;

    private void Awake() {
        playerController = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        myAnimator = GetComponent<Animator>();
        playerControls = new PlayerControls();
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void OnDisable() {
        playerControls.Disable();
    }

    private void OnDestroy() {
        playerControls.Disable();
    }

    void Start(){
        weaponCollider.gameObject.SetActive(false);

        playerControls.Combat.Attack.started += _ => Attack();
    }

    private void Update() {
        MouseFollowWithOffset();
    }

    private void MouseFollowWithOffset() {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerPos = Camera.main.WorldToScreenPoint(playerController.transform.position);

        Vector3 offset = mousePos - playerPos;

        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        if (offset.x < 0) {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 180, 180 - angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 180, 180 - angle);
        } else {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void Attack() {
        // Check if the object is still valid and active
        if (this == null || !gameObject.activeInHierarchy) {
            return;
        }

        myAnimator.SetTrigger("Attack");
        StartCoroutine(EnableColliderForDuration(0.2f));
    }

    private IEnumerator EnableColliderForDuration(float duration)
    {
        // Enable weapon collider
        weaponCollider.gameObject.SetActive(true);

        // Wait for duration
        yield return new WaitForSeconds(duration);

        // Disable weapon collider
        weaponCollider.gameObject.SetActive(false);
    }
}
