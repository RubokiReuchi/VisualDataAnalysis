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

    private void Update()
    {
        if(topCamera.gameObject.activeInHierarchy)
        {
            if (Input.GetKey(KeyCode.A)) topCamera.transform.position = new Vector3(topCamera.transform.position.x - 0.1f, topCamera.transform.position.y, topCamera.transform.position.z);
            if (Input.GetKey(KeyCode.D)) topCamera.transform.position = new Vector3(topCamera.transform.position.x + 0.1f, topCamera.transform.position.y, topCamera.transform.position.z);

            if (Input.GetKey(KeyCode.W)) topCamera.transform.position = new Vector3(topCamera.transform.position.x, topCamera.transform.position.y, topCamera.transform.position.z + 0.1f);
            if (Input.GetKey(KeyCode.S)) topCamera.transform.position = new Vector3(topCamera.transform.position.x, topCamera.transform.position.y, topCamera.transform.position.z - 0.1f);

            if (Input.GetAxis("Mouse ScrollWheel") < 0) topCamera.transform.position = new Vector3(topCamera.transform.position.x, topCamera.transform.position.y + 1, topCamera.transform.position.z);
            if (Input.GetAxis("Mouse ScrollWheel") > 0) topCamera.transform.position = new Vector3(topCamera.transform.position.x, topCamera.transform.position.y - 1, topCamera.transform.position.z);
        }
        else
        {

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
