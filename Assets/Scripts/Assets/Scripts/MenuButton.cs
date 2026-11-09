
using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public static Button Create(
        string label,
        Transform parent,
        Vector2 position,
        Vector2 size,
        Color color,
        Action onClick,
        Font font)
    {
        GameObject obj = new GameObject(label);
        obj.transform.SetParent(parent, false);

        RectTransform rect = obj.AddComponent<RectTransform>();
        rect.sizeDelta = size;
        rect.anchoredPosition = position;

        Image image = obj.AddComponent<Image>();
        image.color = color;

        Button button = obj.AddComponent<Button>();
        button.targetGraphic = image;

        ColorBlock colors = button.colors;
        colors.normalColor = color;
        colors.highlightedColor = new Color(
            Mathf.Min(color.r + 0.15f, 1f),
            Mathf.Min(color.g + 0.15f, 1f),
            Mathf.Min(color.b + 0.15f, 1f)
        );
        colors.pressedColor = MenuConfig.Purple;
        button.colors = colors;

        GameObject textObject = new GameObject("Label");
        textObject.transform.SetParent(obj.transform, false);

        RectTransform textRect =
            textObject.AddComponent<RectTransform>();

        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.offsetMin = Vector2.zero;
        textRect.offsetMax = Vector2.zero;

        Text text = textObject.AddComponent<Text>();
        text.font = font;
        text.text = label;
        text.fontSize = 17;
        text.color = Color.white;
        text.alignment = TextAnchor.MiddleCenter;

        button.onClick.AddListener(() => onClick?.Invoke());

        return button;
    }
}

