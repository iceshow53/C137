using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour, IPointerDownHandler,IPointerUpHandler,IDragHandler
{
	private Text text;
	private RectTransform rectTransform;

	private Color OldColor;

    private void Awake()
    {
		text = GetComponent<Text>();
		rectTransform = text.GetComponent<RectTransform>();
    }

    void Start()
	{
		text.text = text.name;
		rectTransform.sizeDelta = new Vector2(600, 110);
	}

	public void pushButton()
	{


	}

	public void OnDrag(PointerEventData eventData)
	{
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		text.color = OldColor;
		SceneManager.LoadScene(text.text);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		OldColor = text.color;
		text.color = Color.white;
	}
}
