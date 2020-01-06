using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbeProjectile : MonoBehaviour
 
{
    [SerializeField]
   float movementSpeed;
    Vector2 direction;
    float lifecycle;
    float startTime;
    bool hasMessage;
    bool wantsDeath;
    Vector2 message;

    public Vector3 Message { get => message; set => message = value; }
    public Vector2 Direction { get => direction; set => direction = value; }
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public bool HasMessage { get => hasMessage; set => hasMessage = value; }
    public bool WantsDeath { get => wantsDeath; set => wantsDeath = value; }
    public float Lifecycle { get => lifecycle; set => lifecycle = value; }

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.realtimeSinceStartup;
        Lifecycle = 2;
        HasMessage = false;
        wantsDeath = false;
        Debug.Log("Probe created with  Dirx: " + Direction.x + " and Diry: " + Direction.y);

    }

    // Update is called once per frame
    void Update()
    {
        if(Time.realtimeSinceStartup-startTime >= Lifecycle)
        {
 
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision of Probe");
        if(!(collision.collider.tag == "player") && !(collision.collider.tag == "playerProjectile"))
        {
            Debug.Log("Collision leads to Message");
            HasMessage = true;
            message = collision.collider.transform.position;
            WantsDeath = true;
            Debug.Log("Dead Probe");

        }
    }
}
