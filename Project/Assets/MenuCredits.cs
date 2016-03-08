using UnityEngine;
using System.Collections;

//extend MonoBehaviour so that we can attach this script to an object
public class MenuCredits: MonoBehaviour 
{
        //A public bool so that we can change in the editor whether or not the button will quit the application
        public bool isQuit = false;

        //For the next three functions, the collider is absolutely necessary
        //these functions will not fire off if there is no collider


        //Fires off when the mouse hovers over the collider
        //When the mouse is over the item, change the colour of it to 
        //red so that the player knows that it is interacting with it
        void OnMouseEnter()
        {
                renderer.material.color = Color.red;
        }

        //Fires off when the mouse leaves the object
        //We want to change the colour of the object back to it's original when the mouse 
        //is no longer over it so that is exactly what we do here
        void OnMouseExit()
        {
                renderer.material.color = Color.white;
        }


        //Fires off when the mouse is clicked while hovering over the object
        //Here we check if the bool was set to true or not and we load the level id not
        //or quit the application if true
        void OnMouseDown()
        {
                if(isQuit)
                {
                        Application.Quit();
                }
                else
                {
                        Application.LoadLevel(6);
                }
        }
}