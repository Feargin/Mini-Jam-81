using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEgg : Singleton<SpawnEgg>
{
	[SerializeField] private Egg _egg;
    [SerializeField] private int _hpNewKaujy;
    
    public void Spawner(Vector3 position)
    {
        var egg = Instantiate(_egg, position, Quaternion.identity);
        Spawn.Instance.Players.Add(egg);
    }
    
    public void SpawnEpickKaujy(Vector3 position)
    {
        var kaujy = Instantiate(Spawn.Instance.EpicKaujy, position, Quaternion.identity);
        Spawn.Instance.Players.Add(kaujy);
        SoundController.Instance.SetClip(3);
        kaujy.GetComponent<PlayerEntity>()._health = _hpNewKaujy;
    }

    
}
