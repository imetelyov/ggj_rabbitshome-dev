using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace QuasarGames
{

    public enum DebugColors
    {
        aqua,
        black,
        blue,
        brown,
        cyan,
        darkblue,
        fuchsia,
        green,
        grey,
        lightblue,
        lime,
        magenta,
        maroon,
        navy,
        olive,
        purple,
        red,
        silver,
        teal,
        white,
        yellow
    }
    /// <summary>
    /// Class, that contains all of extension methods
    /// </summary>
    public static class ExtensionMethods
    {
        #region colors extenstions

        /// <summary>
        /// Set RGB channels, alpha channel doesn't change
        /// </summary>
        /// <param name="target">Compent, which will change its color</param>
        /// <param name="newColor">new target color</param>
        public static void SetRGB(this SpriteRenderer target, Color newColor)
        {
            target.color = new Color(newColor.r, newColor.g, newColor.b, target.color.a);
        }

        /// <summary>
        /// Return true if colors' RGB channels are equal
        /// </summary>
        /// <param name="color1">First color</param>
        /// <param name="colorForCompare">Color for compare</param>
        public static bool CompareRGB(this Color color1, Color colorForCompare)
        {
            if(color1.r == colorForCompare.r && color1.g == colorForCompare.g && color1.b == colorForCompare.b)
                return true;
            
            return false;
        }


        #region ColorSetAlpha methods
        /// <summary>
        /// Set color with defined alpha channel to target component and 
        /// </summary>
        /// <param name="target">Compent, which will change its color's alpha channel</param>
        /// <param name="alpha">number from 0 to 1, which is responsible for alpha channel (opacity)</param>
        /// <returns>Color with new alpha channel</returns>
        public static Color ColorSetAlpha(this SpriteRenderer target, float alpha)
        {
            Color color = target.color;
            target.color = new Color(color.r, color.g, color.b, alpha);
            return target.color;
        }
        /// <summary>
        /// Set color with defined alpha channel to target component and 
        /// </summary>
        /// <param name="target">Compent, which will change its color's alpha channel</param>
        /// <param name="alpha">number from 0 to 1, which is responsible for alpha channel (opacity)</param>
        /// <returns>Color with new alpha channel</returns>
        public static Color ColorSetAlpha(this Image target, float alpha)
        {
            Color color = target.color;
            target.color = new Color(color.r, color.g, color.b, alpha);
            return target.color;
        }
        /// <summary>
        /// Set color with defined alpha channel to target component and 
        /// </summary>
        /// <param name="target">Compent, which will change its color's alpha channel</param>
        /// <param name="alpha">number from 0 to 1, which is responsible for alpha channel (opacity)</param>
        /// <returns>Color with new alpha channel</returns>
        public static Color ColorSetAlpha(this Text target, float alpha)
        {
            Color color = target.color;
            target.color = new Color(color.r, color.g, color.b, alpha);
            return target.color;
        }
        #endregion


        /// <summary>
        /// Return color with changed alpha channel
        /// </summary>
        /// <param name="col">color</param>
        /// <param name="alpha">new alpha chanel</param>
        /// <returns></returns>
        public static Color RetungWithAlpha(this Color col, float alpha)
        {
            return new Color(col.r, col.g, col.b, alpha);
        }

        #endregion

        #region Transform extenstions

        /// <summary>
        /// Set x,y coordinate position. Z position doesn't change
        /// </summary>
        /// <param name="trans">Transform, which will change it's position</param>
        /// <param name="pos">position, which will be set</param>
        public static void SetPos2D(this Transform trans, Vector3 pos)
        {
            trans.position = new Vector3(pos.x,pos.y,trans.position.z);
        }


        /// <summary>
        /// Set X,Y coordinate position to the transform component 
        /// </summary>
        /// <param name="trans">Transform, which will change it's position</param>
        /// <param name="xPos">X coordinate position</param>
        /// <param name="yPos">Y coordinate position</param>
        public static void SetPos2D(this Transform trans, float xPos, float yPos)
        {
            trans.position = new Vector3(xPos, yPos, trans.position.z);
        }


        /// <summary>
        ///  Set X position. Y,Z position don't change
        /// </summary>
        /// <param name="trans">Transform, which will change it's position</param>
        /// <param name="xPos">X coordinate position</param>
        public static void SetPosX(this Transform trans, float xPos)
        {
            trans.position = new Vector3(xPos, trans.position.y, trans.position.z);
        }

        /// <summary>
        ///  Set Y position. X,Z position don't change
        /// </summary>
        /// <param name="trans">Transform, which will change it's position</param>
        /// <param name="yPos">Y coordinate position</param>
        public static void SetPosY(this Transform trans, float yPos)
        {
            trans.position = new Vector3(trans.position.x, yPos, trans.position.z);
        }


        /// <summary>
        ///  Set Z position. X,Y position don't change
        /// </summary>
        /// <param name="trans">Transform, which will change it's position</param>
        /// <param name="zPos">Z coordinate position</param>
        public static void SetPosZ(this Transform trans, float zPos)
        {
            trans.position = new Vector3(trans.position.x, trans.position.y, zPos);
        }

        /// <summary>
        /// Set euler Z anglle. Euler X,Y angle don't change
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="angle"></param>
        public static void SetAngleZ(this Transform trans, float angle)
        {
            trans.eulerAngles = new Vector3(trans.eulerAngles.x, trans.eulerAngles.y, angle);
        }

        /// <summary>
        /// Set X,Y scale value to the Transform object
        /// </summary>
        /// <param name="trans">Transform object, which will be scaled</param>
        /// <param name="scale">Defined scale for object</param>
        public static void SetScale2D(this Transform trans, Vector2 scale)
        {
            trans.localScale = new Vector3(scale.x, scale.y, trans.localScale.z);
        }

        //Breadth-first search

        /// <summary>
        /// Find child in deep of Transform object
        /// </summary>
        /// <param name="parent">Parent of seeking child object</param>
        /// <param name="seekingTransName">Name of seeking transform object</param>
        /// <returns>Null or finded Transform child</returns>
        public static Transform FindDeepChild(this Transform parent, string seekingTransName)
        {
            var result = parent.Find(seekingTransName);
            if (result != null)
                return result;
            foreach (Transform child in parent)
            {
                result = child.FindDeepChild(seekingTransName);
                if (result != null)
                    return result;
            }
            return null;
        }

        #endregion

        #region For Debug Extension
        /// <summary>
        /// Sets the color of the text according to the parameter value.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="color">Color.</param>
        public static string StrColored(this string message, DebugColors color)
        {
            return string.Format("<color={0}>{1}</color>", color.ToString(), message);
        }

        /// <summary>
        /// Sets the size of the text according to the parameter value, given in pixels.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="size">Size.</param>
        public static string StrSized(this string message, int size)
        {
            return string.Format("<size={0}>{1}</size>", size, message);
        }

        /// <summary>
        /// Renders the text in boldface.
        /// </summary>
        /// <param name="message">Message.</param>
        public static string StrBold(this string message)
        {
            return string.Format("<b>{0}</b>", message);
        }

        /// <summary>
        /// Renders the text in italics.
        /// </summary>
        /// <param name="message">Message.</param>
        public static string StrItalics(this string message)
        {
            return string.Format("<i>{0}</i>", message);
        }
        #endregion

    }
}