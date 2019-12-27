using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject playerPrefab;

    Dictionary<InputDevice, PlayerInput> inputMap;
    IList<PlayerInput> removedPlayers;

    // Start is called before the first frame update
    void Awake()
    {
        InputSystem.onDeviceChange += OnDeviceChange;
        inputMap = new Dictionary<InputDevice, PlayerInput>();
        removedPlayers = new List<PlayerInput>();

        // Check all devices on the system
        foreach(InputDevice device in InputSystem.devices)
        {
            SpawnPlayer(device);
        }
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        switch (change)
        {
            case InputDeviceChange.Reconnected:
            case InputDeviceChange.Added:
                SpawnPlayer(device);
                break;
            case InputDeviceChange.Disconnected:
            case InputDeviceChange.Removed:
            case InputDeviceChange.Destroyed:
                RemovePlayer(device);
                // Remove from Input System entirely; by default, Devices stay in the system once discovered.
                break;
            default:
                // See InputDeviceChange reference for other event types.
                break;
        }
    }
    
    void RemovePlayer(InputDevice device)
    {
        if(inputMap.ContainsKey(device))
        {
            PlayerInput player = inputMap[device];
            removedPlayers.Add(player);
            player.gameObject.SetActive(false);
            inputMap.Remove(device);
        }
    }

    void SpawnPlayer(InputDevice device)
    {
        if(!inputMap.ContainsKey(device))
        {
            PlayerInput player;
            if (removedPlayers.Count > 0)
            {
                player = removedPlayers[removedPlayers.Count - 1];
                removedPlayers.RemoveAt(removedPlayers.Count - 1);
                player.gameObject.SetActive(true);
            }
            else
            {
                player = PlayerInput.Instantiate(playerPrefab, pairWithDevice: device);
            }

            inputMap.Add(device, player);
        }
    }
}
