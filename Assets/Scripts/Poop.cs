using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : MonoBehaviour {

    [SerializeField] private float lifetime;

    private Vector3 direction;

    private void FixedUpdate()
    {
        direction *= 0.9f;

        transform.position += new Vector3(direction.x, -0.5f, direction.z) * Time.deltaTime;
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
            Destroy(gameObject);
    }

    public void SpawnPoop(Vector3 position, Vector3 direction)
    {
        transform.position = position;
        this.direction = direction;
    }
}
