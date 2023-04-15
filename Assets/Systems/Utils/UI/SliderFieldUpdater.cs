using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderFieldUpdater : MonoBehaviour
{
    [SerializeField] private Slider slider;

    [SerializeField] private TMP_InputField inputField;

    private void Start()
    {
        if (!slider || !inputField)
        {
            Debug.LogError("Slider or InputField is null");
            return;
        }

        slider.onValueChanged.AddListener(UpdateInputFieldOnSliderValueChanged);
        inputField.onValueChanged.AddListener(UpdateSliderOnInputFieldValueChanged);
        inputField.onEndEdit.AddListener(UpdateSliderAndInputFieldValueOnEndEdit);

        slider.onValueChanged.Invoke(slider.value);
        UpdateInputFieldOnSliderValueChanged(slider.value);
    }

    private void OnDestroy()
    {
        slider?.onValueChanged.RemoveListener(UpdateInputFieldOnSliderValueChanged);
        inputField?.onValueChanged.RemoveListener(UpdateSliderOnInputFieldValueChanged);
        inputField.onEndEdit.RemoveListener(UpdateSliderAndInputFieldValueOnEndEdit);
    }

    private void UpdateInputFieldOnSliderValueChanged(float input)
    {
        inputField.SetTextWithoutNotify(input.ToString());
    }

    private void UpdateSliderOnInputFieldValueChanged(string input)
    {
        if (!int.TryParse(input, out int value))
            return;

        slider.value = value;
    }

    private void UpdateSliderAndInputFieldValueOnEndEdit(string input)
    {
        var clampInput = ClampInput(input);
        slider.SetValueWithoutNotify(clampInput);
        inputField.SetTextWithoutNotify(clampInput.ToString());
    }

    private int ClampInput(float input)
    {
        return Mathf.RoundToInt(Mathf.Clamp(input, slider.minValue, slider.maxValue));
    }

    private int ClampInput(string input)
    {
        if (int.TryParse(input, out int value))
            return ClampInput(value);

        return ClampInput(0);
    }
}