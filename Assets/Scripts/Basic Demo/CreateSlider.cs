using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSlider : MonoBehaviour
{
    public float testValue = 0.0f;

    public void Createslider()
    {
        /********************************
         ****** Create Test Slider ******
         ********************************/
        UIInteractionSystem.Instance.CreateSlider(
            GameObject.Find("Canvas").GetComponent<Canvas>(),       // canvas gameObject
            "CreateSlider Demo",                                    // name of root(parent) gameObject
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
            testValue);                                             // value is going to be changed

        /*******************************
         *** register root gameObject **
         *******************************/
        UIInteractionSystem.Instance.RegisterRootGameObject(
            "CreateSlider Demo",                                    // dictionary string of specific screen
            GameObject.Find("CreateSlider Demo"));                  // name of root gameObject
    }
}
