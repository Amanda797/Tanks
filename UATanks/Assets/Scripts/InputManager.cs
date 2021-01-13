using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


// Get Player Input and call movement functions
public class InputManager : MonoBehaviour
{
    // Pawn script reference
    [SerializeField]  TankData pawn;

    // Setting up enum to contain WASD and arrows as player movement on keyboard
    public enum InputType
    {
        WASD, ARROWS
    }

    public InputType inputType;

    // Setting up way to move and to rotate tanks.
    Vector3 directionToMove;
    float rotateDirection;

    private void Update()
    {

        directionToMove = Vector3.zero;
        rotateDirection = 0;

        if (inputType == InputType.WASD)
        {
            
            // Get player one input for movement
            // player one uses WASD
            if (Input.GetKey(KeyCode.W))
            {
                // Using input.GetKeyDown returns true once when the key is pressed down, returns false until key is released.
                // Using input.GetKey returns true while the key is down, false while the key is up.
                // Use plus to go forward.
                directionToMove += pawn.transform.forward;
            }

            if (Input.GetKey(KeyCode.A))
            {
                // Use negative when to turn.
                rotateDirection = -1f;
            }

            if (Input.GetKey(KeyCode.S))
            {
                // Use minus to go backward but it is still a transform forward.
                directionToMove -= pawn.transform.forward;
            }

            if (Input.GetKey(KeyCode.D))
            {
                // Use positive 1 to turn in opposite direction.
                rotateDirection = 1f;
            }

            // Use Getkeydown to happen everytime you hit spacebar. Can't just press on spacebar and it continue to shoot like the movement.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // shoot using spacebar. Call function to shoot
                pawn.mover.Shoot();
            }
        }

        else if (inputType == InputType.ARROWS)
        {
            // Get player two input for movement, arrow keys instead of WSAD, otherwise it operates the same way.
            if (Input.GetKey(KeyCode.UpArrow))
            {
                directionToMove += pawn.transform.forward;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                directionToMove -= pawn.transform.forward;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rotateDirection = -1f;
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                rotateDirection = 1f;
            }

            // Use return or enter key to shoot for player 2
            if (Input.GetKeyDown(KeyCode.Return))
            {
                //shoot
                pawn.mover.Shoot();
            }
        }

        // Lets player move and rotate.
        pawn.mover.Move(directionToMove);
        pawn.mover.Rotate(rotateDirection);
    }
}
