using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    private Vector3 TopPlatformPos;
    public GameObject Plaforms;
    public Queue<GameObject> PlaformsPool;
    readonly float[] Xpos = { 2.33f, 0f, -2.33f };
    void Start()
    {
        PlaformsPool= new Queue<GameObject>();
        for (int i = 0; i < Plaforms.transform.childCount; i++)
        {
            PlaformsPool.Enqueue(Plaforms.transform.GetChild(i).gameObject);
        }
        GameManager.instance.Spawn += Spawn;
        TopPlatformPos = Plaforms.transform.GetChild(Plaforms.transform.childCount-1).position;
    }     
    void Spawn()
    {
        StartCoroutine(RearrangingPlatform());
        StartCoroutine(SpawningItems());
    }
    IEnumerator RearrangingPlatform()
    {
        yield return new WaitForSeconds(5f);
        var platform = PlaformsPool.Dequeue();
        Vector3 newpos = TopPlatformPos + new Vector3(0, 0, 15f);
        TopPlatformPos = newpos;
        platform.transform.position = newpos;
        PlaformsPool.Enqueue(platform);
    }
    IEnumerator SpawningItems()
    {
        Vector3 newpos;

        newpos = new Vector3(0f, 0f, GameManager.Instance.transform.position.z) + new Vector3(Xpos[Random.Range(0, 3)], 1.15f, 15f);
        var cargo = GameManager.poolinstance.SpawnObject("cargo", newpos, Quaternion.identity);

        newpos = new Vector3(Xpos[Random.Range(0, 3)], 1, 15f) + new Vector3(0f, 0f, GameManager.Instance.transform.position.z);
        GameObject coin = GameManager.poolinstance.SpawnObject("coin", newpos, Quaternion.identity);

        newpos = new Vector3(Xpos[Random.Range(0, 3)], 0, 15f) + new Vector3(0f, 0f, GameManager.Instance.transform.position.z);
        GameObject hurdle = GameManager.poolinstance.SpawnObject("hurdle", newpos, Quaternion.identity);

        newpos = new Vector3(Xpos[Random.Range(0, 3)],0.7f, 15f) + new Vector3(0f, 0f, GameManager.Instance.transform.position.z);
        GameObject rock = GameManager.poolinstance.SpawnObject("rock", newpos, Quaternion.identity);

        yield return null;
    }   
}
