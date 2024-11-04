using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateText : MonoBehaviour
{
    public void Createtext()
    {
        /*********************************
         * Create Leaderboard Title Text *
         *********************************/
        UIInteractionSystem.Instance.CreateText(
            GameObject.Find("Canvas").GetComponent<Canvas>(),       // canvas gameObject
            "CreateText Demo",                                      // name of root(parent) gameObject
            "LEADER BOARD",                                         // string of text gonna be created
            Resources.Load<Font>("Nunito-Bold"),                    // font for text
            50,                                                     // character size for text
            "#000000",                                              // text color
            new Vector2(0.0f, 0.0f),                                // text offset position
            new Vector2(500.0f, 500.0f),                            // text RectTransform size
            TextAnchor.MiddleCenter);                               // text alignment

        /*******************************
         *** register root gameObject **
         *******************************/
        UIInteractionSystem.Instance.RegisterRootGameObject(
            "CreateText Demo",                                      // dictionary string of specific screen
            GameObject.Find("CreateText Demo"));                    // name of root gameObject
    }
}
