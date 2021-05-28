using UnityEngine.SceneManagement;
using UnityEngine;

public class Cheats : MonoBehaviour
{
	private void Update()
    {
	    if(Input.GetKeyDown(KeyCode.P))
	    {
	    	SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	    }
    }
}
