using Gamekit3D;
using Gamekit3D.Message;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class OwnMessageReciever : MonoBehaviour, IMessageReceiver
{
    [SerializeField] Damageable damageableScript;
    int playerID = -1;

    void OnEnable()
    {
        damageableScript.onDamageMessageReceivers.Add(this);

        StartCoroutine(GetLastPlayerID());
    }

    void OnDisable()
    {
        damageableScript.onDamageMessageReceivers.Remove(this);
    }

    public void OnReceiveMessage(MessageType type, object sender, object data)
    {
        MonoBehaviour senderData = (MonoBehaviour)sender;
        switch (type)
        {
            case MessageType.DAMAGED:
                Debug.Log(senderData.transform.position);
                StartCoroutine(DamagedMessage(senderData.transform.position));
                break;
            case MessageType.DEAD:
                Debug.Log(senderData.transform.position);
                StartCoroutine(DeathMessage(senderData.transform.position));
                break;
            case MessageType.RESPAWN:
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
}
