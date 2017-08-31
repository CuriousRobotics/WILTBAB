using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class LevelManager : MonoBehaviour {

	[Serializable]
	class PlayerData{
		public string taskLevel;
	}	

	public string playerFilename;
	public FileStream playerFile;
	PlayerData playerData;
	public BinaryFormatter binaryFormatter;
	public string maxTaskLevel;

	void Awake() {
        DontDestroyOnLoad(transform.gameObject);
		playerFilename  = Application.persistentDataPath + "playerData.dat";
		playerData = new PlayerData();
		binaryFormatter = new BinaryFormatter();
    }

	void Start(){
		
		if(!File.Exists(playerFilename)){
			File.Create(playerFilename);
			maxTaskLevel = "Task0";
		}else{
			playerFile = File.Open(playerFilename, FileMode.Open);
			playerData = (PlayerData) binaryFormatter.Deserialize(playerFile);
			playerFile.Close();

			maxTaskLevel = playerData.taskLevel;
		}
		Debug.Log("Start(): maxTaskLevel = " + maxTaskLevel);
	}

	public void LoadLevel(string name){
		Debug.Log("Level load requested for: " + name);

		SceneManager.LoadScene(name);
		Save(name);
		/*if(name.Contains("Task")){
			Debug.Log("Max Task Level entering LoadLevel: " + maxTaskLevel);
			if(Load()){
				string taskNumber = name.TrimStart('T', 'a', 's', 'k');
				int taskInt = int.Parse(taskNumber);
				int maxTaskInt = int.Parse(maxTaskLevel);
				if(taskInt > maxTaskInt){
					Save(name);
				}
			}else if(maxTaskLevel == "Task0"){
				Save(name);
			}
			Debug.Log("Max Task Level leaving LoadLevel: " + maxTaskLevel);
		}*/
	}

	public void LoadLastLevel(){
		if(maxTaskLevel != "Task0"){
			//string taskNumber = name.TrimStart('T', 'a', 's', 'k');
			//int taskInt = int.Parse(taskNumber);
			//int maxTaskInt = int.Parse(maxTaskLevel);
			//if(taskInt > maxTaskInt){
			//	Save(name);
			//}
			LoadLevel(maxTaskLevel);
		}else{
			LoadLevel("Task1");
			maxTaskLevel = "Task1";
		}
	}
	
	public void QuitRequest(){
		Debug.Log ("Quit requested.");
		Application.Quit();
	}

	public void Save(string sceneName){
		Debug.Log("Trying to save to... " + playerFilename);
		playerFile = File.Open(playerFilename, FileMode.OpenOrCreate);
		playerData.taskLevel = sceneName;
		Debug.Log("Saving " + playerData.taskLevel + " to " + playerFilename);
		binaryFormatter.Serialize(playerFile, playerData);
		playerFile.Close();
		maxTaskLevel = playerData.taskLevel;
		Debug.Log("Save(): playerData.taskLevel = " + playerData.taskLevel + " maxTaskLevel = " + maxTaskLevel);
	}

	public bool Load(){
		Debug.Log("Load(): " + playerFilename);
		if(File.Exists(playerFilename)){
			playerFile = File.Open(playerFilename, FileMode.OpenOrCreate);
			playerData = (PlayerData) binaryFormatter.Deserialize(playerFile);
			playerFile.Close();

			maxTaskLevel = playerData.taskLevel;
			return true;
		}		
		return false;
	}
}
