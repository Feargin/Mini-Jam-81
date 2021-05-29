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
    [SerializeField] private Transform [] _enemy;
    public List<Transform> Enemyes;
    public List<Transform> Players;
    [SerializeField] private GameObject _spawnPanel;
    [SerializeField] private TMP_Text _countText;
    
    private bool _readySpawn;
    private Vector3 _coordCell;
    private Transform _targetCell;
    
    private void Start()
    {
        _spawnPanel.SetActive(true);
        _countText.text = "Left: " + _countKaujy;
        Invoke("SpawnEnemy", 0.2f);
    }

    private void SpawnEnemy()
    {
        for (int i = 0; i < 4; i++)
        {
            Vector3 spawnCoord = new Vector3(Random.Range(0, 8), 0, Random.Range(0, 8));
            Ray ray = new Ray(new Vector3(spawnCoord.x, 6, spawnCoord.z), Vector3.down);
            RaycastHit hit = new RaycastHit();
            
            if (Physics.Raycast(ray, out hit, 100f ))
            {
                
                if (hit.transform.GetComponent<TileParameters>() != null &&
                    hit.transform.GetComponent<TileParameters>().SpawnPanzer &&
                    hit.transform.GetComponent<Attak>() == null)
                {
                    var enemy = Instantiate(_enemy[Random.Range(0, 3)], spawnCoord + Vector3.up, Quaternion.identity);
                    Enemyes.Add(enemy);
                }
                else
                {
                    //Debug.Log("нельзя выделить");
                }
            }

        }
        
    }

    public void ClearSpawnCoord()
    {
        _coordCell = Vector3.zero;
	    if (_targetCell is { }) _targetCell.gameObject.GetComponent<Tile>().SetColor(Color.white);
        _targetCell = null;
    }


    public void SpawnKaujy()
    {
        if (_targetCell != null)
        {
            var player = Instantiate(_kaujy, _coordCell, Quaternion.identity);
            _countKaujy -= 1;
            _countText.text = "Left: " + _countKaujy;
            Enemyes.Add(player);
            if (_countKaujy <= 0)
            {
                _spawnPanel.SetActive(false);
            }

            _targetCell.GetComponent<TileParameters>().SpawnKaujy = false;
            ClearSpawnCoord();
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
	                    hit.transform.gameObject.GetComponent<Tile>().SetColor(Color.blue);
                        _targetCell = hit.transform;
                    }
                    else
                    {
                        //Debug.Log("нельзя выделить");
                    }
                }
            }
        }
    }
}
