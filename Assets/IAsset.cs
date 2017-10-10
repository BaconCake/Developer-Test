using System;
using System.Collections;

namespace AssemblyCSharp
{
	public interface IAsset
	{
		IEnumerator Load ();

		void Add ();

		void Instantiate ();
	}
}

