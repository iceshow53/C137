using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
	public GameObject ui;
	public RectTransform uiTranspos;

	public float sizeX;
	public float sizeY;
	public float offsetX;
	public float offsetY;

    private void Awake()
    {
		uiTranspos = ui.GetComponent<RectTransform>();
    }

    private void Start()
    {
		offsetX = 5;
		offsetY = 5;
    }

    private void OnEnable()
	{
		StartCoroutine(EffectUI());
	}

    private void OnDisable()
    {
		offsetX = 5;
		offsetY = 5;
		uiTranspos.sizeDelta = new Vector2(offsetX, offsetY);
    }

    IEnumerator EffectUI()
	{
		while (uiTranspos.sizeDelta.y < sizeY)
		{
			offsetY += Time.deltaTime * 1800;
			uiTranspos.sizeDelta = new Vector2(offsetX, offsetY);
			yield return null;
		}

		while (uiTranspos.sizeDelta.x < sizeX)
		{
			offsetX += Time.deltaTime * 1800;
			uiTranspos.sizeDelta = new Vector2(offsetX, offsetY);
			yield return null;
		}
	}
	
}
