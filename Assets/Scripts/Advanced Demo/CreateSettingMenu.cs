using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSettingMenu : MonoBehaviour
{
    public float volumeCoefficient = 0.0f;
    public float questionTimer = 20.0f;

    public void CreateSettingmenu()
    {
        /*******************************
         ********* Create Panel ********
         *******************************/
        UIInteractionSystem.Instance.CreatePanel(
            GameObject.Find("Canvas").GetComponent<Canvas>(),       // canvas gameObject
            "Setting Menu",                                         // the name of root(parent) gameObject
            "Settings",                                             // text on panel
            Resources.Load<Font>("Nunito-Bold"),                    // font usef for text
            30,                                                     // text character size
            "#000000",                                              // text color
            "#F8BA42",                                              // front panel color
            "#FFFCE4",                                              // back panel color
            new Vector2(500.0f, 500.0f));                           // size of the whole panel

        /*******************************
         ***** Create Volume Slider ****
         *******************************/
        UIInteractionSystem.Instance.CreateSlider(
            GameObject.Find("Canvas").GetComponent<Canvas>(),       // canvas gameObject
            "Setting Menu",                                         // name of root(parent) gameObject
            0.0f,                                                   // min value for slider
            100.0f,                                                 // max value for slider
            new Vector2(200.0f, 50.0f),                             // size of the slider
            new Vector2(0.0f, 50.0f),                               // slider offset position
            "Volume Slider",                                        // name of slider gameObject
            "Volume",                                               // text for slider
            Resources.Load<Font>("Nunito-Bold"),                    // font used for slider text
            25,                                                     // character size of slider text
            "FFFFFF",                                               // color of slider text
            Resources.Load<Sprite>("sand"),                         // image for slider FILL
            Resources.Load<Sprite>("desert"),                       // image for slider HANDLE
            "%",                                                    // value unit measurement
            volumeCoefficient);                                     // value is going to be changed

        /********************************
         * Create Question Timer Slider *
         ********************************/
        UIInteractionSystem.Instance.CreateSlider(
            GameObject.Find("Canvas").GetComponent<Canvas>(),       // canvas gameObject
            "Setting Menu",                                         // name of root(parent) gameObject
            20.0f,                                                  // min value for slider
            120.0f,                                                 // max value for slider
            new Vector2(200.0f, 50.0f),                             // size of the slider
            new Vector2(0.0f, -100.0f),                             // slider offset position
            "Question Timer Slider",                                // name of slider gameObject
            "Question Time Limit",                                  // text for slider
            Resources.Load<Font>("Nunito-Bold"),                    // font used for slider text
            25,                                                     // character size of slider text
            "FFFFFF",                                               // color of slider text
            Resources.Load<Sprite>("sand"),                         // image for slider FILL
            Resources.Load<Sprite>("desert"),                       // image for slider HANDLE
            "seconds",                                              // value unit measurement
            questionTimer);                                         // value is going to be changed

        /*******************************
         ******** Create Button ********
         *******************************/
        UIInteractionSystem.Instance.CreateButton(
            GameObject.Find("Canvas").GetComponent<Canvas>(),       // canvas gameObject
            "Setting Menu",                                         // name of root(parent) gameObject
            "Save & Exit",                                          // text appear on button
            Resources.Load<Font>("Nunito-Bold"),                    // font used for button text
            25,                                                     // button text character size
            "#F8BA42",                                              // button text color
            "#FFFCE4",                                              // color of button
            new Vector2(200, 50),                                   // button size
            new Vector2(0, -200),                                   // anchored position of button
            () => UIInteractionSystem.Instance.DestroyScreen("Setting Menu"));// function will be executed when button OnClick

        /*******************************
         *** register root gameObject **
         *******************************/
        UIInteractionSystem.Instance.RegisterRootGameObject(
            "Setting Menu",                                         // dictionary string of specific screen
            GameObject.Find("Setting Menu"));                       // name of root gameObject
    }
}
