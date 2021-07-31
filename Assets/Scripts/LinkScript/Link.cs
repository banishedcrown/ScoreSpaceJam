using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class Link : MonoBehaviour 
{

	public void BanishedCrownStudios()
	{
		#if !UNITY_EDITOR
		openWindow("https://www.banishedcrown.com");
		#endif

	}

	public void Franz()
	{
		#if !UNITY_EDITOR
		openWindow("https://github.com/chavezfk");
		#endif

	}

	public void Isaac()
	{
		#if !UNITY_EDITOR
		openWindow("https://mewbusi.blogspot.com");
		#endif

	}

	public void Greg()
	{
		#if !UNITY_EDITOR
		openWindow("https://bamboozled.live");
		#endif

	}

	public void Neubla()
	{
		#if !UNITY_EDITOR
		openWindow("https://www.blendermarket.com/products/nebula-generator");
		#endif

	}

	[DllImport("__Internal")]
	private static extern void openWindow(string url);

}