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

    public float camGrowthScale = 1.2f;
    public float camMaxMass = 250f;
    public float camMaxSize = 250f;
    public float zoomMultiplier = 2f;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = GetComponentInChildren<Camera>();

        atom = transform.Find("AtomRB").gameObject;
        atomController = atom.GetComponent<AtomController>();

        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {

        if (gameObject.CompareTag("Player"))
        {
            isPlayer = true;
            camObj = GameObject.FindGameObjectWithTag("MainCamera");
            cam = camObj.GetComponent<Camera>();
            gm.AddMass(atomController.currentMass);
            gm.player = this.gameObject;
        }
        transform.localScale = new Vector3(0.02f * atomController.currentMass, 0.02f * atomController.currentMass, 1);
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
                rb.velocity = rb.velocity.normalized * maxSpeed * atomController.currentMass * transform.localScale;
                rb.AddForce(pos * maxForceMultiplier, ForceMode2D.Impulse);
                rb.AddTorque(angle);
            }

            float size = Mathf.Lerp(5f, camMaxSize, atomController.currentMass / camMaxMass);
            cam.orthographicSize = (size * camGrowthScale);
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
        else
        {
            float distanceFromPlayer = Vector2.Distance(gm.player.gameObject.transform.position, this.gameObject.transform.position);
            float distanceLimit = distanceFromPlayer * atomController.currentMass;
            if (distanceFromPlayer > distanceLimit)
            {
                Die();
            }

            rb.mass = atomController.currentMass;
            //rb.velocity = rb.velocity.normalized * maxSpeed * mass;
        }
        
        transform.localScale = new Vector3(0.02f * atomController.currentMass, 0.02f * atomController.currentMass, 1);
        //atom.transform.localScale = new Vector3( mass, mass, 1);

        //nucleusvisual.transform.localScale = atomController.currentNucleusDistance * transform.localScale;
        //electronVisual.transform.localScale = new Vector3(atomController.currentElectronDistance / (0.2f * mass), atomController.currentElectronDistance / (0.2f * mass), 1);

        
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gm.currentTimeSurvided < 3f) return; 

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
        }
    }

    public void Die(float delay = 0f)
    {
        if (isPlayer)
        {
            gm.GameOver();
            GameObject g = GameObject.Instantiate(camObj, spawnManager.gameObject.transform);
            g.transform.position = camObj.transform.position;
            this.gameObject.SetActive(false);
        }
        else
        {
            spawnManager.RemoveAtom(this.gameObject);
            Destroy(this.gameObject, delay);
        }
    }
}
