using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject atomPrefab;
    public int maxAtoms = 100;
    public float maxDistanceFromOrigin = 50f;
    public float maxDistanceFromPlayer = 20f;
    public int maxSizeFromPlayer = 4;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        if(atoms.Count == 0)
        {
            //GenerateStarterAtoms();
            SpawnNewAtom();
        }

        else if(atoms.Count < maxAtoms)
        {
            SpawnNewAtom();
        }
    }

    void GenerateStarterAtoms()
    {
        for (int c = 0; c < maxAtoms; c++)
        {
            Vector2 pos = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            float dist = Random.Range(1f, maxDistanceFromOrigin);
            //GameObject obj = GameObject.Instantiate(atomPrefab, (Vector2)((Vector2)player.transform.position + (pos * dist * (Vector2)playerController.transform.localScale)), Quaternion.identity);
            GameObject obj = GameObject.Instantiate(atomPrefab, pos * dist, Quaternion.identity);
            AtomController ac = obj.GetComponentInChildren<AtomController>();

            ac.electronCount = 1;
            ac.protonCount = 1;
            ac.neutronCount = Random.Range(0, 2);

            obj.transform.localScale = new Vector3(0.02f * ac.currentMass, 0.02f * ac.currentMass, 1);

            ac.enabled = true;

            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            rb.velocity = Random.insideUnitCircle * Random.Range(0.5f, 1f);

            atoms.Add(obj);
        }
    }

    void SpawnNewAtom()
    {
        for (int c = atoms.Count; c < maxAtoms; c++)
        {
            Vector2 pos = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            float dist = Random.Range(5.0f, maxDistanceFromPlayer);
            //float dist = Random.Range(0.0f, maxDistanceFromOrigin);

            GameObject obj = GameObject.Instantiate(atomPrefab, (Vector2) player.transform.position + ( pos * dist * playerController.atomController.currentMass), Quaternion.identity);
            //GameObject obj = GameObject.Instantiate(atomPrefab, ( pos * dist), Quaternion.identity);
            AtomController ac = obj.GetComponentInChildren<AtomController>();

            ac.electronCount = Random.Range(playerController.atomController.electronCount - maxSizeFromPlayer, playerController.atomController.electronCount + maxSizeFromPlayer); ;
            ac.protonCount = Random.Range(playerController.atomController.protonCount - maxSizeFromPlayer, playerController.atomController.protonCount + maxSizeFromPlayer);
            ac.neutronCount = Random.Range(0, ac.protonCount);

            ac.enabled = true;

            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            rb.velocity = Random.insideUnitCircle * Random.Range(0.5f, 2f);

            atoms.Add(obj);
        }
    }

    public void RemoveAtom(GameObject target)
    {
        atoms.Remove(target);
    }
}
