using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    #region Member Variables
    // Inspector Variables
    [Header("Movement/Rotation Settings")]
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float maxRotationY;
    [SerializeField] private float rotateSpeedY;
    [SerializeField] private float maxRotationX;
    [SerializeField] private float rotateSpeedX;
    [SerializeField] private float maxHeight;
    [SerializeField] private float minHeight;

    // Inspector References
    [Header("References")]
    [SerializeField] private Transform poopPrefab;
    [SerializeField] private GameData data;
    [SerializeField] private Transform arrow;
    [SerializeField] private Transform letter;

    // References
    private BoxCollider bxc;
    private GameManager gm;

    // Hidden Variables
    private float currentSpeed;
    private Vector3 direction;
    private float rotateFactorY;
    private float rotateFactorX;
    private AudioSource aSource;
    #endregion

    #region MonoBehaviour
    void Start ()
    {
        direction = new Vector3(0, 0, 1);
        currentSpeed = defaultSpeed;
        bxc = GetComponent<BoxCollider>();
        aSource = GetComponent<AudioSource>();
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
        float poopSize = gm.AttemptPoop();
        if (poopSize != 0)
        {
            Transform p = Instantiate(poopPrefab) as Transform;
            Poop poop = p.GetComponent<Poop>();
            poop.SpawnPoop(new Vector3(transform.position.x, transform.position.y, transform.position.z), direction, poopSize, gm);
           switch((int)poopSize)
            {
                case 1:
                case 2:
                    AudioManager.instance.PlayPoopLaunchSound(aSource, 0);
                    break;
                case 3:
                case 4:
                    AudioManager.instance.PlayPoopLaunchSound(aSource, 1);
                    break;
                case 5:
                case 6:
                    AudioManager.instance.PlayPoopLaunchSound(aSource, 2);
                    break;
            }
        }
    }
    #endregion

    #region Movement Updates
    private void FixedUpdate()
    {
        DoRotation();
        DoArrowUpdate();

        transform.position += (direction * currentSpeed * Time.deltaTime);
    }

    private void DoRotation()
    {
        // Y Rotation
        if (Input.GetKey(KeyCode.LeftArrow))
            rotateFactorY = rotateFactorY >= -maxRotationY ? rotateFactorY -= rotateSpeedY : -maxRotationY;
        else if (Input.GetKey(KeyCode.RightArrow))
            rotateFactorY = rotateFactorY <= maxRotationY ? rotateFactorY += rotateSpeedY : maxRotationY;
        else
            rotateFactorY = Mathf.Abs(rotateFactorY) >= 0.05f ? rotateFactorY *= 0.90f : 0;
        float rY = transform.localRotation.eulerAngles.y + rotateFactorY;

        // Z Rotation and Acceleration
        bool upKey = Input.GetKey(KeyCode.UpArrow);
        bool downKey = Input.GetKey(KeyCode.DownArrow);
        float Y = 0f;
        if (upKey)
        {
            float eX = transform.localRotation.eulerAngles.x;
            currentSpeed = currentSpeed <= maxSpeed ? currentSpeed += acceleration : maxSpeed;
            rotateFactorX = (eX < maxRotationX) || (eX > 360 - maxRotationX * 1.5f ) ? rotateFactorX += rotateSpeedX : 0;
            if (transform.position.y >= minHeight)
                Y = -1f;
        }
        else if (downKey)
        {
            float eX = transform.localRotation.eulerAngles.x;
            currentSpeed = currentSpeed >= minSpeed ? currentSpeed -= acceleration : minSpeed;
            rotateFactorX = (eX > 360f - maxRotationX) || (eX <= maxRotationX * 1.5f) ? rotateFactorX -= rotateSpeedX : 0;
            if (transform.position.y <= maxHeight)
                Y = 1f;
        }
        else
        {
            currentSpeed = currentSpeed >= defaultSpeed ? currentSpeed -= acceleration : defaultSpeed;
            float eX = transform.localRotation.eulerAngles.x;
            if (eX > 1f && eX < 15f)
                rotateFactorX = -(eX * 0.05f);
            else if (eX < 359 && eX > 345f)
                rotateFactorX = ((360 - eX) * 0.05f);
            else
                rotateFactorX = 0f;
        }
        gm.PlayerDash(upKey);
        float rX = transform.localRotation.eulerAngles.x + rotateFactorX;
        transform.localRotation = Quaternion.Euler(rX, rY, transform.localRotation.z);

        float yAngle = transform.rotation.eulerAngles.y % 360;
        float ratio = Mathf.Tan(yAngle * Mathf.Deg2Rad);
        float X = Mathf.Abs(ratio), Z = 1;
        Vector3 newDir;

        if (yAngle >= 0 && yAngle < 90)
            newDir = new Vector3(X, Y, Z);
        else if (yAngle >= 90 && yAngle < 180)
            newDir = new Vector3(X, Y, -Z);
        else if (yAngle >= 180 && yAngle < 270)
            newDir = new Vector3(-X, Y, -Z);
        else if (yAngle >= 270 && yAngle < 360)
            newDir = new Vector3(-X, Y, Z);
        else
            newDir = new Vector3();

        if (newDir == Vector3.zero)
            newDir = new Vector3(0f, 0f, 1f);

        direction = newDir.normalized;
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

    public void Delivery(bool pickup)
    {
        letter.gameObject.SetActive(pickup);
    }

    #endregion
}
