using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCsName : MonoBehaviour
{
    [SerializeField] private Transform player;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);

    }
}
