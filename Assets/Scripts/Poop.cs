using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : MonoBehaviour {

    [SerializeField] private float lifetime;

    private float size;
    private Vector3 direction;
    private float speed;
    private GameManager gm;

    private void FixedUpdate()
    {
        //direction *= 0.9f;

        transform.position += new Vector3(direction.x, -1f, direction.z) * speed * Time.deltaTime;
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
            Destroy(gameObject);
    }

    public void SpawnPoop(Vector3 position, Vector3 direction, float size, float speed, GameManager gm)
    {
        transform.position = position;
        this.direction = direction;
        transform.localScale = transform.localScale *= size;
        this.size = size;
        this.gm = gm;
        this.speed = speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Successful poop");
        NPC npc;
        npc = other.gameObject.GetComponent<NPC>();
        if (npc != null)
        {
            gm.collidedPoop(other.gameObject);
            Destroy(gameObject);
        }
    }
}
