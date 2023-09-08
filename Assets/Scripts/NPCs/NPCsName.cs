using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCsName : MonoBehaviour
{
    [SerializeField] private Transform player;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Camera.main == null) return;

        transform.LookAt(Camera.main.transform.position);
        transform.Rotate(0, 180, 0);
    }
}
