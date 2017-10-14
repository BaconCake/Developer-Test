using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp;

public class JsonReader
{
	//Returns a matching asset file depending on type in given string
	public Asset ReadFromJson (string jsonString)
	{
		Asset asset = JsonUtility.FromJson<Asset> (jsonString);
		switch (asset.type) {
		case "audio":
			asset = JsonUtility.FromJson<AudioAsset> (jsonString);
			return asset;
		case "character":
			asset = JsonUtility.FromJson<CharacterAsset> (jsonString);
			return asset;
		default:
			return asset;
		}
	}
}
