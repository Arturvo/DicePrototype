using TMPro;
using UnityEngine;

public class RollResultsUI : MonoBehaviour
{
    [SerializeField] private DieRollManager dieRollManager;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private TextMeshProUGUI totalText;
    [SerializeField] private string resultTextPrefix = "Result:";
    [SerializeField] private string resultUnknownSymbol = "?";
    [SerializeField] private string resultEmptySymbol = "-";
    [SerializeField] private string totalTextPrefix = "Total:";

    private void OnEnable()
    {
        dieRollManager.DieStartedRolling += OnRollStarted;
        dieRollManager.DieStoppedRolling += OnRollFinished;
    }

    private void Start()
    {
        resultText.text = $"{resultTextPrefix} {resultEmptySymbol}";
        totalText.text = $"{totalTextPrefix} {dieRollManager.TotalRollResultSum}";
    }

    private void OnDisable()
    {
        dieRollManager.DieStartedRolling -= OnRollStarted;
        dieRollManager.DieStoppedRolling -= OnRollFinished;
    }

    private void OnRollStarted()
    {
        resultText.text = $"{resultTextPrefix} {resultUnknownSymbol}";
    }

    private void OnRollFinished(int result)
    {
        resultText.text = $"{resultTextPrefix} {result}";
        totalText.text = $"{totalTextPrefix} {dieRollManager.TotalRollResultSum}";
    }
}
