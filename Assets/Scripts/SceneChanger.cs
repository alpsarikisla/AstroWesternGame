using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
	public void ChangeScene(int scene_ID)
	{
		SceneManager.LoadScene(scene_ID);
	}
	public void Exit()
	{
		Application.Quit();
	}
}
