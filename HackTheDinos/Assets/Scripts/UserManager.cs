using UnityEngine;
using System.Collections;
using Parse;

public class UserManager : MonoBehaviour {

	public string UserEmail = "";

	public string MessageString = "";

	public string pageNum = "1";

	public string lineEntry = "";

	public string[] PageLineIDs = {"4hw33l7L9a","gmZuodMiSw","GUhdsMFsPX","NOLLjplqnf"};

	// Use this for initialization
	void Start () {

		GetTest("VX4sttNJvL");

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyUp(KeyCode.Equals)) {
			SetTest();
		}

	}

	void SetTest () {

		ParseObject userEntry = new ParseObject("DigDig");
		userEntry["ImgArray"] = "55, 55";
		userEntry["LineArray"] = lineEntry;
		userEntry["PaleoCount"] = "0";
		userEntry["PaleoID"] = "555";
		userEntry["SRC"] = "none";
		userEntry["Title"] = "page" + pageNum;
		userEntry["UserEmail"] = UserEmail;
		userEntry.SaveAsync();

	}

	void GetTest (string ID) {

		ParseQuery<ParseObject> query = ParseObject.GetQuery("DigDig");
		query.GetAsync(ID).ContinueWith(t =>
		                                          {
			ParseObject ReqObj = t.Result;
			MessageString = ReqObj.Get<string>("LineArray");
			Debug.Log(MessageString);
		});

	}

}
