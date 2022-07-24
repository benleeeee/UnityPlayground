using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class will maintain the floor tiles lifecycles, spawning and removing them
/// when necessary
/// 
/// It will also maintain the floor tile spawn sensors that are invisibly placed to
/// detect when the player moves close enough that a tile should spawn
/// </summary>
public class FloorManagerScript : MonoBehaviour
{
    public GameObject _Sensor;
    public GameObject _BlackTile;
    public GameObject _WhiteTile;
    public Transform _SensorParent;
    public Transform _FloorParent;

    private void Start()
    {
        SpawnSensor();
    }
    private void SpawnSensor()
    {
        GameObject sensor = GameObject.Instantiate(_Sensor, new Vector3(5.514f, 0, 0), Quaternion.identity, _SensorParent);
        sensor.GetComponent<TileSpawnSensor>().SetFloorManager(this);
        sensor.GetComponent<TileSpawnSensor>().SetColour(false);
        sensor = GameObject.Instantiate(_Sensor, new Vector3(5.514f + (1 * 1), 0, 0), Quaternion.identity, _SensorParent);
        sensor.GetComponent<TileSpawnSensor>().SetFloorManager(this);
        sensor.GetComponent<TileSpawnSensor>().SetColour(true);
        sensor = GameObject.Instantiate(_Sensor, new Vector3(5.514f + (2 * 1), 0, 0), Quaternion.identity, _SensorParent);
        sensor.GetComponent<TileSpawnSensor>().SetFloorManager(this);
        sensor.GetComponent<TileSpawnSensor>().SetColour(false);
        sensor = GameObject.Instantiate(_Sensor, new Vector3(5.514f + (3 * 1), 0, 0), Quaternion.identity, _SensorParent);
        sensor.GetComponent<TileSpawnSensor>().SetFloorManager(this);
        sensor.GetComponent<TileSpawnSensor>().SetColour(true);
    }
    public void SpawnTile(Vector3 worldPos, bool blackOrWhite)
    {
        //TODO: Use object pooling here instead of instantiating new tiles every time
        if (blackOrWhite)
            GameObject.Instantiate(_WhiteTile, worldPos, Quaternion.identity, _FloorParent);
        else
            GameObject.Instantiate(_BlackTile, worldPos, Quaternion.identity, _FloorParent);
    }
    private void RemoveTile()
    {

    }

}
