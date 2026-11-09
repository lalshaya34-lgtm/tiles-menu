
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TilesMenu : MonoBehaviour
{
    [Header("Wrist Settings")]
    public Transform wristAnchor;
    public Vector3 wristPosition =
        new Vector3(0f, 0.04f, 0.08f);

    [Header("Menu Settings")]
    public bool startVisible = true;

    private GameObject menuRoot;
    private Text pageTitle;
    private Transform content;
    private Font menuFont;

    private void Start()
    {
        menuFont = Resources.GetBuiltinResource<Font>("Arial.ttf");

        CreateEventSystem();
        CreateMenu();

        if (wristAnchor != null)
        {
            menuRoot.transform.SetParent(wristAnchor, false);
            menuRoot.transform.localPosition = wristPosition;
            menuRoot.transform.localRotation = Quaternion.identity;
        }

        menuRoot.SetActive(startVisible);
    }

    private void Update()
    {
        // Desktop testing shortcut.
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        if (menuRoot != null)
            menuRoot.SetActive(!menuRoot.activeSelf);
    }

    private void CreateEventSystem()
    {
        if (FindObjectOfType<EventSystem>() != null)
            return;

        GameObject events = new GameObject("TilesMenuEventSystem");
        events.AddComponent<EventSystem>();
        events.AddComponent<StandaloneInputModule>();
    }

    private void CreateMenu()
    {
        menuRoot = new GameObject("TilesMenuCanvas");

        Canvas canvas = menuRoot.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;

        CanvasScaler scaler = menuRoot.AddComponent<CanvasScaler>();
        scaler.dynamicPixelsPerUnit = 10;

        menuRoot.AddComponent<GraphicRaycaster>();

        RectTransform canvasRect =
            menuRoot.GetComponent<RectTransform>();

        canvasRect.sizeDelta = new Vector2(500, 360);
        canvasRect.localScale = Vector3.one * 0.001f;

        CreatePanel(
            "Background",
            menuRoot.transform,
            new Vector2(500, 360),
            Vector2.zero,
            MenuConfig.Background
        );

        CreateText(
            "TILES MENU",
            menuRoot.transform,
            30,
            new Vector2(0, 145),
            new Vector2(460, 45),
            MenuConfig.Purple
        );

        pageTitle = CreateText(
            "HOME",
            menuRoot.transform,
            22,
            new Vector2(-145, 85),
            new Vector2(180, 35),
            Color.white
        );

        content = new GameObject("Content").transform;
        content.SetParent(menuRoot.transform, false);

        RectTransform contentRect =
            content.gameObject.AddComponent<RectTransform>();

        contentRect.sizeDelta = new Vector2(440, 190);
        contentRect.anchoredPosition = new Vector2(0, -45);

        MenuTabs tabs = menuRoot.AddComponent<MenuTabs>();
        tabs.Setup(menuRoot.transform, ChangeTab);

        ChangeTab(0);
    }

    private void ChangeTab(int tab)
    {
        pageTitle.text = MenuConfig.TabNames[tab];

        foreach (Transform child in content)
            Destroy(child.gameObject);

        if (tab == 0)
        {
            AddOption("Welcome to Tiles Menu", 55);
            AddOption("Version 1.0", 5);
            AddOption("PCVR UI Prototype", -45);
        }
        else if (tab == 1)
        {
            AddOption("Visual Effects Demo", 55);
            AddOption("Color Theme Demo", 5);
            AddOption("UI Preview", -45);
        }
        else if (tab == 2)
        {
            AddOption("Menu Scale", 55);
            AddOption("Menu Position", 5);
            AddOption("Close Menu", -45, ToggleMenu);
        }
        else if (tab == 3)
        {
            AddOption("Tiles Menu", 55);
            AddOption("Made with Unity C#", 5);
            AddOption("Version 1.0", -45);
        }
    }

    private void AddOption(string label, float y, System.Action action = null)
    {
        MenuButton.Create(
            label,
            content,
            new Vector2(0, y),
            new Vector2(390, 42),
            MenuConfig.ButtonColor,
            action ?? (() => Debug.Log(label + " clicked!")),
            menuFont
        );
    }

    private void CreatePanel(
        string name,
        Transform parent,
        Vector2 size,
        Vector2 position,
        Color color)
    {
        GameObject panel = new GameObject(name);
        panel.transform.SetParent(parent, false);

        RectTransform rect = panel.AddComponent<RectTransform>();
        rect.sizeDelta = size;
        rect.anchoredPosition = position;

        Image image = panel.AddComponent<Image>();
        image.color = color;
    }

    private Text CreateText(
        string value,
        Transform parent,
        int fontSize,
        Vector2 position,
        Vector2 size,
        Color color)
    {
        GameObject obj = new GameObject("Text");
        obj.transform.SetParent(parent, false);

        RectTransform rect = obj.AddComponent<RectTransform>();
        rect.sizeDelta = size;
        rect.anchoredPosition = position;

        Text text = obj.AddComponent<Text>();
        text.text = value;
        text.font = menuFont;
        text.fontSize = fontSize;
        text.color = color;
        text.alignment = TextAnchor.MiddleCenter;

        return text;
    }
}

