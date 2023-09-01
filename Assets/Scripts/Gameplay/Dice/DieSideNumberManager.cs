using TMPro;
using UnityEngine;

public class DieSideNumberManager : MonoBehaviour
{
    [SerializeField] private TextMeshPro numberText;
    [SerializeField] private int number;
    [SerializeField] private string numberSufix = "";
    [SerializeField] private string numberPrefix = "";

    public int Number { get => number; }
    public Vector3 Position { get => transform.position; }

    private const float distanceFromSnappedMesh = 0.01f;
    private const float additionalRaycastDistance = 1f;

    private void OnValidate()
    {
         UpdateNumber();
    }

    private void Awake()
    {
        UpdateNumber();
    }

    public void Init(int number)
    {
        this.number = number;
        UpdateNumber();
    }

    public void SnapToMesh()
    {
        Vector3 vectorToParentPosition = transform.parent.transform.position - transform.position;
        Vector3 raycastOrigin = transform.position - vectorToParentPosition.normalized * additionalRaycastDistance;
        if (Physics.Raycast(raycastOrigin, vectorToParentPosition, out var hit, Mathf.Infinity))
        {
            transform.position = hit.point + hit.normal * distanceFromSnappedMesh;
            transform.LookAt(transform.position - hit.normal);
        }
        else
        {
            Debug.LogError("No mesh was found to snap to");
        }
    }

    private void UpdateNumber()
    {
        numberText.text = $"{numberPrefix}{number}{numberSufix}";
        gameObject.name = DieSetupManager.sideNumberNamePrefix + number;
    }
}
