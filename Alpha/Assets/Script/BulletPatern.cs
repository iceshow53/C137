using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPatern : MonoBehaviour
{
	public enum Pattern
	{
		ScrewPattern, 
		DelayScrew,
		GuideBullet,
		FollowScrewPattern,
		ExplosionPattern, F,G
	}

	public Pattern pattern;

	private List<GameObject> Bullets = new List<GameObject>();
	private GameObject bulletPrefab;



	// Start is called before the first frame update
	void Start()
	{
		bulletPrefab = Resources.Load("Prefabs/PatternBullet") as GameObject;

		// pattern = Pattern.ScrewPattern;
		switch(pattern)
		{
			case Pattern.ScrewPattern:
				GetScrewPattern(5.0f,(int)(360 / 5));
				break;            
			case Pattern.DelayScrew:
				StartCoroutine(GetDelayScrewPattern());
				break;
			case Pattern.GuideBullet:
				GuideBulletPattern();
				break;
			case Pattern.FollowScrewPattern:
				GetScrewPattern(10.0f, (int)(360 / 10), false, true);
				break;
			case Pattern.ExplosionPattern:
				StartCoroutine(ExplosionPattern());
				break;
			case Pattern.F:
				StartCoroutine(TwistedPattern());
				break;
			case Pattern.G:
				break;
		}
	}

	// Update is called once per frame
	void Update()
	{
	}

	private void GetScrewPattern(float fAngle,int _count, bool _option = false, bool _option2 = false)
	{
		float _Angle = fAngle;

		for(int i = 0; i< _count; ++i)
        {
			GameObject Obj = Instantiate(bulletPrefab);
			BulletControl controller = Obj.GetComponent<BulletControl>();

			controller.Target = GameObject.Find("Square");
			controller.Option = _option;
			controller.Option2 = _option2;

			controller.Direction = new Vector3(
				Mathf.Cos(_Angle * Mathf.Deg2Rad),
				Mathf.Sin(_Angle * Mathf.Deg2Rad),
				0.0f) * 5 + transform.position;

			controller.transform.rotation = Quaternion.AngleAxis(_Angle + 90f, Vector3.forward);

			_Angle += fAngle;
			Obj.transform.position = transform.position;

			Bullets.Add(Obj);

		}
	}

	private void explosionEX(float fAngle,int _count, Vector3 pos, bool _option = false, bool _option2 = false)
	{
		float _Angle = fAngle;

		for(int i = 0; i< _count; ++i)
        {
			GameObject Obj = Instantiate(bulletPrefab);
			BulletControl controller = Obj.GetComponent<BulletControl>();

			controller.Target = GameObject.Find("Square");
			controller.Option = _option;
			controller.Option2 = _option2;

			controller.Direction = new Vector3(
				Mathf.Cos(_Angle * Mathf.Deg2Rad),
				Mathf.Sin(_Angle * Mathf.Deg2Rad),
				0.0f) * 5 + transform.position;

			controller.transform.rotation = Quaternion.AngleAxis(_Angle + 90f, Vector3.forward);

			_Angle += fAngle;
			Obj.transform.position = pos;

			Bullets.Add(Obj);

		}
	}

	private IEnumerator GetDelayScrewPattern()
    {
		float fAngle = 30.0f;

		int iCount = (int)(360 / fAngle);

		int i = 0;

		while(i < iCount * 2)
        {
			GameObject Obj = Instantiate(bulletPrefab);
			BulletControl controller = Obj.GetComponent<BulletControl>();

			// controller.Target = GameObject.Find("Square");
			controller.Option = false;

			controller.Direction = new Vector3(
				Mathf.Cos(fAngle * Mathf.Deg2Rad),
				Mathf.Sin(fAngle * Mathf.Deg2Rad),
				0.0f) * 5 + transform.position;

			controller.transform.rotation = Quaternion.AngleAxis(fAngle + 90f, Vector3.forward);

			fAngle += 30.0f;
			Obj.transform.position = transform.position;

			Bullets.Add(Obj);
			++i;
			yield return new WaitForSeconds(0.1f);
		}
    }

	private void GuideBulletPattern()
	{
		GameObject Obj = Instantiate(bulletPrefab);
		BulletControl controller = Obj.GetComponent<BulletControl>();

		controller.Target = GameObject.Find("Square");
		controller.Option = true;
		Obj.transform.position = transform.position;

		Bullets.Add(Obj);

	}

	public IEnumerator ExplosionPattern()
    {
		GameObject ParentObj = new GameObject("Bullet");

		SpriteRenderer sprite = ParentObj.AddComponent<SpriteRenderer>();

		ParentObj.AddComponent<MyGizmo>();
		BulletControl controll = ParentObj.AddComponent<BulletControl>();

		controll.Option = controll.Option2 = false;

		controll.Direction = (GameObject.Find("Square").transform.position - transform.position);

		ParentObj.transform.position = transform.position;

		yield return new WaitForSeconds(1.5f);

		Vector3 pos = ParentObj.transform.position;

		explosionEX(5.0f, (int)(360 / 5), pos);

		Destroy(ParentObj);
    }

	public IEnumerator TwistedPattern()
    {
		float fTime = 3.0f;

		while(fTime > 0)
        {
			fTime -= Time.deltaTime;

			GameObject obj = Instantiate(Resources.Load("Prefabs/Twist")) as GameObject;

			yield return null;
        }
	}
}
