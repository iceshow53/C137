using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillController : MonoBehaviour
{
	private List<GameObject> images = new List<GameObject>();
	private List<GameObject> Buttons = new List<GameObject>();
	private List<Image> ButtonImages = new List<Image>();
	private float[] cooltime = new float[5];

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
		cooltime[0] = 1.0f;
		cooltime[1] = 0.5f;
		cooltime[2] = 0.1f;
		cooltime[3] = 0.1f;
		cooltime[4] = 0.1f;

	}

	public void skill(int i)
    {
		ButtonImages[i].fillAmount = 0;
		Buttons[i].GetComponent<Button>().enabled = false;
		StartCoroutine(PushButton_Coroutine(i, cooltime[i]));
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
