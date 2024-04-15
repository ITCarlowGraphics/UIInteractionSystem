using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class UIInteractionSystem : MonoBehaviour
{
    // gameobject attributes
    public Canvas canvas;
    // funtion attributes
    public delegate void TestDelegate();
    public TestDelegate function1;
    public TestDelegate function2;
    // Setting attributes
    public int questionTimeLimit;
    public float soundVolumeCoefficient;
    // button attributes
    // public bool createButtonOrNot;
    // public int buttonNumbers;

    // instance attributes
    public static UIInteractionSystem _instance;

    public static UIInteractionSystem Instance
    {
        get
        {
            if (_instance == null)
            {
                // Check if an existing GameManager is present in the scene
                _instance = FindObjectOfType<UIInteractionSystem>();

                if (_instance == null)
                {
                    // No existing GameManager found, so create a new GameObject and add this script
                    GameObject em = new GameObject("UIInteractionSystem");
                    _instance = em.AddComponent<UIInteractionSystem>();

                    // Optionally, make this object persistent
                    DontDestroyOnLoad(em);
                }
            }
            return _instance;
        }
    }

    // trigger one button only
    public void ShowDialog(string dialogText, string buttonText, TestDelegate buttonFunction)
    {
        canvas = FindObjectOfType<Canvas>();
        SetFunction1(buttonFunction);
        // dialog box init
        GameObject dialogBoxPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/DialogBox.prefab", typeof(GameObject)) as GameObject;
        GameObject dialogBox = Instantiate(dialogBoxPrefab);
        dialogBox.transform.SetParent(canvas.transform, false);
        dialogBox.transform.Find("One_Button").gameObject.SetActive(true);
        // dialog box text init
        TextMeshProUGUI dialogTextComponent = dialogBox.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();
        dialogTextComponent.text = dialogText;
        // dialog box button init
        Button button = dialogBox.transform.Find("One_Button/Button_1").gameObject.GetComponent<Button>();
        button.GetComponentInChildren<TextMeshProUGUI>().text = buttonText;
        button.onClick.AddListener(() => function1());
    }

    public void ShowDialog(string dialogText, string buttonText_1, TestDelegate buttonFunction_1, string buttonText_2, TestDelegate buttonFunction_2)
    {
        canvas = FindObjectOfType<Canvas>();
        SetFunction1(buttonFunction_1);
        SetFunction2(buttonFunction_2);
        // dialog box init
        GameObject dialogBoxPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/DialogBox.prefab", typeof(GameObject)) as GameObject;
        GameObject dialogBox = Instantiate(dialogBoxPrefab);
        dialogBox.transform.SetParent(canvas.transform, false);
        dialogBox.transform.Find("Two_Button").gameObject.SetActive(true);
        // dialog box text init
        TextMeshProUGUI dialogTextComponent = dialogBox.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>();
        dialogTextComponent.text = dialogText;
        // dialog box button init
        Button button_1 = dialogBox.transform.Find("Two_Button/Button_1").gameObject.GetComponent<Button>();
        button_1.GetComponentInChildren<TextMeshProUGUI>().text = buttonText_1;
        button_1.onClick.AddListener(() => function1());
        Button button_2 = dialogBox.transform.Find("Two_Button/Button_2").gameObject.GetComponent<Button>();
        button_2.GetComponentInChildren<TextMeshProUGUI>().text = buttonText_2;
        button_2.onClick.AddListener(() => function2());

    }



    public void SetFunction1(TestDelegate t_function)
    {
        function1 = t_function;
    }

    public void SetFunction2(TestDelegate t_function)
    {
        function2 = t_function;
    }

    // test function
    public void ButtonClicked()
    {
        Debug.Log("Button Clicked!");
    }
}
