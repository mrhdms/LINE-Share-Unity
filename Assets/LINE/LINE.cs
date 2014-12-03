using UnityEngine;
using System.Runtime.InteropServices;

public class LINE {
	private const string URI = "line://msg/{0}/{1}";

	/// <summary>
	/// LINEでテキストをシェアします
	/// </summary>
	/// <param name="message">シェアするメッセージ</param>
	public static void ShareText(string message) {
		Application.OpenURL(string.Format(URI, "text", System.Uri.EscapeDataString(message)));
	}

	/// <summary>
	/// LINEで画像をシェアします
	/// </summary>
	/// <param name="textureURL">シェアしたい画像のローカルパス</param>
	public static void ShareImage(string textureURL) {
		ShareImageSub(textureURL);
	}
	#if UNITY_IPHONE && !UNITY_EDITOR
	
	private static void ShareImageSub(string textureURL) {
		string shareImageString = CopyClipBoard.Image(textureURL);
		Application.OpenURL(string.Format(URI, "image", WWW.EscapeURL(shareImageString)));
	}

	
#elif UNITY_ANDROID && !UNITY_EDITOR
	
	private static void ShareImageSub(string textureURL) {
		string shareImageString = textureURL;
		Application.OpenURL(string.Format(URI, "image", shareImageString));//androidはエスケープしなくて良い
	}

	
#else
	private static void ShareImageSub(string textureURL) {
		Debug.Log("PCでは動作しません。" + textureURL + "を指定しました。");
	}
	#endif
}
