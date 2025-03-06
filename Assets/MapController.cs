using System;
using System.Collections.Generic;
using System.Linq;
using _scripts.Objects.Door;
using _scripts.UI.UI_Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[Serializable]
public struct Room
{
    public int ID;
    public Vector2 mapUIPosition;
}

public class MapController : MonoBehaviour
{
    private RoomIdInfo _lastEnteredRoom;
    [SerializeField] private List<Room> rooms = new List<Room>();
    [SerializeField] private GameObject playerIcon;
    private GameObject _currentImage;

    private void Start()
    {
        LoadRooms();
    }
    private void LoadRooms()
    {
        RoomUIPos[] roomUIObjects = FindObjectsOfType<RoomUIPos>();
        rooms = roomUIObjects.Select(rui => new Room {
            ID = rui.roomID,
            mapUIPosition = rui.GetComponent<RectTransform>().anchoredPosition}).ToList();
    }

    public void OnPlayerEnterRoom(RoomIdInfo roomId)
    {
        Debug.Log($"Room Entered: {roomId.name}");
        Room newRoom = rooms.FirstOrDefault(r => r.ID == roomId.ID);
        if (newRoom.ID == 0 && newRoom.mapUIPosition == Vector2.zero && roomId.ID != 0)
        {
            Debug.Log($"Room id {roomId.ID} not found in UI positions");
            return;
        }

        GameObject roomUIObject = FindRoomUIObjectById(newRoom.ID);
        if (roomUIObject != null)
        {
            if (_currentImage != null)
            {
                _currentImage.GetComponent<RectTransform>().anchoredPosition = newRoom.mapUIPosition;
            }
            else
            {
                _currentImage = Instantiate(playerIcon, roomUIObject.transform);
                _currentImage.GetComponent<RectTransform>().anchoredPosition = newRoom.mapUIPosition;
            }
        }
        else
        {
            Debug.Log($"No UI object found for ROOM ID {newRoom.ID}");
        }
        _lastEnteredRoom = roomId;
    }

    private GameObject FindRoomUIObjectById(int roomId)
    {
        RoomUIPos roomUIObject = FindObjectsOfType<RoomUIPos>().FirstOrDefault(r => r.roomID == roomId);
        return roomUIObject ? roomUIObject.gameObject : null;
    }
        // Buscar todos los cuartos en el mapa (Estos son objetos de UI vacios)
        // Por cada uno de ellos mirar cual es el ID que corresponde con el _lastEnteredRoom.ID
        // Si uno de estos coincide, poner el sprite dentro de este
        //return _rooms.First(e => e.ID == _lastEnteredRoom.ID);
}

