using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(DieSetup), typeof(Rigidbody))]
public class DieRollController : MonoBehaviour
{
    [Tooltip("A roll can only be completed after this time has passed since it started.")]
    [SerializeField] private float minRollTime = 0.1f;
    [Tooltip("A roll is finished when die velocity is lower than this value.")]
    [SerializeField] private float maxStoppedVelocity = 0.001f;

    public event Action DieStartedRolling;
    public event Action<int> DieStoppedRolling;

    public bool IsDieRolling { get => isDieRolling; }
    public int TotalRollResultSum { get => totalRollResultSum; }

    private DieSetup dieSetup;
    private Rigidbody dieRigidBody;
    private bool isDieRolling;
    private bool didMinRollTimePass;
    private int totalRollResultSum;

    private void Awake()
    {
        dieSetup = GetComponent<DieSetup>();
        dieRigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (isDieRolling && didMinRollTimePass && dieRigidBody.velocity.magnitude < maxStoppedVelocity)
        {
            FinishRolling();
        }
    }

    public void StartRolling()
    {
        isDieRolling = true;
        didMinRollTimePass = false;
        StartCoroutine(WaitMinRollTime());
        DieStartedRolling?.Invoke();
    }

    private void FinishRolling()
    {
        isDieRolling = false;
        int rollResult = GetRollResult();
        totalRollResultSum += rollResult;
        DieStoppedRolling?.Invoke(rollResult);
    }

    private IEnumerator WaitMinRollTime()
    {
        yield return new WaitForSeconds(minRollTime);
        didMinRollTimePass = true;
    }

    private int GetRollResult()
    {
        DieSideNumber topNumber = null;

        foreach (DieSideNumber sideNumber in dieSetup.SideNumbers)
        {
            if (topNumber == null || sideNumber.Position.y > topNumber.Position.y)
            {
                topNumber = sideNumber;
            }
        }

        return topNumber.Number;
    }
}
