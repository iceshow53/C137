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


// 회원가입



// 로그인

public class ExampleManager : MonoBehaviour
{
	string URL = "https://script.google.com/macros/s/AKfycbzUpmqjd0kb98OjJhkech_Xfuw6XjJgNvBA0DoUqUnERG2cnEVVoWQg1ZBI2UKNdUn2Dg/exec";

	IEnumerator Start()
	{
		// ** 요청을 하기위한 작업

		// UnityWebRequest request = UnityWebRequest.Get(URL);

		MemberForm member = new MemberForm("변사또", 45);

		WWWForm form = new WWWForm();

		form.AddField("Name", member.Name);
		form.AddField("Age", member.Age);

		UnityWebRequest request = UnityWebRequest.Post(URL,form);

		yield return request.SendWebRequest();

		print(request.downloadHandler.text);

		// ** 응답에 대한 작업
	}

}
