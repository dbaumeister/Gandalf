using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputManager))]
public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject[] playerPrefab;

    Dictionary<InputDevice, PlayerInput> inputMap;
    IList<GameObject> availablePlayers;

    // Start is called before the first frame update
    void Awake()
    {
        GetComponent<PlayerInputManager>().DisableJoining();

        InputSystem.onDeviceChange += OnDeviceChange;
        inputMap = new Dictionary<InputDevice, PlayerInput>();
        availablePlayers = new List<GameObject>();


        foreach(GameObject player in playerPrefab)
        {
            GameObject instance = Instantiate(player);
            instance.SetActive(false);
            instance.name = "Player " + availablePlayers.Count;
            availablePlayers.Add(instance);
        }
    }

    private void Start()
    {
        // Check all devices on the system
        foreach (InputDevice device in InputSystem.devices)
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
		Debug.Log("INPUT DEVICE ADDED");
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
            availablePlayers.Add(player.gameObject);
            player.gameObject.SetActive(false);
            inputMap.Remove(device);
        }
    }

    void SpawnPlayer(InputDevice device)
    {
        if(!inputMap.ContainsKey(device) && availablePlayers.Count > 0)
        {
            GameObject old = availablePlayers[availablePlayers.Count - 1];
            availablePlayers.RemoveAt(availablePlayers.Count - 1);

            PlayerInput player = PlayerInput.Instantiate(old, pairWithDevice: device);
            player.name = "Player " + availablePlayers.Count;
            inputMap.Add(device, player);
            Destroy(old);
        }
    }
}
