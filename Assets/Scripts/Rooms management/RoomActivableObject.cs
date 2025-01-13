using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class RoomActivableObject : MonoBehaviour
{
    protected virtual void Awake()
    {
        RoomManager man = GetComponentInParent<RoomManager>();

        //Debug.Log(gameObject.ToString() + " listeners added");

        man.activateRoomEvent.AddListener(onRoomActivate);
        man.deactivateRoomEvent.AddListener(onRoomDeactivate);
    }

    protected abstract void onRoomActivate();
    protected abstract void onRoomDeactivate();

}
