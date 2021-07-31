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

    // Start is called before the first frame update
    void Start()
    {
        atoms = new List<GameObject>();
        GenerateStarterAtoms();
    }

    // Update is called once per frame
    void Update()
    {
        
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

            atoms.Add(obj);
        }
    }
}
