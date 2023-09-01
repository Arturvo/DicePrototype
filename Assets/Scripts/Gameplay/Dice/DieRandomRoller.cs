using UnityEngine;

[RequireComponent(typeof(DieRollManager), typeof(Rigidbody))]
public class DieRandomRoller : MonoBehaviour
{
    [Tooltip("Roll strength will be picked randomly within this range.")]
    [SerializeField] private Vector2 rollStrengthRange = new Vector2(2f,4f);
    [Tooltip("Horizontal angle of deviation from Vector3.forward(0,0,1) will be picked randomly within this range.")]
    [SerializeField] private Vector2 horizontalAngleRange = new Vector2(-80f, 80f);
    [Tooltip("Vertical angle of deviation from Vector3.forward(0,0,1) will be picked randomly within this range.")]
    [SerializeField] private Vector2 verticalAngleRange = new Vector2(10f, 30f);

    private DieRollManager dieRollManager;
    private Rigidbody dieRigidBody;
    private Vector3 rollStartPosition;
    private Transform transformCashed;

    private void Awake()
    {
        transformCashed = transform;
        rollStartPosition = transformCashed.position;
        dieRollManager = GetComponent<DieRollManager>();
        dieRigidBody = GetComponent<Rigidbody>();
    }

    public void RollDie()
    {
        if (dieRollManager.IsDieRolling) return;

        transformCashed.position = rollStartPosition;
        transformCashed.rotation = Random.rotation;

        Vector3 rollDirection = GetRandomDirection();
        float rollStrength = Random.Range(rollStrengthRange.x, rollStrengthRange.y);

        dieRollManager.StartRolling();
        dieRigidBody.AddForce(rollDirection * rollStrength, ForceMode.Impulse);
    }

    private Vector3 GetRandomDirection()
    {
        float horizontalRotationAngle = Random.Range(horizontalAngleRange.x, horizontalAngleRange.y);
        float verticalRotationAngle = Random.Range(verticalAngleRange.x, verticalAngleRange.y);
        Vector3 rollDirection = Quaternion.AngleAxis(verticalRotationAngle, Vector3.right) * (Quaternion.AngleAxis(horizontalRotationAngle, Vector3.up) * Vector3.forward);
        return rollDirection.normalized;
    }
}
