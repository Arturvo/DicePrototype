using UnityEngine;
using UnityEngine.UI;

public class RollDiceUI : MonoBehaviour
{
    [SerializeField] private DieRandomRoller dieRandomRoller;
    [SerializeField] private Button rollButton;

    private void OnEnable()
    {
        rollButton.onClick.AddListener(dieRandomRoller.RollDie);
    }

    private void OnDisable()
    {
        rollButton.onClick.RemoveListener(dieRandomRoller.RollDie);
    }
}
