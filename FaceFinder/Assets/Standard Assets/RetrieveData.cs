using UnityEngine;
using System.Collections;
using MiniJSON;
using System.IO;
using System.Text;
using System.Xml;

public class RetrieveData : MonoBehaviour {
	string[] line;
	IEnumerator UploadFile(byte[] file, string uploadURL){
		WWWForm postForm = new WWWForm();
		postForm.AddBinaryData("file",file,"file.png","image/png");
		WWW upload = new WWW(uploadURL,postForm);        
		yield return upload;
		if (upload.error == null) {
			Debug.Log ("upload done :" + upload.text);
		} else {
			Debug.Log ("Error during upload: " + upload.error + uploadURL);
		}
	}
	void Start () {
		line = new string[10];
		StreamReader theReader = new StreamReader (Application.dataPath + "/keys.txt", Encoding.Default);
		using (theReader) {
			int x = 0;
			line [x] = theReader.ReadLine ();
			while (line[x] != null) {
				Debug.Log(line[x]);
				x ++;
				line [x] = theReader.ReadLine ();
			}
			theReader.Close ();
		}
		StartCoroutine(UploadFile(new byte[] {(byte) 0}, line[3] + "/send_screenshot.php"));
	}
}
