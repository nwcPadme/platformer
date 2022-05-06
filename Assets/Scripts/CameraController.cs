using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 heroPos;

    private void Awake()
    {
        if (!player)
        {
            player = FindObjectOfType<Hero>().transform;
            
        }
    }

    private void Update()
    {
        heroPos = player.position;
        heroPos.z = -10.0f;
        transform.position = Vector3.Lerp(transform.position, heroPos, Time.deltaTime);

    }



}

