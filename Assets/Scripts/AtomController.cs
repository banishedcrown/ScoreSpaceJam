using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomController : MonoBehaviour
{
    public float radius;
    public float[] ElectronVelocityRange = new float[2];
    public float[] ProtonNeutronVelocityRange = new float[2];

    Rigidbody2D rb;
    Transform parentTransform;

    public float maxNucleusDistance = 2.0f;
    public float maxElectronDistance = 4.0f;
    public float maxGravity = 35.0f;
    public float maxSpeed = 5f;

    public int electronCount = 1;
    public int neutronCount = 0;
    public int protonCount = 1;

    public GameObject ElectronPrefab;
    public GameObject NeutronPrefab;
    public GameObject ProtonPrefab;

    List<Transform> electrons;
    List<Transform> protons;
    List<Transform> neutrons;

    // Start is called before the first frame update
    void Start()
    {
        electrons = new List<Transform>();
        protons = new List<Transform>();
        neutrons = new List<Transform>();

        GenerateElectrons();
        GenerateProtons();
        GenerateNeutrons();
        parentTransform = transform.parent;

        rb = transform.parent.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SimulateElectrons();
        SimulateNeutrons();
        SimulateProtons();
    }

    void GenerateProtons()
    {
        Transform NewPosition = transform;
        Vector2 nPos = Vector2.zero;
        if (NewPosition != null)
        {
            nPos = NewPosition.position;
        }
        for (int c = 0; c < protonCount; c++)
        {
            Vector2 pos = Random.insideUnitCircle;
            
            GameObject planet = GameObject.Instantiate(ProtonPrefab, transform);
            planet.transform.localPosition = pos;
            Rigidbody2D rigidbody2D = planet.GetComponent<Rigidbody2D>();
            float x = Random.Range(ProtonNeutronVelocityRange[0], ProtonNeutronVelocityRange[1]);
            float y = Random.Range(ProtonNeutronVelocityRange[0], ProtonNeutronVelocityRange[1]);
            rigidbody2D.AddRelativeForce(new Vector2(x, y));

            protons.Add(planet.transform);
        }
    }

    void GenerateNeutrons()
    {
        Transform NewPosition = transform;
        Vector2 nPos = Vector2.zero;
        if (NewPosition != null)
        {
            nPos = NewPosition.position;
        }
        for (int c = 0; c < neutronCount; c++)
        {
            Vector2 pos = Random.insideUnitCircle;

            GameObject planet = GameObject.Instantiate(NeutronPrefab, transform);
            planet.transform.localPosition = pos;
            Rigidbody2D rigidbody2D = planet.GetComponent<Rigidbody2D>();
            float x = Random.Range(ProtonNeutronVelocityRange[0], ProtonNeutronVelocityRange[1]);
            float y = Random.Range(ProtonNeutronVelocityRange[0], ProtonNeutronVelocityRange[1]);
            rigidbody2D.AddRelativeForce(new Vector2(x, y));

            neutrons.Add(planet.transform);
        }
    }

    void GenerateElectrons()
    {
        Transform NewPosition = transform;
        Vector2 pos = new Vector2(0.5f, 0);
        float velMultiplier = 1f;

        int state = 1;

        for (int c = 0; c < electronCount; c++)
        {
            if (c % (2 << state) == 0)
            {
                pos.x += 0.5f;
                velMultiplier *= -1f;
            }

            GameObject planet = GameObject.Instantiate(ElectronPrefab, transform);
            Rigidbody2D rigidbody2D = planet.GetComponent<Rigidbody2D>();
            rigidbody2D.isKinematic = true;
            planet.transform.localPosition = pos;
            rigidbody2D.isKinematic = false;
            rigidbody2D.velocity =  new Vector2(0, velMultiplier * 2);

            Debug.Log("new Pos: " + planet.transform.position + " and new velocity" + rigidbody2D.velocity);
            electrons.Add(planet.transform);
        }
    }


    void SimulateElectrons()
    {
        foreach (Transform t in electrons)
        {
            GameObject planet = t.gameObject;
            Rigidbody2D rigidbody2D = planet.GetComponent<Rigidbody2D>();
            planet.transform.position = transform.position + planet.transform.localPosition;

            float dist = Vector2.Distance(transform.position, planet.transform.position);
            Vector2 v = transform.position - planet.transform.position;

            if (dist <= maxElectronDistance)
            {
                rigidbody2D.drag = 0.01f;
                rigidbody2D.AddForce(v.normalized * (1.0f - dist / maxElectronDistance) * maxGravity * rigidbody2D.mass);
                //Debug.Log("maths: " + rigidbody2D.velocity);
            }
            else
            {
                rigidbody2D.AddForce(rb.velocity + (v.normalized * 3f));
                rigidbody2D.drag = dist;
            }
        }
    }void SimulateProtons()
    {
        foreach (Transform t in protons)
        {
            GameObject planet = t.gameObject;
            Rigidbody2D rigidbody2D = planet.GetComponent<Rigidbody2D>();
            planet.transform.position = transform.position + planet.transform.localPosition;
            float dist = Vector2.Distance(transform.position, planet.transform.position);
            Vector2 v = transform.position - planet.transform.position;

            if (dist <= maxNucleusDistance)
            {
                rigidbody2D.drag = 0.01f;
                rigidbody2D.AddForce(v.normalized * (1.0f - dist / maxNucleusDistance) * 2 * maxGravity * rigidbody2D.mass);
                //Debug.Log("maths: " + rigidbody2D.velocity);
            }
            else
            {
                rigidbody2D.velocity = rb.velocity +( v.normalized * 2f);
                rigidbody2D.drag = dist;
            }
        }
    }void SimulateNeutrons()
    {
        foreach (Transform t in neutrons)
        {
            GameObject planet = t.gameObject;
            Rigidbody2D rigidbody2D = planet.GetComponent<Rigidbody2D>();
            planet.transform.position = transform.position + planet.transform.localPosition;
            float dist = Vector2.Distance(transform.position, planet.transform.position);
            Vector2 v = transform.position - planet.transform.position;

            if (dist <= maxNucleusDistance)
            {
                rigidbody2D.drag = 0.01f;
                rigidbody2D.AddForce(v.normalized * (1.0f - dist / maxNucleusDistance) * 2 * maxGravity * rigidbody2D.mass);
                //Debug.Log("maths: " + rigidbody2D.velocity);
            }
            else
            {
                rigidbody2D.velocity = rb.velocity + (v.normalized * 2f);
                rigidbody2D.drag = dist;
            }
        }
    }
}
