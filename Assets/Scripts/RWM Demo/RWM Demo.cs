using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RWMDemo : MonoBehaviour
{
    public void CreateToggleButton()
    {
        UIInteractionSystem.Instance.CreateToggle(
            GameObject.Find("Canvas"),                       // Canvas GameObject
            "ToggleDemo",                                   // Name of root GameObject
            Resources.Load<Font>("Nunito-Bold"),            // Font for the label
            20,                                              // Font size
            "#000000",                                      // Label text color
            "ON",                                            // Text to show when toggle is on
            "OFF",                                           // Text to show when toggle is off
            new Vector2(300.0f, 50.0f),                     // Toggle background size
            new Vector2(40.0f, 40.0f),                      // Toggle handle size
            new Vector2(0.0f, 0.0f),                        // Toggle position
            isOn => Debug.Log($"Toggle is now: {isOn}")    // Callback for toggle state change
        );
    }
}
