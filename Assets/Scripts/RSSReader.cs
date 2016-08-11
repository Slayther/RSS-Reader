using UnityEngine;
using System.Collections.Generic;
using System.Xml;

public class RSSReader : MonoBehaviour
{
	public GameObject newsItemObject;

	XmlDocument getXmlDocument(string xmlUrl) {
		XmlTextReader rssReader = new XmlTextReader (xmlUrl);
		XmlDocument rssDoc = new XmlDocument ();
		rssDoc.Load (rssReader);

		return rssDoc;
	}

	XmlNode getNodeByName(XmlNode parent, string name) {
		if (parent == null) {
			return null;
		}

		return parent [name];
	}

	List<XmlNode> getNodesByName(XmlNode parent, string name) {
		if (parent == null) {
			return null;
		}

		List<XmlNode> nodes = new List<XmlNode> ();

		for (int i = 0; i < parent.ChildNodes.Count; i++) {
			XmlNode childNode = parent.ChildNodes [i];
			if (childNode.Name == name) {
				nodes.Add (childNode);
			}
		}

		if (nodes.Count == 0) {
			return null;
		} else {
			return nodes;
		}
	}

	GameObject cloneObject(GameObject objectToClone, GameObject parent) {
		GameObject newObject = Instantiate (objectToClone) as GameObject;
		newObject.transform.SetParent (parent.transform, false);
		return newObject;
	}

	void Start() {
		string feedUrl = "http://feeds.reuters.com/Reuters/worldNews?format=xml";
		XmlDocument rssDoc = getXmlDocument (feedUrl);

		XmlNode rssNode = getNodeByName (rssDoc, "rss");
		XmlNode channelNode = getNodeByName (rssNode, "channel");

		List<XmlNode> newsItems = getNodesByName (channelNode, "item");

		foreach (XmlNode newsItem in newsItems) {
			string title = newsItem ["title"].InnerText;
			string description = newsItem ["description"].InnerText;
			string link = newsItem ["link"].InnerText;

			GameObject newNewsItem = cloneObject (newsItemObject, gameObject);
			newNewsItem.GetComponent<NewsItem> ().setNewsItemInfo (title, description, link);
		}
	}
}