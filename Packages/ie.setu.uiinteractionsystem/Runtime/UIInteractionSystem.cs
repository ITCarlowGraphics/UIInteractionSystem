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
    // instance attributes
    public static UIInteractionSystem Instance;
    // gameobject attributes
    public Canvas canvas;
    // funtion attributes
    public delegate void TestDelegate();
    public TestDelegate function1;
    public TestDelegate function2;
    // button attributes
    // public bool createButtonOrNot;
    // public int buttonNumbers;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        canvas = FindAnyObjectByType<Canvas>();
        if (null == canvas)
        {
            Debug.LogError("No Canvas found in the scene. Please add a Canvas in order to use UI elements.");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    public void CreateButtons()
    {
        for (int i = 0; i < buttonNumbers; i++)
        {
            GameObject buttonObj = new("MyButton");
            buttonObj.transform.SetParent(canvas.transform, false);
            RectTransform rectTransform = buttonObj.AddComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(160, 30);
            rectTransform.anchoredPosition = new Vector3(0, i * 50);

            Button button = buttonObj.AddComponent<Button>();
            Image image = buttonObj.AddComponent<Image>();

            GameObject textObj = new("ButtonText");
            textObj.transform.SetParent(buttonObj.transform, false);

            // button init
            button.targetGraphic = image;
            // text init
            Text text = textObj.AddComponent<Text>();
            text.text = "Click Me!";
            text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            text.color = Color.black;
            text.alignment = TextAnchor.MiddleCenter;

            // image.sprite = Resources.Load<Sprite>("Resources/unity_builtin_extra/Background");

            RectTransform textRectTransform = textObj.GetComponent<RectTransform>();
            textRectTransform.sizeDelta = new Vector2(160, 30);
            button.onClick.AddListener(() => ButtonClicked());
        }
    }
    */

    // trigger one button only
    public void ShowDialog(string dialogText, string buttonText, TestDelegate buttonFunction)
    {
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

    public void ButtonClicked()
    {
        Debug.Log("Button Clicked!");
    }
}
