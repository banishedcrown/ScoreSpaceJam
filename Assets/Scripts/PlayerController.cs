using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool isPlayer = false;
    Rigidbody2D rb;
    Camera cam;
    GameManager gm;

    public float maxSpeed = 5f;
    public float maxForceMultiplier = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = GetComponentInChildren<Camera>();
        if(gameObject.CompareTag("Player"))
        {
            isPlayer = true;
        }

        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayer) {
            if (Input.GetMouseButton(0))
            {
                Vector2 mousePos = Input.mousePosition;
                Vector2 pos =  cam.ScreenToWorldPoint(mousePos) - transform.position;

                rb.velocity = rb.velocity.normalized * maxSpeed;
                rb.AddForce(pos * maxForceMultiplier);

            }

            
        }
    }
}
