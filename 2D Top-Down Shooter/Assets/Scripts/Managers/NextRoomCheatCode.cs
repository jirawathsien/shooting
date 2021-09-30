using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRoomCheatCode : MonoBehaviour
{
    [SerializeField] private Transform[] nextRooms;
    [SerializeField] private EnemyWaveSpawner[] triggers;

    private Transform playerT;

    private void Awake()
    {
        playerT = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private int i = 0;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
           
            playerT.transform.position = nextRooms[i].transform.position;
            i++;
            if (i > nextRooms.Length - 1)
            {
                i = 0;
            }
        }
    }
}
