using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Reflection;

public class ToggleSwitch : MonoBehaviour, IPointerClickHandler
{
    [SerializeField, Range(0, 1f)] private float sliderValue;

    public Image backgroundImage;
    public TextMeshProUGUI statusText;

    public bool currentValue { get; private set; }

    private Slider slider;

    private float animationDuration = 0.25f;

    [SerializeField]
    private AnimationCurve slideEase =
        AnimationCurve.EaseInOut(0, 0, 1, 1);

    private Coroutine animateSliderCoroutine;

    [Header("Dynamic Boolean Settings")]
    [SerializeField]
    private string targetBoolean; // Name of the boolean property to modify.

    protected virtual void OnValidate()
    {
        SetupToggleComponents();

        slider.value = 0;
    }

    private void SetupToggleComponents()
    {
        if (slider != null)
            return;

        SetupSliderComponent();
    }

    private void SetupSliderComponent()
    {
        slider = GetComponent<Slider>();

        if (slider == null)
        {
            Debug.Log("No slider found!", this);
            return;
        }

        slider.interactable = false;
        var sliderColors = slider.colors;
        sliderColors.disabledColor = Color.white;
        slider.colors = sliderColors;
        slider.transition = Selectable.Transition.None;
    }

    protected virtual void Awake()
    {
        SetupSliderComponent();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Toggle();
    }

    public void Toggle()
    {
        SetStateAndStartAnimation(!currentValue);
    }

    private void SetStateAndStartAnimation(bool state)
    {
        currentValue = state;

        // Dynamically update the boolean in UIInteractionSystem.
        if (UIInteractionSystem.Instance != null && !string.IsNullOrEmpty(targetBoolean))
        {
            var targetField = UIInteractionSystem.Instance.GetType().GetField(targetBoolean, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (targetField != null && targetField.FieldType == typeof(bool))
            {
                targetField.SetValue(UIInteractionSystem.Instance, currentValue);
            }
            else
            {
                Debug.LogWarning($"Field '{targetBoolean}' not found or is not a boolean.");
            }
        }

        if (animateSliderCoroutine != null)
            StopCoroutine(animateSliderCoroutine);

        animateSliderCoroutine = StartCoroutine(AnimateSlider());
    }

    private IEnumerator AnimateSlider()
    {
        float startValue = slider.value;
        float endValue = currentValue ? 1 : 0;

        float time = 0;
        if (animationDuration > 0)
        {
            while (time < animationDuration)
            {
                time += Time.deltaTime;

                float lerpFactor = slideEase.Evaluate(time / animationDuration);
                slider.value = sliderValue = Mathf.Lerp(startValue, endValue, lerpFactor);

                yield return null;
            }
        }

        slider.value = endValue;

        if (endValue > 0)
        {
            backgroundImage.color = new Color32(62, 166, 0, 255);
            statusText.text = "On";
        }
        else
        {
            backgroundImage.color = new Color32(166, 6, 0, 255);
            statusText.text = "Off";
        }
    }
}
