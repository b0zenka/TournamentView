using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class ProgressSlider : MonoBehaviour
{
    [SerializeField] private float smooth = 10f;

    private Slider slider;

    private TMP_Text sliderText;

    private float currentProgress;

    private void Start()
    {
        slider = GetComponent<Slider>();
        sliderText = GetComponentInChildren<TMP_Text>();

        slider.value = 0;
        sliderText.text = string.Empty;

        TournamentTable.onUpdateTournamentProgress += OnUpdateProgress;
    }

    private void OnDestroy()
    {
        TournamentTable.onUpdateTournamentProgress -= OnUpdateProgress;
    }

    private void Update()
    {
        if (slider.value == currentProgress) return;

        slider.value = Mathf.Lerp(slider.value, currentProgress, Time.deltaTime * smooth);
        DisplayProgresValue(slider.value);
    }

    private void OnUpdateProgress(float progress)
    {
        currentProgress = progress;
    }

    private void DisplayProgresValue(in float progress)
    {
        int value = Mathf.RoundToInt(progress * 100);
        sliderText.text = $"{value}%";
    }
}