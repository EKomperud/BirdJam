using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public bool shaking = false;
    private Vector3 defaultPosition;

    private void Start()
    {
        defaultPosition = transform.localPosition;
    }

    public void StartShake()
    {
        StartCoroutine("Shake");
    }

    private IEnumerator Shake()
    {
        while (shaking)
        {
            if (transform.localPosition == defaultPosition)
            {
                Vector3 shakeDirection = new Vector3(Random.Range(-0.01f, 0.01f), Random.Range(-0.01f, 0.01f), Random.Range(-0.01f, 0.01f));
                transform.localPosition += shakeDirection;
            }
            else
            {
                transform.localPosition = defaultPosition;
            }
            yield return new WaitForSeconds(0.1f);
        }
        transform.localPosition = defaultPosition;
    }
}
