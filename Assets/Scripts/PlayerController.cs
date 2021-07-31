using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject nucleusvisual;
    public GameObject electronVisual;

    bool isPlayer = false;
    Rigidbody2D rb;
    Camera cam;
    GameManager gm;
    GameObject atom;
    AtomController atomController;

    public float maxSpeed = 5f;
    public float maxForceMultiplier = 0.25f;

    public float camGrowthRate = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = GetComponentInChildren<Camera>();

        atom = transform.Find("AtomRB").gameObject;
        atomController = atom.GetComponent<AtomController>();

        if (gameObject.CompareTag("Player"))
        {
            isPlayer = true;
            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            gm.AddMass(atomController.currentMass);
        }

    }

    // Update is called once per frame
    void Update()
    {
        float mass = Mathf.Log(atomController.currentMass);

        if (isPlayer) {

            if (Input.GetMouseButton(0))
            {
                Vector2 mousePos = Input.mousePosition;
                Vector2 pos =  cam.ScreenToWorldPoint(mousePos) - transform.position;

                rb.velocity = rb.velocity.normalized * maxSpeed * mass;
                rb.AddForce(pos * maxForceMultiplier * transform.localScale.x, ForceMode2D.Impulse);
                
            }

            cam.orthographicSize = mass * camGrowthRate;

        }
        
        transform.localScale = new Vector3(0.2f * mass, 0.2f * mass, 1);
        //atom.transform.localScale = new Vector3( mass, mass, 1);

        //nucleusvisual.transform.localScale = atomController.currentNucleusDistance * transform.localScale;
        //electronVisual.transform.localScale = new Vector3(atomController.currentElectronDistance / (0.2f * mass), atomController.currentElectronDistance / (0.2f * mass), 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Atom"))
        {
            Debug.Log("collision! : " + collision.gameObject.name);
            atom.SendMessage("OnCollisionEnter2D", collision);
        }
    }
}
