using UnityEngine;
using UnityEngine.UI;

public class NewsItem : MonoBehaviour {
	public Text titleText;
	public Button titleButton;
	public Text descText;

	public void setNewsItemInfo(string title, string description, string link) {
		titleText.text = title;
		descText.text = description;

		titleButton.onClick.AddListener (delegate {
			Application.OpenURL (link);
		});
	}
}
