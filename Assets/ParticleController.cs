using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public bool isElectron = false;
    AtomController parent;
    Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        parent = transform.parent.GetComponent<AtomController>();
        Rigidbody2D rb = parent.rb;

        float dist = Vector2.Distance(transform.parent.position, transform.position);
        Vector2 v = transform.parent.position - transform.position;

        //rigidbody2D.velocity = (rb.velocity.normalized + rigidbody2D.velocity.normalized) * rigidbody2D.velocity.magnitude;

        if (isElectron)
        {
            if (dist > parent.currentElectronDistance)
            {
                transform.localPosition = -v.normalized * dist;
                rigidbody2D.velocity = ((rb.velocity.normalized + v.normalized) * 10f * parent.parentTransform.transform.localScale.x);
            }

            transform.RotateAround(transform.parent.position, Vector3.forward, 0.2f);
        }
        else
        {
            if (dist > parent.currentNucleusDistance)
            {
                transform.localPosition = -v.normalized * dist;
                rigidbody2D.velocity = ((rb.velocity.normalized + v.normalized) * 10f * parent.parentTransform.transform.localScale.x);
            }
        }

        rigidbody2D.AddForce((v.normalized * 30f * parent.parentTransform.transform.localScale.x) * rigidbody2D.mass);
       
    }
}
