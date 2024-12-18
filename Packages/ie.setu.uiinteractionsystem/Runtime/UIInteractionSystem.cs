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
                             Vector2 handleSize, Vector2 position, UnityAction<bool> onValueChanged,
                             bool rounded, bool dropShadow, bool sliderBackground, bool greyOut,
                             Vector2 titlePosition, Vector2 statusTextPosition, Color onColor, Color offColor)
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

        // Create Label (toggle title)
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
        labelRect.anchoredPosition = titlePosition; // Set position for toggle title

        // Create Background
        GameObject backgroundGO = new GameObject("Background");
        backgroundGO.transform.SetParent(toggleGO.transform, false);
        RectTransform bgRect = backgroundGO.AddComponent<RectTransform>();
        bgRect.sizeDelta = toggleSize;
        bgRect.anchoredPosition = Vector2.zero;

        Image backgroundImage = backgroundGO.AddComponent<Image>();
        backgroundImage.color = greyOut ? Color.gray : offColor;

        Button backgroundButton = backgroundGO.AddComponent<Button>();

        if (dropShadow)
        {
            Shadow shadow = backgroundGO.AddComponent<Shadow>();
            shadow.effectColor = new Color(0, 0, 0, 0.8f);
            shadow.effectDistance = new Vector2(4, -4);
        }

        // Create Slider Background only if sliderBackground is true
        if (sliderBackground)
        {
            GameObject sliderBG = new GameObject("SliderBackground");
            sliderBG.transform.SetParent(backgroundGO.transform, false);
            RectTransform sliderRect = sliderBG.AddComponent<RectTransform>();
            sliderRect.sizeDelta = new Vector2(toggleSize.x - handleSize.x, handleSize.y * 0.6f); // Bar width minus handle
            sliderRect.anchoredPosition = Vector2.zero;

            Image sliderImage = sliderBG.AddComponent<Image>();
            sliderImage.color = Color.gray; // Grey color for the bar
        }

        // Create Handle
        GameObject handleGO = new GameObject("Handle");
        handleGO.transform.SetParent(backgroundGO.transform, false);
        RectTransform handleRect = handleGO.AddComponent<RectTransform>();
        handleRect.sizeDelta = handleSize;
        handleRect.anchoredPosition = new Vector2(-toggleSize.x / 2 + handleSize.x / 2, 0);
        Image handleImage = handleGO.AddComponent<Image>();
        handleImage.color = greyOut ? Color.gray : Color.white;

        // Create Status Text (the "ON" or "OFF" text)
        GameObject statusTextGO = new GameObject("StatusText");
        statusTextGO.transform.SetParent(toggleGO.transform, false);
        Text statusTextComponent = statusTextGO.AddComponent<Text>();
        statusTextComponent.text = textOffState;
        statusTextComponent.font = font;
        statusTextComponent.fontSize = fontSize;
        statusTextComponent.alignment = TextAnchor.MiddleCenter;
        statusTextComponent.color = parsedTextColor;

        RectTransform statusTextRect = statusTextGO.GetComponent<RectTransform>();
        statusTextRect.sizeDelta = new Vector2(toggleSize.x, fontSize + 10);
        statusTextRect.anchoredPosition = statusTextPosition; // Set position for status text

        bool isOn = false;

        UnityAction toggleAction = () =>
        {
            isOn = !isOn;

            if (greyOut)
            {
                if (isOn)
                {
                    backgroundImage.color = onColor;
                    handleImage.color = Color.white;
                    statusTextComponent.text = textOnState;

                    handleRect.anchoredPosition = new Vector2(toggleSize.x / 2 - handleSize.x / 2, 0);
                }
                else
                {
                    backgroundImage.color = Color.gray;
                    handleImage.color = Color.gray;
                    statusTextComponent.text = textOffState;

                    handleRect.anchoredPosition = new Vector2(-toggleSize.x / 2 + handleSize.x / 2, 0);
                }
            }
            else
            {
                if (isOn)
                {
                    backgroundImage.color = onColor;
                    handleImage.color = Color.white;
                    statusTextComponent.text = textOnState;

                    handleRect.anchoredPosition = new Vector2(toggleSize.x / 2 - handleSize.x / 2, 0);
                }
                else
                {
                    backgroundImage.color = offColor;
                    handleImage.color = Color.white;
                    statusTextComponent.text = textOffState;

                    handleRect.anchoredPosition = new Vector2(-toggleSize.x / 2 + handleSize.x / 2, 0);
                }
            }

            onValueChanged.Invoke(isOn);
        };

        backgroundButton.onClick.AddListener(toggleAction);

        if (greyOut)
        {
            backgroundImage.color = Color.gray;
            handleImage.color = Color.gray;
            statusTextComponent.text = textOffState;
        }
    }



    public void AddSliderRWM(GameObject parentPanel, string sliderName, float minValue, float maxValue, float initialValue,
                             Vector2 sliderSize, Vector2 sliderPosition, string labelText, Vector2 labelPosition,
                             Sprite fillSprite, Sprite handleSprite, string valueTextFormat = "{0:F1}%", Color labelColor = default)
    {
        // Check if UIInteractionSystem instance exists
        UIInteractionSystem instance = FindObjectOfType<UIInteractionSystem>();
        if (instance == null)
        {
            Debug.Log("UIInteractionSystem instance not found.");
            return;
        }

        // Avoid creating duplicate sliders
        if (parentPanel.transform.Find(sliderName) != null)
        {
            return;
        }

        // Create Volume Slider GameObject
        GameObject sliderObj = new GameObject(sliderName);
        sliderObj.transform.SetParent(parentPanel.transform);

        // Add Slider component and set properties
        Slider slider = sliderObj.AddComponent<Slider>();
        RectTransform sliderRect = slider.GetComponent<RectTransform>();
        sliderRect.sizeDelta = sliderSize;
        sliderRect.localPosition = sliderPosition;

        slider.minValue = minValue;
        slider.maxValue = maxValue;
        slider.value = initialValue;

        // Create Fill Area
        GameObject fillAreaObj = new GameObject("Fill Area");
        fillAreaObj.transform.SetParent(sliderObj.transform);
        RectTransform fillAreaRect = fillAreaObj.AddComponent<RectTransform>();
        fillAreaRect.anchorMin = new Vector2(0, 0.25f);
        fillAreaRect.anchorMax = new Vector2(1, 0.75f);
        fillAreaRect.offsetMin = new Vector2(0, 0);
        fillAreaRect.offsetMax = new Vector2(-30, 0);

        // Add Fill Image
        GameObject fillObj = new GameObject("Fill");
        fillObj.transform.SetParent(fillAreaObj.transform);
        Image fillImage = fillObj.AddComponent<Image>();
        fillImage.sprite = fillSprite;
        fillImage.type = Image.Type.Filled;
        fillImage.fillMethod = Image.FillMethod.Horizontal;
        RectTransform fillRect = fillObj.GetComponent<RectTransform>();
        fillRect.anchorMin = Vector2.zero;
        fillRect.anchorMax = Vector2.one;
        fillRect.offsetMin = Vector2.zero;
        fillRect.offsetMax = Vector2.zero;

        slider.fillRect = fillRect;

        // Create Handle
        GameObject handleObj = new GameObject("Handle");
        handleObj.transform.SetParent(sliderObj.transform);
        RectTransform handleRect = handleObj.AddComponent<RectTransform>();
        handleRect.sizeDelta = new Vector2(60, 35);
        handleRect.anchorMin = new Vector2(1, 0.5f);
        handleRect.anchorMax = new Vector2(1, 0.5f);
        handleRect.pivot = new Vector2(0.5f, 0.5f);
        handleRect.anchoredPosition = new Vector2(-5, -5);

        Image handleImage = handleObj.AddComponent<Image>();
        handleImage.sprite = handleSprite;
        handleImage.color = Color.white;

        slider.handleRect = handleRect;

        // Create Label (slider name)
        GameObject labelObj = new GameObject(sliderName + "Label");
        labelObj.transform.SetParent(parentPanel.transform);
        TextMeshProUGUI labelTextComponent = labelObj.AddComponent<TextMeshProUGUI>();
        labelTextComponent.text = labelText;
        labelTextComponent.fontSize = 32;
        labelTextComponent.color = labelColor == default ? Color.black : labelColor;
        RectTransform labelRect = labelObj.GetComponent<RectTransform>();
        labelRect.sizeDelta = new Vector2(200, 50);
        labelRect.localPosition = labelPosition;

        // Create Value Text
        GameObject valueTextObj = new GameObject(sliderName + "ValueText");
        valueTextObj.transform.SetParent(parentPanel.transform);
        TextMeshProUGUI valueTextComponent = valueTextObj.AddComponent<TextMeshProUGUI>();
        valueTextComponent.fontSize = 28;
        valueTextComponent.color = Color.black;
        valueTextComponent.text = string.Format(valueTextFormat, slider.value);
        RectTransform valueTextRect = valueTextObj.GetComponent<RectTransform>();
        valueTextRect.localPosition = new Vector3(labelPosition.x, labelPosition.y - 40, 0);

        // Slider Value Change Listener
        slider.onValueChanged.AddListener((v) =>
        {
            instance.soundVolumeCoefficient = v / 100.0f;
            valueTextComponent.text = string.Format(valueTextFormat, v);
        });
    }



    // Helper Method to Create Circles
    private GameObject CreateCircle(string name, Transform parent, float radius, Vector2 position, Color color)
    {
        Debug.Log("Attempted to make circle");

        GameObject circle = new GameObject(name);
        circle.transform.SetParent(parent, false);
        LineRenderer lineRenderer = circle.AddComponent<LineRenderer>();

        lineRenderer.positionCount = 50; // Smoothness
        lineRenderer.loop = true;
        lineRenderer.startWidth = 0.1f; // Adjust width as needed
        lineRenderer.endWidth = 0.1f;
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.useWorldSpace = false;

        Vector3[] points = new Vector3[lineRenderer.positionCount];
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            float angle = (float)i / lineRenderer.positionCount * Mathf.PI * 2;
            points[i] = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
        }
        lineRenderer.SetPositions(points);

        RectTransform rectTransform = circle.AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(radius * 2, radius * 2);
        rectTransform.anchoredPosition = position;

        return circle;
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