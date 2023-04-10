using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class MemberForm
{
	public string Name;
	public int Age;

    public MemberForm(string name, int age)
    {
        this.Name = name;
        this.Age = age;
    }
}


// ȸ������



// �α���

public class ExampleManager : MonoBehaviour
{
	string URL = "https://script.google.com/macros/s/AKfycbzUpmqjd0kb98OjJhkech_Xfuw6XjJgNvBA0DoUqUnERG2cnEVVoWQg1ZBI2UKNdUn2Dg/exec";

	IEnumerator Start()
	{
		// ** ��û�� �ϱ����� �۾�

		// UnityWebRequest request = UnityWebRequest.Get(URL);

		MemberForm member = new MemberForm("�����", 45);

		WWWForm form = new WWWForm();

		form.AddField("Name", member.Name);
		form.AddField("Age", member.Age);

		UnityWebRequest request = UnityWebRequest.Post(URL,form);

		yield return request.SendWebRequest();

		print(request.downloadHandler.text);

		// ** ���信 ���� �۾�
	}

}
