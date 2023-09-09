using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteKey("checkPoint");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetFloat("checkPointX", other.transform.position.x);
            PlayerPrefs.SetFloat("checkPointY", other.transform.position.y);
            PlayerPrefs.SetFloat("checkPointZ", other.transform.position.z);
            Destroy(gameObject);
        }
    }
}
