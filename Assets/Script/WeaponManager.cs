using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public Weapon[] weapons; // Array of all available weapons
    public WeaponUIManager weaponUIManager;

    private int currentWeaponIndex = 0; // Index of the currently selected weapon

    

    void Start()
    {
        // Ensure there is a valid current weapon
        if (weapons.Length > 0)
        {
            currentWeaponIndex = 0; // Start with the first weapon
            UpdateWeaponUI();
        }
    }

    void Update()
    {
        HandleWeaponSwitching();
        HandleWeaponFiring();
    }

    // Method to handle weapon switching (e.g., using number keys or scroll wheel)
    private void HandleWeaponSwitching()
    {
        bool weaponSwitched = false; // Track if a weapon switch occurred

        if (Input.GetKeyDown(KeyCode.Alpha1) && weapons.Length > 0)
        {
            currentWeaponIndex = 0; // Switch to the first weapon
            weaponSwitched = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && weapons.Length > 1)
        {
            currentWeaponIndex = 1; // Switch to the second weapon
            weaponSwitched = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && weapons.Length > 2)
        {
            currentWeaponIndex = 2; // Switch to the second weapon
            weaponSwitched = true;
        }

        // Update the UI if the weapon was switched
        if (weaponSwitched)
        {
            UpdateWeaponUI();
        }

    }

    // Method to handle weapon firing
    private void HandleWeaponFiring()
    {
        if (weapons.Length > 0 && weapons[currentWeaponIndex] != null)
        {
            // Check if the fire button is pressed and call the Fire method of the current weapon
            if (Input.GetButton("Fire1"))
            {
                weapons[currentWeaponIndex].Fire();
            }
        }
    }

    // Method to update the weapon UI
    private void UpdateWeaponUI()
    {
        if (weaponUIManager != null)
        {
            weaponUIManager.SelectWeapon(currentWeaponIndex); // Update the UI to highlight the selected weapon
        }
    }
}
