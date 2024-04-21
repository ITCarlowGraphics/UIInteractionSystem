using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    public UIInteractionSystem Instance;

    [SerializeField]
    private GameObject _volumeSlider;
    private GameObject _volumeSliderNumberText;
    private GameObject _questionTimeLimitSlider;
    private GameObject _questionTimeLimitNumberText;

    // Start is called before the first frame update
    void Start()
    {
        Instance = FindObjectOfType<UIInteractionSystem>();

        // volume init
        _volumeSlider = GameObject.Find("VolumeSlider");
        _volumeSliderNumberText = GameObject.Find("VolumeNumberText");
        if (Instance != null )
        {
            _volumeSlider.GetComponent<Slider>().value = Instance.soundVolumeCoefficient * 100.0f;
            _volumeSliderNumberText.GetComponent<TextMeshProUGUI>().text = _volumeSlider.GetComponent<Slider>().value.ToString("0") + "%";
        }
        // question time init
        _questionTimeLimitSlider = GameObject.Find("QuestionTimeLimitSlider");
        _questionTimeLimitNumberText = GameObject.Find("QuestionTimeLimitNumberText");
        if (Instance != null )
        {
            _questionTimeLimitSlider.GetComponent<Slider>().value = Instance.questionTimeLimit;
            _questionTimeLimitNumberText.GetComponent<TextMeshProUGUI>().text = _questionTimeLimitSlider.GetComponent<Slider>().value.ToString("0") + " seconds";
        }

        _volumeSlider.GetComponent<Slider>().onValueChanged.AddListener((v) =>
        {
            Instance.soundVolumeCoefficient = _volumeSlider.GetComponent<Slider>().value / 100.0f;
            _volumeSliderNumberText.GetComponent<TextMeshProUGUI>().text = _volumeSlider.GetComponent<Slider>().value.ToString("0") + "%";
        });

        _questionTimeLimitSlider.GetComponent<Slider>().onValueChanged.AddListener((v) =>
        {
            Instance.questionTimeLimit = (int)_questionTimeLimitSlider.GetComponent<Slider>().value;
            _questionTimeLimitNumberText.GetComponent<TextMeshProUGUI>().text = _questionTimeLimitSlider.GetComponent<Slider>().value.ToString("0") + " seconds";
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
