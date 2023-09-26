using TMPro;
using UnityEngine;

public class RollResultsUI : MonoBehaviour
{
    [SerializeField] private DieRollController dieRollController;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private TextMeshProUGUI totalText;
    [SerializeField] private string resultTextPrefix = "Result:";
    [SerializeField] private string resultUnknownSymbol = "?";
    [SerializeField] private string resultEmptySymbol = "-";
    [SerializeField] private string totalTextPrefix = "Total:";

    private void OnEnable()
    {
        dieRollController.DieStartedRolling += OnRollStarted;
        dieRollController.DieStoppedRolling += OnRollFinished;
    }

    private void Start()
    {
        resultText.text = $"{resultTextPrefix} {resultEmptySymbol}";
        totalText.text = $"{totalTextPrefix} {dieRollController.TotalRollResultSum}";
    }

    private void OnDisable()
    {
        dieRollController.DieStartedRolling -= OnRollStarted;
        dieRollController.DieStoppedRolling -= OnRollFinished;
    }

    private void OnRollStarted()
    {
        resultText.text = $"{resultTextPrefix} {resultUnknownSymbol}";
    }

    private void OnRollFinished(int result)
    {
        resultText.text = $"{resultTextPrefix} {result}";
        totalText.text = $"{totalTextPrefix} {dieRollController.TotalRollResultSum}";
    }
}
