using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject atomPrefab;
    public int maxAtoms = 100;
    public float maxDistanceFromOrigin = 50f;
    public int maxSizeAbovePlayer = 4;

    List<GameObject> atoms;

    float negXBound = -50f;
    float posXBound = +50f;
    float negYBound = -50f;
    float posYBound = +50f;

    GameObject player;
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        atoms = new List<GameObject>();
        GenerateStarterAtoms();
    }

    // Update is called once per frame
    void Update()
    {
        if(atoms.Count < maxAtoms)
        {
            SpawnNewAtom();
        }
    }

    void GenerateStarterAtoms()
    {
        for (int c = 0; c < maxAtoms; c++)
        {
            Vector2 pos = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            float dist = Random.Range(0.0f, maxDistanceFromOrigin);

            GameObject obj = GameObject.Instantiate(atomPrefab, pos * dist, Quaternion.identity);
            AtomController ac = obj.GetComponentInChildren<AtomController>();

            ac.electronCount = 1;
            ac.protonCount = Random.Range(0,10) == 0 ? 1: 0;
            ac.neutronCount = Random.Range(0, ac.protonCount);

            ac.enabled = true;

            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            rb.velocity = Random.insideUnitCircle * Random.Range(0.5f, 1f);

            atoms.Add(obj);
        }
    }

    void SpawnNewAtom()
    {

    }

    public void RemoveAtom(GameObject target)
    {
        
    }
}
