using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomController : MonoBehaviour
{
    public float radius;
    public float[] ElectronVelocityRange = new float[2];
    public float[] ProtonNeutronVelocityRange = new float[2];

    public Rigidbody2D rb { get; private set; }
    public Transform parentTransform { get; private set; }
    public Transform myTransform { get; private set; }

    public float currentNucleusDistance = 2.0f;
    public float currentElectronDistance = 4.0f;
    public float maxGravity = 35.0f;
    public float maxSpeed = 5f;

    public float NucleusDistanceMul = 0.5f;
    public float ElectronDistanceMul = 1.0f;

    public float currentMass = 1f;

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
    void OnEnable()
    {
        electrons = new List<Transform>();
        protons = new List<Transform>();
        neutrons = new List<Transform>();

        GenerateElectrons();
        GenerateProtons();
        GenerateNeutrons();
        parentTransform = transform.parent;
        myTransform = transform;
        rb = transform.parent.GetComponent<Rigidbody2D>();

        UpdateMass();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //SimulateElectrons();
        //SimulateNeutrons();
        //SimulateProtons();
        float mass = Mathf.Log(currentMass);
        currentNucleusDistance = Mathf.Max(mass * NucleusDistanceMul, 0.05f);
        currentElectronDistance = Mathf.Max(mass * ElectronDistanceMul, 0.15f);

        electronCount = electrons.Count;
        protonCount = protons.Count;
        neutronCount = neutrons.Count;

        if(currentMass <= 0f)
        {
            parentTransform.SendMessage("AtomIsEmpty");
        }
    }

    public void UpdateMass()
    {
        currentMass = 0f;
        foreach(Transform t in transform)
        {
            currentMass += t.gameObject.GetComponent<Rigidbody2D>().mass;
        }
        rb.mass = currentMass;
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
            Vector2 pos = Random.insideUnitCircle * currentNucleusDistance;
            
            GameObject planet = GameObject.Instantiate(ProtonPrefab, transform);
            planet.transform.localPosition = pos;
            /*Rigidbody2D rigidbody2D = planet.GetComponent<Rigidbody2D>();
            float x = Random.Range(ProtonNeutronVelocityRange[0], ProtonNeutronVelocityRange[1]);
            float y = Random.Range(ProtonNeutronVelocityRange[0], ProtonNeutronVelocityRange[1]);
            rigidbody2D.AddRelativeForce(new Vector2(x, y));*/

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
        Vector2 pos = Random.onUnitSphere;
        float velMultiplier = 1f;

        int state = 1;

        for (int c = 0; c < electronCount; c++)
        {
            pos = Random.onUnitSphere;
            pos.x += 0.5f * state;

            if (c % (2 << state) == 0)
            {
                velMultiplier *= -1f;
                state++;
            }

            GameObject planet = GameObject.Instantiate(ElectronPrefab, transform);
            Rigidbody2D rigidbody2D = planet.GetComponent<Rigidbody2D>();
           
            planet.transform.localPosition = pos;
            
            //rigidbody2D.velocity =  new Vector2(0, velMultiplier * 2);

            //Debug.Log("new Pos: " + planet.transform.position + " and new velocity" + rigidbody2D.velocity);
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

            if (dist <= currentElectronDistance)
            {
                rigidbody2D.drag = 0.01f;
                rigidbody2D.AddForce(v.normalized * (1.0f - dist / currentElectronDistance) * maxGravity * rigidbody2D.mass);
                //Debug.Log("maths: " + rigidbody2D.velocity);
            }
            else
            {
                rigidbody2D.AddForce(rb.velocity + (v.normalized * maxGravity));
                
            }
            rigidbody2D.drag = 1/dist;
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

            if (dist <= currentNucleusDistance)
            {
                rigidbody2D.drag = 0.01f;
                rigidbody2D.AddForce(v.normalized * (1.0f - dist / currentNucleusDistance) * 2 * maxGravity * rigidbody2D.mass);
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

            if (dist <= currentNucleusDistance)
            {
                rigidbody2D.drag = 0.01f;
                rigidbody2D.AddForce(v.normalized * (1.0f - dist / currentNucleusDistance) * 2 * maxGravity * rigidbody2D.mass);
                //Debug.Log("maths: " + rigidbody2D.velocity);
            }
            else
            {
                rigidbody2D.velocity = rb.velocity + (v.normalized * 2f);
                rigidbody2D.drag = dist;
            }
        }
    }

    public void OnHitAnotherAtom(Collision2D collision)
    {
        AtomController targetController = collision.gameObject.GetComponentInChildren<AtomController>();
        Rigidbody2D enemyrb = collision.gameObject.GetComponent<Rigidbody2D>();

        if (enemyrb.mass > rb.mass)
        {
            TransferMyParticlesTo(collision.gameObject, targetController);
        }
        else
        {
            TransferTheirParticlesToMe(collision.gameObject, targetController);
        }
        UpdateMass();
        targetController.UpdateMass();
    }

    private void TransferMyParticlesTo(GameObject target, AtomController targetController)
    {
        foreach (Transform t in targetController.myTransform)
        {
            t.parent = target.transform;
            t.localScale = Vector3.one;
        }

        targetController.electrons.AddRange(this.electrons);
        targetController.protons.AddRange(this.neutrons);
        targetController.neutrons.AddRange(this.protons);

        this.electrons.Clear();
        this.protons.Clear();
        this.neutrons.Clear();

    }

    private void TransferTheirParticlesToMe(GameObject target, AtomController targetController)
    {
        foreach (Transform t in targetController.myTransform)
        {
            t.parent = transform;
            t.localScale = Vector3.one * 0.25f;
        }

        this.electrons.AddRange(targetController.electrons);
        this.protons.AddRange(targetController.neutrons);
        this.neutrons.AddRange(targetController.protons);

        targetController.electrons.Clear();
        targetController.protons.Clear();
        targetController.neutrons.Clear();
    }
}
