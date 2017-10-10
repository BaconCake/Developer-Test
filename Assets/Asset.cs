using UnityEngine;
using System;
using System.Collections;
using System.Linq;
using AssemblyCSharp;

[Serializable]
public class Asset : IAsset
{
    public string Name;
    public string url;
    public string type;
	public WWW www;

	public virtual IEnumerator Load ()
	{
		yield return null;
	}

	public virtual void Add ()
	{
		GameManager.assets.Add(this);
	}

	public virtual void Instantiate ()
	{

	}
}

