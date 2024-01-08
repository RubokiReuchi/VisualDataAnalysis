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

    [SerializeField] TestForReceiver controllCheckBox;

    private void Start()
    {
        playerList.ClearOptions();

        playerList.options.Add(new TMP_Dropdown.OptionData() {text = "All"});

        for (int i = 1; i <= 50; i++)
        {
            playerList.options.Add(new TMP_Dropdown.OptionData() { text = i.ToString() });
        }

        topCamera.transform.position = new Vector3(7.7f, 45, 0);
    }

    private void Update()
    {
        //Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.None;

        if (topCamera.gameObject.activeInHierarchy)
        {
            if (Input.GetKey(KeyCode.A) && topCamera.transform.position.x >= 0) 
                topCamera.transform.position = new Vector3(topCamera.transform.position.x - 0.1f, topCamera.transform.position.y, topCamera.transform.position.z);
            if (Input.GetKey(KeyCode.D) && topCamera.transform.position.x <= 55)
                topCamera.transform.position = new Vector3(topCamera.transform.position.x + 0.1f, topCamera.transform.position.y, topCamera.transform.position.z);

            if (Input.GetKey(KeyCode.W) && topCamera.transform.position.z <= 25) 
                topCamera.transform.position = new Vector3(topCamera.transform.position.x, topCamera.transform.position.y, topCamera.transform.position.z + 0.1f);
            if (Input.GetKey(KeyCode.S) && topCamera.transform.position.z >= -20) 
                topCamera.transform.position = new Vector3(topCamera.transform.position.x, topCamera.transform.position.y, topCamera.transform.position.z - 0.1f);

            if (Input.GetAxis("Mouse ScrollWheel") < 0 && topCamera.transform.position.y <= 100) 
                topCamera.transform.position = new Vector3(topCamera.transform.position.x, topCamera.transform.position.y + 1, topCamera.transform.position.z);
            if (Input.GetAxis("Mouse ScrollWheel") > 0 && topCamera.transform.position.y >= 15) 
                topCamera.transform.position = new Vector3(topCamera.transform.position.x, topCamera.transform.position.y - 1, topCamera.transform.position.z);
        }
        else
        {
            if (Input.GetKey(KeyCode.A))
                frontCamera.transform.eulerAngles += new Vector3(0,-0.1f,0);
            if (Input.GetKey(KeyCode.D))
                frontCamera.transform.eulerAngles += new Vector3(0,0.1f,0);

            if (Input.GetKey(KeyCode.W))
                frontCamera.transform.position += Vector3.forward * 0.1f;
            if (Input.GetKey(KeyCode.S))
                frontCamera.transform.position += Vector3.back * 0.1f;
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
        controllCheckBox.userID = (uint)playerList.value;
    }

    public void ActivateDeath()
    {
        if(death.isOn)
        { 
            controllCheckBox.showDeath = true;
        }
        else
        {
            controllCheckBox.showDeath = false;
        }
    }

    public void ActivateDamage()
    {
        if (damage.isOn)
        {
            controllCheckBox.showHit = true;
        }
        else
        {
            controllCheckBox.showHit = false;
        }
    }

    public void ActivateInteraction()
    {
        if (interaction.isOn)
        {
            controllCheckBox.showAttack = true;
        }
        else
        {
            controllCheckBox.showAttack = false;
        }
    }

    public void ActivatePath()
    {
        if (path.isOn)
        {
            controllCheckBox.showPath = true;
        }
        else
        {
            controllCheckBox.showPath = false;
        }
    }
}
