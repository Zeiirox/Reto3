using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantaSolar : MonoBehaviour
{
    public static bool activatePlant { get; set; }
    [SerializeField] private GameObject solarPlantActive;
    [SerializeField] private ManageCrystalsController manageCrystals;
    [SerializeField] private GameObject letter;


    private bool supply;

    // Start is called before the first frame update
    void Start()
    {
        supply = false;
        solarPlantActive.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            letter.SetActive(true);
            supply = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            letter.SetActive(false);
            supply = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (activatePlant && supply && Input.GetKeyDown(KeyCode.E))
        {
            manageCrystals.ClearScore();
            gameObject.SetActive(false);
            solarPlantActive.SetActive(true);
            letter.SetActive(false);
        }
        
    }
}
