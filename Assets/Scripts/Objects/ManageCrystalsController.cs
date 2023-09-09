using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManageCrystalsController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI counter;

    public static int collectedCrystals { get; set; }
    public static bool enableCrystals { get; set; }
    private int totalCrystals;
    private List<Transform> crystals = new List<Transform>();


    private void Start()
    {
        enableCrystals = false;
        collectedCrystals = 0;
        totalCrystals = gameObject.transform.childCount;
        counter.text = "0 / " + totalCrystals;
        getAllChild();
    }

    void Update()
    {

        if (collectedCrystals == totalCrystals)
        {
            PlantaSolar.activatePlant = true;
        }
        if (collectedCrystals < totalCrystals)
        {
            crystals[collectedCrystals].gameObject.SetActive(true);
        }
        counter.text = collectedCrystals + " / " + totalCrystals;
    }

    private void getAllChild()
    {
        for (int i = 0; i < totalCrystals; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
            crystals.Add(transform.GetChild(i));
        }
    }

    public void ClearScore()
    {
        collectedCrystals = 0;
        totalCrystals = 0;
        counter.text = "0 / 0";
    }

}
