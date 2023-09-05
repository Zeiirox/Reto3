using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManageCrystalsController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI counter;

    public static int collectedCrystals { get; set; }
    private int totalCrystals;
    private Transform[] crystals;


    private void Start()
    {
        collectedCrystals = 0;
        totalCrystals = gameObject.transform.childCount;
        counter.text = "0 / " + totalCrystals;
        getAllChild();
    }

    void Update()
    {
        int indexToEnable = totalCrystals - collectedCrystals;
        crystals[indexToEnable].gameObject.SetActive(true);
        if (collectedCrystals == totalCrystals)
        {
            Debug.Log("SIIIII GANASTE OMEEE");
        }

        counter.text = collectedCrystals + " / " + totalCrystals;
    }

    private void getAllChild()
    {
        for (int i = 0; i < totalCrystals; i++)
        {
            crystals[i] = transform.GetChild(i);
            crystals[i].gameObject.SetActive(false);
        }
    }
}
