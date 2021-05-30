using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _failPanel;
    private void Start()
    {
        _winPanel.SetActive(false);
        _failPanel.SetActive(false);
    }

    
    private void FixedUpdate()
    {
        if (ChangeTurn.Instance.CountTurn >= 2)
        {
            if (Spawn.Instance.Enemyes.Count <= 0)
            {
                _winPanel.SetActive(true);
                ChangeTurn.Instance.CountTurn = 0;
            }
            else if (Spawn.Instance.Players.Count <= 0)
            {
                _failPanel.SetActive(true);
                ChangeTurn.Instance.CountTurn = 0;
            }
        }
    }

    public void Restart() => SceneManager.LoadScene(0);
    public void Exit() => Application.Quit();
}
