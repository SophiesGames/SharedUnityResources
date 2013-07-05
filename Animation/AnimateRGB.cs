using UnityEngine;
using System.Collections;


/// <summary>
/// Improvements: Instead of original values for inspector there should be a public material so you can make the original material.
/// 
/// </summary>

public class AnimateRGB : MonoBehaviour
{
    private float fadeSpeedDelta = 0;
    public float fadeSpeed = 0.1f;

    public float origRed = 1.0f;
    public float origGreen = 1.0f;
    public float origBlue = 1.0f;
    public float origAlpha = 1.0f;

    [HideInInspector]
    public Color newColor;

    private Color color;
    private float alpha;

    private void Awake()
    {
        if (gameObject.guiText == null)//its not a text
        {
            gameObject.renderer.material.color = new Color(origRed, origGreen, origBlue, origAlpha);
            color = gameObject.renderer.material.color;
        }
        else
        {
            guiText.material.color = new Color(origRed, origGreen, origBlue, origAlpha);
            color = guiText.material.color;
        }
        //guiText.font.material.color = new Color(origRed, origGreen, origBlue, origAlpha);
        
        newColor = color;
    }
    private void Update()
    {
        ////if (this.gameObject.name == "PlantHead")
        ////{
        ////    Debug.Log("newColor.a" + newColor.a);
        ////    Debug.Log("color.a" + color.a);
        ////}

        fadeSpeedDelta = fadeSpeed * Time.deltaTime;//delta time gets time to complete frame. 
        //if you want 1 unit to move every second, doing the above will times 1 unit by a fraction equivelent to the number of fractins in a second.
        //DeltaTime * x = 1 second.

        if (newColor.r != color.r)
        {
            CheckR();
        }
        if (newColor.g != color.g)
        {
            CheckG();
        }
        if (newColor.b != color.b)
        {
            CheckB();
        }
        if (newColor.a != color.a)
        {
            CheckAlpha();
        }
        if (gameObject.guiText == null)//its not a text
        {
            renderer.material.SetColor("_Color", color);
        }
        else
        {
            guiText.material.color = new Color(color.r, color.g, color.b, color.a);
        }
    }

    private void CheckR()
    {
        if (color.r > newColor.r && color.r - fadeSpeedDelta > newColor.r ) //if its greater and taking it away, its still bigger
        {
			this.color.r = this.color.r - fadeSpeedDelta; //take it away
        }
        else if (this.color.r < newColor.r && this.color.r + fadeSpeedDelta < newColor.r)//if its smaller and if adding it, its still smaller
        {
            this.color.r = this.color.r + fadeSpeedDelta; //add it on.
        }
		else
		{
			color.r = newColor.r;						//else if none of the abvoe it needs to be made =
		}
    }
    private void CheckG()
    {
        if (color.g > newColor.g && color.g - fadeSpeedDelta > newColor.g ) //if its greater and taking it away, its still bigger
        {
			this.color.g = this.color.g - fadeSpeedDelta; //take it away
        }
        else if (this.color.g < newColor.g && this.color.g + fadeSpeedDelta < newColor.g)//if its smaller and if adding it, its still smaller
        {
            this.color.g = this.color.g + fadeSpeedDelta; //add it on.
        }
		else
		{
			color.g = newColor.g;						//else if none of the abvoe it needs to be made =
		}
    }
    private void CheckB()
    {
        if (color.b > newColor.b && color.b - fadeSpeedDelta > newColor.b ) //if its greater and taking it away, its still bigger
        {
			this.color.b = this.color.b - fadeSpeedDelta; //take it away
        }
        else if (this.color.b < newColor.b && this.color.b + fadeSpeedDelta < newColor.b)//if its smaller and if adding it, its still smaller
        {
            this.color.b = this.color.b + fadeSpeedDelta; //add it on.
        }
		else
		{
			color.b = newColor.b;						//else if none of the abvoe it needs to be made =
		}
    }
    private void CheckAlpha()
    {
        if (color.a > newColor.a && color.a - fadeSpeedDelta > newColor.a) //if its greater and taking it away, its still bigger
        {
			this.color.a = this.color.a - fadeSpeedDelta; //take it away
        }
        else if (this.color.a < newColor.a && this.color.a + fadeSpeedDelta < newColor.a)//if its smaller and if adding it, its still smaller
        {
            this.color.a = this.color.a + fadeSpeedDelta; //add it on.
        }
		else
		{
			color.a = newColor.a;						//else if none of the abvoe it needs to be made =
		}
    }

    /// <summary>
    /// Takes a number from 1-4 corresponding to the RGBA channels. Takes the new value to set them too. Optionally takes a new speed
    /// </summary>
    /// <param name="channelToChange"></param>
    /// <param name="newValue"></param>
    public void SetRGBA(int channelToChange, float newValue, float newFadeSpeed = 0.0f)
    {


        if (newFadeSpeed != 0.0f)
        {
            fadeSpeed = newFadeSpeed;
        }

        switch (channelToChange)
        {
            case 1:
                {
                    newColor.r = newValue;
                }
                break;

            case 2:
                {
                    newColor.g = newValue;
                }
                break;

            case 3:
                {
                    newColor.b = newValue;
                }
                break;

            case 4:
                {
                    newColor.a = newValue;
                }
                break;

            default:
                {
                    Debug.LogError("No or incorrect RGBA value given - give a value between 1-4 to correspond to RGBA");
                }
                break;
        }
    }
}