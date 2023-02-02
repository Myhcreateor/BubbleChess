using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor;

public class ClickListener : MonoBehaviour
{
	List<RaycastResult> list = new List<RaycastResult>();

	// Update is called once per frame
	void Update()
	{
		/// ������û�е�����Ͳ�ִ���ж��߼�
		if (!Input.GetMouseButtonDown(0))
		{
			return;
		}

		///��Ӧ��GameObject����
		GameObject go = null;

		///�ж��Ƿ����ui��
		if (EventSystem.current.IsPointerOverGameObject())
		{
			go = ClickUI();
		}
		else
		{
			go = ClickScene();
		}

		if (go == null)
		{
			Debug.Log("Click Nothing");
		}
		else
		{
			// ��������GameObject
#if UNITY_EDITOR
			EditorGUIUtility.PingObject(go);
			Selection.activeObject = go;
#endif
		}

	}

	/// <summary>
	/// ����ui
	/// </summary>
	private GameObject ClickUI()
	{
		//�����е�EventSystem

		PointerEventData eventData = new PointerEventData(EventSystem.current);

		//���λ��
		eventData.position = Input.mousePosition;

		//��������GraphicsRacaster�����Raycast��Ȼ���ڲ����������
		//ֱ���ó�����ȡ��һ���Ϳ�������
		EventSystem.current.RaycastAll(eventData, list);

		//�����������unityԴ��ģ�����ȡ��һ��ֵ
		var raycast = FindFirstRaycast(list);

		//��ȡ�������¼�ע��ӿ�
		//��Button��Toggle֮��ģ��Ͼ�������֪���ĸ�Button������ˣ�����������Image�������
		//��Ȼ����ϸ��ΪIPointerClickHandler, IBeginDragHandler֮��ϸ��һ��ģ���λ�����Լ�ȡ����
		var go = ExecuteEvents.GetEventHandler<IEventSystemHandler>(raycast.gameObject);

		//��Ȼû�õ�button֮��ģ�˵��ֻ��Image��ס�ˣ�ȡ���н������
		if (go == null)
		{
			go = raycast.gameObject;
		}
		if (go.tag == "Floor")
		{
			//�����������ӵ��¼�
			EventHandler.CallGenerateChessEvent(go);
		}
		return go;
	}

	/// <summary>
	/// Return the first valid RaycastResult.
	/// </summary>
	private RaycastResult FindFirstRaycast(List<RaycastResult> candidates)
	{
		for (var i = 0; i < candidates.Count; ++i)
		{
			if (candidates[i].gameObject == null)
				continue;

			return candidates[i];
		}
		return new RaycastResult();
	}

	/// <summary>
	/// ���г����ж���
	/// </summary>
	private GameObject ClickScene()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
		{
			GameObject go = hit.collider.gameObject;
			return go;
		}
		return null;
	}
}