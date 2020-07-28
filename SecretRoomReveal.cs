using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretRoomReveal : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collisionInfo)
    {
        if(collisionInfo.CompareTag("Player")){
            transform.gameObject.SetActive(false);
        }
    }

}
