using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCrystals : MonoBehaviour
{

    [SerializeField] private ManageCrystalsController controller;
    public int index;
    public int Index
    {
        get { return index; }
        set { 
            index = value;
            PlayerPrefs.SetInt("indexCrystal", index);
        }
    }

    private void Start()
    {
        index = PlayerPrefs.GetInt("indexCrystal");
    }

    private void Update()
    {
        controller.crystals[index].gameObject.GetComponent<CrystalsController>().isVisible = true;
    }
}
