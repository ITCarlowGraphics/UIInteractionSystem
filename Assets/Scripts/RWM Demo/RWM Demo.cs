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
            isOn => Debug.Log($"Toggle is now: {isOn}"),    // Callback for toggle state change
            true,                                             // Rounded
            true,                                             // Drop shadow
            true,                                             // Slider background
            true,                                             // GreyOut
            new Vector2(0.0f, 50.0f),                        // Title above the toggle
            new Vector2(0.0f, -70.0f),                       // Status text below the toggle
            Color.black,                                     // On color
            Color.black                                        // Off color
        );
    }

    public void CreateToggleButton_2()
    {
        UIInteractionSystem.Instance.CreateToggle(
            GameObject.Find("Canvas"),                       // Canvas GameObject
            "Toggle",                                       // Name of root GameObject
            Resources.Load<Font>("Arial"),                  // Different Font
            18,                                              // Different Font size
            "#FF0000",                                      // Red text for label
            "Enabled",                                       // Different Text for ON state
            "Disabled",                                      // Different Text for OFF state
            new Vector2(250.0f, 60.0f),                     // Different Toggle background size
            new Vector2(30.0f, 30.0f),                      // Different toggle handle size
            new Vector2(100.0f, 100.0f),                    // Moved to a different position
            isOn => Debug.Log($"Second Toggle is now: {isOn}"), // Callback
            false,                                            // Non-rounded
            true,                                             // Drop shadow
            false,                                            // No slider background
            true,                                             // GreyOut
            new Vector2(-180.0f, 0.0f),                      // Title to the left
            new Vector2(180.0f, 0.0f),                       // Status text to the right
            new Color(1.0f, 0.65f, 0.0f),                    // On color (orange)
            Color.blue                                       // Off color
        );
    }

    public void CreateToggleButton_3()
    {
        UIInteractionSystem.Instance.CreateToggle(
            GameObject.Find("Canvas"),                       // Canvas GameObject
            "ToggleDemo_3",                                 // Name of root GameObject
            Resources.Load<Font>("Times New Roman"),        // Another unique font
            24,                                              // Different Font size
            "#0000FF",                                      // Blue text for the label
            "Active",                                         // Different text for ON state
            "Inactive",                                       // Different text for OFF state
            new Vector2(400.0f, 70.0f),                     // Another background size
            new Vector2(50.0f, 50.0f),                      // A different handle size
            new Vector2(-100.0f, -100.0f),                  // Moved to yet another position
            isOn => Debug.Log($"Third Toggle is now: {isOn}"), // Callback
            true,                                             // Rounded
            false,                                            // No drop shadow
            true,                                             // Slider background
            false,                                            // No greyOut
            new Vector2(0.0f, -60.0f),                        // Title above the toggle
            new Vector2(250.0f, 0.0f),                       // Status text to the right
            Color.green,                                      // On color
            Color.red                                       // Off color
        );
    }

    public void CreateSlider()
    {
        // First slider with unique parameters
        UIInteractionSystem.Instance.AddSliderRWM(
            GameObject.Find("Canvas"),                  // Parent Panel
            "VolumeSlider",                             // Slider name
            0,                                          // Min value
            100,                                        // Max value
            50,                                         // Initial value
            new Vector2(600, 40),                       // Slider size
            new Vector3(335, -155, 0),                  // Slider position
            "Volume",                                   // Label text
            new Vector3(200, -140, 0),                  // Label position
            Resources.Load<Sprite>("slideFill"),        // Fill sprite
            Resources.Load<Sprite>("slideHandle"),      // Handle sprite
            "{0:F1}%",                                  // Value text format
            Color.green                                 // Label color
        );
    }

    public void CreateSlider_2()
    {
        // Second slider with unique parameters
        UIInteractionSystem.Instance.AddSliderRWM(
            GameObject.Find("Canvas"),                   // Parent Panel
            "BrightnessSlider",                         // Slider name
            0,                                          // Min value
            100,                                        // Max value
            75,                                         // Initial value
            new Vector2(500, 40),                       // Slider size
            new Vector3(335, -225, 0),                  // Slider position
            "Brightness",                               // Label text
            new Vector3(200, -210, 0),                  // Label position
            Resources.Load<Sprite>("slideFill"),        // Fill sprite
            Resources.Load<Sprite>("slideHandle"),      // Handle sprite
            "{0:F1}%",                                  // Value text format
            Color.blue                                  // Label color
        );
    }
}

