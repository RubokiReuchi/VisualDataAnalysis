using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] Camera topCamera;
    [SerializeField] Camera frontCamera;

    [SerializeField] TMP_Dropdown playerList;

    [SerializeField] Toggle death;
    [SerializeField] Toggle damage;
    [SerializeField] Toggle interaction;
    [SerializeField] Toggle path;

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
            Debug.Log("Front");
            frontCamera.gameObject.SetActive(true);
            topCamera.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("top");
            topCamera.gameObject.SetActive(true);
            frontCamera.gameObject.SetActive(false);
        }
    }

    public void SelectPlayer()
    {
        Debug.Log("Player selectes is: " + playerList.value.ToString());
    }

    public void ActivateDeath()
    {
        if(death.isOn)
        { 
           Debug.Log("Show Death");
        }
        else
        {
            Debug.Log("Hide Death");
        }
    }

    public void ActivateDamage()
    {
        if (damage.isOn)
        {
            Debug.Log("Show Damage");
        }
        else
        {
            Debug.Log("Hide Damage");
        }
    }

    public void ActivateInteraction()
    {
        if (interaction.isOn)
        {
            Debug.Log("Show Interaction");
        }
        else
        {
            Debug.Log("Hide Interaction");
        }
    }

    public void ActivatePath()
    {
        if (path.isOn)
        {
            Debug.Log("Show Path");
        }
        else
        {
            Debug.Log("Hide Path");
        }
    }
}
