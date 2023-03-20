using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillController : MonoBehaviour
{
	public List<GameObject> images = new List<GameObject>();
	public List<GameObject> Buttons = new List<GameObject>();
	public List<Image> ButtonImages = new List<Image>();

    private void Start()
    {
		GameObject SkillsObj = GameObject.Find("Skills");
		for (int i = 0; i < SkillsObj.transform.childCount; ++i)
		{
			GameObject Obj = SkillsObj.transform.GetChild(i).gameObject;
			images.Add(Obj);
			Buttons.Add(Obj.transform.GetChild(0).gameObject);
			ButtonImages.Add(Obj.transform.GetChild(0).GetComponent<Image>());
		}
	}


	public void skill1()
	{
		ButtonImages[0].fillAmount = 0;
		Buttons[0].GetComponent<Button>().enabled = false;
		StartCoroutine(PushButton_Coroutine(0, 0.5f));
	}

	public void skill2()
	{
		ButtonImages[1].fillAmount = 0;
		Buttons[1].GetComponent<Button>().enabled = false;
		StartCoroutine(PushButton_Coroutine(1, 0.1f));
	}

	public void skill3()
	{
		ButtonImages[2].fillAmount = 0;
		Buttons[2].GetComponent<Button>().enabled = false;
		StartCoroutine(PushButton_Coroutine(2, 0.1f));
	}

	public void skill4()
	{
		ButtonImages[3].fillAmount = 0;
		Buttons[3].GetComponent<Button>().enabled = false;
		StartCoroutine(PushButton_Coroutine(3, 0.5f));
	}

	public void skill5()
	{
		ButtonImages[4].fillAmount = 0;
		Buttons[4].GetComponent<Button>().enabled = false;
		StartCoroutine(PushButton_Coroutine(4, 0.5f));
	}

	IEnumerator PushButton_Coroutine(int _index, float cooldown)
    {
		while (ButtonImages[_index].fillAmount != 1)
		{
			ButtonImages[_index].fillAmount += Time.deltaTime * cooldown;
			yield return null;
		}

		Buttons[_index].GetComponent<Button>().enabled = true;
	}
}
