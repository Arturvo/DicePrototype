using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

public class DieSetup : MonoBehaviour
{
    [Tooltip("Number assigned to a newly created side number.")]
    [SerializeField] private int defaultNumber = 1;
    [SerializeField] private DieSideNumber sideNumberPrefab;
    [Tooltip("Parent object of newly created side numbers. If not specified, new one will be created.")]
    [SerializeField] private Transform sideNumberParent;

    public List<DieSideNumber> SideNumbers { get => sideNumbers; }

    private List<DieSideNumber> sideNumbers;

    public static string sideNumberNamePrefix = "Number";

    private const string sideNumberParentName = "SideNumbers";

    private readonly Vector3 numberSpawnPosition = new Vector3(0, 0, -1.5f);

    private void Awake()
    {
        CreateSideNumberList();
    }

    private void OnValidate()
    {
        Assert.IsNotNull(sideNumberPrefab, "Side number prefab is not assigned");
    }

    public void CreateSideNumber()
    {
        CreateSideNumberParent();
        GameObject sideNumberObject = (GameObject)PrefabUtility.InstantiatePrefab(sideNumberPrefab.gameObject, sideNumberParent);
        DieSideNumber sideNumber = sideNumberObject.GetComponent<DieSideNumber>();
        sideNumberObject.transform.localPosition = numberSpawnPosition;
        sideNumber.Init(defaultNumber);
    }

    public void RemoveAllSideNumbers()
    {
        foreach (DieSideNumber sideNumber in sideNumbers)
        {
            if (sideNumber != null)
            {
                DestroyImmediate(sideNumber.gameObject);
            }
        }
        sideNumbers = null;
    }

    private void CreateSideNumberParent()
    {
        if (sideNumberParent == null)
        {
            sideNumberParent = new GameObject(sideNumberParentName).transform;
            sideNumberParent.SetParent(transform);
            sideNumberParent.localPosition = Vector3.zero;
        }
    }

    private void CreateSideNumberList()
    {
        sideNumbers = new List<DieSideNumber>();
        foreach (Transform sideNumberObject in sideNumberParent)
        {
            sideNumbers.Add(sideNumberObject.GetComponent<DieSideNumber>());
        }
    }
}
