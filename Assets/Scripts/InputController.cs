using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : Controller
{
    //store camera following player
    [SerializeField, Tooltip("Set the player's Camera")]
    private Camera playerCam;
    

    // Start is called before the first frame update
    public override void Start()
    {
        

       

        base.Start();//run start from controller
    }

    // Update is called once per frame
    public override void Update()
    {

        //send movement speed to pawn (x and z)
        pawn.Move(new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")));

        //call rotate towards mouse
        pawn.RotateTowards(GetMousePosition());
        
        if (Input.GetButton("Jump"))
        {
            pawn.Jump();
        }

        base.Update();//run update from controller
    }
    private Vector3 GetMousePosition()
    {
        //create vector
        Vector3 mousePointInTheWorld = new Vector3();

        //set plane to up in worldspace and players position
        Plane charPlane = new Plane(Vector3.up, pawn.transform.position);

        //create ray
        Ray viewFinder;

        //look through player camera to mouse point, assign to viewfinder
        viewFinder = playerCam.ScreenPointToRay(Input.mousePosition);

        //find the point where the 2 rays cross
        float crossDistance;

        if (charPlane.Raycast(viewFinder, out crossDistance))//if the 2 rays intersect output distance
        {
            mousePointInTheWorld = viewFinder.GetPoint(crossDistance);// use rays to get distance
        }
        //send ray cross point out
        return mousePointInTheWorld;
    }

}
