using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    [SerializeField] private int _liveCount;
    private void OnEnable() => ChangeTurn.TheNextTurn += StartCountLive;
    private void OnDisable() => ChangeTurn.TheNextTurn -= StartCountLive;
    

    private void StartCountLive(bool go)
    {
        if (!go) return;
        _liveCount += 1;
        if (_liveCount >= 2)
        {
            SpawnEgg.Instance.SpawnEpickKaujy(transform.position);
            Destroy(gameObject);
        }
    }
    
}
