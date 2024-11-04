using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMainMenu : MonoBehaviour
{
    public void CreateMainmenu()
    {
        /* Start Screen*/
        UIInteractionSystem.Instance.CreatePanel(
            GameObject.Find("Canvas").GetComponent<Canvas>(),                               // canvas gameObject
            "Start Screen",                                                                 // the name of root(parent) gameObject
            "",                                                                             // text on panel
            Resources.Load<Font>("Nunito-Bold"),                                            // font usef for text
            0,                                                                              // text character size
            "#FFFFFF",                                                                      // text color
            "#EBFF00",                                                                      // front panel color
            "#FFFFFF",                                                                      // back panel color
            new Vector2(500.0f, 800.0f));                                                   // size of the whole panel
        UIInteractionSystem.Instance.CreateButton(
            GameObject.Find("Canvas").GetComponent<Canvas>(),                               // canvas gameObject
            "Start Screen",                                                                 // name of root(parent) gameObject
            "Press To Play",                                                                // text appear on button
            Resources.Load<Font>("Nunito-Bold"),                                            // font used for button text
            20,                                                                             // button text character size
            "000000",                                                                       // button text color
            "#D9D9D9",                                                                      // color of button
            new Vector2(300.0f, 50.0f),                                                     // button size
            new Vector2(0, -300.0f),                                                        // anchored position of button
            () => UIInteractionSystem.Instance.DisableScreen("Start Screen"),               // function will be executed when button OnClick
            () => UIInteractionSystem.Instance.EnableScreen("Menu Screen"));                // 2nd function will be executed when button OnClick
        UIInteractionSystem.Instance.CreateImage(
            GameObject.Find("Canvas").GetComponent<Canvas>(),                               // canvas gameObject
            "Start Screen",                                                                 // name of root(parent) gameObject
            "TOTC_Title",                                                                   // name of image gameObject
            new Vector2(0.0f, 0.0f),                                                        // image offset position
            new Vector2(200.0f, 100.0f),                                                    // image size
            Resources.Load<Sprite>("TOTC_Title"));                                          // image resource


        /* Menu Screen */
        UIInteractionSystem.Instance.CreatePanel(
            GameObject.Find("Canvas").GetComponent<Canvas>(),                               // canvas gameObject
            "Menu Screen",                                                                  // the name of root(parent) gameObject
            "",                                                                             // text on panel
            Resources.Load<Font>("Nunito-Bold"),                                            // font usef for text
            0,                                                                              // text character size
            "#FFFFFF",                                                                      // text color
            "#EBFF00",                                                                      // front panel color
            "#FFFFFF",                                                                      // back panel color
            new Vector2(500.0f, 800.0f));                                                   // size of the whole panel

        string[] buttonText1 = { "Setting", "Choose\nGameMode" };
        for (int i = 0; i < buttonText1.Length; i++)
        {
            UIInteractionSystem.Instance.CreateButton(
                GameObject.Find("Canvas").GetComponent<Canvas>(),                           // canvas gameObject
                "Menu Screen",                                                              // name of root(parent) gameObject
                buttonText1[i],                                                             // text appear on button
                Resources.Load<Font>("Nunito-Bold"),                                        // font used for button text
                25,                                                                         // button text character size
                "000000",                                                                   // button text color
                "#D9D9D9",                                                                  // color of button
                new Vector2(300.0f, 50.0f),                                                 // button size
                new Vector2(0, -300.0f + i * 75.0f),                                        // anchored position of button
                () => UIInteractionSystem.Instance.DisableScreen("Menu Screen"),            // function will be executed when button OnClick
                () => UIInteractionSystem.Instance.EnableScreen("GameModes Screen"));       // 2nd function will be executed when button OnClick
        }
        UIInteractionSystem.Instance.CreateImage(
            GameObject.Find("Canvas").GetComponent<Canvas>(),                               // canvas gameObject
            "Menu Screen",                                                                  // name of root(parent) gameObject
            "TOTC_Title",                                                                   // name of image gameObject
            new Vector2(0.0f, 300.0f),                                                      // image offset position
            new Vector2(200.0f, 100.0f),                                                    // image size
            Resources.Load<Sprite>("TOTC_Title"));                                          // image resource

        /* GameModes Screen */
        UIInteractionSystem.Instance.CreatePanel(
            GameObject.Find("Canvas").GetComponent<Canvas>(),                               // canvas gameObject
            "GameModes Screen",                                                             // the name of root(parent) gameObject
            "",                                                                             // text on panel
            Resources.Load<Font>("Nunito-Bold"),                                            // font usef for text
            0,                                                                              // text character size
            "#FFFFFF",                                                                      // text color
            "#EBFF00",                                                                      // front panel color
            "#FFFFFF",                                                                      // back panel color
            new Vector2(500.0f, 800.0f));                                                   // size of the whole panel

        string[] buttonText2 = { "Companion", "AR", "Local" };
        for (int i = 0; i < buttonText2.Length; i++)
        {
            UIInteractionSystem.Instance.CreateButton(
                GameObject.Find("Canvas").GetComponent<Canvas>(),                           // canvas gameObject
                "GameModes Screen",                                                         // name of root(parent) gameObject
                buttonText2[i],                                                             // text appear on button
                Resources.Load<Font>("Nunito-Bold"),                                        // font used for button text
                25,                                                                         // button text character size
                "000000",                                                                   // button text color
                "#D9D9D9",                                                                  // color of button
                new Vector2(300.0f, 50.0f),                                                 // button size
                new Vector2(0, -300.0f + i * 75.0f),                                        // anchored position of button
                () => UIInteractionSystem.Instance.DisableScreen("GameModes Screen"));      // function will be executed when button OnClick
        }
        UIInteractionSystem.Instance.CreateImage(
            GameObject.Find("Canvas").GetComponent<Canvas>(),                               // canvas gameObject
            "GameModes Screen",                                                             // name of root(parent) gameObject
            "TOTC_Title",                                                                   // name of image gameObject
            new Vector2(0.0f, 300.0f),                                                      // image offset position
            new Vector2(200.0f, 100.0f),                                                    // image size
            Resources.Load<Sprite>("TOTC_Title"));                                          // image resource

        /*******************************
         *** register root gameObject **
         *******************************/
        UIInteractionSystem.Instance.RegisterRootGameObject(
            "Start Screen",                                         // dictionary string of specific screen
            GameObject.Find("Start Screen"));                       // name of root gameObject
        UIInteractionSystem.Instance.RegisterRootGameObject(
            "Menu Screen",                                          // dictionary string of specific screen
            GameObject.Find("Menu Screen"));                        // name of root gameObject
        UIInteractionSystem.Instance.RegisterRootGameObject(
            "GameModes Screen",                                     // dictionary string of specific screen
            GameObject.Find("GameModes Screen"));                   // name of root gameObject

        UIInteractionSystem.Instance.DisableScreen("Menu Screen");
        UIInteractionSystem.Instance.DisableScreen("GameModes Screen");
    }
}
