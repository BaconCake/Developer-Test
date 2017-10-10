using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
	public static List<Asset> assets;
	public static List<GameObject> InstantiatedAssets;
	public static Asset ActiveLoadingAsset;
	public static AsyncOperation CurrentInstantiatingAssetOperation { get; internal set; }
	private static bool isSceneSetup;

	public List<TextAsset> jsonTexts;
	private JsonReader jsonReader;



	// Use this for initialization
	private void Awake ()
	{
		jsonReader = new JsonReader ();
		assets = new List<Asset> ();
		InstantiatedAssets = new List<GameObject> ();
	}

	private void Start ()
	{
		foreach (var text in jsonTexts) {
			var asset = jsonReader.ReadFromJson (text.text);

			StartCoroutine (asset.Load ());
		}
	}

	public void Update ()
	{
		if (GameManager.CurrentInstantiatingAssetOperation != null) {
			if (GameManager.CurrentInstantiatingAssetOperation.isDone == true) {
				if (GameManager.ActiveLoadingAsset.type.Equals ("character")) {
					Debug.Log (GameManager.ActiveLoadingAsset.Name + "has finished loading from" + GameManager.ActiveLoadingAsset.url);
				}

				if (GameManager.ActiveLoadingAsset.type.Equals ("location"))
					Debug.Log ("Location " + GameManager.ActiveLoadingAsset.Name + "has been loaded!");
			}
		}

		SetupScene ();
	}

	private static void SetupScene ()
	{
		if (assets.Count == ((GameManager)GameObject.Find ("GameManager").GetComponent ("GameManager")).jsonTexts.Count && isSceneSetup == false) {
			foreach (var asset in assets) {
				asset.Instantiate ();
			}

			isSceneSetup = true;
		}
	}
}