using System.Collections;
using UnityEngine;
using AssemblyCSharp;


[System.Serializable]
public class CharacterAsset : Asset, IAsset
{
	private UnityEngine.Object assetObj;

	//Connects to url and loads the Character Asset
	public override IEnumerator Load ()
	{
		Debug.Log ("Character Asset Load");
		if (www == null) {
			www = new WWW (url);
			yield return www;

			if (string.IsNullOrEmpty (www.error)) {
				var request = www.assetBundle.LoadAllAssetsAsync ();
				yield return request;

				assetObj = request.allAssets [0];

				Add ();
			}
		}
	}

	//Adds this Character Asset to the asset-List of the GameManager
	public override void Add ()
	{
		GameManager.assets.Add (this);
		Debug.Log ("Character loaded");
	}

	//Intantiates a new GameObject from this Character Asset
	public override void Instantiate ()
	{
		GameObject instantiatedAsset = null;
		instantiatedAsset = GameObject.Instantiate (assetObj, Vector3.zero, Quaternion.identity) as GameObject;
		GameManager.InstantiatedAssets.Add (instantiatedAsset);
	}
}