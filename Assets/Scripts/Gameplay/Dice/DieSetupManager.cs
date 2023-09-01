using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

public class DieSetupManager : MonoBehaviour
{
    [Tooltip("Number assigned to a newly created side number.")]
    [SerializeField] private int defaultNumber = 1;
    [SerializeField] private DieSideNumberManager sideNumberPrefab;
    [Tooltip("Parent object of newly created side numbers. If not specified, new one will be created.")]
    [SerializeField] private Transform sideNumberParent;

    public List<DieSideNumberManager> SideNumbers { get => sideNumbers; }

    private List<DieSideNumberManager> sideNumbers;

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
        DieSideNumberManager sideNumber = sideNumberObject.GetComponent<DieSideNumberManager>();
        sideNumberObject.transform.localPosition = numberSpawnPosition;
        sideNumber.Init(defaultNumber);
    }

    public void RemoveAllSideNumbers()
    {
        foreach (DieSideNumberManager sideNumber in sideNumbers)
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
        sideNumbers = new List<DieSideNumberManager>();
        foreach (Transform sideNumberObject in sideNumberParent)
        {
            sideNumbers.Add(sideNumberObject.GetComponent<DieSideNumberManager>());
        }
    }
}
