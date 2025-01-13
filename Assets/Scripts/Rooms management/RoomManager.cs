using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomManager : MonoBehaviour
{
    public UnityEvent activateRoomEvent;
    public UnityEvent deactivateRoomEvent;

    public void activateRoom()
    {
        //Debug.Log(this.ToString() + " activated");

        activateRoomEvent.Invoke();
    }

    public void deactivateRoom()
    {
        //Debug.Log(this.ToString() + " deactivated");

        deactivateRoomEvent.Invoke();
    }
}
