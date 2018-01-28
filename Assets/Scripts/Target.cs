using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    GameManager gm;
    AudioSource aSource;
    [SerializeField] bool pickup;

    private void Start()
    {
        GameManager.TryGetInstance(out gm);
        aSource = GetComponentInParent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            if (pickup)
            {
                gm.collidedPickUp(transform.parent.gameObject);
            }
            else
            { gm.collidedDropOff(transform.parent.gameObject); 
                AudioManager.instance.PlayThankyouSound(aSource);
            }

        }
    }

}
