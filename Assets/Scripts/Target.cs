using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    GameManager gm;
    [SerializeField] bool pickup;

    private void Start()
    {
        GameManager.TryGetInstance(out gm);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            if (pickup)
                gm.collidedPickUp(transform.parent.gameObject);
            else
                gm.collidedDropOff(transform.parent.gameObject);
        }
    }

}
