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
    const float TileSpawnHeight = 0f;
    public GameObject _Sensor;
    public GameObject _BlackTile;
    public GameObject _WhiteTile;
    public Transform _SensorParent;
    public Transform _FloorParent;

    private Dictionary<Vector2, Transform> _CoordToTileMap = new Dictionary<Vector2, Transform>();
    private Dictionary<Vector2, Transform> _CoordToSensorMap = new Dictionary<Vector2, Transform>();
    private void Start()
    {
        //Add initial tiles
        _CoordToTileMap.Add(new Vector2(0, 0), _FloorParent.transform.Find("BlackTile"));
        SpawnInitialSensors();
    }
    private void SpawnInitialSensors()
    {
        SpawnSensor(new Vector2(0, 1), true);
        SpawnSensor(new Vector2(1, 0), true);
        SpawnSensor(new Vector2(0, -1), true);
        SpawnSensor(new Vector2(-1, 0), true);
        SpawnSensor(new Vector2(1, 1), false);        
        SpawnSensor(new Vector2(-1, 1), false);
        SpawnSensor(new Vector2(1, -1), false);
        SpawnSensor(new Vector2(-1, -1), false);
    }
    private void SpawnSensor(Vector2 worldPos, bool BlackOrWhite)
    {
        //TODO: Use object pooling here instead of instantiating new sensors every time
        GameObject sensor = GameObject.Instantiate(_Sensor, new Vector3(worldPos.x, 0, worldPos.y), Quaternion.identity, _SensorParent);
        sensor.GetComponent<TileSpawnSensor>().SetFloorManager(this);
        sensor.GetComponent<TileSpawnSensor>().SetColour(BlackOrWhite);
        _CoordToSensorMap.Add(worldPos, sensor.transform);
    }
    public void SpawnTile(Vector3 worldPos, bool blackOrWhite)
    {
        //TODO: Use object pooling here instead of instantiating new tiles every time
        if (blackOrWhite)
        {
            GameObject t = GameObject.Instantiate(_WhiteTile, new Vector3(worldPos.x, TileSpawnHeight, worldPos.z), Quaternion.identity, _FloorParent);
            t.transform.GetChild(0).GetComponent<Animation>().Play("FlyUp");
        }
        else
        {
            GameObject t = GameObject.Instantiate(_BlackTile, new Vector3(worldPos.x, TileSpawnHeight, worldPos.z), Quaternion.identity, _FloorParent);
            t.transform.GetChild(0).GetComponent<Animation>().Play("FlyUp");
        }
    }
    private void RemoveTile()
    {

    }

}
