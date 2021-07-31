using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsLink : MonoBehaviour
{
	public void BanishedCrownStudios()
	{
		if (Application.platform != RuntimePlatform.WebGLPlayer)
		{
			Application.OpenURL("https://www.banishedcrown.com");
		}
	}

	public void Franz()
	{
		if (Application.platform != RuntimePlatform.WebGLPlayer)
		{
			Application.OpenURL("https://github.com/chavezfk");
		}
	}

	public void Isaac()
	{
		if (Application.platform != RuntimePlatform.WebGLPlayer)
		{
			Application.OpenURL("https://mewbusi.blogspot.com");
		}
	}

	public void Greg()
	{
		if (Application.platform != RuntimePlatform.WebGLPlayer)
		{
			Application.OpenURL("https://bamboozled.live");
		}
	}

	public void Neubla()
	{
		if (Application.platform != RuntimePlatform.WebGLPlayer)
		{
			Application.OpenURL("https://www.blendermarket.com/products/nebula-generator");
		}
	}
}
