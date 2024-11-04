using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePartyMenu : MonoBehaviour
{
    public void CreatePartymenu()
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
}
