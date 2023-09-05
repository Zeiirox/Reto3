using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ManageCrystalsController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI counter;
    [SerializeField] public GameObject[] crystals;

    public static int collectedCrystals;
    private int totalCristals;

    private void Start()
    {
        collectedCrystals = 0;
        totalCristals = gameObject.transform.childCount;
        counter.text = "0 / " + totalCristals;
    }

    void Update()
    {
        if (collectedCrystals == totalCristals)
        {

        }

        counter.text = collectedCrystals + " / " + totalCristals;
    }
}
