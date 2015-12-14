using UnityEngine;
using System.Collections;
using System;

public class PaintInventory : MonoBehaviour {
    [SerializeField]
    ld34MenuController TheMenuController;

    public float selectedHue{
		get {
            // We should get the current color in the menu controller...
            return getHueFromColor(TheMenuController.getCurrentlySelectedColor());
        }
		private set {
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
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
