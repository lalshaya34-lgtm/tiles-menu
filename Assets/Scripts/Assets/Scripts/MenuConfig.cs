
using UnityEngine;

public static class MenuConfig
{
    public const string MenuName = "TILES MENU";
    public const string Version = "1.0";

    public static readonly Color Background =
        new Color(0.055f, 0.06f, 0.09f);

    public static readonly Color Purple =
        new Color(0.65f, 0.35f, 1f);

    public static readonly Color ButtonColor =
        new Color(0.15f, 0.13f, 0.21f);

    public static readonly string[] TabNames =
    {
        "HOME",
        "VISUALS",
        "SETTINGS",
        "ABOUT"
    };
}

