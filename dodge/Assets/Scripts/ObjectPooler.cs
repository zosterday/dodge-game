using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    private const int ObstaclePoolAmount = 200;

    [SerializeField]
    private GameObject obstacleObj;

    private List<GameObject> obstaclePool;


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        obstaclePool = new();

        for (var i = 0; i < ObstaclePoolAmount; i++)
        {
            var obj = Instantiate(obstacleObj);
            obj.SetActive(false);
            obstaclePool.Add(obj);
            obj.transform.SetParent(this.transform);
        }
    }

    public GameObject GetCheckpointObject()
    {
        foreach (var checkpoint in obstaclePool)
        {
            if (!checkpoint.activeInHierarchy)
            {
                return checkpoint;
            }
        }

        return null;
    }
}
