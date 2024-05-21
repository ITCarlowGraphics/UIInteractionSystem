using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePanel : MonoBehaviour
{
    public void Createpanel()
    {
        /*******************************
         ********* Create Panel ********
         *******************************/
        UIInteractionSystem.Instance.CreatePanel(
            GameObject.Find("Canvas").GetComponent<Canvas>(),       // canvas gameObject
            "CreatePanel Demo",                                     // the name of root(parent) gameObject
            "Settings",                                             // text on panel
            Resources.Load<Font>("Nunito-Bold"),                    // font usef for text
            30,                                                     // text character size
            "#000000",                                              // text color
            "#F8BA42",                                              // front panel color
            "#FFFCE4",                                              // back panel color
            new Vector2(500.0f, 500.0f));                           // size of the whole panel

        /*******************************
         *** register root gameObject **
         *******************************/
        UIInteractionSystem.Instance.RegisterRootGameObject(
            "CreatePanel Demo",                                     // dictionary string of specific screen
            GameObject.Find("CreatePanel Demo"));                   // name of root gameObject
    }
}
