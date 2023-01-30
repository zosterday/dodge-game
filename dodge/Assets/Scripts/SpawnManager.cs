using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public const float UpperBound = 7.5f;

    public const float LowerBound = -7.5f;

    public const float RightBound = 10f;

    public const float LeftBound = -10f;

    private const int MaxSpawnCount = 12;

    [SerializeField]
    private GameManager gameManager;

    private bool spawningStarted;

    private int spawnCount;

    // Start is called before the first frame update
    void Start()
    {
        spawningStarted = false;
        spawnCount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.IsGameActive)
        {
            return;
        }

        if (!spawningStarted)
        {
            InvokeRepeating(nameof(SpawnObject), 1.5f, 0.1f);
            InvokeRepeating(nameof(IncreaseSpawnCount), 5f, 8f);
            spawningStarted = true;
        }
    }

    private void SpawnObject()
    {
        for (var i = 0; i < spawnCount; i++)
        {
            //Get obstacle to spawn
            var obstacle = ObjectPooler.Instance.GetObstacleObject();
            var obTransform = obstacle.transform;

            //Set obstacle position
            var spawnPos = GenerateSpawnPos();
            obTransform.position = spawnPos;

            //Set obstacle rotation
            var rotation = Random.Range(-42f, 42f);
            obTransform.LookAt(Vector2.zero);
            obTransform.eulerAngles = new Vector3(0f, 0f, obTransform.eulerAngles.z + rotation);

            obstacle.gameObject.SetActive(true);
        }
    }

    private Vector3 GenerateSpawnPos()
    {
        var spawnType = (SpawnType)Random.Range(0, 4);
        var spawnPosValX = Random.Range(LeftBound, RightBound);
        var spawnPosValY = Random.Range(LowerBound, UpperBound);
        var spawnPos = new Vector3();

        switch (spawnType)
        {
            case SpawnType.top:
                spawnPos = new Vector3(spawnPosValX, UpperBound, -1f);
                break;

            case SpawnType.right:
                spawnPos = new Vector3(RightBound, spawnPosValY, -1f);
                break;

            case SpawnType.bottom:
                spawnPos = new Vector3(spawnPosValX, LowerBound, -1f);
                break;

            case SpawnType.left:
                spawnPos = new Vector3(-LeftBound, spawnPosValY, -1f);
                break;

            default:
                break;
        }

        return spawnPos;
    }

    private void IncreaseSpawnCount()
    {
        if (spawnCount >= MaxSpawnCount)
        {
            CancelInvoke(nameof(IncreaseSpawnCount));
            return;
        }
        spawnCount++;
    }

    private enum SpawnType
    {
        top = 0,
        right = 1,
        bottom = 2,
        left = 3
    };
}
