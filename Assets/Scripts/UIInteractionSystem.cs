using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIInteractionSystem : MonoBehaviour
{
    // gameobject attributes
    Canvas canvas;
    // button attributes
    public bool createButtonOrNot;
    public int buttonNumbers;

    // Start is called before the first frame update
    void Start()
    {
        if (!TryGetComponent(out canvas))
        {
            Debug.LogError("No Canvas found in the scene. Please add a Canvas in order to use UI elements.");
            return;
        }

        if (createButtonOrNot)
        {
            CreateButtons();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateButtons()
    {
        for (int i = 0; i < buttonNumbers; i++)
        {
            GameObject buttonObj = new("MyButton");
            buttonObj.transform.SetParent(this.transform, false);
            RectTransform rectTransform = buttonObj.AddComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(160, 30);
            rectTransform.anchoredPosition = new Vector3(0, i * 50);

            Button button = buttonObj.AddComponent<Button>();
            Image image = buttonObj.AddComponent<Image>();

            buttonObj.transform.SetParent(canvas.transform, false);
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

    void ButtonClicked()
    {
        Debug.Log("Button Clicked!");
    }
}
