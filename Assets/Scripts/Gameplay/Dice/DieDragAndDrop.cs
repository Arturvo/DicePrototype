using UnityEngine;

[RequireComponent(typeof(DieRollController), typeof(Rigidbody))]
public class DieDragAndDrop : MonoBehaviour
{
    [SerializeField] private float dragStrength = 15f;
    [SerializeField] private float spinStrength = 0.2f;
    [SerializeField] private float maxVelocity = 50f;
    [Tooltip("A roll will not be started if die is released with lower velocity than this value.")]
    [SerializeField] private float minVelocityToRoll = 5f;
    [SerializeField] private int desktopBorderLayerIndex = 9;
    [Tooltip("Limits the ability to push the die into the border to prevent collider issues.")]
    [SerializeField] private float minDistanceFromBorder = 1.5f;

    private float clickPoisitionDepth;
    private DieRollController dieRollController;
    private Rigidbody dieRigidBody;
    private Transform transformCashed;
    private bool isDieDragged;
    private int desktopBorderLayerMask;

    private void Awake()
    {
        dieRollController = GetComponent<DieRollController>();
        dieRigidBody = GetComponent<Rigidbody>();
        desktopBorderLayerMask = 1 << desktopBorderLayerIndex;
        transformCashed = transform;
    }

    void OnMouseDown()
    {
        if (dieRollController.IsDieRolling) return;

        isDieDragged = true;
        clickPoisitionDepth = Camera.main.WorldToScreenPoint(transformCashed.position).z;
    }

    void OnMouseUp()
    {
        bool isRollValid = isDieDragged && !dieRollController.IsDieRolling && dieRigidBody.velocity.magnitude > minVelocityToRoll;

        if (isRollValid) 
        {
            dieRollController.StartRolling();
        }

        isDieDragged = false;
    }

    void OnMouseDrag()
    {
        if (!isDieDragged || dieRollController.IsDieRolling) return;

        Vector3 targetPosition = CalculateTargetDragPosition();
        Vector3 dieVelocity = CalculateDieVelocity(targetPosition);
        Vector3 dieTorque = CalculateDieTorque(dieVelocity);

        dieRigidBody.velocity = dieVelocity;
        dieRigidBody.AddTorque(dieTorque);
    }

    private Vector3 CalculateTargetDragPosition()
    {
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, clickPoisitionDepth);
        return Camera.main.ScreenToWorldPoint(cursorPoint);
    }

    private Vector3 CalculateDieVelocity(Vector3 targetPosition)
    {
        bool isDieTooCloseToTheWall = IsDieTooCloseToTheWall(targetPosition);
        Vector3 targetVelocity = isDieTooCloseToTheWall ? Vector3.zero : (targetPosition - transformCashed.position) * dragStrength;
        return Vector3.ClampMagnitude(targetVelocity, maxVelocity);
    }

    private Vector3 CalculateDieTorque(Vector3 dieVelocity)
    {
        Vector3 torqueDirectionVector = Vector3.Cross(dieVelocity * spinStrength, Vector3.down);
        return torqueDirectionVector * spinStrength;
    }

    private bool IsDieTooCloseToTheWall(Vector3 targetPosition)
    {
        if (Physics.Raycast(transformCashed.position, targetPosition - transformCashed.position, out var hit, Mathf.Infinity, desktopBorderLayerMask))
        {
            return hit.distance < minDistanceFromBorder;
        }

        return false;
    }
}
