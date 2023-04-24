using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewObjectSelector : MonoBehaviour
{

	private TMPro.TMP_Dropdown dropdown;

	void Start()
	{
		dropdown = gameObject.GetComponent<TMPro.TMP_Dropdown>();
		refreshOptions();
	}

	public void refreshOptions()
	{
		List<string> options = new List<string>();
		options.Add("Select Object");
		options.Add("Global");

		foreach (DetObjectHoldable obj in DetObject.objects)
		{
			options.Add(obj.gameObj.name);
		}

		dropdown.ClearOptions();
		dropdown.AddOptions(options);
	}

	public void onObjectSelected(int index)
	{
		if (index != 0)
		{
			dropdown.value = 0;

			if (index == 1)
			{
				EventController.Instance.select(null);
			}
			else
			{
				EventController.Instance.select(DetObject.objects[index - 2]);
			}

		}
	}
}