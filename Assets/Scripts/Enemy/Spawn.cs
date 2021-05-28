using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Spawn : MonoBehaviour
{
    [Header("----------------------- Количество кайдзю -----------------------")]
    [SerializeField] private int _countKaujy = 3;
    [Space]
    [Header("---------------------------- Системные --------------------------")]
    [SerializeField] private Transform _kaujy;
    [SerializeField] private Transform _panzer;
    [SerializeField] private GameObject _spawnPanel;
    [SerializeField] private TMP_Text _countText;
    
    private bool _readySpawn;
    private Vector3 _coordCell;
    private Transform _targetCell;
    
    private void Start()
    {
        _spawnPanel.SetActive(true);
        _countText.text = "Left: " + _countKaujy;
    }

    private void SpawnEnemy()
    {
        
    }

    public void ClearSpawnCoord()
    {
        _coordCell = Vector3.zero;
        if (_targetCell is { }) _targetCell.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
        _targetCell = null;
    }


    public void SpawnKaujy()
    {
        if (_targetCell != null)
        {
            Instantiate(_kaujy, _coordCell, Quaternion.identity);
            _countKaujy -= 1;
            _countText.text = "Left: " + _countKaujy;
            if (_countKaujy <= 0)
            {
                _spawnPanel.SetActive(false);
                ClearSpawnCoord();
            }
        }
        else
        {
            Debug.Log("нельзя создать");
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown (0))
        {
            
            if(!EventSystem.current.IsPointerOverGameObject())
            {
                ClearSpawnCoord();
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(ray, out hit, 100f))
                {
                    if(hit.transform.GetComponent<TileParameters>() != null && hit.transform.GetComponent<TileParameters>().SpawnKaujy)
                    {
                        _coordCell = new Vector3(hit.transform.position.x, hit.transform.position.y + 0.8f, hit.transform.position.z);
                        hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
                        _targetCell = hit.transform;
                    }
                    else
                    {
                        Debug.Log("нельзя выделить");
                    }
                }
            }
        }
    }
}
