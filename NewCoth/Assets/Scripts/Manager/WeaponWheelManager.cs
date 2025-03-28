using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponWheelManager : MonoBehaviour
{
    public GameObject weaponWheelCanvas;
    public GameObject orbGO;

    public GameObject[] weapons; // Array to hold your weapon GameObjects
    public Image[] weaponIcons; // UI Icons for the weapon wheel
    public KeyCode weaponWheelKey = KeyCode.Tab; // Key to open weapon wheel

    private int currentWeaponIndex = 0;
    private bool isWeaponWheelActive = false;

    void Start()
    {
        InitializeWeapons();
    }

    void Update()
    {
        HandleWeaponWheelInput();
        HandleWeaponSwitchInput();
    }

    private void InitializeWeapons()
    {
        // Initialize all weapons and set only the first weapon active
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].SetActive(i == currentWeaponIndex);
        }
    }

    private void HandleWeaponWheelInput()
    {
        // Toggle weapon wheel UI
        if (Input.GetKeyDown(weaponWheelKey))
        {
            ToggleWeaponWheel();
        }
    }

    private void ToggleWeaponWheel()
    {
        isWeaponWheelActive = !isWeaponWheelActive;
        weaponWheelCanvas.SetActive(isWeaponWheelActive);
        if (!isWeaponWheelActive)
        {
            orbGO.SetActive(true);
        }
        else
        {
            //orbGO.SetActive(false);
            StartCoroutine(Enum_SelectOrb());
        }

        UpdateWeaponWheelUI();
    }

    IEnumerator Enum_SelectOrb()
    {
        yield return new WaitForSeconds(6f);
        orbGO.SetActive(false);
    }

    private void HandleWeaponSwitchInput()
    {
        if (isWeaponWheelActive)
        {
            // Use mouse scroll or number keys to navigate the weapon wheel
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll > 0f)
            {
                SwitchWeapon((currentWeaponIndex + 1) % weapons.Length);
            }
            else if (scroll < 0f)
            {
                SwitchWeapon((currentWeaponIndex - 1 + weapons.Length) % weapons.Length);
            }

            // Alternatively, use number keys (1-4) to select a weapon
            for (int i = 0; i < weapons.Length; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                {
                    SwitchWeapon(i);
                }
            }
        }
    }

    private void SwitchWeapon(int newIndex)
    {
        if (newIndex != currentWeaponIndex && newIndex >= 0 && newIndex < weapons.Length)
        {
            // Deactivate current weapon and activate the selected one
            weapons[currentWeaponIndex].SetActive(false);
            weapons[newIndex].SetActive(true);
            currentWeaponIndex = newIndex;
            UpdateWeaponWheelUI();
        }
    }

    private void UpdateWeaponWheelUI()
    {
        if (weaponIcons.Length == weapons.Length)
        {
            for (int i = 0; i < weaponIcons.Length; i++)
            {
                weaponIcons[i].color = (i == currentWeaponIndex) ? Color.white : Color.gray; // Highlight selected weapon
            }
        }
        else
        {
            Debug.LogError("Weapon icons array length does not match weapons array length.");
        }
    }
}
