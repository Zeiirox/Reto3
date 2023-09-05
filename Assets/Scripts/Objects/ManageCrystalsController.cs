using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManageCrystalsController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI counter;

    public static int collectedCrystals { get; set; }
    private int totalCrystals;
    private List<Transform> crystals = new List<Transform>();


    private void Start()
    {
        collectedCrystals = 0;
        totalCrystals = gameObject.transform.childCount;
        counter.text = "0 / " + totalCrystals;
        getAllChild();
    }

    void Update()
    {
        if (collectedCrystals == totalCrystals)
        {
            Debug.Log("SIIIII GANASTE OMEEE");
        }
        else
        {
            int indexToEnable = totalCrystals - (collectedCrystals + 1);
            crystals[indexToEnable].gameObject.SetActive(true);
        }

        counter.text = collectedCrystals + " / " + totalCrystals;
    }

    private void getAllChild()
    {
        for (int i = 0; i < totalCrystals; i++)
        {
            crystals.Add(transform.GetChild(i));
        }
    }
}
