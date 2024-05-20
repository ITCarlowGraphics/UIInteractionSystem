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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UIInteractionSystem.Instance.CreatePanel(GameObject.Find("Canvas").GetComponent<Canvas>(), 
                "What doesn't fail you\nIt's just not finished yet!", 
                Resources.Load<Font>("Nunito-Bold"), 
                30, 
                "#FFFFFF", 
                "#000000",
                "#8031D0", 
                new Vector2(500.0f, 500.0f));
            UIInteractionSystem.Instance.CreateButton(GameObject.Find("Canvas").GetComponent<Canvas>(), 
                "Click Me!", 
                Resources.Load<Font>("Nunito-Bold"), 
                25,
                "FFFFFF", 
                "#528120", 
                new Vector2(300, 100), 
                new Vector2(0, -100), ButtonClicked);
            UIInteractionSystem.Instance.CreateSlider(GameObject.Find("Canvas").GetComponent<Canvas>(),
                new Vector2(300.0f, 100.0f),
                new Vector2(0.0f, 0.0f), 
                "Test Slider",
                "TEST SLIDER", 
                Resources.Load<Font>("Nunito-Bold"),
                30,
                "FFFFFF",
                Resources.Load<Sprite>("sand"),
                Resources.Load<Sprite>("desert"));
        }


        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log(testValue);
        }

        testValue = UIInteractionSystem.Instance.GetSliderValue("Test Slider", testValue);
    }

    public void ButtonClicked()
    {
        Debug.Log("Button Clicked!");
    }
}
