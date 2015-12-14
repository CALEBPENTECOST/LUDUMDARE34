using UnityEngine;
using System.Collections;
using System;

public class PaintInventory : MonoBehaviour
{
    [SerializeField]
    ld34MenuController TheMenuController;


    static Texture2D colorSelectBar_black;
    static Texture2D colorSelectBar_white;
    static Texture2D colorSelectBar_gray;

    static Texture2D colorSelectBar_green;
    static Texture2D colorSelectBar_red;
    static Texture2D colorSelectBar_blue;
    static Texture2D colorSelectBar_orange;
    static Texture2D colorSelectBar_yellow;
    static Texture2D colorSelectBar_majenta;
    static Texture2D colorSelectBar_purple;
    static Texture2D colorSelectBar_cyan;


    static Color _black;
    static Color _white;
    static Color _gray;
    static Color _green;
    static Color _red;
    static Color _blue;
    static Color _orange;
    static Color _yellow;
    static Color _majenta;
    static Color _purple;
    static Color _cyan;

    static bool initialized = false;

    static void Init()
    {
        _black = (Color.black);
        _white = (Color.white);
        _gray = (Color.gray);
        _green = (Color.green);
        _red = (Color.red);
        _blue = (Color.blue);
        _orange = (new Color(1f, 0.62f, 0f));
        _yellow = (Color.yellow);
        _majenta = (Color.magenta);
        _purple = (new Color(0.5f, 0f, 0.5f));
        _cyan = (Color.cyan);

        colorSelectBar_black = new Texture2D(1, 1);
        colorSelectBar_white = new Texture2D(1, 1);
        colorSelectBar_gray = new Texture2D(1, 1);
        colorSelectBar_green = new Texture2D(1, 1);
        colorSelectBar_red = new Texture2D(1, 1);
        colorSelectBar_blue = new Texture2D(1, 1);
        colorSelectBar_orange = new Texture2D(1, 1);
        colorSelectBar_yellow = new Texture2D(1, 1);
        colorSelectBar_majenta = new Texture2D(1, 1);
        colorSelectBar_purple = new Texture2D(1, 1);
        colorSelectBar_cyan = new Texture2D(1, 1);

        colorSelectBar_black.SetPixel(0, 0, _black);
        colorSelectBar_white.SetPixel(0, 0, _white);
        colorSelectBar_gray.SetPixel(0, 0, _gray);
        colorSelectBar_green.SetPixel(0, 0, _green);
        colorSelectBar_red.SetPixel(0, 0, _red);
        colorSelectBar_blue.SetPixel(0, 0, _blue);
        colorSelectBar_orange.SetPixel(0, 0, _orange);
        colorSelectBar_yellow.SetPixel(0, 0, _yellow);
        colorSelectBar_majenta.SetPixel(0, 0, _majenta);
        colorSelectBar_purple.SetPixel(0, 0, _purple);
        colorSelectBar_cyan.SetPixel(0, 0, _cyan);

        colorSelectBar_black.wrapMode = TextureWrapMode.Repeat;
        colorSelectBar_white.wrapMode = TextureWrapMode.Repeat;
        colorSelectBar_gray.wrapMode = TextureWrapMode.Repeat;
        colorSelectBar_green.wrapMode = TextureWrapMode.Repeat;
        colorSelectBar_red.wrapMode = TextureWrapMode.Repeat;
        colorSelectBar_blue.wrapMode = TextureWrapMode.Repeat;
        colorSelectBar_orange.wrapMode = TextureWrapMode.Repeat;
        colorSelectBar_yellow.wrapMode = TextureWrapMode.Repeat;
        colorSelectBar_majenta.wrapMode = TextureWrapMode.Repeat;
        colorSelectBar_purple.wrapMode = TextureWrapMode.Repeat;
        colorSelectBar_cyan.wrapMode = TextureWrapMode.Repeat;

        colorSelectBar_black.Apply();
        colorSelectBar_white.Apply();
        colorSelectBar_gray.Apply();
        colorSelectBar_green.Apply();
        colorSelectBar_red.Apply();
        colorSelectBar_blue.Apply();
        colorSelectBar_orange.Apply();
        colorSelectBar_yellow.Apply();
        colorSelectBar_majenta.Apply();
        colorSelectBar_purple.Apply();
        colorSelectBar_cyan.Apply();

        initialized = true;
    }

    public float selectedHue
    {
        get
        {
            // We should get the current color in the menu controller...
            return getHueFromColor(TheMenuController.getCurrentlySelectedColor());
        }
        private set
        {
            // We dont actually set the hue. The menu controller will do this
            return;
        }
    }

    internal int getCurrentAvailableHueCount()
    {
        // We need to ask the menu controller for this one
        return TheMenuController.getNumColorsLeft();
    }

    public float getHueFromColor(int colorIndex)
    {
        return getHueFromColor((ld34ColorController.Colors)colorIndex);
    }

    public float getHueFromColor(ld34ColorController.Colors color)
    {
        switch (color)
        {
            case ld34ColorController.Colors.Green:
                // Our hue for green is:
                return 120 / 360f;

            case ld34ColorController.Colors.Red:
                return 0 / 360f;

            case ld34ColorController.Colors.Blue:
                return 240 / 360f;

            case ld34ColorController.Colors.Yellow:
                return 60 / 360f;

            case ld34ColorController.Colors.Orange:
                return 30 / 360f;

            case ld34ColorController.Colors.Majenta:
                return 300 / 360f;

            case ld34ColorController.Colors.Teal:
                return 180 / 360f;

            case ld34ColorController.Colors.Purple:
                return 270 / 360f;

            default:
                // Assume that default should be green
                return 120 / 360f;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    internal static Texture2D getGUITextureForColor(ld34ColorController.Colors colorAtIndex)
    {
        // Big ol' switch statement
        if (!initialized)
            Init();

        switch (colorAtIndex)
        {
            case ld34ColorController.Colors.Green:
                return colorSelectBar_green;
                break;
            case ld34ColorController.Colors.Red:
                return colorSelectBar_red;
                break;
            case ld34ColorController.Colors.Blue:
                return colorSelectBar_blue;
                break;
            case ld34ColorController.Colors.Yellow:
                return colorSelectBar_yellow;
                break;
            case ld34ColorController.Colors.Orange:
                return colorSelectBar_orange;
                break;
            case ld34ColorController.Colors.Majenta:
                return colorSelectBar_majenta;
                break;
            case ld34ColorController.Colors.Teal:
                return colorSelectBar_cyan;
                break;
            case ld34ColorController.Colors.Purple:
                return colorSelectBar_purple;
                break;
            case ld34ColorController.Colors.White:
                return colorSelectBar_white;
            case ld34ColorController.Colors.Black:
                return colorSelectBar_black;
            case ld34ColorController.Colors.Gray:
                return colorSelectBar_gray;
            default:
                return colorSelectBar_gray;
                break;
        }
    }
}
