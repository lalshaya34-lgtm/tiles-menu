
using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuTabs : MonoBehaviour
{
    public void Setup(Transform parent, Action<int> onTabSelected)
    {
        Font font = Resources.GetBuiltinResource<Font>("Arial.ttf");

        for (int i = 0; i < MenuConfig.TabNames.Length; i++)
        {
            int index = i;

            MenuButton.Create(
                MenuConfig.TabNames[i],
                parent,
                new Vector2(-180 + i * 120, 115),
                new Vector2(110, 42),
                MenuConfig.ButtonColor,
                () => onTabSelected?.Invoke(index),
                font
            );
        }
    }
}

