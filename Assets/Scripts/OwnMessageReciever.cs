using Gamekit3D;
using Gamekit3D.Message;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class OwnMessageReciever : MonoBehaviour, IMessageReceiver
{
    [SerializeField] Damageable playerDamageableScript;
    [SerializeField] Damageable[] enemyDamageableScript;
    [SerializeField] Damageable[] boxDamageableScript;
    int playerID = -1;

    void OnEnable()
    {
        playerDamageableScript.onDamageMessageReceivers.Add(this);

        foreach (var script in enemyDamageableScript)
        {
            script.onDamageMessageReceivers.Add(this);
        }

        foreach (var script in boxDamageableScript)
        {
            script.onDamageMessageReceivers.Add(this);
        }

        StartCoroutine(GetLastPlayerID());
    }

    void OnDisable()
    {
        playerDamageableScript.onDamageMessageReceivers.Remove(this);

        foreach (var script in enemyDamageableScript)
        {
            script.onDamageMessageReceivers.Remove(this);
        }

        foreach (var script in boxDamageableScript)
        {
            script.onDamageMessageReceivers.Remove(this);
        }
    }

    void Start()
    {
        StartCoroutine(PathMessage());
    }

    public void OnReceiveMessage(MessageType type, object sender, object data)
    {
        MonoBehaviour senderData = (MonoBehaviour)sender;
        switch (type)
        {
            case MessageType.DAMAGED:
                if (senderData.name == "Ellen")
                {
                    Debug.Log(senderData.transform.position);
                    StartCoroutine(DamagedMessage(senderData.transform.position));
                }
                else
                {
                    Debug.Log(senderData.transform.position);
                    StartCoroutine(HitMessage(senderData.transform.position, senderData.name));
                }
                break;
            case MessageType.DEAD:
                if (senderData.name == "Ellen")
                {
                    Debug.Log(senderData.transform.position);
                    StartCoroutine(DeathMessage(senderData.transform.position));
                }
                break;
            default:
                break;
        }
    }

    IEnumerator GetLastPlayerID()
    {
        WWWForm form = new WWWForm();

        UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~bielrg/GetLastPlayerID.php", form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            playerID = int.Parse(www.downloadHandler.text) + 1;
            Debug.Log("Current PlayerID is " + playerID);
        }
    }

    IEnumerator DamagedMessage(Vector3 position)
    {
        WWWForm form = new WWWForm();
        form.AddField("PlayerID", playerID.ToString());
        form.AddField("PositionX", position.x.ToString().Replace(",", "."));
        form.AddField("PositionY", position.y.ToString().Replace(",", "."));
        form.AddField("PositionZ", position.z.ToString().Replace(",", "."));

        UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~bielrg/GetDamageSim.php", form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Damage Message Complete");
        }
    }

    IEnumerator DeathMessage(Vector3 position)
    {
        WWWForm form = new WWWForm();
        form.AddField("PlayerID", playerID.ToString());
        form.AddField("PositionX", position.x.ToString().Replace(",", "."));
        form.AddField("PositionY", position.y.ToString().Replace(",", "."));
        form.AddField("PositionZ", position.z.ToString().Replace(",", "."));

        UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~bielrg/DeathSim.php", form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Death Message Complete");
        }
    }

    IEnumerator PathMessage()
    {
        WWWForm form = new WWWForm();
        form.AddField("PlayerID", playerID.ToString());
        form.AddField("PositionX", playerDamageableScript.transform.position.x.ToString().Replace(",", "."));
        form.AddField("PositionY", playerDamageableScript.transform.position.y.ToString().Replace(",", "."));
        form.AddField("PositionZ", playerDamageableScript.transform.position.z.ToString().Replace(",", "."));
        float rot = playerDamageableScript.transform.rotation.eulerAngles.y;
        form.AddField("Rotation", rot.ToString().Replace(",", "."));

        UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~bielrg/PathSim.php", form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Path Message Complete");
        }

        yield return new WaitForSeconds(2);

        StartCoroutine(PathMessage());
    }

    IEnumerator HitMessage(Vector3 position, string entityName)
    {
        WWWForm form = new WWWForm();
        form.AddField("PlayerID", playerID.ToString());
        form.AddField("PositionX", position.x.ToString().Replace(",", "."));
        form.AddField("PositionY", position.y.ToString().Replace(",", "."));
        form.AddField("PositionZ", position.z.ToString().Replace(",", "."));
        form.AddField("ObjectHitted", entityName);

        UnityWebRequest www = UnityWebRequest.Post("https://citmalumnes.upc.es/~bielrg/HitSim.php", form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Hit Message Complete");
        }
    }
}
