using UnityEngine;
using UnityEngine.UI;

public class LoadSceneButton : MonoBehaviour
{
	[SerializeField]
	private int sceneToLoadIndex;
	
	private void Awake()
	{
		GetComponent<Button>().onClick.AddListener(LoadScene);
	}

	public void LoadScene()
	{
		gameObject.SetActive(false);
		FindObjectOfType<SceneProgressLoader>().LoadScene(sceneToLoadIndex);
	}
}