using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AutoResizeText : MonoBehaviour
{
    private Image backgroundImage;
    private TextMeshProUGUI textMesh;

    void Start()
    {
        backgroundImage = GetComponent<Image>();
        textMesh = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        ResizeText();
    }

    void Update()
    {
        //ResizeText();
    }

    void ResizeText()
    {
        textMesh.ForceMeshUpdate();

        // Adjust the height of the TextMeshPro component based on the text height
        float preferredHeight = textMesh.preferredHeight;
        RectTransform textRectTransform = textMesh.GetComponent<RectTransform>();
        RectTransform backgroundRectTransform = backgroundImage.GetComponent<RectTransform>();
        backgroundRectTransform.sizeDelta = new Vector2(backgroundImage.GetComponent<RectTransform>().sizeDelta.x, preferredHeight + 10f);
    }
}