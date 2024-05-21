using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLeaderBoard : MonoBehaviour
{
    public void CreateLeaderboard()
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
            GameObject.Find("Canvas").GetComponent<Canvas>(),           // canvas gameObject
            "Leaderboard Menu",                                         // name of root(parent) gameObject
            "Trophy",                                                   // name of image gameObject
            new Vector2(0.0f, -250.0f),                                 // image offset position
            new Vector2(200.0f, 150.0f),                                // image size
            Resources.Load<Sprite>("Trophy"));                          // image resource

        /*******************************
         *** register root gameObject **
         *******************************/
        UIInteractionSystem.Instance.RegisterRootGameObject(
            "Leaderboard Menu",                                         // dictionary string of specific screen
            GameObject.Find("Leaderboard Menu"));                       // name of root gameObject
    }
}
