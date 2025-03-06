using System;
using TMPro;
using UnityEngine;

namespace _scripts.Objects.Door
{
    [Serializable]
    public struct RoomIdInfo
    {
        public string name;
        public int ID;
    }
    public class NameRoom : MonoBehaviour
    {
        public RoomIdInfo roomInfo;
        public TextMeshProUGUI textNameRoom;
        private MapController _mapController;

        private void Start()
        {
            _mapController = FindObjectOfType<MapController>();
            if (!_mapController)
            {
                Debug.LogError("Map Controller not found");
                return;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _mapController.OnPlayerEnterRoom(roomInfo);
                textNameRoom.text = roomInfo.name;
            }
        }
    }
}