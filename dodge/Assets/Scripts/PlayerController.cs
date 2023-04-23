using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const string ObstacleTag = "Obstacle";

    private const string PlayerSprite = "PlayerSprite";

    private const float XBounds = 9.75f;

    private const float YBounds = 7.225f;

    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private SpawnManager spawnManager;

    [SerializeField]
    private ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        explosionParticle.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.IsGameActive)
        {
            return;
        }
        //Update player position
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        var camMain = Camera.main;
        var mousePos = camMain.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = camMain.transform.position.z + camMain.nearClipPlane;
        transform.position = Vector3.Lerp(transform.position, mousePos, 0.1f);

        if (transform.position.x > XBounds)
        {
            transform.position = new Vector3(XBounds, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -XBounds)
        {
            transform.position = new Vector3(-XBounds, transform.position.y, transform.position.z);
        }
        if (transform.position.y > YBounds)
        {
            transform.position = new Vector3(transform.position.x, YBounds, transform.position.z);
        }
        else if (transform.position.y < -YBounds)
        {
            transform.position = new Vector3(transform.position.x, -YBounds, transform.position.z);
        }
    }

    private void EndGame()
    {
        explosionParticle.gameObject.SetActive(true);
        explosionParticle.transform.position = transform.position;
        explosionParticle.Play();
        var planeMesh = transform.Find(PlayerSprite);
        planeMesh.gameObject.SetActive(false);
        gameManager.GameOver();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ObstacleTag))
        {
            EndGame();
        }
    }
}
