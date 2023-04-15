using UnityEngine;

public class SetIterations : MonoBehaviour
{
    public void OnChangeIterations(float value)
    {
        var iterations = Mathf.RoundToInt(value);
        TournamentConfig.Instance.Iterations = iterations;

        Debug.Log($"Set iterations to: {value}");
    }
}