using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : Entity
{
    [SerializeField] private int _liveCount;
    private void OnEnable() => ChangeTurn.TheNextTurn += StartCountLive;
    private void OnDisable() => ChangeTurn.TheNextTurn -= StartCountLive;
    

    private void StartCountLive(bool go)
    {
        if (!go) return;
        _liveCount += 1;
        if (_liveCount >= 3)
        {
            SpawnEgg.Instance.SpawnEpickKaujy(transform.position);
            Spawn.Instance.Players.Remove(transform);
            Destroy(gameObject);
        }
    }
    
}
