using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Mono.Xml;
using System.Text.RegularExpressions;

public class AnimationXML : SmallXmlParser.IContentHandler
{
    private bool readingFrames;
    private bool newDictTag;
    private int dictClosingTags = 0;
    private int stringNumb = 0;
    private bool getSheetSize = false;

    private MatchCollection matchCollection;
    private Rect frameRect = new Rect();
    public List<Rect> frameDetails = new List<Rect>();
    public Vector2 atlasDimension = new Vector2();

    public void OnStartParsing(SmallXmlParser parser)
    { // start of file
        //Debug.Log("OnStartParsing");
    }

    public void OnEndParsing(SmallXmlParser parser)
    { // end of file
        //Debug.Log("OnEndParsing");
    }

    public void OnStartElement(string name, SmallXmlParser.IAttrList attrs)
    { // opening tag, with attributes
        if (dictClosingTags < 2)    //two clsoing tags means its left key frames
        {
            if (name == "dict") //check that a new dict tag has been opened
            {
                newDictTag = true;
            }

            if (name == "string" && newDictTag) //check that a new string tag has been opened AND its under a new dict tag.
            {
                readingFrames = true;
                dictClosingTags = 0;
            }
        }
        else//get teh sheet size
        {
            if (name == "string" && stringNumb == 0) //check that a new dict tag has been opened
            {
                stringNumb = 1;
            }
            else if (name == "string" &&  stringNumb == 1)
            {
                getSheetSize = true;
            }
        }
    }

    public void OnEndElement(string name)
    { // closing tag
        if (name == "string")
        {
            readingFrames = false;
            newDictTag = false;
            if (stringNumb == 999)
            {
                getSheetSize = false;       //for later on with sprite sheet size
            }

        }
        if (name == "dict")
        {
            dictClosingTags++;
        }
    }

    public void OnChars(string s)
    { // string between open and closing tag

        if (readingFrames)
        {
            matchCollection = Regex.Matches(s, @"\d+");
            frameRect.x = int.Parse(matchCollection[0].Value); //(matchCollection[0].Value);
            frameRect.y = int.Parse(matchCollection[1].Value);
            frameRect.width = int.Parse(matchCollection[2].Value);
            frameRect.height = int.Parse(matchCollection[3].Value);

            frameDetails.Add(frameRect);}

        if (getSheetSize)
        {
            matchCollection = Regex.Matches(s, @"\d+");
            atlasDimension.x = int.Parse(matchCollection[0].Value);
            atlasDimension.y = int.Parse(matchCollection[1].Value);

            stringNumb = 999; //puts it out of use
        }
    }

    public void OnIgnorableWhitespace(string s)
    {
    }

    public void OnProcessingInstruction(string name, string text)
    {
    }
}
