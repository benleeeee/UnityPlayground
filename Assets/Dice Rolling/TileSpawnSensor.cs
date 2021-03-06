using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawnSensor : MonoBehaviour
{
    private FloorManagerScript _FM;
    [SerializeField]
    private bool _BlackOrWhite; //false = black, true = white
    public void SetFloorManager(FloorManagerScript fm) { _FM = fm; }
    public void SetColour(bool blackOrWhite) { _BlackOrWhite = blackOrWhite; }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("TileSpawnSensor"))
        {
            _FM.SpawnTile(new Vector2(transform.position.x, transform.position.z), _BlackOrWhite);
            GameObject.Destroy(this.gameObject);
        }
    }
}
