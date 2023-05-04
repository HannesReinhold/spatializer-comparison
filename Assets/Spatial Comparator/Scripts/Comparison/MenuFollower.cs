using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFollower : MonoBehaviour
{
    public Vector3 MenuOffset;
    public float FollowSmoothness = 1;

    public Transform Player;
    public Transform MenuSlot;

    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<Camera>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = MenuOffset.z;
        

        Vector3 forward = new Vector3(Player.forward.x, 0, Player.forward.z).normalized;
        Vector3 right = new Vector3(Player.right.x, 0, Player.right.z).normalized;

        Vector3 direction = forward * dist + right * MenuOffset.x + Vector3.up * MenuOffset.y;
        Vector3 newPosition = Player.position + direction;

        MenuSlot.position = Vector3.Lerp(MenuSlot.position, newPosition, Time.deltaTime*FollowSmoothness);
        MenuSlot.LookAt(new Vector3(Player.position.x, newPosition.y, Player.position.z));
        MenuSlot.Rotate(Vector3.up - new Vector3(0, 180, 0));
    }


}
