using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    // [SerializeField] private GameObject Sword;
    // [SerializeField] private GameObject Bow;

    // private PlayerControls playerControls;
    // private int activeWeaponIndex = 1;

    // private void Awake() {
    //     playerControls = new PlayerControls();
    // }

    // private void OnEnable() {
    //     playerControls.Enable();
    //     playerControls.Combat.ChangeSword.performed += _ => SwitchWeapon(1);
    //     playerControls.Combat.ChangeBow.performed += _ => SwitchWeapon(2);
    // }

    // private void OnDisable() {
    //     playerControls.Disable();
    // }

    // private void Start() {
    //     // Ensure the sword is active and the bow is inactive at the start
    //     UpdateWeaponState();
    // }

    // private void SwitchWeapon(int weaponIndex) {
    //     if (activeWeaponIndex != weaponIndex) {
    //         activeWeaponIndex = weaponIndex;
    //         UpdateWeaponState();
    //     }
    // }

    // private void UpdateWeaponState() {
    //     if (activeWeaponIndex == 1) {
    //         Sword.SetActive(true);
    //         Bow.SetActive(false);
    //     } else if (activeWeaponIndex == 2) {
    //         Sword.SetActive(false);
    //         Bow.SetActive(true);
    //     }
    // }
    public GameObject sword;
    // [SerializeField] private GameObject player;
    // private DamageSource dampak;
    public GameObject bow;
    
    private PlayerControls playerControls;
    
    void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Enable();

        // Menghubungkan aksi switch weapon ke metode SwitchWeapon
        playerControls.Combat.ChangeSword.performed += _ => SwitchToSword();
        playerControls.Combat.ChangeBow.performed += _ => SwitchToBow();
    }

    void SwitchToSword()
    {
        sword.SetActive(true);
        bow.SetActive(false);
    }

    void SwitchToBow()
    {
        // dampak = player.GetComponent<DamageSource>();
        // dampak.damageAmount = (float)0.2; 
        sword.SetActive(false);
        bow.SetActive(true);
    }

    void OnDisable()
    {
        playerControls.Disable();
    }
}
