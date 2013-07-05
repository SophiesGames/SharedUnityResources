using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationXMLData
{
    private static AnimationXMLData _AnimationXMLData;
    public static AnimationXMLData animationXMLData
    {
        get
        {
            if (_AnimationXMLData == null)
            {
                _AnimationXMLData = new AnimationXMLData();
            }
            return _AnimationXMLData;
        }
    }

    public List<List<Rect>> ListOfLists = new List<List<Rect>>();
    public List<string> loadedXMLS = new List<string>();
    public List<Vector2> sheetDimensions = new List<Vector2>();
    public string currentXMLName = "";
}
