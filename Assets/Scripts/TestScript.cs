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
            UIInteractionSystem.Instance.ShowDialog("Pick two players to swap", "Okay", UIInteractionSystem.Instance.ButtonClicked, "How Are You", UIInteractionSystem.Instance.ButtonClicked);
        }
    }
}