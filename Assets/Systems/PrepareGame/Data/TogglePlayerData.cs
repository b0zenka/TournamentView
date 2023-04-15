using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Toggle))]
public class TogglePlayerData : MonoBehaviour, ITogglePlayerData
{
    private Toggle toggle;

    private TMP_Text txtPlayerName;
    private Action onToggleChanged;

    public string PlayerPath { get; private set; }

    public bool IsFree => !gameObject.activeSelf;

    public bool IncludedInTournament
    {
        get => toggle.isOn;
        set => toggle.isOn = value;
    }

    private void Start()
    {
        toggle.onValueChanged.AddListener(ToggleValueChanged);
    }

    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(ToggleValueChanged);
    }

    public void SetupToggle(string path, Action onToggleChanged)
    {
        if (!toggle || !txtPlayerName)
            InitComponents();

        PlayerPath = path;
        toggle.isOn = true;
        txtPlayerName.text = PathHelper.GetFileNameFromPath(path);

        gameObject.SetActive(true);

        this.onToggleChanged = onToggleChanged;
        ToggleValueChanged(toggle.isOn);
    }

    public void Free()
    {
        if (!toggle || !txtPlayerName)
            InitComponents();

        toggle.isOn = false;
        PlayerPath = string.Empty;
        gameObject.SetActive(false);
    }

    private void InitComponents()
    {
        toggle ??= GetComponent<Toggle>();
        txtPlayerName ??= GetComponentInChildren<TMP_Text>();
    }

    private void ToggleValueChanged(bool isOn)
    {
        onToggleChanged?.Invoke();
    }
}
