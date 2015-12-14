using UnityEngine;
using System.Collections;
using System;

public class PaintInventory : MonoBehaviour
{
    private const float greenHue = 120 / 360f;
    private const float redHue = 0 / 360f;
    private const float blueHue = 240 / 360f;
    private const float yellowHue = 60 / 360f;
    private const float orangeHue = 30 / 360f;
    private const float majentaHue = 300 / 360f;
    private const float cyanHue = 180 / 360f;
    private const float purpleHue = 270 / 360f;
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

    static bool initialized = false;

    static void Init()
    {

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

        colorSelectBar_black.SetPixel(0, 0, Color.HSVToRGB(0, 0, 0));
        colorSelectBar_white.SetPixel(0, 0, Color.HSVToRGB(0,0,1));
        colorSelectBar_gray.SetPixel(0, 0, Color.HSVToRGB(0,0,0.5f));
        colorSelectBar_green.SetPixel(0, 0, Color.HSVToRGB(greenHue, 1, 1));
        colorSelectBar_red.SetPixel(0, 0, Color.HSVToRGB(redHue, 1, 1));
        colorSelectBar_blue.SetPixel(0, 0, Color.HSVToRGB(blueHue, 1, 1));
        colorSelectBar_orange.SetPixel(0, 0, Color.HSVToRGB(orangeHue, 1, 1));
        colorSelectBar_yellow.SetPixel(0, 0, Color.HSVToRGB(yellowHue, 1, 1));
        colorSelectBar_majenta.SetPixel(0, 0, Color.HSVToRGB(majentaHue, 1, 1));
        colorSelectBar_purple.SetPixel(0, 0, Color.HSVToRGB(purpleHue, 1, 1));
        colorSelectBar_cyan.SetPixel(0, 0, Color.HSVToRGB(cyanHue, 1, 1));

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
                return greenHue;

            case ld34ColorController.Colors.Red:
                return redHue;

            case ld34ColorController.Colors.Blue:
                return blueHue;

            case ld34ColorController.Colors.Yellow:
                return yellowHue;

            case ld34ColorController.Colors.Orange:
                return orangeHue;

            case ld34ColorController.Colors.Majenta:
                return majentaHue;

            case ld34ColorController.Colors.Cyan:
                return cyanHue;

            case ld34ColorController.Colors.Purple:
                return purpleHue;

            default:
                // Assume that default should be green
                return greenHue;
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

            case ld34ColorController.Colors.Red:
                return colorSelectBar_red;

            case ld34ColorController.Colors.Blue:
                return colorSelectBar_blue;

            case ld34ColorController.Colors.Yellow:
                return colorSelectBar_yellow;

            case ld34ColorController.Colors.Orange:
                return colorSelectBar_orange;

            case ld34ColorController.Colors.Majenta:
                return colorSelectBar_majenta;

            case ld34ColorController.Colors.Cyan:
                return colorSelectBar_cyan;

            case ld34ColorController.Colors.Purple:
                return colorSelectBar_purple;

            case ld34ColorController.Colors.White:
                return colorSelectBar_white;
            case ld34ColorController.Colors.Black:
                return colorSelectBar_black;
            case ld34ColorController.Colors.Gray:
                return colorSelectBar_gray;
            default:
                return colorSelectBar_gray;

        }
    }
}
