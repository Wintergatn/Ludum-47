using UnityEngine;
using System.Collections;
using PathCreation;

// Moves along a path at constant speed.
// Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
public class PathFollower : MonoBehaviour
{
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    public float speed = 5;
    float distanceTravelled;

    void Start() {
        if (pathCreator != null)
        {
            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            pathCreator.pathUpdated += OnPathChanged;
        }
    }

    void Update()
    {
        if (pathCreator != null)
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);

            Quaternion tempQuat = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            //Quaternion finalQuat = new Quaternion(tempQuat.x, tempQuat.y, tempQuat.z, tempQuat.w);
            Quaternion finalQuat = new Quaternion(0, tempQuat.y, 0, tempQuat.w);
            Debug.Log("PAEWEWE " + finalQuat.ToString());
            transform.rotation = finalQuat;
        }
    }

    // If the path changes during the game, update the distance travelled so that the follower's position on the new path
    // is as close as possible to its position on the old path
    void OnPathChanged() {
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }
}