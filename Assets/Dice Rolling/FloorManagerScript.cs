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
    public Transform _PlayerDice;
    [SerializeField]
    private Vector2 _PlayerCoord; 

    private Dictionary<Vector2, Transform> _CoordToTileMap = new Dictionary<Vector2, Transform>();
    private Dictionary<Vector2, Transform> _CoordToSensorMap = new Dictionary<Vector2, Transform>();
    private void Start()
    {
        //Add initial tiles
        _CoordToTileMap.Add(new Vector2(0, 0), _FloorParent.transform.Find("BlackTile"));
        _CoordToSensorMap.Add(new Vector2(0, 0), null);
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
    private void SpawnNearbySensors()
    {
        //for each coordinate within a certain distance of the player, check if sensor is already there, if not spawn it
        float maxDist = 5;
        for (int x = (int)-maxDist; x <= maxDist; x++)
        {
            for (int z = (int)-maxDist; z <= maxDist; z++)
            {
                //sqrMag to avoid square rooting
                if ( new Vector2(x,z).sqrMagnitude <= maxDist * maxDist )
                {
                    Transform tSensor, tTile;
                    Vector2 potentialCoord = _PlayerCoord + new Vector2(x, z);
                    _CoordToSensorMap.TryGetValue(potentialCoord, out tSensor);
                    _CoordToTileMap.TryGetValue(potentialCoord, out tTile);
                    
                    if (tSensor == null && tTile == null)
                    {
                        //Couldn't find sensor or tile so spawn a sensor                        
                        if ((Mathf.Abs(potentialCoord.x) + Mathf.Abs(potentialCoord.y)) % 2 == 0)
                        {
                            SpawnSensor(potentialCoord, false);
                            Debug.Log(Mathf.Abs(potentialCoord.x) + " + " + Mathf.Abs(potentialCoord.y) + " % 2 == " + ((Mathf.Abs(potentialCoord.x) + Mathf.Abs(potentialCoord.y)) % 2) + " creating black");
                        }
                        else
                        {
                            SpawnSensor(potentialCoord, true);
                            Debug.Log(Mathf.Abs(potentialCoord.x) + " + " + Mathf.Abs(potentialCoord.y) + " % 2 == " + ((Mathf.Abs(potentialCoord.x) + Mathf.Abs(potentialCoord.y)) % 2) + " creating white");
                        }
                    }
                }
            }
        }
    }
    private void SpawnSensor(Vector2 coord, bool BlackOrWhite)
    {
        //TODO: Use object pooling here instead of instantiating new sensors every time
        GameObject sensor = GameObject.Instantiate(_Sensor, new Vector3(coord.x, 0, coord.y), Quaternion.identity, _SensorParent);
        sensor.GetComponent<TileSpawnSensor>().SetFloorManager(this);
        sensor.GetComponent<TileSpawnSensor>().SetColour(BlackOrWhite);
        _CoordToSensorMap.Add(coord, sensor.transform);
        Debug.Log("Spawned sensor @ " + coord.x + "," + coord.y);
    }
    public void SpawnTile(Vector2 coord, bool blackOrWhite)
    {
        if (_CoordToTileMap.ContainsKey(coord))
        {
            Debug.LogError("Trying to add tile that already exists");
            return;
        }

        //TODO: Use object pooling here instead of instantiating new tiles every time
        if (blackOrWhite)
        {
            GameObject t = GameObject.Instantiate(_WhiteTile, new Vector3(coord.x, TileSpawnHeight, coord.y), Quaternion.identity, _FloorParent);
            t.transform.GetChild(0).GetComponent<Animation>().Play("FlyUp");
            _CoordToTileMap.Add(coord, t.transform);
        }
        else
        {
            GameObject t = GameObject.Instantiate(_BlackTile, new Vector3(coord.x, TileSpawnHeight, coord.y), Quaternion.identity, _FloorParent);
            t.transform.GetChild(0).GetComponent<Animation>().Play("FlyUp");
            _CoordToTileMap.Add(coord, t.transform);
        }
        Debug.Log("Spawned tile @ " + coord.x + "," + coord.y);
    }
    private void RemoveTile()
    {

    }


    private void Update()
    {
        if (_PlayerCoord != new Vector2((int)_PlayerDice.position.x, (int)_PlayerDice.position.z))
        {
            ChangeCoord(new Vector2((int)_PlayerDice.position.x, (int)_PlayerDice.position.z));
        }        
    }
    private void ChangeCoord(Vector2 newCoord)
    {
        _PlayerCoord = newCoord;

        //Perform any actions taken when player enters new tile
        SpawnNearbySensors();
    }
    
}
