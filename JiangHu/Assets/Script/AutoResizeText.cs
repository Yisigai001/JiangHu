using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutoResizeText : MonoBehaviour
{
    private RectTransform EventLine;
    private RectTransform desImage;
    private RectTransform itemImage;
    private TextMeshProUGUI desText;
    private TextMeshProUGUI itemText;
    

    void Start()
    {
        EventLine = GetComponent<RectTransform>();
        desImage = transform.Find("Image_Up").GetComponent<RectTransform>();
        desText = transform.Find("Image_Up").transform.Find("Text").GetComponent<TextMeshProUGUI>();
        itemText = transform.Find("Image_Down").transform.Find("Text").GetComponent<TextMeshProUGUI>();
        itemImage = transform.Find("Image_Down").GetComponent<RectTransform>();

        ResizeText();
    }

    void Update()
    {
        //ResizeText();
    }

    void ResizeText()
    {
        desText.ForceMeshUpdate();
        float preferredHeight = desText.preferredHeight;
        desImage.sizeDelta = new Vector2(desImage.sizeDelta.x, preferredHeight + 30);
        EventLine.sizeDelta = new Vector2(EventLine.sizeDelta.x, preferredHeight + itemImage.sizeDelta.y + 30);

        //RectTransform textRectTransform = textMesh.GetComponent<RectTransform>();
        //RectTransform backgroundRectTransform = backgroundImage.GetComponent<RectTransform>();
        //backgroundRectTransform.sizeDelta = new Vector2(backgroundImage.GetComponent<RectTransform>().sizeDelta.x, preferredHeight + 10f);
    }
}