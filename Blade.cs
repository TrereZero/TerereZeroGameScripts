using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public float waitTime;

    private float waitTimeVar;

    public float bladeVelocity;

    public float direction;

    public bool horizontalDirection;

    private Rigidbody2D rigidbody2D;

    public bool stopped;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        waitTimeVar = waitTime;
    }

    void FixedUpdate()
    {
        Move();
    }
    void Update()
    {
        // Controla o temporizador do período de parada da lamina
        if(stopped){
            waitTimeVar -= Time.deltaTime;
        }
    }

    void Move(){

        if(!stopped){
            if(horizontalDirection){
                rigidbody2D.velocity = new Vector2(direction * bladeVelocity, rigidbody2D.velocity.y);
            }else{
                rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x,direction * bladeVelocity);
            }
            
        }
    }

    void OnTriggerEnter2D(Collider2D collisionInfo)
    {
        if(collisionInfo.CompareTag("Edge")){
            direction *=-1;
            if(horizontalDirection){
            rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
            }else{rigidbody2D.velocity = new Vector2( rigidbody2D.velocity.x,0);}

            stopped = true;
        }
        
    }

    void OnTriggerStay2D(Collider2D collisionInfo)
    {
        if(waitTimeVar <=0){
            stopped = false;
        }
    }

    void OnTriggerExit2D(Collider2D collisionInfo)
    {
        waitTimeVar = waitTime;
        stopped = false;        
    }

}
