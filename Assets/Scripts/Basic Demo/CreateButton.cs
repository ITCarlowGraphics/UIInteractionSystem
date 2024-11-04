using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateButton : MonoBehaviour
{
    public void Createbutton()
    {
        UIInteractionSystem.Instance.CreateButton(
            GameObject.Find("Canvas").GetComponent<Canvas>(),       // canvas gameObject
            "CreateButton Demo",                                    // name of root(parent) gameObject
            "TEST",                                                 // text appear on button
            Resources.Load<Font>("Nunito-Bold"),                    // font used for button text
            25,                                                     // button text character size
            "000000",                                               // button text color
            "#D9D9D9",                                              // color of button
            new Vector2(100.0f, 100.0f),                            // button size
            new Vector2(0.0f, 0.0f));                               // anchored position of button

        /*******************************
         *** register root gameObject **
         *******************************/
        UIInteractionSystem.Instance.RegisterRootGameObject(
            "CreateButton Demo",                                    // dictionary string of specific screen
            GameObject.Find("CreateButton Demo"));                  // name of root gameObject
    }
}
