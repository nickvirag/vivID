using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using System.IO;
using System.Text;
using System.Xml;

public class RetrieveData : MonoBehaviour {
	string[] line;

	// upload a file with
	// StartCoroutine(UploadFile(binaryImage));
	IEnumerator UploadFile(byte[] file){
		string uploadURL = line [3] + "/send_screenshot.php";
		WWWForm postForm = new WWWForm();
		postForm.AddBinaryData("file",file,"file.png","image/png");
		WWW upload = new WWW(uploadURL,postForm);        
		yield return upload;
		if (upload.error == null) {
			Debug.Log ("upload done:" + upload.text);
			string jsonString = upload.text;
			var dict = Json.Deserialize(jsonString) as Dictionary<string,object>;
			var id = ( ((Dictionary<string, object>)((List<object>)((Dictionary<string, object>)((List<object>)((Dictionary<string, object>)((List<object>)dict ["photos"]) [0]) ["tags"]) [0]) ["uids"]) [0]) ["uid"] );
			string sid = id.ToString ().Remove (id.ToString ().IndexOf ('@'));
			StartCoroutine(GetDetailsFromId(sid));
		}
	}

	IEnumerator GetDetailsFromId( string id ){
		WWW details = new WWW(line[3] + "/user.php?id=" + id );        
		yield return details;
		if (details.error == null) {
			Debug.Log ("upload done:" + details.text);
			var dict = Json.Deserialize(details.text) as Dictionary<string,object>;
			Debug.Log ((string)dict[ "name" ]);
			Debug.Log ((string)dict[ "yo_password" ]);
		} else {
			Debug.Log ("Error during upload: " + details.error);
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
		//StartCoroutine(GetDetailsFromId("7"));
//		string response = @"{
//		  ""status"": ""success"",
//		  ""photos"": [
//		    {
//		      ""url"": ""http://www.lambdal.com/tiger.jpg"",
//		      ""width"": 600,
//		      ""tags"": [
//		        {
//		          ""eye_left"": {
//		            ""y"": 116,
//		            ""x"": 357
//		          },
//		          ""confidence"": 0.978945010372561,
//		          ""center"": {
//		            ""y"": 130,
//		            ""x"": 339
//		          },
//		          ""mouth_right"": {
//		            ""y"": 119,
//		            ""x"": 341
//		          },
//		          ""mouth_left"": {
//		            ""y"": 119,
//		            ""x"": 288
//		          },
//		          ""height"": 140,
//		          ""width"": 140,
//		          ""mouth_center"": {
//		            ""y"": 119,
//		            ""x"": 314.5
//		          },
//		          ""nose"": {
//		            ""y"": 147,
//		            ""x"": 336
//		          },
//		          ""eye_right"": {
//		            ""y"": 115,
//		            ""x"": 314
//		          },
//		          ""tid"": ""31337"",
//		          ""attributes"": [
//		            {
//		              ""gender"": ""male"",
//		              ""confidence"": 59
//		            }
//		          ],
//		          ""uids"": [
//		            {
//		              ""confidence"": 0.742,
//		              ""prediction"": ""TigerWoods"",
//		              ""uid"": ""TigerWoods@CELEBS""
//		            },
//		            {
//		              ""confidence"": 0.161,
//		              ""prediction"": ""ArnoldS"",
//		              ""uid"": ""ArnoldS@CELEBS""
//		            },
//		            {
//		              ""confidence"": 0.065,
//		              ""prediction"": ""JenniferAniston"",
//		              ""uid"": ""JenniferAniston@CELEBS""
//		            }
//		          ]
//		        }
//		      ],
//		      ""height"": 585
//		    }
//		  ]
//		} ";
	}
}
