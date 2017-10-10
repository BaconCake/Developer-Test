using System.Collections;
using UnityEngine;


[System.Serializable]
public class CharacterAsset : Asset
{
	private UnityEngine.Object assetObj;

	public override IEnumerator Load()
	{
		Debug.Log("Character Asset Load");
		if (www == null)
		{
			www = new WWW(url);
			yield return www;

			if (string.IsNullOrEmpty(www.error))
			{
				var request = www.assetBundle.LoadAllAssetsAsync();
				yield return request;

				assetObj = request.allAssets[0];

				Add();
			}
		}
	}

	public override void Add()
	{
		base.Add();
		Debug.Log("Character loaded");
	}

	public override void Instantiate()
	{
		GameObject instantiatedAsset = null;
		instantiatedAsset = GameObject.Instantiate(assetObj, Vector3.zero, Quaternion.identity) as GameObject;
		GameManager.InstantiatedAssets.Add(instantiatedAsset);
	}
}