using UnityEngine;
using System.Collections;
using System.Linq;
using AssemblyCSharp;


[System.Serializable]
public class AudioAsset : Asset, IAsset
{
	public AudioClip AudioClip;

	public AudioClip AudioClipProperty {
		get { return AudioClip; }
		set {
			if (AudioClip == null) {
				AudioClip = value;
				GameManager.assets.Add (this);
			} else {
				for (int i = 0; i < GameManager.assets.Count (); ++i) {
					if (GameManager.assets [i] == this) {
						Debug.LogWarning ("There is already an audio asset in audioasset like this: " + AudioClip);
					}
				}
			}
		}
	}

	//Connects to url and loads the Audio Asset
	public override IEnumerator Load ()
	{
		if (www == null) {
			www = new WWW (url);
			yield return www;

			if (string.IsNullOrEmpty (www.error)) {
				yield return new WaitForEndOfFrame ();

				if (www.GetAudioClip () != null) {
					var request = www.GetAudioClip (true);

					yield return request;

					AudioClip = request;
					Add ();
				}
			}


		}
	}

	//Adds this Audio Asset to the asset-List of the GameManager
	public override void Add ()
	{
		GameManager.assets.Add (this);
		Debug.Log ("Audio loaded");
	}

	//Generates a new GameObject with this Audio Asset as Source
	public override void Instantiate ()
	{
		new GameObject ("Audio", typeof(AudioSource));
		AudioSource audiosource = GameObject.Find ("Audio").GetComponent ("AudioSource") as AudioSource;

		audiosource.clip = AudioClip;
		audiosource.Play ();
		Debug.Log ("For audio assets there is nothing to instantiate!");
	}
}