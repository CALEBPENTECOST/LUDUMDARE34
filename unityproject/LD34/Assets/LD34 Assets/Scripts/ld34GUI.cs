using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class ld34GUI : MonoBehaviour {

    [SerializeField]
    ld34PlatformerCharacter2D thePlayerCharacter;

    [SerializeField]
    ld34MenuController theMenuController;



    void OnGUI()
    {


        GUI.skin.label.fontSize = Screen.height / 80;
        int mainGUIBox_startx = 10;
        int mainGUIBox_starty = Screen.height - ((Screen.height / 100) * 20);
        int mainGUIBox_width = Screen.width - (2 * mainGUIBox_startx);
        int mainGUIBox_height = ((Screen.height / 100) * 20) - 10;

        // Make a background box
        GUI.Box(
            new Rect(mainGUIBox_startx, mainGUIBox_starty, mainGUIBox_width, mainGUIBox_height),
            "Controls/Info");

        int GUIlabelYOffset = mainGUIBox_starty + 15;
        int GUIlabelWidth = Screen.width / 100 * 20;
        int GUILabelHeight = 150;
        // We should print off the controls real quick (start with b)
        GUI.Label(
            new Rect(mainGUIBox_startx + mainGUIBox_startx, GUIlabelYOffset, GUIlabelWidth, GUILabelHeight),
            "B\nPRESS TO JUMP\nHOLD TO JUMP HIGHER\nHOLD TO FALL THROUGH LEDGES\n(WHEN IN MENU)\nPRESS TO SELECT RIGHT OPTION");

        // now for A
        GUI.Label(
            new Rect(mainGUIBox_startx + mainGUIBox_startx + GUIlabelWidth, GUIlabelYOffset, GUIlabelWidth, GUILabelHeight),
            "A\nPRESS TO ENTER MENU\n(WHEN IN MENU)\nPRESS TO SELECT LEFT OPTION");

        // now for instructons
        GUI.Label(
            new Rect(mainGUIBox_startx + mainGUIBox_startx + GUIlabelWidth + GUIlabelWidth, GUIlabelYOffset, GUIlabelWidth, GUILabelHeight),
            "TAG THE SQUARES WITH THE CORRECT COLOR\nSPEED INCREASES OVER TIME\nCOLORS UNLOCK OVER TIME");


        // Now things get complicated: The color select bar

        // Set up the location for the bar
        int colorSelectBarStartX = mainGUIBox_startx + mainGUIBox_startx + GUIlabelWidth;
        int colorSelectBarStartY = mainGUIBox_starty + mainGUIBox_height - 20;
        int colorSelectBarWidth = Screen.width - colorSelectBarStartX - 20;
        int colorSelectBarHeight = 15;

        // Draw a black box around everything
        GUI.DrawTexture(new Rect(colorSelectBarStartX, colorSelectBarStartY, colorSelectBarWidth, colorSelectBarHeight),
           PaintInventory.getGUITextureForColor(ld34ColorController.Colors.Black)
           );


        // Set up for the "drawing all those damn squares" part of this operation
        var currentMenuSelection = theMenuController.getCurrentMenuSelection();
        int totalNumSelections = theMenuController.getMaxNumberOfSelections();
        int totalPossibleResults = theMenuController.getMaxNumberOfResults();
        int numSelectionsMade = currentMenuSelection.Count;

        // First, determine the width of the box we are going to make...
        int boxesToHighlight = (int)(totalPossibleResults / Mathf.Pow(2, numSelectionsMade));

        // Okay, now we need to see where we actually put this thing...
        int positionsToMove = 0;
        int totalPossiblePositions = totalPossibleResults;

        bool drawWhiteBox = false;

        // Now is where we need to know the state of things. Is the player in a menu mode?
        if (PlayerStateTransition.isMenuState(thePlayerCharacter.CurrentState))
        {
            // The player is! We now need to determine which part of the state they are in...

            foreach (var v in currentMenuSelection)
            {
                if (v == ld34MenuController.MenuSelection.B)
                {
                    // we add by half of the remaining total
                    positionsToMove += totalPossiblePositions / 2;
                }

                // Halve the possible results
                totalPossiblePositions = totalPossiblePositions / 2;
            }

            // Okay, we now know how many spaces to move over by.
            drawWhiteBox = true;

        }

        // Figure out coloring dimensions
        float colorAreaPercent = 1f / totalPossibleResults;
        // the area for spacing should be 20% of that...
        float spacingArea = colorAreaPercent / 5f;

        int barChunkWidth = (int)(colorSelectBarWidth * colorAreaPercent);
        int h = (int)(barChunkWidth * spacingArea);

        int colorStartPosFromOwnEdge = (int)(barChunkWidth * spacingArea);
        int colorWidth = barChunkWidth - (h * 2);

        int colorBoxHeight = colorSelectBarHeight - 5;
        int colorBoxHeight_fromTop = (colorSelectBarHeight - colorBoxHeight) / 2;

        // Draw the white box?
        if (drawWhiteBox)
        {
            GUI.DrawTexture(
                new Rect(
                    colorSelectBarStartX + barChunkWidth * positionsToMove,
                    colorSelectBarStartY,
                    barChunkWidth * boxesToHighlight,
                    colorSelectBarHeight),
                PaintInventory.getGUITextureForColor(ld34ColorController.Colors.White));
        }

        // Now its time to draw each colored boxes...
        // for the first box
        for (int boxIndex = 0; boxIndex < totalPossibleResults; boxIndex++)
        {
            paintColorBoxForColorAtIndex(colorSelectBarStartX, colorSelectBarStartY, barChunkWidth, colorStartPosFromOwnEdge, colorWidth, colorBoxHeight, colorBoxHeight_fromTop, boxIndex);
        }
    }

    private void paintColorBoxForColorAtIndex(int colorSelectBarStartX, int colorSelectBarStartY, int barChunkWidth, int colorStartPosFromOwnEdge, int colorWidth, int colorBoxHeight, int colorBoxHeight_fromTop, int boxIndex)
    {
        var colorAtIndex = theMenuController.colorAtIndex(boxIndex);
        // Now get a texture from the paintcan for the color we should draw
        Texture2D drawingPaint = PaintInventory.getGUITextureForColor(colorAtIndex);

        // start drawing at index*width plus start
        GUI.DrawTexture(
            new Rect(
                    colorSelectBarStartX + (boxIndex * barChunkWidth) + colorStartPosFromOwnEdge,
                    colorSelectBarStartY + colorBoxHeight_fromTop,
                    colorWidth,
                    colorBoxHeight),
            drawingPaint);
    }
}
