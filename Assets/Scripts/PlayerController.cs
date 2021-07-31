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
    public float zoomMultiplier = 2f;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = GetComponentInChildren<Camera>();

        atom = transform.Find("AtomRB").gameObject;
        atomController = atom.GetComponent<AtomController>();
    }

    void Start()
    {

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

            cam.orthographicSize = (mass * camGrowthRate);
            cam.orthographicSize *= Input.GetMouseButton(1) ? zoomMultiplier : 1f;
            if (gm.currentMass != atomController.currentMass)
            {
                gm.AddMass((float)(gm.currentMass - atomController.currentMass));
            }
        }
        
        transform.localScale = new Vector3(0.2f * mass, 0.2f * mass, 1);
        //atom.transform.localScale = new Vector3( mass, mass, 1);

        //nucleusvisual.transform.localScale = atomController.currentNucleusDistance * transform.localScale;
        //electronVisual.transform.localScale = new Vector3(atomController.currentElectronDistance / (0.2f * mass), atomController.currentElectronDistance / (0.2f * mass), 1);

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision! : " + collision.gameObject.name + " with: " + gameObject.name);
        if (collision.gameObject.CompareTag("Atom") && atom != null)
        {
            
            atom.SendMessage("OnHitAnotherAtom", collision);
        }
    }
}
