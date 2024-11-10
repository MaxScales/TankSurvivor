using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReticleController : MonoBehaviour
{
  
    public Image reticle; // The reticle UI element

    void Update()
    {
        if (reticle == null) return;

        // Get the current mouse position in screen space
        Vector3 cursorPosition = Input.mousePosition;

        // Update the reticle's position to follow the cursor
        reticle.rectTransform.position = cursorPosition;
    }
}
