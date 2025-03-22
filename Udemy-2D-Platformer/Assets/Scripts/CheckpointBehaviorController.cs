using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBehaviorController : MonoBehaviour
{
    public static CheckpointBehaviorController instance;

    private CheckpointController[] checkpoints;

    public Vector3 spawnPoint;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        checkpoints = FindObjectsOfType<CheckpointController>();

        spawnPoint = PlayerController.instance.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeactivateCheckpoints()
    {
        foreach (var checkpoint in checkpoints)
        {
            checkpoint.ResetCheckpoint();
        }
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }
}
