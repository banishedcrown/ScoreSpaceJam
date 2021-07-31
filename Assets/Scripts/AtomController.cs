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

    public float maxGravDist = 4.0f;
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
        GenerateElectrons();
        GenerateParticle(ProtonPrefab, protonCount);
        GenerateParticle(NeutronPrefab, neutronCount);
        parentTransform = transform.parent;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (Transform t in transform)
        {
            GameObject planet = t.gameObject;
            Rigidbody2D rigidbody2D = planet.GetComponent<Rigidbody2D>();
            planet.transform.position = transform.position + planet.transform.localPosition;
            float dist = Vector3.Distance(transform.position, planet.transform.position);
            Vector3 v = transform.position - planet.transform.position;

            if (dist <= maxGravDist)
            {
                rigidbody2D.AddForce(v.normalized * (1.0f - dist / maxGravDist) * maxGravity);
                //Debug.Log("maths: " + rigidbody2D.velocity);
            }
            else
            {
                rigidbody2D.velocity = v.normalized * ( dist / maxGravDist) * maxGravity;
            }
        }
    }

    void GenerateParticle(GameObject prefab, int count, Transform NewPosition = null)
    {
        Vector2 nPos = Vector2.zero;
        if (NewPosition != null)
        {
            nPos = NewPosition.position;
        }
        for (int c = 0; c < count; c++)
        {
            Vector2 pos = new Vector2(Random.Range(-1, 1), Random.Range(1, -1));
            pos.x += nPos.x;
            pos.y += nPos.y;
            
            GameObject planet = GameObject.Instantiate(prefab, transform);
            planet.transform.position = pos;
            Rigidbody2D rigidbody2D = planet.GetComponent<Rigidbody2D>();
            float x = Random.Range(ProtonNeutronVelocityRange[0], ProtonNeutronVelocityRange[1]);
            float y = Random.Range(ProtonNeutronVelocityRange[0], ProtonNeutronVelocityRange[1]);
            rigidbody2D.AddRelativeForce(new Vector2(x, y));
        }
    }

    void GenerateElectrons()
    {
        Transform NewPosition = transform;
        Vector2 pos = NewPosition.position;
        float velMultiplier = 1f;

        int state = 1;

        for (int c = 0; c < electronCount; c++)
        {
            if(c % (2 << state) == 0)
            {
                pos.x += 0.5f;
                velMultiplier *= -1f;
            }
            GameObject planet = GameObject.Instantiate(ElectronPrefab, transform);
            Rigidbody2D rigidbody2D = planet.GetComponent<Rigidbody2D>();
            rigidbody2D.isKinematic = true;
            planet.transform.localPosition = pos;
            rigidbody2D.isKinematic = false;
            rigidbody2D.velocity =  new Vector2(Random.Range(ElectronVelocityRange[0], ElectronVelocityRange[1]), velMultiplier * Random.Range(ElectronVelocityRange[0], ElectronVelocityRange[1]));

            Debug.Log("new Pos: " + planet.transform.position + " and new velocity" + rigidbody2D.velocity);
        }
    }
}
