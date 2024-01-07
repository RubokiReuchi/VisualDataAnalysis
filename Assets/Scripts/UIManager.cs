using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] Camera topCamera;
    [SerializeField] Camera FrontCamera;

    [SerializeField] TMP_Dropdown playerList;

    private void Start()
    {
        playerList.ClearOptions();

        playerList.options.Add(new TMP_Dropdown.OptionData() {text = "All"});

        for (int i = 1; i <= 50; i++)
        {
            playerList.options.Add(new TMP_Dropdown.OptionData() { text = i.ToString() });
        }
    }

    public void SwitchCamera()
    {
        if(topCamera.gameObject.activeInHierarchy)
        {
            FrontCamera.gameObject.SetActive(true);
            topCamera.gameObject.SetActive(false);
        }
        else
        {
            topCamera.gameObject.SetActive(true);
            FrontCamera.gameObject.SetActive(false);
        }
    }
}
