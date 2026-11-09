
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TilesMenu : MonoBehaviour
{
    public Transform wristAnchor;

    private GameObject menu;
    private Font font;

    private Color purple = new Color(0.65f, 0.3f, 1f);
    private Color background = new Color(0.06f, 0.06f, 0.09f);
    private Color buttonColor = new Color(0.17f, 0.14f, 0.23f);

    private string[] tabs =
    {
        "HOME", "VISUALS", "SETTINGS", "ABOUT"
    };

    private void Start()
    {
        font = Resources.GetBuiltinResource<Font>("Arial.ttf");

        if (FindObjectOfType<EventSystem>() == null)
        {
            GameObject events = new GameObject("EventSystem");
            events.AddComponent<EventSystem>();
            events.AddComponent<StandaloneInputModule>();
        }

        CreateMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            menu.SetActive(!menu.activeSelf);
    }

    private void CreateMenu()
    {
        menu = new GameObject("TilesMenu");

        Canvas canvas = menu.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;

        menu.AddComponent<CanvasScaler>();
        menu.AddComponent<GraphicRaycaster>();

        RectTransform rect = menu.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(500, 400);
        rect.localScale = Vector3.one * 0.001f;

        if (wristAnchor != null)
        {
            menu.transform.SetParent(wristAnchor, false);
            menu.transform.localPosition = new Vector3(0, 0.04f, 0.08f);
            menu.transform.localRotation = Quaternion.identity;
        }

        Panel("Background", menu.transform, Vector2.zero,
            new Vector2(500, 400), background);

        Label("TILES MENU", menu.transform,
            new Vector2(0, 155), 30, purple);

        for (int i = 0; i < tabs.Length; i++)
        {
            int index = i;

            MakeButton(tabs[i], menu.transform,
                new Vector2(-180 + i * 120, 110),
                new Vector2(110, 42),
                () => OpenTab(index));
        }

        OpenTab(0);
    }

    private void OpenTab(int index)
    {
        Transform old = menu.transform.Find("Content");

        if (old != null)
            Destroy(old.gameObject);

        GameObject content = new GameObject("Content");
        content.transform.SetParent(menu.transform, false);

        RectTransform rect = content.AddComponent<RectTransform>();
        rect.sizeDelta = new Vector2(440, 220);
        rect.anchoredPosition = new Vector2(0, -35);

        Label(tabs[index], content.transform,
            new Vector2(0, 85), 24, Color.white);

        switch (index)
        {
            case 0:
                AddOption(content.transform, "Welcome to Tiles Menu", 35);
                AddOption(content.transform, "Version 1.0", -15);
                AddOption(content.transform, "PCVR UI Prototype", -65);
                break;

            case 1:
                AddOption(content.transform, "Visual Effects Preview", 35);
                AddOption(content.transform, "Color Theme Preview", -15);
                AddOption(content.transform, "UI Preview", -65);
                break;

            case 2:
                AddOption(content.transform, "Menu Information", 35);
                AddOption(content.transform, "Purple Theme", -15);
                MakeButton("CLOSE MENU", content.transform,
                    new Vector2(0, -65), new Vector2(390, 42),
                    () => menu.SetActive(false));
                break;

            case 3:
                AddOption(content.transform, "Tiles Menu", 35);
                AddOption(content.transform, "Made with Unity C#", -15);
                AddOption(content.transform, "Version 1.0", -65);
                break;
        }
    }

    private void AddOption(Transform parent, string label, float y)
    {
        MakeButton(label, parent,
            new Vector2(0, y),
            new Vector2(390, 42),
            () => Debug.Log(label + " clicked!"));
    }

    private void MakeButton(
        string label,
        Transform parent,
        Vector2 position,
        Vector2 size,
        UnityEngine.Events.UnityAction action)
    {
        GameObject obj = new GameObject(label);
        obj.transform.SetParent(parent, false);

        RectTransform rect = obj.AddComponent<RectTransform>();
        rect.sizeDelta = size;
        rect.anchoredPosition = position;

        Image image = obj.AddComponent<Image>();
        image.color = buttonColor;

        Button button = obj.AddComponent<Button>();
        button.targetGraphic = image;
        button.onClick.AddListener(action);

        ColorBlock colors = button.colors;
        colors.highlightedColor = purple;
        colors.pressedColor = new Color(0.4f, 0.2f, 0.7f);
        button.colors = colors;

        Label(label, obj.transform, Vector2.zero, 17, Color.white);
    }

    private void Label(
        string value,
        Transform parent,
        Vector2 position,
        int size,
        Color color)
    {
        GameObject obj = new GameObject("Label");
        obj.transform.SetParent(parent, false);

        RectTransform rect = obj.AddComponent<RectTransform>();
        rect.sizeDelta = new Vector2(450, 40);
        rect.anchoredPosition = position;

        Text text = obj.AddComponent<Text>();
        text.font = font;
        text.text = value;
        text.fontSize = size;
        text.color = color;
        text.alignment = TextAnchor.MiddleCenter;
    }

    private void Panel(
        string name,
        Transform parent,
        Vector2 position,
        Vector2 size,
        Color color)
    {
        GameObject obj = new GameObject(name);
        obj.transform.SetParent(parent, false);

        RectTransform rect = obj.AddComponent<RectTransform>();
        rect.sizeDelta = size;
        rect.anchoredPosition = position;

        Image image = obj.AddComponent<Image>();
        image.color = color;
    }
}
