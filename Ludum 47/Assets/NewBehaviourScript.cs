using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    private string face;
    private int spr_index;
    private float prev_h_speed = 0;
    private float translation;
    private Rigidbody2D rb;
    private int frame_speed = 32;
    private float h_move = 0;
    public Sprite[] idle_r;
    public Sprite[] idle_l;
    public Sprite[] walk_r;
    public Sprite[] walk_l;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        face = "idle_r";
        spr_index = 0;
    }

    void Update() {
        translation = Input.GetAxis("Horizontal");
        h_move = 2 * translation;
        switch (face) {
            case "idle_r":
                GetComponent<SpriteRenderer>().sprite = idle_r[(int)(spr_index/frame_speed)];
                spr_index = (spr_index + 1) % (idle_r.Length * frame_speed);
                break;
            case "idle_l":
                GetComponent<SpriteRenderer>().sprite = idle_l[(int)(spr_index/frame_speed)];
                spr_index = (spr_index + 1) % (idle_l.Length * frame_speed);
                break;
            case "walk_r":
                GetComponent<SpriteRenderer>().sprite = walk_r[(int)(spr_index/frame_speed)];
                spr_index = (spr_index + 1) % (walk_r.Length * frame_speed);
                break;
            case "walk_l":
                GetComponent<SpriteRenderer>().sprite = walk_l[(int)(spr_index/frame_speed)];
                spr_index = (spr_index + 1) % (walk_l.Length * frame_speed);
                break;
        }
        Debug.Log(spr_index);
        if (h_move == 0) {
            face = (prev_h_speed >= 0) ? "idle_r" : "idle_l";
            return;
        } else if (h_move > 0)
            face = "walk_r";
        else
            face = "walk_l";
        prev_h_speed = h_move;
        rb.velocity = new Vector2(transform.position.x * h_move, 0);
    }
}
