using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using static UIInteractionSystem;
using UnityEngine.Events;

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
    public TestDelegate scriptButtonFunction;
    // Setting attributes
    public int questionTimeLimit = 20;
    public float soundVolumeCoefficient = 100.0f;
    // script button attributes
    public bool createButtonOrNot;
    public bool powerupsEnabled;
    public bool enabledShakeFeature;
    public int buttonNumbers;
    // store root game objects
    private Dictionary<string, GameObject> rootGameObjects = new Dictionary<string, GameObject>();

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
                    GameObject em = new("UIInteractionSystem");
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

    public void CreateButtons(string buttonText, TestDelegate buttonFunction)
    {
        canvas = FindObjectOfType<Canvas>();
        SetScriptButtonFunction(buttonFunction);

        GameObject buttonObj = new("MyButton");
        buttonObj.transform.SetParent(this.transform, false);
        RectTransform rectTransform = buttonObj.AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(160, 30);
        rectTransform.anchoredPosition = new Vector3(0, 0);

        Button button = buttonObj.AddComponent<Button>();
        Image image = buttonObj.AddComponent<Image>();

        buttonObj.transform.SetParent(canvas.transform, false);
        GameObject textObj = new("ButtonText");
        textObj.transform.SetParent(buttonObj.transform, false);

        // button init
        button.targetGraphic = image;
        // text init
        Text text = textObj.AddComponent<Text>();
        text.text = buttonText;
        text.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        text.color = Color.black;
        text.alignment = TextAnchor.MiddleCenter;

        RectTransform textRectTransform = textObj.GetComponent<RectTransform>();
        textRectTransform.sizeDelta = new Vector2(160, 30);
        button.onClick.AddListener(() => scriptButtonFunction());
    }

    public void SetFunction1(TestDelegate t_function)
    {
        function1 = t_function;
    }

    public void SetFunction2(TestDelegate t_function)
    {
        function2 = t_function;
    }

    public void SetScriptButtonFunction(TestDelegate t_function)
    {
        scriptButtonFunction = t_function;
    }

    //-----------------------------------------------------------------------------------------------------------------//
    // Enable or Disable Screen
    //-----------------------------------------------------------------------------------------------------------------//

    public void RegisterRootGameObject(string name, GameObject obj)
    {
        if (!rootGameObjects.ContainsKey(name))
        {
            rootGameObjects.Add(name, obj);
        }
    }

    public void EnableScreen(string _rootGameObjectName)
    {
        if (rootGameObjects.TryGetValue(_rootGameObjectName, out GameObject obj))
        {
            obj.SetActive(true);
        }
    }

    public void DisableScreen(string _rootGameObjectName)
    {
        if (rootGameObjects.TryGetValue(_rootGameObjectName, out GameObject obj))
        {
            obj.SetActive(false);
        }
    }

    public void ToggleScreen(string _rootGameObjectName)
    {
        if (rootGameObjects.TryGetValue(_rootGameObjectName, out GameObject obj))
        {
            obj.SetActive(!obj.activeSelf);
        }
    }

    public void DestroyScreen(string _rootGameObjectName)
    {
        if (rootGameObjects.TryGetValue(_rootGameObjectName, out GameObject obj))
        {
            rootGameObjects.Remove(_rootGameObjectName);
            Destroy(obj);
        }
    }

    public void DestroyAllScreen()
    {
        foreach (var KeynValue in rootGameObjects)
        {
            if (KeynValue.Value != null)
            {
                Destroy(KeynValue.Value);
            }
        }
        rootGameObjects.Clear();
    }

    //-----------------------------------------------------------------------------------------------------------------//
    // Script Elements
    //-----------------------------------------------------------------------------------------------------------------//

    // Creates a toggle button that when clicked on switches its state (bool)
    public void CreateToggle(GameObject parentObject, string labelText, Font font, int fontSize,
                             string textColor, string textOnState, string textOffState, Vector2 toggleSize,
                             Vector2 handleSize, Vector2 position, UnityAction<bool> onValueChanged)
    {
        if (parentObject == null)
        {
            Debug.LogError("Parent object is null for creating toggle.");
            return;
        }

        if (font == null)
        {
            font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        }

        GameObject toggleGO = new GameObject("ToggleContainer");
        toggleGO.transform.SetParent(parentObject.transform, false);

        RectTransform containerRect = toggleGO.AddComponent<RectTransform>();
        containerRect.sizeDelta = toggleSize;
        containerRect.anchoredPosition = position;

        GameObject labelGO = new GameObject("Label");
        labelGO.transform.SetParent(toggleGO.transform, false);
        Text labelTextComponent = labelGO.AddComponent<Text>();
        labelTextComponent.text = labelText;
        labelTextComponent.font = font;
        labelTextComponent.fontSize = fontSize;
        ColorUtility.TryParseHtmlString(textColor, out Color parsedTextColor);
        labelTextComponent.color = parsedTextColor;
        labelTextComponent.alignment = TextAnchor.MiddleCenter;

        RectTransform labelRect = labelGO.GetComponent<RectTransform>();
        labelRect.sizeDelta = new Vector2(toggleSize.x, fontSize + 10);
        labelRect.anchoredPosition = new Vector2(0, (toggleSize.y / 1.5f) + 5);

        GameObject backgroundGO = new GameObject("Background");
        backgroundGO.transform.SetParent(toggleGO.transform, false);
        RectTransform bgRect = backgroundGO.AddComponent<RectTransform>();
        bgRect.sizeDelta = toggleSize;
        bgRect.anchoredPosition = Vector2.zero;

        Image backgroundImage = backgroundGO.AddComponent<Image>();
        backgroundImage.color = Color.red;

        Button backgroundButton = backgroundGO.AddComponent<Button>();

        GameObject handleGO = new GameObject("Handle");
        handleGO.transform.SetParent(backgroundGO.transform, false);
        RectTransform handleRect = handleGO.AddComponent<RectTransform>();
        handleRect.sizeDelta = handleSize;
        handleRect.anchoredPosition = new Vector2(-toggleSize.x / 2 + handleSize.x / 2, 0);
        Image handleImage = handleGO.AddComponent<Image>();
        handleImage.color = Color.white;

        GameObject statusTextGO = new GameObject("StatusText");
        statusTextGO.transform.SetParent(toggleGO.transform, false);
        Text statusTextComponent = statusTextGO.AddComponent<Text>();
        statusTextComponent.text = textOffState;
        statusTextComponent.font = font;
        statusTextComponent.fontSize = fontSize;
        statusTextComponent.alignment = TextAnchor.MiddleCenter;
        ColorUtility.TryParseHtmlString(textColor, out Color parsedStatusTextColor);
        statusTextComponent.color = parsedStatusTextColor;

        RectTransform statusTextRect = statusTextGO.GetComponent<RectTransform>();
        statusTextRect.sizeDelta = new Vector2(toggleSize.x, fontSize + 10);
        statusTextRect.anchoredPosition = new Vector2(0, -toggleSize.y / 1.5f - 10);

        bool isOn = false;

        UnityAction toggleAction = () =>
        {
            isOn = !isOn;

            if (isOn)
            {
                handleRect.anchoredPosition = new Vector2(toggleSize.x / 2 - handleSize.x / 2, 0);
                backgroundImage.color = Color.green;
                statusTextComponent.text = textOnState;
            }
            else
            {
                handleRect.anchoredPosition = new Vector2(-toggleSize.x / 2 + handleSize.x / 2, 0);
                backgroundImage.color = Color.red;
                statusTextComponent.text = textOffState;
            }

            onValueChanged.Invoke(isOn);
        };

        backgroundButton.onClick.AddListener(toggleAction);
        handleGO.AddComponent<Button>().onClick.AddListener(toggleAction);

        backgroundImage.color = Color.red;
    }








    public void CreatePanel(Canvas _canvas,
                            string _rootGameObjectName,
                            string _dialogText,
                            Font _dialogTextFont,
                            int _dialogTextSize,
                            string _dialogTextColor,
                            string _frontPanelColor,
                            string _backPanelColor,
                            Vector2 _panelSize)
    {
        // Color attributes init
        Color dialogTextColor = new(0, 0, 0);
        Color frontPanelColor = new(0, 0, 0);
        Color backPanelColor = new(0, 0, 0);
        if (!_dialogTextColor.Contains("#"))
        {
            _dialogTextColor = _dialogTextColor.Insert(0, "#");
        }
        if (!_frontPanelColor.Contains("#"))
        {
            _frontPanelColor = _frontPanelColor.Insert(0, "#");
        }
        if (!_backPanelColor.Contains("#"))
        {
            _backPanelColor = _backPanelColor.Insert(0, "#");
        }
        // setParent init
        if (GameObject.Find(_rootGameObjectName) == null)
        {
            GameObject dialogBox = new(_rootGameObjectName);
            dialogBox.transform.SetParent(_canvas.transform, false);
        }
        // color parsement
        if (ColorUtility.TryParseHtmlString(_dialogTextColor, out Color _color1))
        {
            dialogTextColor = _color1;
        }
        if (ColorUtility.TryParseHtmlString(_frontPanelColor, out Color _color2))
        {
            frontPanelColor = _color2;
        }
        if (ColorUtility.TryParseHtmlString(_backPanelColor, out Color _color3))
        {
            backPanelColor = _color3;
        }

        // panels init
        GameObject panelObj_1 = new("Back_Panel");
        GameObject panelObj_2 = new("Front_Panel");
        panelObj_1.transform.SetParent(GameObject.Find(_rootGameObjectName).transform, false);
        panelObj_2.transform.SetParent(GameObject.Find(_rootGameObjectName).transform, false);
        // panels rect init
        RectTransform rectTransform_1 = panelObj_1.AddComponent<RectTransform>();
        RectTransform rectTransform_2 = panelObj_2.AddComponent<RectTransform>();
        rectTransform_1.sizeDelta = _panelSize;
        rectTransform_2.sizeDelta = _panelSize - new Vector2(25.0f, 25.0f);
        rectTransform_1.anchoredPosition = new Vector2(0.0f, 0.0f);
        rectTransform_2.anchoredPosition = new Vector2(0.0f, 0.0f);
        // panels color init
        Image image_1 = panelObj_1.AddComponent<Image>();
        Image image_2 = panelObj_2.AddComponent<Image>();
        image_1.color = backPanelColor;
        image_2.color = frontPanelColor;
        // text init
        GameObject textObj = new("DialogText");
        textObj.transform.SetParent(GameObject.Find(_rootGameObjectName).transform, false);
        Text text = textObj.AddComponent<Text>();
        text.text = _dialogText;
        text.fontSize = _dialogTextSize;
        text.font = _dialogTextFont;
        text.color = dialogTextColor;
        text.alignment = TextAnchor.UpperCenter;
        RectTransform textRectTransform = textObj.GetComponent<RectTransform>();
        textRectTransform.sizeDelta = _panelSize - new Vector2(50.0f, 50.0f);
    }

    public void CreateButton(Canvas _canvas,
                            string _rootGameObjectName,
                            string _buttonText,
                            Font _buttonTextFont,
                            int _buttonTextSize,
                            string _buttonTextColor,
                            string _buttonColor,
                            Vector2 _buttonSize,
                            Vector2 _buttonAnchoredPosition,
                            TestDelegate _buttonFunction = null,
                            TestDelegate _buttonFunction2 = null,
                            TestDelegate _buttonFunction3 = null)
    {
        // Color attributes init
        Color buttonTextColor = new(0, 0, 0);
        Color buttonColor = new(0, 0, 0);
        if (!_buttonTextColor.Contains("#"))
        {
            _buttonTextColor = _buttonTextColor.Insert(0, "#");
        }
        if (!_buttonColor.Contains("#"))
        {
            _buttonColor = _buttonColor.Insert(0, "#");
        }
        // setParent init
        if (GameObject.Find(_rootGameObjectName) == null)
        {
            GameObject dialogBox = new(_rootGameObjectName);
            dialogBox.transform.SetParent(_canvas.transform, false);
        }
        // color parsement
        if (ColorUtility.TryParseHtmlString(_buttonTextColor, out Color _color1))
        {
            buttonTextColor = _color1;
        }
        if (ColorUtility.TryParseHtmlString(_buttonColor, out Color _color2))
        {
            buttonColor = _color2;
        }

        GameObject buttonObj = new("Button");
        buttonObj.transform.SetParent(GameObject.Find(_rootGameObjectName).transform, false);
        RectTransform rectTransform = buttonObj.AddComponent<RectTransform>();
        rectTransform.sizeDelta = _buttonSize;
        rectTransform.anchoredPosition = _buttonAnchoredPosition;

        Button button = buttonObj.AddComponent<Button>();
        Image image = buttonObj.AddComponent<Image>();

        GameObject textObj = new("ButtonText");
        textObj.transform.SetParent(buttonObj.transform, false);

        // button init
        button.targetGraphic = image;
        button.image.color = buttonColor;
        // text init
        Text text = textObj.AddComponent<Text>();
        text.text = _buttonText;
        text.fontSize = _buttonTextSize;
        text.font = _buttonTextFont;
        text.color = buttonTextColor;
        text.alignment = TextAnchor.MiddleCenter;

        RectTransform textRectTransform = textObj.GetComponent<RectTransform>();
        textRectTransform.sizeDelta = _buttonSize;
        if (_buttonFunction != null)
        {
            button.onClick.AddListener(() => _buttonFunction());
        }
        if (_buttonFunction2 != null)
        {
            button.onClick.AddListener(() => _buttonFunction2());
        }
        if (_buttonFunction3 != null)
        {
            button.onClick.AddListener(() => _buttonFunction3());
        }
    }

    public void CreateSlider(Canvas _canvas,
                            string _rootGameObjectName,
                            float _minValue,
                            float _maxValue,
                            Vector2 _sliderSize,
                            Vector2 _sliderAnchoredPosition,
                            string _sliderName,
                            string _sliderText,
                            Font _sliderTextFont,
                            int _sliderTextSize,
                            string _sliderTextColor,
                            Sprite _fillImage,
                            Sprite _handleImage,
                            string _valueUnitMeasurement,
                            float _changedValue = 0.0f)
    {
        // setParent init
        if (GameObject.Find(_rootGameObjectName) == null)
        {
            GameObject dialogBox = new(_rootGameObjectName);
            dialogBox.transform.SetParent(_canvas.transform, false);
        }

        // Create Slider
        GameObject sliderObj = new(_sliderName);
        sliderObj.transform.SetParent(GameObject.Find(_rootGameObjectName).transform, false);
        RectTransform rectTransform = sliderObj.AddComponent<RectTransform>();
        rectTransform.sizeDelta = _sliderSize;
        rectTransform.anchoredPosition = _sliderAnchoredPosition;
        // Create Text
        GameObject textObj = new("SliderText");
        textObj.transform.SetParent(sliderObj.transform, false);
        Text sliderText = textObj.AddComponent<Text>();
        sliderText.font = _sliderTextFont;
        sliderText.fontSize = _sliderTextSize;
        if (!_sliderTextColor.Contains("#"))
        {
            _sliderTextColor = _sliderTextColor.Insert(0, "#");
        }
        if (ColorUtility.TryParseHtmlString(_sliderTextColor, out Color textColor))
        {
            sliderText.color = textColor;
        }
        sliderText.alignment = TextAnchor.MiddleCenter;
        // Position the text above the slider
        RectTransform textRectTransform = textObj.GetComponent<RectTransform>();
        textRectTransform.sizeDelta = new Vector2(_sliderSize.x * 2, _sliderTextSize * 2); // Adjust height based on text size
        textRectTransform.anchoredPosition = new Vector2(0, _sliderSize.y / 2 + _sliderTextSize); // Position text above sliders

        // Create Slider
        Slider slider = sliderObj.AddComponent<Slider>();
        // fill
        GameObject fill = new("Fill");
        fill.transform.SetParent(slider.transform, false);
        Image fillImage = fill.AddComponent<Image>();
        Sprite fillSprite = _fillImage;
        fillImage.sprite = fillSprite;
        // handle
        GameObject handle = new("Handle");
        handle.transform.SetParent(slider.transform, false);
        Image handleImage = handle.AddComponent<Image>();
        Sprite handleSprite = _handleImage;
        handleImage.sprite = handleSprite;
        // slider, fill & handle init
        fill.GetComponent<RectTransform>().sizeDelta = new Vector2(10.0f, _sliderSize.y / 10);
        fill.GetComponent<RectTransform>().anchoredPosition = new Vector2(-10.0f, 0.0f);
        handle.GetComponent<RectTransform>().sizeDelta = new Vector2(_sliderSize.x / 5, _sliderSize.x / 10);
        handle.GetComponent<RectTransform>().anchoredPosition = new Vector2(-10.0f, 0.0f);
        slider.fillRect = fill.GetComponent<RectTransform>();
        slider.handleRect = handle.GetComponent<RectTransform>();
        slider.minValue = _minValue;
        slider.maxValue = _maxValue;
        slider.value = _changedValue;
        sliderText.text = _sliderText + ": " + slider.value.ToString() + " " + _valueUnitMeasurement;
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x / 2, rectTransform.sizeDelta.y / 2);
        slider.onValueChanged.AddListener((v) =>
        {
            sliderText.text = _sliderText + ": " + slider.value.ToString() + " " + _valueUnitMeasurement;
        });
    }

    public float GetSliderValue(string _sliderName, string _rootGameObjectName, float _changedValue)
    {
        if (GameObject.Find(_sliderName) != null && rootGameObjects.TryGetValue(_rootGameObjectName, out _))
        {
            return GameObject.Find(_sliderName).GetComponent<Slider>().value;
        }
        return _changedValue;
    }

    public void CreateText(Canvas _canvas,
                        string _rootGameObjectName,
                        string _text,
                        Font _font,
                        int _fontSize,
                        string _textColor,
                        Vector2 _position,
                        Vector2 _size,
                        TextAnchor _alignment = TextAnchor.MiddleCenter)
    {
        // Ensure the parent exists
        if (GameObject.Find(_rootGameObjectName) == null)
        {
            GameObject dialogBox = new(_rootGameObjectName);
            dialogBox.transform.SetParent(_canvas.transform, false);
        }

        // Create the text object
        GameObject textObj = new("Text");
        textObj.transform.SetParent(GameObject.Find(_rootGameObjectName).transform, false);
        RectTransform textRectTransform = textObj.AddComponent<RectTransform>();
        textRectTransform.sizeDelta = _size;
        textRectTransform.anchoredPosition = _position;

        Text textComponent = textObj.AddComponent<Text>();
        textComponent.text = _text;
        textComponent.font = _font;
        textComponent.fontSize = _fontSize;
        textComponent.alignment = _alignment;

        if (!_textColor.Contains("#"))
        {
            _textColor = _textColor.Insert(0, "#");
        }
        if (ColorUtility.TryParseHtmlString(_textColor, out Color textColor))
        {
            textComponent.color = textColor;
        }
    }

    public void CreateImage(Canvas _canvas,
                        string _rootGameObjectName,
                        string _imageName,
                        Vector2 _position,
                        Vector2 _size,
                        Sprite _image)
    {
        // Ensure the parent exists
        if (GameObject.Find(_rootGameObjectName) == null)
        {
            GameObject dialogBox = new(_rootGameObjectName);
            dialogBox.transform.SetParent(_canvas.transform, false);
        }

        // Create the image object
        GameObject imageObj = new(_imageName);
        imageObj.transform.SetParent(GameObject.Find(_rootGameObjectName).transform, false);
        RectTransform imageRectTransform = imageObj.AddComponent<RectTransform>();
        imageRectTransform.sizeDelta = _size;
        imageRectTransform.anchoredPosition = _position;

        Image imageComponent = imageObj.AddComponent<Image>();
        imageComponent.sprite = _image;
    }


}