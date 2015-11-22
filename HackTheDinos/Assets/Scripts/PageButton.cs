using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using Parse;

public class PageButton : MonoBehaviour {

	public int pageNum = 0;

	public Transform[] Pages;

	public Transform InputField;

	public string InputText;

	public string[] PageLineIDs = {"4hw33l7L9a","gmZuodMiSw","GUhdsMFsPX","NOLLjplqnf"};

	public string currentLineText;

	public int currentDist = -1;

	public string currentDistString = "";

	public float currentPrec = 0f;

	public Image PrecObj;

	public Text Points;

	public string UserEmail = "pierce@gmail.com";

	// Use this for initialization
	void Start () {

		GetLineFromParse(PageLineIDs[0]);

	}
	
	// Update is called once per frame
	void Update () {
		PrecObj.fillAmount = Mathf.Lerp(PrecObj.fillAmount, currentPrec, Time.deltaTime * 10f);

		float myPercent = Mathf.Round(currentPrec*100);
		
		Points.text = currentDistString + "Differences";
	}

	public void Next () {

		if (pageNum < Pages.Length-1) {
			LeanTween.rotateZ(Pages[pageNum].gameObject, 181 + pageNum, 1.0f).setEase(LeanTweenType.easeInOutExpo);
			pageNum ++;
			GetLineFromParse(PageLineIDs[pageNum]);
			currentPrec = -1f;
			hideText();
		}

	}

	public void Prev () {

		if (pageNum > 0) {
			pageNum --;
			LeanTween.rotateZ(Pages[pageNum].gameObject, 355 + pageNum, 1.0f).setEase(LeanTweenType.easeInOutExpo);
			GetLineFromParse(PageLineIDs[pageNum]);
			currentPrec = -1f;
			hideText();
		}

	}

	public void showText () {
		LeanTween.value (Points.gameObject, (float value) => {
			Points.color = new Color(99f,99f,99f,value);
		}, 0f, 1f, 1.0f);
	}

	public void hideText () {
		Points.color = new Color(99f,99f,99f,0f);
	}

	public void Submit () {
		InputText = InputField.GetComponent<InputField>().text;
		InputField.GetComponent<InputField>().text = "";

		InputText = RemoveWhiteSpace(InputText);
		InputText = InputText.ToLower();

		SendLineToParse(InputText);

		//currentDist = (float)currentDistString
		
		//currentDist = LevenshteinDistance(currentLineText,InputText);

		//currentPrec = ((float)currentLineText.Length-(float)currentDist)/(float)currentLineText.Length;

		showText();

	}

	public void SignIn () {
		LeanTween.moveLocalZ(Camera.main.gameObject, -1.8f, 2.0f).setEase(LeanTweenType.easeInOutExpo);
	}

	public void GetLineFromParse (string ID) {

		
		ParseQuery<ParseObject> query = ParseObject.GetQuery("DigDig");
		query.GetAsync(ID).ContinueWith(t =>
		                                {
			ParseObject ReqObj = t.Result;
			currentLineText = ReqObj.Get<string>("LineArray");


		});

	}

	public void GetDistFromParse () {
		
		
		ParseQuery<ParseObject> query = ParseObject.GetQuery("SweetPrince");
		query.GetAsync("Fwxwzn1qFh").ContinueWith(t =>
		                                {
			ParseObject ReqObj = t.Result;
			currentDistString = ReqObj.Get<string>("LineArray");
			
			
		});
		
	}

	public void SendLineToParse (string LineText) {
		ParseObject userEntry = new ParseObject("DigDig");
		userEntry["ImgArray"] = "55, 55";
		userEntry["LineArray"] = LineText;
		userEntry["PaleoCount"] = "0";
		userEntry["PaleoID"] = "661";
		userEntry["SRC"] = "none";
		userEntry["Title"] = "page 1";
		userEntry["UserEmail"] = UserEmail;
		userEntry.SaveAsync();

		GetDistFromParse();
	}
	
	public int LevenshteinDistance(string s, string t) {
		int n = s.Length;
		int m = t.Length;
		int[,] d = new int[n + 1, m + 1];
		
		if (n == 0)
		{
			return m;
		}
		
		if (m == 0)
		{
			return n;
		}
		
		for (int i = 0; i <= n; i++)
			d[i, 0] = i;
		for (int j = 0; j <= m; j++)
			d[0, j] = j;
		
		for (int j = 1; j <= m; j++)
			for (int i = 1; i <= n; i++)
				if (s[i - 1] == t[j - 1])
					d[i, j] = d[i - 1, j - 1];  //no operation
		else
			d[i, j] = Mathf.Min(Mathf.Min(
				d[i - 1, j] + 1,    //a deletion
				d[i, j - 1] + 1),   //an insertion
			                   d[i - 1, j - 1] + 1 //a substitution
			                   );
		return d[n, m];
	}

	public string RemoveWhiteSpace (string textIn) {
		string textOut = Regex.Replace(textIn, "( )+", "");
		return textOut;
	}

}
