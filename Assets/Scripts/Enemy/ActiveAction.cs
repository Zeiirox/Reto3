using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAction : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private GameObject _gameObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _gameObject.SetActive(true);
            _particleSystem.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _gameObject.SetActive(false);
            _particleSystem.Stop();
        }
    }
}
