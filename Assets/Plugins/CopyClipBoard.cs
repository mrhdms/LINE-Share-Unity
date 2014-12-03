using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class CopyClipBoard {
	#if UNITY_IPHONE && !UNITY_EDITOR
	[DllImport ("__Internal")]
	static extern string _SetImage(string textureURL) ;
	[DllImport ("__Internal")]
	static extern string _SetText(string text) ;
	#endif

	public static void Text(string text) {
		#if UNITY_IPHONE && !UNITY_EDITOR
		string shareImageString = _SetText(text);
		#elif UNITY_ANDROID && !UNITY_EDITOR
	AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
	AndroidJavaObject activity = jc.GetStatic<AndroidJavaObject>("currentActivity");

	activity.Call("runOnUiThread", new AndroidJavaRunnable(() => {
	AndroidJavaObject clipboardManager = activity.Call<AndroidJavaObject>("getSystemService", "clipboard");
	//clipboardManager.Call("setText", exportData);
	AndroidJavaClass clipDataClass = new AndroidJavaClass("android.content.ClipData");
	AndroidJavaObject clipData = clipDataClass.CallStatic<AndroidJavaObject>("newPlainText", "simple text", text);
	clipboardManager.Call("setPrimaryClip", clipData);
	}));
		#else
		Debug.Log("PCではコピーできませんが、" + text + " がクリップボードにコピーされます。");
		#endif
	}

	public static string Image(string textureURL) {
		#if UNITY_IPHONE && !UNITY_EDITOR
		return _SetImage(textureURL);
		#elif UNITY_ANDROID
		Debug.LogError("未実装です");
		return "";
		#else
		Debug.Log("PCではコピーできませんが、" + textureURL + " がクリップボードにコピーされます。");
		return "";
		#endif
	}
}
