using System;

public enum Selection
{
    First   = 0,
    Second  = 1,
    Third   = 2,
    Fourth  = 3,
    Fifth   = 4,
    Sixth   = 5,
    Seventh = 6,
    Eighth  = 7
}
/// <summary>
/// Done using offset "on" bits to make it easy to create a bit field of "selectable values."
/// </summary>
public enum SelectionValue
{
    White       = 1 << Selection.First,
    Black       = 1 << Selection.Second,
    Red         = 1 << Selection.Third,
    Green       = 1 << Selection.Fourth,
    Orange      = 1 << Selection.Fifth,
    Blue        = 1 << Selection.Sixth,
    Yellow      = 1 << Selection.Seventh,
    Purple      = 1 << Selection.Eighth,
}

public class SelectableValues
{
    private int enabledValues = 0;

    public SelectableValues(params Selection[] values)
    {
        foreach (var v in values)
        {
            enabledvalues &= 1 << v;
        }
    }

    public SelectableValues(params SelectionValue[] values)
    {
        foreach (var v in values)
        {
            enabledValues &= v;
        }
    }

    public SelectableValues(int EnabledValues)
    {
        enabledvalues = EnabledValues;
    }

    public bool IsEnabled(SelectionValue v)
    {
        return (enabledvalues & v != 0);
    }
}

public class Menu
{
    public enum MenuInput
    {
        InputA  = 0,
        InputB  = 1
    }

    private SelectableValues selectables = new SelectableValues(0);
    /// <summary>
    /// Menu selections are done by entering binary through two possible inputs. For example, Entry
    /// AAA would be the 1st entry, and AAB would be the 2nd entry, and ABA would be the 3rd. Because
    /// the selection values are generated using offset "on" bits, the "actual" selection number (ex 
    /// the 6th thing) becomes an offset for the enumeration.
    /// </summary>
    private int currentSelectionOffset = 0;

    public Menu(SelectableValues v)
    {
        if (v != null)
        {
            selectables = v;
        }
    }

    public void SetSelectables(SelectableVaues v)
    {
        if (v != null)
        {
            selectables = v;
        }
    }

    public void StartSelection()
    {
        currentSelection = 0;
    }

    public bool AddInput(MenuInput i)
    {
        // Add a new "on" or "off" bit using the current input.
        currentSelection = currentSelection << 1;
        currentSelection |= i;
        if (((1 << i) & selectables) == 0)
        {
            bool canShortCircuit = CheckShortCircuit(currentSelection);
        }
        return CheckShortCircuit(currentSelection);
    }

    private bool CheckShortCircuit(int i)
    {
        // Case 1: The current node IS a selectable value. Can't short-circuit.
        if (((1 << i) & selectables) != 0)
        {
            return false;
        }
        // Case 2: The current node NOT a selectable value. Check if there are any selectable
        // values stemming from this one that may be selectable. If so, can't short-circuit.
        else
        {
            int subNode1 = i << 1 | 1; // offset number
            int subNode0 = i << 1 | 0; // offset number
            // Case 2a: There are NO sub-nodes to check. It's OK to short-circuit
            if (false)
            {
                return true;
            }

            // Case 2b: There ARE sub-nodes to check. Check them.
            return CheckShortCircuit(subNode1) && CheckShortCircuit(subNode0);
        }
    }
}
