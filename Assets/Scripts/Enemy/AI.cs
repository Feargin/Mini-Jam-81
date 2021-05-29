using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    private Transform _target;
    private void OnEnable() => ChangeTurn.TheNextTurn += StartAi;
    private void OnDisable() => ChangeTurn.TheNextTurn -= StartAi;
    private void StartAi(bool b)
    {
        if (b)
        {
            StartCoroutine(AITrack());
        }
    }

    private IEnumerator AITrack()
    {
        while (true)
        {
            TargetFainder();
            //проверка на возможность атаки цели
            //если невозможно, то тогда идет перемещение
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void TargetFainder()
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.position, transform.localScale * 2, Quaternion.identity);
        float distanse = Mathf.Infinity;
        
        Vector3 position = transform.position;
        foreach (var v in hitColliders)
        {
            if (!v.GetComponent<PlayerEntity>()) continue;
            Vector3 difference = v.transform.position - position;
            float currentDistanse = difference.sqrMagnitude;
            if (!(currentDistanse < distanse)) continue;
            _target = v.transform;
            distanse = currentDistanse;
        }
        
    }

    
    void Update()
    {
        
    }
}
