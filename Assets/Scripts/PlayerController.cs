using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    #region Member Variables
    // Inspector Variables
    [Header("Movement/Rotation Settings")]
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float maxRotationX;
    [SerializeField] private float rotateSpeedX;
    [SerializeField] private float maxRotationZ;
    [SerializeField] private float rotateSpeedZ;

    // Inspector References
    [Header("References")]
    [SerializeField] private Transform poopPrefab;
    [SerializeField] private GameData data;
    [SerializeField] private Transform arrow;

    // References
    private BoxCollider bxc;
    private GameManager gm;

    // Hidden Variables
    private float currentSpeed;
    private Vector3 direction;
    private float rotateFactorX;
    private float rotateFactorZ;
    #endregion

    #region MonoBehaviour
    void Start ()
    {
        direction = new Vector3(0, 0, 1);
        currentSpeed = defaultSpeed;
        bxc = GetComponent<BoxCollider>();

        GameManager.TryGetInstance(out gm);
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            AttemptPoop();

        if (data.poopSize >= data.maxPoopSize)
            AttemptPoop();
    }

    private void AttemptPoop()
    {
        float poopSize = gm.TryPoop();
        if (poopSize != 0)
        {
            Transform p = Instantiate(poopPrefab) as Transform;
            Poop poop = p.GetComponent<Poop>();
            poop.SpawnPoop(new Vector3(transform.position.x, transform.position.y, transform.position.z), direction, poopSize);
        }
    }
    #endregion

    #region Movement Updates
    private void FixedUpdate()
    {
        DoRotation();
        DoAcceleration();
        DoArrowUpdate();

        transform.position += (direction * currentSpeed * Time.deltaTime);
    }

    private void DoRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            rotateFactorX = rotateFactorX >= -maxRotationX ? rotateFactorX -= rotateSpeedX : -maxRotationX;
        else if (Input.GetKey(KeyCode.RightArrow))
            rotateFactorX = rotateFactorX <= maxRotationX ? rotateFactorX += rotateSpeedX : maxRotationX;
        else
            rotateFactorX = Mathf.Abs(rotateFactorX) >= 0.05f ? rotateFactorX *= 0.90f : 0;
        transform.Rotate(new Vector3(0f, rotateFactorX, 0f));

        float yAngle = transform.rotation.eulerAngles.y % 360;
        float ratio = Mathf.Tan(yAngle * Mathf.Deg2Rad);
        float X = Mathf.Abs(ratio), Z = 1;
        Vector3 newDir;

        if (yAngle >= 0 && yAngle < 90)
            newDir = new Vector3(X, 0f, Z);
        else if (yAngle >= 90 && yAngle < 180)
            newDir = new Vector3(X, 0f, -Z);
        else if (yAngle >= 180 && yAngle < 270)
            newDir = new Vector3(-X, 0f, -Z);
        else if (yAngle >= 270 && yAngle < 360)
            newDir = new Vector3(-X, 0f, Z);
        else
            newDir = new Vector3();

        if (newDir == Vector3.zero)
            newDir = new Vector3(0f, 0f, 1f);

        direction = newDir.normalized;
    }

    private void DoAcceleration()
    {
        bool upKey = Input.GetKey(KeyCode.UpArrow);
        if (upKey)
            currentSpeed = currentSpeed <= maxSpeed ? currentSpeed += acceleration : maxSpeed;
        else
            currentSpeed = currentSpeed >= defaultSpeed ? currentSpeed -= acceleration : defaultSpeed;

        gm.PlayerDash(upKey);
    }

    private void DoArrowUpdate()
    {
        arrow.transform.LookAt(data.target, Vector3.up);
    }
    #endregion

    #region External Functions

    public float GetSpeed()
    {
        return currentSpeed;
    }

    #endregion
}
