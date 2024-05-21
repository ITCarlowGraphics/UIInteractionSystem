using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDialogBox : MonoBehaviour
{
    public void CreateDialogbox()
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
}