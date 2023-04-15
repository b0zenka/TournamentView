using UnityEngine;

[CreateAssetMenu(fileName = "New ResultRowSettings", menuName = "Create ResultRowSettings")]
public class ResultRowSettings : ScriptableObject
{
    [Header("Main Row Color")]
    [SerializeField] private Color oddMainColor = Color.white;
    [SerializeField] private Color evenMainColor = Color.white;

    [Header("Text Colors")]
    [SerializeField] private Color winTextColor = Color.white;
    [SerializeField] private Color defaultTextColor = Color.white;

    public Color GetMainColor(int childNumber) => childNumber % 2 == 0 ? oddMainColor : evenMainColor;

    public Color GetTextColor(bool isWin) => isWin ? winTextColor : defaultTextColor;
}
