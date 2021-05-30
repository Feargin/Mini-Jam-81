using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEgg : Singleton<SpawnEgg>
{
    [SerializeField] private GameObject _egg;
    [SerializeField] private int _hpNewKaujy;
    
    public void Spawner(Vector3 position)
    {
        Instantiate(_egg, position, Quaternion.identity);
    }
    
    public void SpawnEpickKaujy(Vector3 position)
    {
        var kaujy = Instantiate(Spawn.Instance.EpicKaujy, position, Quaternion.identity);
        kaujy.GetComponent<PlayerEntity>()._health = _hpNewKaujy;
    }

    
}
