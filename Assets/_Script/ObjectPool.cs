using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class pool
{
    public string tag;
    public GameObject Preafab;
    public int Size;
}

public class ObjectPool : MonoBehaviour
{
    Dictionary<string, Queue<GameObject>> PoolDictionary;
    public List<pool> PoolList;

    void Start()
    {
        GameManager.poolinstance = this;
        PoolDictionary = new Dictionary<string,Queue<GameObject>>();
        foreach (pool item in PoolList)
        {
            Queue<GameObject> pools=new Queue<GameObject>();
            for (int i = 0; i < item.Size; i++)
            {
                var poolitem = Instantiate(item.Preafab);
                poolitem.SetActive(false);
                pools.Enqueue(poolitem);
                if (item.tag == "cargo")
                {
                    item.Preafab.GetComponent<MeshRenderer>().material = GameManager.instance.mat[Random.Range(0,3)];
                }
            }
            PoolDictionary[item.tag] = pools;
        }
    }

    public GameObject  SpawnObject(string tag,Vector3 pos, Quaternion rot)
    {
        if (!PoolDictionary.ContainsKey(tag))
        {
            return null;
        }
        var ObjectToSpawn = PoolDictionary[tag].Dequeue();
        ObjectToSpawn.SetActive(true);
        if(ObjectToSpawn.transform.childCount > 0)
        {
            foreach (Transform t in  ObjectToSpawn.transform)
            {
                t.gameObject.SetActive(true);
            }
        }
        ObjectToSpawn.transform.position = pos;
        ObjectToSpawn.transform.rotation = rot;

        PoolDictionary[tag].Enqueue(ObjectToSpawn);

        return ObjectToSpawn;
    }

}
