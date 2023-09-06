using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//if you use this code you are contractually obligated to like the YT video
public class RandomMovement : MonoBehaviour //don't forget to change the script name if you haven't
{
    [SerializeField, Range(0, 10)] private float distance = 5;

    private Quaternion angle;
    private float degrees;

    private void Start()
    {
    }

    private void Update()
    {
        degrees = Random.Range(0, 360);
        angle = Quaternion.Euler(0, degrees, 0);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, angle, 0);
        transform.Translate(Vector3.left * distance * Time.deltaTime);
    }
}