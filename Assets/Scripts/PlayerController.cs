using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject nucleusvisual;
    public GameObject electronVisual;

    bool isPlayer = false;
    Rigidbody2D rb;
    GameObject camObj;
    Camera cam;
    GameManager gm;
    GameObject atom;

    SpawnManager spawnManager;
    public AtomController atomController { get; private set; }

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

        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    void Start()
    {

        if (gameObject.CompareTag("Player"))
        {
            isPlayer = true;
            camObj = GameObject.FindGameObjectWithTag("MainCamera");
            cam = camObj.GetComponent<Camera>();
            gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            gm.AddMass(atomController.currentMass);
        }

    }

    // Update is called once per frame
    void Update()
    {
        float mass = Mathf.Log10(atomController.currentMass);
        if (mass < 0) mass = 0.001f;
        if (isPlayer) {

            if (Input.GetMouseButton(0))
            {
                Vector2 mousePos = Input.mousePosition;
                Vector2 pos =  cam.ScreenToWorldPoint(mousePos) - transform.position;
                float angle = Vector2.Angle(rb.velocity, pos);
                
                rb.mass = atomController.currentMass;
                rb.velocity = rb.velocity.normalized * maxSpeed * mass;
                rb.AddForce(pos * maxForceMultiplier * transform.localScale.x, ForceMode2D.Impulse);
                rb.AddTorque(angle);
            }

            cam.orthographicSize = (mass * camGrowthRate);
            cam.orthographicSize *= Input.GetMouseButton(1) ? zoomMultiplier : 1f;
           
            if (gm.currentMass != atomController.currentMass)
            {
                gm.AddMass((float)(atomController.currentMass - gm.currentMass));
            }

            if (gm.isDead)
            {
                Die(1.5f);
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


    public void AtomIsEmpty()
    {
        if (isPlayer)
        {
            Die();
        }
        else
        {
            Die();
            spawnManager.RemoveAtom(this.gameObject);
        }
    }

    public void Die(float delay = 0f)
    {
        if (isPlayer)
        {
            gm.GameOver();
            GameObject g = GameObject.Instantiate(camObj, gm.gameObject.transform);
            g.transform.position = camObj.transform.position;
            Destroy(this.gameObject);
        }

        Destroy(this.gameObject, delay);
    }
}
