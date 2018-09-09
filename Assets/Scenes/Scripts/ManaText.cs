using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Asset.Scripts.Mana
{
	public class ManaText : MonoBehaviour
	{

		private Text _manaText;

		// Use this for initialization
		void Start ()
		{
			_manaText = GetComponent<Text>();

		}

		public void UpdateManaText(int mana)
		{
			_manaText.text = mana.ToString() + "/3";
		}
	}

}


