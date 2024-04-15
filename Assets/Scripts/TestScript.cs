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
            // StartCoroutine(UIInteractionSystem.Instance.ShowDialogTwoButton("Pick two players to swap", "One Player", ButtonClicked, "Two Players", ButtonClicked));
            StartCoroutine(UIInteractionSystem.Instance.ShowSettingMenu());
        }
    }

    public void ButtonClicked()
    {
        Debug.Log("Button Clicked!");
    }
}
