﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    static private GameManager Instance;

    public List<TextAsset> _jsonTexts;
	private JsonReader jsonReader;

    static public List<Asset> assets;
    static public List<GameObject> InstantiatedAssets;

    public static Asset ActiveLoadingAsset;
    public static AsyncOperation CurrentInstantiatingAssetOperation { get; internal set; }

    private static bool _isSceneSetup;

    // Use this for initialization
    private void Awake()
    {
		jsonReader = new JsonReader();
        Instance = this;
        assets = new List<Asset>();
        InstantiatedAssets = new List<GameObject>();
    }

    private void Start()
    {
        foreach (var text in _jsonTexts)
        {
			var asset = jsonReader.ReadFromJson(text.text);

            StartCoroutine(asset.Load());
        }

        AssetCount = 0;
    }

    public int AssetCount
    {
        set
        {
            if (assets.Count > value)
                assets.RemoveRange(value, assets.Count - value);
        }
    }

    private static void SetupScene()
    {
        if (assets.Count == ((GameManager)GameObject.Find("GameManager").GetComponent("GameManager"))._jsonTexts.Count && _isSceneSetup == false)
        {
            foreach (var asset in assets)
            {
                asset.Instantiate();
            }

            _isSceneSetup = true;

            //reset
            GameManager.Instance.AssetCount = 0;
        }
    }

    public void Update()
    {
        if (GameManager.CurrentInstantiatingAssetOperation != null)
        {
            if (GameManager.CurrentInstantiatingAssetOperation.isDone == true)
            {
                if (GameManager.ActiveLoadingAsset.type.Equals("character"))
                {
                    Debug.Log(GameManager.ActiveLoadingAsset.Name + "has finished loading from" + GameManager.ActiveLoadingAsset.url);
                }

                if (GameManager.ActiveLoadingAsset.type.Equals("location"))
                    Debug.Log("Location " + GameManager.ActiveLoadingAsset.Name + "has been loaded!");
            }
        }

        SetupScene();
    }
}