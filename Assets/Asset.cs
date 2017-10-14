using UnityEngine;
using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using AssemblyCSharp;

[Serializable]
public class Asset : IAsset
{
	public string Name;
	public string url;
	public string type;
	public WWW www;

	#region IAsset implementation

	public virtual IEnumerator Load ()
	{
		yield return null;
	}

	public virtual void Add ()
	{
	}

	public virtual void Instantiate ()
	{
	}

	#endregion
}



