using UnityEngine;
using System.Collections;

public class Demo : MonoBehaviour
{
		// Use this for initialization
		void Start ()
		{
	
		}
		// Update is called once per frame
		void Update ()
		{
	
		}

		void OnGUI ()
		{


				if (GUILayout.Button("<size=30><b>ShareText</b></size>", GUILayout.Height(60))) {
						LINE.ShareText ("こんにちは！");
				}



				if (GUILayout.Button ("<size=30><b>ShareImage</b></size>", GUILayout.Height(60))) {
						StartCoroutine ("CaptureAndShareImage");
				}
		}

		IEnumerator CaptureAndShareImage ()
		{
				string folderPath = Application.persistentDataPath + "/";
				string fileName = "sc.png";
				string filePath = folderPath + fileName;
				Application.CaptureScreenshot (fileName);
				while (!System.IO.File.Exists (filePath)) {
						//書き込み待ち
						yield return null;
				}
				LINE.ShareImage (filePath);
		}
}
