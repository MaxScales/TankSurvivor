using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUIManager : MonoBehaviour
{
    public Text[] weaponLabels; // Array of Text components for each weapon label
    public Color selectedColor = Color.white; // Color for the selected weapon
    public Color normalColor = Color.black; // Color for unselected weapons
    public int selectedFontSize = 18; // Font size for the selected weapon
    public int normalFontSize = 14; // Font size for unselected weapons

    // Method to update the weapon labels and highlight the selected one
    public void SelectWeapon(int weaponIndex)
    {
        if (weaponIndex < 0 || weaponIndex >= weaponLabels.Length) return;

        // Loop through all weapon labels to update their appearance
        for (int i = 0; i < weaponLabels.Length; i++)
        {
            if (i == weaponIndex)
            {
                // Highlight the selected weapon label
                weaponLabels[i].color = selectedColor;
                weaponLabels[i].fontSize = selectedFontSize;
            }
            else
            {
                // Reset the unselected weapon labels to their normal appearance
                weaponLabels[i].color = normalColor;
                weaponLabels[i].fontSize = normalFontSize;
            }
        }
    }
}
