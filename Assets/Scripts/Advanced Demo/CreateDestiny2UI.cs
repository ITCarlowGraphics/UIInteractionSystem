using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDestiny2UI : MonoBehaviour
{
    public void CreateDestiny2ui()
    {
        /* Top UI */
        UIInteractionSystem.Instance.CreatePanel(
            GameObject.Find("Canvas").GetComponent<Canvas>(),                       // canvas gameObject
            "Top UI",                                                               // the name of root(parent) gameObject
            "HairyBear515#2831\nLight Level 1832",                                  // text on panel
            Resources.Load<Font>("Nunito-Bold"),                                    // font usef for text
            40,                                                                     // text character size
            "#000000",                                                              // text color
            "#989e99",                                                              // front panel color
            "#000000",                                                              // back panel color
            new Vector2(1800.0f, 1000.0f));                                         // size of the whole panel
        UIInteractionSystem.Instance.CreateButton(
            GameObject.Find("Canvas").GetComponent<Canvas>(),                       // canvas gameObject
            "Top UI",                                                               // name of root(parent) gameObject
            "CHARACTER",                                                            // text appear on button
            Resources.Load<Font>("Nunito-Bold"),                                    // font used for button text
            25,                                                                     // button text character size
            "000000",                                                               // button text color
            "#D9D9D9",                                                              // color of button
            new Vector2(300.0f, 50.0f),                                             // button size
            new Vector2(500.0F, 400.0f),                                            // anchored position of button
            () => UIInteractionSystem.Instance.DisableScreen("Collection UI"),      // function will be executed when button OnClick
            () => UIInteractionSystem.Instance.EnableScreen("Character UI"));       // 2nd function will be executed when button OnClick
        UIInteractionSystem.Instance.CreateButton(
            GameObject.Find("Canvas").GetComponent<Canvas>(),                       // canvas gameObject
            "Top UI",                                                               // name of root(parent) gameObject
            "COLLECTION",                                                           // text appear on button
            Resources.Load<Font>("Nunito-Bold"),                                    // font used for button text
            25,                                                                     // button text character size
            "000000",                                                               // button text color
            "#D9D9D9",                                                              // color of button
            new Vector2(300.0f, 50.0f),                                             // button size
            new Vector2(-500.0F, 400.0f),                                           // anchored position of button
            () => UIInteractionSystem.Instance.DisableScreen("Character UI"),       // function will be executed when button OnClick
            () => UIInteractionSystem.Instance.EnableScreen("Collection UI"));      // 2nd function will be executed when button OnClick
        /* Character UI */
        /* Left Side Weapon Section */
        string[] weaponStrings = { "Subclass", "Kinetic\nWeapon", "Energy\nWeapon", "Power\nWeapon", "Pet" };
        for (int i = 0; i < weaponStrings.Length; i++)
        {
            UIInteractionSystem.Instance.CreateButton(
            GameObject.Find("Canvas").GetComponent<Canvas>(),                       // canvas gameObject
            "Character UI",                                                         // name of root(parent) gameObject
            weaponStrings[i],                                                       // text appear on button
            Resources.Load<Font>("Nunito-Bold"),                                    // font used for button text
            20,                                                                     // button text character size
            "000000",                                                               // button text color
            "#D9D9D9",                                                              // color of button
            new Vector2(100.0f, 100.0f),                                            // button size
            new Vector2(-350.0f, 300.0f - i * 125.0f));                             // anchored position of button
        }
        /* Right Side Armour Section */
        string[] armourStrings = { "Helmet", "Gauntlet", "Chest", "Leg", "Class Item" };
        for (int i = 0; i < armourStrings.Length; i++)
        {
            UIInteractionSystem.Instance.CreateButton(
            GameObject.Find("Canvas").GetComponent<Canvas>(),                       // canvas gameObject
            "Character UI",                                                         // name of root(parent) gameObject
            armourStrings[i],                                                       // text appear on button
            Resources.Load<Font>("Nunito-Bold"),                                    // font used for button text
            20,                                                                     // button text character size
            "000000",                                                               // button text color
            "#D9D9D9",                                                              // color of button
            new Vector2(100.0f, 100.0f),                                            // button size
            new Vector2(350.0f, 300.0f - i * 125.0f));                              // anchored position of button
        }

        /* Collection UI */
        /* Item Section */
        UIInteractionSystem.Instance.CreateText(
            GameObject.Find("Canvas").GetComponent<Canvas>(),        // canvas gameObject
            "Collection UI",                                         // name of root(parent) gameObject
            "ITEMS",                                                 // string of text gonna be created
            Resources.Load<Font>("Nunito-Bold"),                     // font for text
            50,                                                      // character size for text
            "#FFFFFF",                                               // text color
            new Vector2(-450.0f, 300.0f),                            // text offset position
            new Vector2(200.0f, 200.0f),                             // text RectTransform size
            TextAnchor.MiddleCenter);                                // text alignment
        string[] itemButtons = { "EXOTIC", "WEAPONS", "ARMOUR", "GHOST SHELLS", "VEHICLES", "FLAIR" };
        for (int i = 0; i < itemButtons.Length; i++)
        {
            UIInteractionSystem.Instance.CreateButton(
            GameObject.Find("Canvas").GetComponent<Canvas>(),                       // canvas gameObject
            "Collection UI",                                                        // name of root(parent) gameObject
            itemButtons[i],                                                         // text appear on button
            Resources.Load<Font>("Nunito-Bold"),                                    // font used for button text
            20,                                                                     // button text character size
            "000000",                                                               // button text color
            "#D9D9D9",                                                              // color of button
            new Vector2(200.0f, 300.0f),                                            // button size
            new Vector2(-700.0f + (i % 3) * 250.0f, 100.0f - (i / 3) * 350.0f));    // anchored position of button
        }
        /* Recently Discovered Section */
        UIInteractionSystem.Instance.CreateText(
            GameObject.Find("Canvas").GetComponent<Canvas>(),       // canvas gameObject
            "Collection UI",                                        // name of root(parent) gameObject
            "RECENTLY DISCOVERED",                                  // string of text gonna be created
            Resources.Load<Font>("Nunito-Bold"),                    // font for text
            50,                                                     // character size for text
            "#FFFFFF",                                              // text color
            new Vector2(450.0f, 300.0f),                            // text offset position
            new Vector2(500.0f, 200.0f),                            // text RectTransform size
            TextAnchor.MiddleCenter);                               // text alignment
        UIInteractionSystem.Instance.CreateImage(
            GameObject.Find("Canvas").GetComponent<Canvas>(),                               // canvas gameObject
            "Collection UI",                                                                // name of root(parent) gameObject
            "TOTC_Title",                                                                   // name of image gameObject
            new Vector2(450.0f, 200.0f),                                                    // image offset position
            new Vector2(200.0f, 100.0f),                                                    // image size
            Resources.Load<Sprite>("TOTC_Title"));                                          // image resource
        /* Badge Section */
        UIInteractionSystem.Instance.CreateText(
            GameObject.Find("Canvas").GetComponent<Canvas>(),       // canvas gameObject
            "Collection UI",                                        // name of root(parent) gameObject
            "BADGES",                                               // string of text gonna be created
            Resources.Load<Font>("Nunito-Bold"),                    // font for text
            50,                                                     // character size for text
            "#FFFFFF",                                              // text color
            new Vector2(450.0f, 0.0f),                              // text offset position
            new Vector2(500.0f, 200.0f),                            // text RectTransform size
            TextAnchor.MiddleCenter);                               // text alignment
        UIInteractionSystem.Instance.CreateImage(
            GameObject.Find("Canvas").GetComponent<Canvas>(),                               // canvas gameObject
            "Collection UI",                                                                // name of root(parent) gameObject
            "Haotai Badge",                                                                 // name of image gameObject
            new Vector2(450.0f, -150.0f),                                                   // image offset position
            new Vector2(100.0f, 100.0f),                                                    // image size
            Resources.Load<Sprite>("Haotai_Xiong-Head"));                                   // image resource


        /*******************************
         *** register root gameObject **
         *******************************/
        UIInteractionSystem.Instance.RegisterRootGameObject(
            "Top UI",                                                               // dictionary string of specific screen
            GameObject.Find("Top UI"));                                             // name of root gameObject
        UIInteractionSystem.Instance.RegisterRootGameObject(
            "Character UI",                                                         // dictionary string of specific screen
            GameObject.Find("Character UI"));                                       // name of root gameObject
        UIInteractionSystem.Instance.RegisterRootGameObject(
            "Collection UI",                                                        // dictionary string of specific screen
            GameObject.Find("Collection UI"));                                      // name of root gameObject

        UIInteractionSystem.Instance.DisableScreen("Collection UI");
    }
}
