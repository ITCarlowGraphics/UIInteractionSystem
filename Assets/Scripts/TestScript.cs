using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS;
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
        GetComponent<CreateSettingMenu>().volumeCoefficient = UIInteractionSystem.Instance.GetSliderValue(
            "Volume Slider",                                                                                    // name of slider gameObject
            "Setting Menu",                                                                                     // dictionary string of specific screen
            GetComponent<CreateSettingMenu>().volumeCoefficient);                                               // the value gonna be changed
        GetComponent<CreateSettingMenu>().questionTimer = UIInteractionSystem.Instance.GetSliderValue(
            "Question Timer Slider",                                                                            // name of slider gameObject
            "Setting Menu",                                                                                     // dictionary string of specific screen
            GetComponent<CreateSettingMenu>().questionTimer);                                                   // the value gonna be changed
    }

    public void DestroyAllScreen()
    {
        UIInteractionSystem.Instance.DestroyAllScreen();
    }
}