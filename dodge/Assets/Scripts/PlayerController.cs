using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
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
    }
}
