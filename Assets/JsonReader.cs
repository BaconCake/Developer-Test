using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonReader {

	public Asset ReadFromJson(string jSONString)
	{
		Asset asset = JsonUtility.FromJson<Asset>(jSONString);
		switch(asset.type){
		case "audio":
			return JsonUtility.FromJson<AudioAsset>(jSONString);
		case "character":
			return JsonUtility.FromJson<CharacterAsset>(jSONString);
			default:
			return asset;
		}
	}
}
