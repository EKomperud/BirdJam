using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    #region Member Variables
    // Inspector Variables
    [Header("Speed Settings")]
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float maxRotation;
    [SerializeField] private float rotateSpeed;

    // Inspector References
    [Header("References")]
    [SerializeField] private Transform poopPrefab;

    // References
    BoxCollider bxc;

    // Hidden Variables
    private float currentSpeed;
    private Vector3 direction;
    private float rotateFactor;
    #endregion

    #region MonoBehaviour
    void Start ()
    {
        direction = new Vector3(0, 0, 1);
        currentSpeed = defaultSpeed;
        bxc = GetComponent<BoxCollider>();
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            AttemptPoop();
    }

    private void AttemptPoop()
    {

    }
    #endregion

    #region Movement Updates
    private void FixedUpdate()
    {
        DoRotation();
        DoAcceleration();

        transform.position += (direction * currentSpeed * Time.deltaTime);
    }

    private void DoRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            rotateFactor = rotateFactor >= -maxRotation ? rotateFactor -= rotateSpeed : -maxRotation;
        else if (Input.GetKey(KeyCode.RightArrow))
            rotateFactor = rotateFactor <= maxRotation ? rotateFactor += rotateSpeed : maxRotation;
        else
            rotateFactor = Mathf.Abs(rotateFactor) >= 0.05f ? rotateFactor *= 0.90f : 0;
        transform.Rotate(new Vector3(0f, rotateFactor, 0f));

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
        if (Input.GetKey(KeyCode.UpArrow))
            currentSpeed = currentSpeed <= maxSpeed ? currentSpeed += acceleration : maxSpeed;
        else
            currentSpeed = currentSpeed >= defaultSpeed ? currentSpeed -= acceleration : defaultSpeed;
    }
    #endregion
}
