using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantaSolar : MonoBehaviour
{
    [SerializeField] private GameObject solarPlantActive;
    public static bool activatePlant { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        solarPlantActive.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (activatePlant)
        {
            solarPlantActive.SetActive(true);
        }
        
    }
}
