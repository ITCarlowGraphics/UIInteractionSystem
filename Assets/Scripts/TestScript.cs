using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
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
                Resources.Load("Nunito-Bold") as Font, 
                30, 
                "#FFFFFF", 
                "#000000",
                "#8031D0", 
                new Vector2(500.0f, 500.0f));
            UIInteractionSystem.Instance.CreateButton(GameObject.Find("Canvas").GetComponent<Canvas>(), 
                "Click Me!", 
                Resources.Load("Nunito-Bold") as Font, 
                25,
                "FFFFFF", 
                "#528120", 
                new Vector2(300, 100), 
                new Vector2(0, -100), ButtonClicked);
        }
    }

    public void ButtonClicked()
    {
        Debug.Log("Button Clicked!");
    }
}
