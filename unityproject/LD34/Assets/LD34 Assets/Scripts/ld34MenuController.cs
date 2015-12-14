using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityStandardAssets._2D;

#region CalMenu
// A menu is not really a tree, you just traverse it that way
// A player character will supply three selections
public class ld34ColorController
{
    // Number of user inputs
    public static readonly int NumSelections = 3;

    // Number of possible colors, based on these selections
    public static readonly int NumColors = (int)Math.Pow(2, 3);

    private static Dictionary<int, Colors> d_intToColor_AllPossibleColors;
    private static int enabledColors = 0;

    public int getEnabledColors()
    {
        return enabledColors;
    }

    // Now we need an enumeration, for ALL possible colors, in order of appearance
    public enum Colors
    {
        Green,
        Red,
        Blue,
        Yellow,
        Orange,
        Purple,
        Teal,
        Pink
    }

    /// <summary>
    /// Returns the index in the dictionary for the provided color
    /// </summary>
    /// <param name="colorNum"></param>
    /// <returns></returns>
    private int getDictionaryIndexFromColorNumber(int colorNum)
    {
        if(colorNum % 2 == 0)
        {
            // The dictionary index is just the number
            return colorNum;
        }
        else
        {
            // The dictionary index is the max count, minus the number
            return NumColors - colorNum;
        }
    }

    /// <summary>
    /// Constructor. Sets up the color dictionary, and takes in a parameter of how many colors to enable
    /// </summary>
    /// <param name="numToEnable"></param>
    public ld34ColorController(int numToEnable)
    {
        // Init the color dictionary
        d_intToColor_AllPossibleColors = new Dictionary<int, Colors>(NumColors);

        // Thge constructor needs to figure out where to place colors, for best effect
        for (int currentColor = 0; currentColor < NumColors; currentColor++)
        {
            int dictIndex = getDictionaryIndexFromColorNumber(currentColor);

            // We place this color at the provided index
            d_intToColor_AllPossibleColors[dictIndex] = (Colors)currentColor;
        }

        // We have created our dictionary of possible colors. Lets enable the ones requested to be enabled
        enabledColors = numToEnable; 
    }

    /// <summary>
    /// Attempts to get the color at the provided index. If the color is not enabled, returns false
    /// </summary>
    /// <param name="colorNum"></param>
    /// <param name="color"></param>
    /// <returns></returns>
    public bool GetColorAtIndexIfAvailable(int colorNum, ref Colors color)
    {
        if(isColorEnabled(colorNum))
        {
            // Color is enabled. Get it from the dictionary
            color = d_intToColor_AllPossibleColors[colorNum];
            Debug.Log("Color to " + color.ToString() + " " + colorNum);
            return true;
        }
        else
        {
            Debug.Log("Color not enabled: "
                + d_intToColor_AllPossibleColors[colorNum]
                + " " + colorNum);
            // Color is not enabled. return false
            return false; 
        }
    }

    /// <summary>
    /// Returns true if the color at the provided index is available
    /// </summary>
    /// <param name="colorNum"></param>
    /// <returns></returns>
    public bool isColorEnabled(int colorNum)
    {
        // If the color num is larger than the number enabled - 1, then it is not available
        if (getDictionaryIndexFromColorNumber(colorNum) > enabledColors - 1)
        {
            return false;
        }
        else return true;
    }

    /// <summary>
    /// Enables an additional color in the color list. Returns false if a color could not be added
    /// </summary>
    /// <returns></returns>
    public bool enableAdditionalColor()
    {
        if (enabledColors == NumColors)
            return false;

        enabledColors++;
        return true;
    }

    /// <summary>
    /// Removed a color from the enabled color lis. Returns false if a color could not be removed
    /// </summary>
    /// <returns></returns>
    public bool disableEnabledColor()
    {
        if (enabledColors == 0)
            return false;
        enabledColors--;
        return true;
    }
} // ld34 color controller

#endregion
public class ld34MenuController : MonoBehaviour {

    public enum MenuSelection
    {
        A = 0,
        B = 1
    }

    /// <summary>
    /// The currently selected color (defaults to green, the first color)
    /// </summary>
    private ld34ColorController.Colors currentlySelectedColor = ld34ColorController.Colors.Green;

    /// <summary>
    /// An internal array used for keeping track of the current menu selection.
    /// </summary>
    private MenuSelection[] currentMenuSelection = new MenuSelection[ld34ColorController.NumSelections];

    private int currentCountSelections = 0;

    /// <summary>
    /// Color controller, which will keep track of which colors are available
    /// </summary>
    ld34ColorController theColorController;

	// Use this for initialization
	void Start () {
        // We need to initialize a color controller. Start with two colors available
        theColorController = new ld34ColorController(2);
	}


    /// <summary>
    /// Tell the menu to unlock a color
    /// </summary>
    /// <returns></returns>
    public bool unlockNewColor()
    {
        return theColorController.enableAdditionalColor();
    }

    /// <summary>
    /// Tell the menu to lose a color
    /// </summary>
    /// <returns></returns>
    public bool loseAColor()
    {
        return theColorController.disableEnabledColor();
    }

    /// <summary>
    /// Returns the number of colors left in the internal color controller
    /// </summary>
    /// <returns></returns>
    public int getNumColorsLeft()
    {
        return theColorController.getEnabledColors();
    }

    /// <summary>
    /// Returns the enumeration corresponding with the currently selected color
    /// </summary>
    /// <returns></returns>
    public ld34ColorController.Colors getCurrentlySelectedColor()
    {
        return currentlySelectedColor;
    }

    public bool performMenuSelection(StateInput stateInput, ref bool selectionFinished)
    {
        switch (stateInput)
        {
            case StateInput.Apress:
                // Call the inner function
                return performMenuSelection(false, ref selectionFinished);
            case StateInput.Bpress:
                // Call the inner function
                return performMenuSelection(transform, ref selectionFinished);

            default:
                // If it is not one of these two states, its an error. We will return false on all accounts
                selectionFinished = false;
                return false;
        }
    }
    /// <summary>
    /// Adds to the menu selection. Will return true if the color is changed.
    /// The selectionFinished parameter will be set to true when a full selection completes.
    /// </summary>
    /// <param name="selectedB"></param>
    /// <param name="selectionFinished"></param>
    /// <returns></returns>
    public bool performMenuSelection(bool selectedB, ref bool selectionFinished)
    { 
        // set the value in our array, depending on the parameter
        currentMenuSelection[currentCountSelections++] = (selectedB?MenuSelection.B :MenuSelection.A);

        // How many selections do we have now?
        if(currentCountSelections == ld34ColorController.NumSelections)
        {
            // Thats all our selections! We need to attempt to select a color
            selectionFinished = true;
            currentCountSelections = 0;
            Debug.Log("selectionFinished");
            int selection = getIntegerRepresentationOfMenusSelectionArray(currentMenuSelection);
            if (theColorController.GetColorAtIndexIfAvailable(selection, ref currentlySelectedColor))
            {
                // Successfully selected a color. return true

                return true;
            }
            else
            {
                // The color was not enabled
                // the currently selected color was left unchanged, so return false
                return false;
            }
        }
        else
        {
            // We did not finish selecting. This also means that we did not change colors
            selectionFinished = false;
            return false;
        }
    }

    /// <summary>
    /// Takes an array of menu selections and returns the appropriate integer representation
    /// </summary>
    /// <param name="currentMenuSelection"></param>
    /// <returns></returns>
    public static int getIntegerRepresentationOfMenusSelectionArray(MenuSelection[] currentMenuSelection)
    {
        int numSelections = ld34ColorController.NumSelections;
        // We need to create a boolean array
        bool[] bits = new bool[numSelections];
        for(int i = 0; i < numSelections; i++)
        {
            int wantedMenuIndex = numSelections - 1 - i;
            bits[i] = (currentMenuSelection[wantedMenuIndex] == MenuSelection.B);
            Debug.Log("Bit " + i + " is " + bits[i].ToString());
        }

        // Convert the boolean array to an int
        BitArray ba = new BitArray(bits);
        var result = new int[1];
        ba.CopyTo(result, 0);
        Debug.Log("Result: " + result[0]);
        return result[0];
    }

    public List<MenuSelection> getCurrentMenuSelection()
    {
        List<MenuSelection> currentSelection = new List<MenuSelection>();
        for(int i = 0; i < currentCountSelections; i++)
        {
            // Add to the list
            currentSelection.Add(currentMenuSelection[i]);
        }
        // Return the list
        return currentSelection;
    }
	
	// Update is called once per frame
	void Update () {
	    // No need to really do anything on update
	}

    
}
