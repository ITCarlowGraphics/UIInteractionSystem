using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public float testValue = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CreateDialogBox();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CreatePartyMenu();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CreateLeaderboard();
        }


        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log(testValue);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            UIInteractionSystem.Instance.EnableScreen("Dialog Box");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            UIInteractionSystem.Instance.DisableScreen("Dialog Box");
        }
        else if (Input.GetKeyDown (KeyCode.T))
        {
            UIInteractionSystem.Instance.ToggleScreen("Dialog Box");
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            UIInteractionSystem.Instance.DestroyScreen("Dialog Box");
        }

        testValue = UIInteractionSystem.Instance.GetSliderValue(
            "Test Slider",                                              // name of slider gameObject
            "Dialog Box",                                               // dictionary string of specific screen
            testValue);                                                 // the value gonna be changed
    }

    public void ButtonClicked()
    {
        Debug.Log("Button Clicked!");
    }

    void CreateDialogBox()
    {
        /*******************************
         ********* Create Panel ********
         *******************************/
        UIInteractionSystem.Instance.CreatePanel(
            GameObject.Find("Canvas").GetComponent<Canvas>(),       // canvas gameObject
            "Dialog Box",                                           // the name of root(parent) gameObject
            "What doesn't fail you\nIt's just not finished yet!",   // text on panel
            Resources.Load<Font>("Nunito-Bold"),                    // font usef for text
            30,                                                     // text character size
            "#FFFFFF",                                              // text color
            "#000000",                                              // front panel color
            "#8031D0",                                              // back panel color
            new Vector2(500.0f, 500.0f));                           // size of the whole panel

        /*******************************
         ******** Create Button ********
         *******************************/
        UIInteractionSystem.Instance.CreateButton(
            GameObject.Find("Canvas").GetComponent<Canvas>(),       // canvas gameObject
            "Dialog Box",                                           // name of root(parent) gameObject
            "Click Me!",                                            // text appear on button
            Resources.Load<Font>("Nunito-Bold"),                    // font used for button text
            25,                                                     // button text character size
            "FFFFFF",                                               // button text color
            "#528120",                                              // color of button
            new Vector2(300, 100),                                  // button size
            new Vector2(0, -100),                                   // anchored position of button
            () => UIInteractionSystem.Instance.DestroyScreen("Dialog Box"));// function will be executed when button OnClick

        /*******************************
         *** register root gameObject **
         *******************************/
        UIInteractionSystem.Instance.RegisterRootGameObject(
            "Dialog Box",                                           // dictionary string of specific screen
            GameObject.Find("Dialog Box"));                         // name of root gameObject
    }

    void CreatePartyMenu()
    {
        /*******************************
         ********* Create Panel ********
         *******************************/
        UIInteractionSystem.Instance.CreatePanel(
            GameObject.Find("Canvas").GetComponent<Canvas>(),                   // canvas gameObject
            "Party Mode",                                                       // the name of root(parent) gameObject
            "",                                                                 // text on panel
            Resources.Load<Font>("Nunito-Bold"),                                // font usef for text
            0,                                                                  // text character size
            "#FFFFFF",                                                          // text color
            "#FFFFFF",                                                          // front panel color
            "#FCDA00",                                                          // back panel color
            new Vector2(500.0f, 800.0f));                                       // size of the whole panel

        /*******************************
         ****** Join Party button ******
         *******************************/
        UIInteractionSystem.Instance.CreateButton(
            GameObject.Find("Canvas").GetComponent<Canvas>(),                   // canvas gameObject
            "Party Mode",                                                       // name of root(parent) gameObject
            "Join Party",                                                       // text appear on button
            Resources.Load<Font>("Nunito-Bold"),                                // font used for button text
            25,                                                                 // button text character size
            "000000",                                                           // button text color
            "#FCDA00",                                                          // color of button
            new Vector2(300, 100),                                              // button size
            new Vector2(0, 100),                                                // anchored position of button
            () => UIInteractionSystem.Instance.DestroyScreen("Party Mode"));    // function will be executed when button OnClick

        /*******************************
         ***** Create Party button *****
         *******************************/
        UIInteractionSystem.Instance.CreateButton(
            GameObject.Find("Canvas").GetComponent<Canvas>(),                   // canvas gameObject
            "Party Mode",                                                       // name of root(parent) gameObject
            "Create Party",                                                     // text appear on button
            Resources.Load<Font>("Nunito-Bold"),                                // font used for button text
            25,                                                                 // button text character size
            "000000",                                                           // button text color
            "#FCDA00",                                                          // color of button
            new Vector2(300, 100),                                              // button size
            new Vector2(0, -100),                                               // anchored position of button
            () => UIInteractionSystem.Instance.DestroyScreen("Party Mode"));    // function will be executed when button OnClick

        /*******************************
         *** Create TOTC Title Image ***
         *******************************/
        UIInteractionSystem.Instance.CreateImage(
            GameObject.Find("Canvas").GetComponent<Canvas>(),                   // canvas gameObject
            "Party Mode",                                                       // name of root(parent) gameObject
            "TOTC_Title",                                                       // name of image gameObject
            new Vector2(0.0f, 300.0f),                                          // image offset position
            new Vector2(200.0f, 100.0f),                                        // image size
            Resources.Load<Sprite>("TOTC_Title"));                              // image resource

        /*******************************
         *** register root gameObject **
         *******************************/
        UIInteractionSystem.Instance.RegisterRootGameObject(
            "Party Mode",                                                       // dictionary string of specific screen
            GameObject.Find("Party Mode"));                                     // name of root gameObject
    }

    void CreateLeaderboard()
    {
        /*******************************
         ********* Create Panel ********
         *******************************/
        UIInteractionSystem.Instance.CreatePanel(
            GameObject.Find("Canvas").GetComponent<Canvas>(),                       // canvas gameObject
            "Leaderboard Menu",                                                     // the name of root(parent) gameObject
            "",                                                                     // text on panel
            Resources.Load<Font>("Nunito-Bold"),                                    // font usef for text
            0,                                                                      // text character size
            "#FFFFFF",                                                              // text color
            "#FCDA02",                                                              // front panel color
            "#FFFFFF",                                                              // back panel color
            new Vector2(500.0f, 800.0f));                                           // size of the whole panel

        /*********************************
         * Create Leaderboard Title Text *
         *********************************/
        UIInteractionSystem.Instance.CreateText(
            GameObject.Find("Canvas").GetComponent<Canvas>(),                       // canvas gameObject
            "Leaderboard Menu",                                                     // name of root(parent) gameObject
            "LEADER BOARD",                                                         // string of text gonna be created
            Resources.Load<Font>("Nunito-Bold"),                                    // font for text
            50,                                                                     // character size for text
            "#000000",                                                              // text color
            new Vector2(0.0f, 300.0f),                                              // text offset position
            new Vector2(500.0f, 500.0f),                                            // text RectTransform size
            TextAnchor.MiddleCenter);                                               // text alignment

        /*********************************
         ** Create 4 Buttons for Player **
         *********************************/
        string[] names = { "Harry1", "Harry2", "Harry3", "Harry4" };
        for (int i = 0; i < 4; i++)
        {
            UIInteractionSystem.Instance.CreateButton(
                GameObject.Find("Canvas").GetComponent<Canvas>(),                           // canvas gameObject
                "Leaderboard Menu",                                                         // name of root(parent) gameObject
                names[i],                                                                   // text appear on button
                Resources.Load<Font>("Nunito-Bold"),                                        // font used for button text
                25,                                                                         // button text character size
                "000000",                                                                   // button text color
                "#FEF2A7",                                                                  // color of button
                new Vector2(300.0f, 50.0f),                                                 // button size
                new Vector2(0, 200.0f - i * 100.0f),                                        // anchored position of button
                () => UIInteractionSystem.Instance.DestroyScreen("Leaderboard Menu"));      // function will be executed when button OnClick
        }

        /**************************************
         ** Create 4 Images for Player Avtar **
         **************************************/
        for (int i = 0; i < 4; i++)
        {
            UIInteractionSystem.Instance.CreateImage(
                GameObject.Find("Canvas").GetComponent<Canvas>(),       // canvas gameObject
                "Leaderboard Menu",                                     // name of root(parent) gameObject
                "Haotai's Huge Head",                                   // name of image gameObject
                new Vector2(-150.0f, 200.0f - i * 100.0f),              // image offset position
                new Vector2(50.0f, 50.0f),                              // image size
                Resources.Load<Sprite>("Haotai_Xiong-Head"));           // image resource
        }

        /*******************************
         ********* Create Text *********
         *******************************/
        int[] scores = { 63, 61, 52, 46 };
        for (int i = 0; i < 4; i++)
        {
            UIInteractionSystem.Instance.CreateText(
                GameObject.Find("Canvas").GetComponent<Canvas>(),       // canvas gameObject
                "Leaderboard Menu",                                     // name of root(parent) gameObject
                scores[i].ToString(),                                   // string of text gonna be created
                Resources.Load<Font>("Nunito-Bold"),                    // font for text
                50,                                                     // character size for text
                "#212121",                                              // text color
                new Vector2(125.0f, 200.0f - i * 100.0f),               // text offset position
                new Vector2(100.0f, 100.0f),                            // text RectTransform size
                TextAnchor.MiddleCenter);                               // text alignment
        }

        /*******************************
         ***** Create Trophy Image *****
         *******************************/
        UIInteractionSystem.Instance.CreateImage(
            GameObject.Find("Canvas").GetComponent<Canvas>(),                   // canvas gameObject
            "Leaderboard Menu",                                                 // name of root(parent) gameObject
            "Trophy",                                                           // name of image gameObject
            new Vector2(0.0f, -250.0f),                                          // image offset position
            new Vector2(200.0f, 150.0f),                                        // image size
            Resources.Load<Sprite>("Trophy"));                                  // image resource

        /*******************************
         *** register root gameObject **
         *******************************/
        UIInteractionSystem.Instance.RegisterRootGameObject(
            "Leaderboard Menu",                                                     // dictionary string of specific screen
            GameObject.Find("Leaderboard Menu"));                                   // name of root gameObject
    }
}

///*******************************
// ******** Create Slider ********
// *******************************/
//UIInteractionSystem.Instance.CreateSlider(
//    GameObject.Find("Canvas").GetComponent<Canvas>(),       // canvas gameObject
//    "Dialog Box",                                           // name of root(parent) gameObject
//    0.0f,                                                   // min value for slider
//    100.0f,                                                 // max value for slider
//    new Vector2(300.0f, 100.0f),                            // size of the slider
//    new Vector2(0.0f, 0.0f),                                // slider offset position
//    "Test Slider",                                          // name of slider gameObject
//    "TEST SCORE",                                           // text for slider
//    Resources.Load<Font>("Nunito-Bold"),                    // font used for slider text
//    30,                                                     // character size of slider text
//    "FFFFFF",                                               // color of slider text
//    Resources.Load<Sprite>("sand"),                         // image for slider FILL
//    Resources.Load<Sprite>("desert"));                      // image for slider HANDLE

///*******************************
// ********* Create Text *********
// *******************************/
//UIInteractionSystem.Instance.CreateText(
//    GameObject.Find("Canvas").GetComponent<Canvas>(),       // canvas gameObject
//    "Dialog Box",                                           // name of root(parent) gameObject
//    "Testing Generating Text",                              // string of text gonna be created
//    Resources.Load<Font>("Nunito-Bold"),                    // font for text
//    50,                                                     // character size for text
//    "#FFFFFF",                                              // text color
//    new Vector2(0.0f, 300.0f),                              // text offset position
//    new Vector2(500.0f, 500.0f),                            // text RectTransform size
//    TextAnchor.MiddleCenter);                               // text alignment

///*******************************
// ******** Create Image *********
// *******************************/
//UIInteractionSystem.Instance.CreateImage(
//    GameObject.Find("Canvas").GetComponent<Canvas>(),       // canvas gameObject
//    "Dialog Box",                                           // name of root(parent) gameObject
//    "Haotai's Huge Head",                                   // name of image gameObject
//    new Vector2(-300.0f, -200.0f),                          // image offset position
//    new Vector2(500.0f, 500.0f),                            // image size
//    Resources.Load<Sprite>("Haotai_Xiong-Head"));           // image resource