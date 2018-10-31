using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpController : SingletonMonoBehaviour<PopUpController> {

	protected override void Init()
	{
		base.Init();
		GetComponent<Image>().enabled = false;
		transform.Find("Text").GetComponent<Text>().enabled = false;
	}

	public void PopUpOpen()
	{
		GetComponent<Image>().enabled = true;
		transform.Find("Text").GetComponent<Text>().enabled = true;
	}

	public void PopUpClose()
	{
		GetComponent<Image>().enabled = false;
		transform.Find("Text").GetComponent<Text>().enabled = false;
	}

	public void SetPopUpText(string popUpText)
	{
		transform.Find("Text").GetComponent<Text>().text = popUpText;
	}
}
