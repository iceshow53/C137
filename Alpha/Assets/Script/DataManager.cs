using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;


[System.Serializable]
class DataForm
{
	public string name;
	public string age;

	public DataForm(string _name, string _age)
	{
		name = _name;
		age = _age;
	}
}

public class DataManager : MonoBehaviour
{
	private int value;
	private string userName;
	
	void Start()
	{

		var JsonData = Resources.Load<TextAsset>("saveFile/Data");

		DataForm form = JsonUtility.FromJson<DataForm>(JsonData.ToString());

		value = int.Parse(form.age);
		userName = form.name;

		print(userName + " : " + value);
	}

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
			--value;
        }
		if(Input.GetKeyDown(KeyCode.UpArrow))
        {
			++value;
        }

		if(Input.GetKeyDown(KeyCode.Return))
        {
			SaveData(userName, value.ToString());
        }
    }

	public void SaveData(string _name, string _age)
    {
		DataForm form = new DataForm(_name, _age);

		string Jsondata = JsonUtility.ToJson(form);

		FileStream fileStream = new FileStream(Application.dataPath + "/Resources/saveFile/Data.json", FileMode.Create);

		byte[] data = Encoding.UTF8.GetBytes(Jsondata);

		fileStream.Write(data, 0, data.Length);
		fileStream.Close(); // ** 항상 볼일이 끝나면 닫아주어야 함.(리소스 차지)
	}
}
