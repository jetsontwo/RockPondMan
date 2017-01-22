﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour {

    public int need_to_collect;
    public VacuumCollision[] vc;
    public start_to_level_selection level_select_script;
    public Text Win_Text;
    public Canvas canvas;
    public Camera cam;
    private bool won = false;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        int vacuum_total = 0;
        for (int i = 0; i < vc.Length; ++i)
            vacuum_total += vc[i].return_items_caught();
        
        if (need_to_collect == vacuum_total && !won)
        {
            StartCoroutine(Win_Level());
            won = true;
        }
    }


    IEnumerator Win_Level()
    {
        yield return new WaitForSeconds(0.5f);
        Text win_text = Instantiate(Win_Text, canvas.transform);
        win_text.transform.position = Vector3.zero;
        yield return new WaitForSeconds(2);
        while(cam.transform.position.x < 22)
        {
            if (cam.transform.position.x + 0.18f > 22)
                cam.transform.position +=  new Vector3(22 - cam.transform.position.x, 0, 0);
            else
                cam.transform.position += new Vector3(0.18f, 0, 0);
            win_text.transform.localPosition += new Vector3(-1f, 0, 0);
            yield return new WaitForSeconds(0.00001f);
        }
        level_select_script.LoadOnClick(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
