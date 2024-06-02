using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawnPoint;
    [SerializeField] private float arrowSpeed = 10f;

    private PlayerControls playerControls;
    private Animator myAnimator;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        myAnimator = GetComponent<Animator>();
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
        playerControls.Combat.Shoot.performed += _ => ShootArrow();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        AimBowAtMouse();
    }

    private void AimBowAtMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerPos = Camera.main.WorldToScreenPoint(playerController.transform.position);

        Vector3 offset = mousePos - playerPos;
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        if (offset.x < 0)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 180, 180 - angle);
        }
        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void ShootArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
        arrow.transform.Rotate(0, 0, -90);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.velocity = arrowSpawnPoint.right * arrowSpeed;
    }
}
