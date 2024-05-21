using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateImage : MonoBehaviour
{
    public void Createimage()
    {
        UIInteractionSystem.Instance.CreateImage(
            GameObject.Find("Canvas").GetComponent<Canvas>(),       // canvas gameObject
            "CreateImage Demo",                                     // name of root(parent) gameObject
            "Haotai's Head",                                        // name of image gameObject
            new Vector2(0.0f, 0.0f),                                // image offset position
            new Vector2(100.0f, 100.0f),                            // image size
            Resources.Load<Sprite>("Haotai_Xiong-Head"));           // image resource

        /*******************************
         *** register root gameObject **
         *******************************/
        UIInteractionSystem.Instance.RegisterRootGameObject(
            "CreateImage Demo",                                     // dictionary string of specific screen
            GameObject.Find("CreateImage Demo"));                   // name of root gameObject
    }
}
