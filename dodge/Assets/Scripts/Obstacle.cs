using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float speed;

    private void Start()
    {
        speed = Random.Range(2f, 8f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameObject.activeInHierarchy || GameManager.GameEnded)
        {
            return;
        }

        if (CheckOutOfBounds())
        {
            gameObject.SetActive(false);
        }
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    public bool CheckOutOfBounds()
    {
        var pos = transform.position;
        if (pos.x > SpawnManager.RightBound
            || pos.x < SpawnManager.LeftBound
            || pos.y > SpawnManager.UpperBound
            || pos.y < SpawnManager.LowerBound)
        {
            return true;
        }

        return false;
    }
}
