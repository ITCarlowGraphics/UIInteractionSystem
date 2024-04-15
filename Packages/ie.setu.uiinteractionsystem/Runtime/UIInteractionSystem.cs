using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

// [CreateAssetMenu(fileName = "DialogBox")]

public class UIInteractionSystem : MonoBehaviour
{
    // gameobject attributes
    public Canvas canvas;
    public GameObject dialogBoxPrefab;
    public GameObject settingMenuPrefab;
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

    public void InstantiatePrefab(string addressableKey)
    {
        Addressables.InstantiateAsync(addressableKey).Completed += OnPrefabInstantiated;
    }

    private void OnPrefabInstantiated(AsyncOperationHandle<GameObject> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            dialogBoxPrefab = obj.Result;
        }
        else
        {
            Debug.LogError("Failed to load the prefab.");
        }
    }

    public void InstantiateSettingMenuPrefab(string addressableKey)
    {
        Addressables.InstantiateAsync(addressableKey).Completed += OnSettingMenuInstantiated;
    }

    private void OnSettingMenuInstantiated(AsyncOperationHandle<GameObject> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            settingMenuPrefab = obj.Result;
        }
        else
        {
            Debug.LogError("Failed to load the setting menu prefab.");
        }
    }

    // trigger one button only
    public IEnumerator ShowDialog(string dialogText, string buttonText, TestDelegate buttonFunction)
    {
        canvas = FindObjectOfType<Canvas>();
        SetFunction1(buttonFunction);
        // dialog box init
        InstantiatePrefab("Packages/ie.setu.uiinteractionsystem/Runtime/DialogBox.prefab");
        yield return new WaitUntil(() => dialogBoxPrefab != null);
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

    public IEnumerator ShowDialogTwoButton(string dialogText, string buttonText_1, TestDelegate buttonFunction_1, string buttonText_2, TestDelegate buttonFunction_2)
    {
        canvas = FindObjectOfType<Canvas>();
        SetFunction1(buttonFunction_1);
        SetFunction2(buttonFunction_2);
        // dialog box init
        InstantiatePrefab("Packages/ie.setu.uiinteractionsystem/Runtime/DialogBox.prefab");
        yield return new WaitUntil(() => dialogBoxPrefab != null);
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

    public IEnumerator ShowSettingMenu()
    {
        canvas = FindObjectOfType<Canvas>();
        if (GameObject.FindGameObjectWithTag("SettingMenu") != null)
        {
            GameObject.FindGameObjectWithTag("SettingMenu").SetActive(true);
            yield return null;
        }
        else
        {
            // setting menu init
            InstantiateSettingMenuPrefab("Packages/ie.setu.uiinteractionsystem/Runtime/SettingMenu.prefab");
            yield return new WaitUntil(() => settingMenuPrefab != null);
            GameObject.FindGameObjectWithTag("SettingMenu").transform.SetParent(canvas.transform, false);
        }
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
