using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DailyEventFunction : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject eventLine;
    private DailyEventTable eventTable;
    private GameObject eventLineParent;
    private RectTransform eventLineParentRect;
    private GameObject newEvent;
    private List<GameObject> eventList;

    void Start()
    {
        eventLine = Resources.Load<GameObject>("UI/Prefab/Event_Daily_Line");
        eventTable = GameObject.Find("DataTable").GetComponent<DailyEventTable>();
        eventLineParent = GameObject.Find("Event_Daily_Content").gameObject;
        eventLineParentRect = eventLineParent.GetComponent<RectTransform>();
        eventList = new List<GameObject>();
    }

    public void OnClickEventButton()
    {
        int i = Random.Range(1, 7);
        DailyEventTable.DailyEvent dailyEvent = eventTable.GetDataByID(i);
        if (dailyEvent.Name != null)
        {
            newEvent =Instantiate(eventLine);
            newEvent.transform.SetParent(eventLineParent.transform);
            newEvent.transform.SetSiblingIndex(0);
            TMP_Text des = newEvent.transform.Find("Image_Up").transform.Find("Text").GetComponent<TMP_Text>();
            des.text = dailyEvent.Describe;
            eventLineParentRect.anchoredPosition = new Vector2(eventLineParentRect.anchoredPosition.x, 0f);
            StartCoroutine(SetNewEventHeight());
        }
        else
        {
            Debug.Log("没找到数据");
        }

    }

    /// <summary>
    /// 携程，修改内容Group的高度
    /// </summary>
    /// <returns></returns>
    IEnumerator SetNewEventHeight()
    {
        yield return null;
        float y = newEvent.GetComponent<RectTransform>().sizeDelta.y;
        eventLineParentRect.sizeDelta = new Vector2(eventLineParentRect.sizeDelta.x, eventLineParentRect.sizeDelta.y + y);
        eventList.Add(newEvent);
        if (eventList.Count > 10)
        {
            Debug.Log("大于10" + eventList.Count);
            float z = eventList[0].GetComponent<RectTransform>().sizeDelta.y;
            eventLineParentRect.sizeDelta = new Vector2(eventLineParentRect.sizeDelta.x, eventLineParentRect.sizeDelta.y - z);
            GameObject destroyEvent = eventList[0];
            eventList.RemoveAt(0);
            Destroy(destroyEvent);
        }
        else
        {
            Debug.Log(eventList.Count);
        }
    }
}
