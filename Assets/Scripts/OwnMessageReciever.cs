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

    void OnEnable()
    {
        damageableScript.onDamageMessageReceivers.Add(this);
    }

    void OnDisable()
    {
        damageableScript.onDamageMessageReceivers.Remove(this);
    }

    public void OnReceiveMessage(MessageType type, object sender, object data)
    {
        switch (type)
        {
            case MessageType.DAMAGED:
                MonoBehaviour senderData = (MonoBehaviour)sender;
                Debug.Log(senderData.transform.position);
                StartCoroutine(DamagedMessage(senderData.transform.position));
                break;
            case MessageType.DEAD:
                break;
            case MessageType.RESPAWN:
                break;
            default:
                break;
        }
    }

    IEnumerator DamagedMessage(Vector3 position)
    {
        WWWForm form = new WWWForm();
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
            Debug.Log(www.downloadHandler.text);
            Debug.Log("Form upload complete!");
        }
    }
}
