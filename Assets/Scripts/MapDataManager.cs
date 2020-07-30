using System.Collections.Generic;
using UnityEngine;

public class MapDataManager : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public Transform poolHolder;
    public static Dictionary<string, Queue<GameObject>> poolDictionary;
    public List<Pool> pools;
    public static List<Transform> enemyUnits;
    public static List<Transform> enemyBuildings;
    public static List<Transform> playerUnits;
    public static List<Transform> playerBuildings;

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        enemyUnits = new List<Transform>();
        enemyBuildings = new List<Transform>();
        playerUnits = new List<Transform>();
        playerBuildings = new List<Transform>();
        playerBuildings.Add(GameObject.Find("PlayerBase").transform);
        enemyBuildings.Add(GameObject.Find("EnemyBase").transform);
        CreatePools();
    }

    private void CreatePools()
    {
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                var obj = Instantiate(pool.prefab);
                obj.transform.parent = poolHolder;
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public static void Add(List<Transform> targetList, Transform objectToAdd)
    {
        if(targetList != null)
        {
            targetList.Add(objectToAdd);
        }
        else
        {
            targetList = new List<Transform>();
            targetList.Add(objectToAdd);
        }
    }
    public static GameObject GetClosestTarget(List<Transform> targetTree, Transform origin)
    {
        return FindClosest(targetTree, origin);
    }
    private static GameObject FindClosest(List<Transform> targetTree, Transform origin)
    {
        if (targetTree.Count > 0)
        {
            var closest = targetTree[0];

            foreach (Transform t in targetTree)
            {
                if (Vector2.Distance(origin.position, t.position) < Vector2.Distance(origin.position, closest.transform.position))
                {
                    closest = t;
                }
            }
            return closest.gameObject;
        }
        return null;
    }
}
