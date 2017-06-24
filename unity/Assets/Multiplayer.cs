using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

// define classed needed to deserialize recieved data
[Serializable]
public class Position {
    public Vector3 position;
    public int timestamp;
}
[Serializable]
public class Players {
   public List<Position> players;
}

public class Multiplayer : MonoBehaviour {

  // define public game object used to visualize other players
  public GameObject otherPlayerObject;

  private Vector3 prevPosition;
  private List<GameObject> otherPlayers = new List<GameObject>();

  IEnumerator Start () {
    // get player
    GameObject player = GameObject.Find("FPSController");

    // connect to server
    WebSocket w = new WebSocket(new Uri("ws://localhost:8000"));
    yield return StartCoroutine(w.Connect());
    Debug.Log("CONNECTED TO WEBSOCKETS");

    // generate random ID to have idea for each client (feels unsecure)
    System.Guid myGUID = System.Guid.NewGuid();

    // wait for messages
    while (true) {
      // read message
      string message = w.RecvString();
      // check if message is not empty
      if (message != null) {
        // Debug.Log("RECEIVED FROM WEBSOCKETS: " + reply);

        // deserialize recieved data
        Players data = JsonUtility.FromJson<Players>(message);

        // if number of players is not enough, create new ones
        if (data.players.Count > otherPlayers.Count) {
          for (int i = 0; i < data.players.Count - otherPlayers.Count; i++) {
            otherPlayers.Add(Instantiate(otherPlayerObject, data.players[otherPlayers.Count + i].position, Quaternion.identity));
          }
        }

        // update players positions
        for (int i = 0; i < otherPlayers.Count; i++) {
          // using animation
          otherPlayers[i].transform.position = Vector3.Lerp(otherPlayers[i].transform.position, data.players[i].position, Time.deltaTime * 10F);
          // or without animation
          // otherPlayers[i].transform.position = data.players[i].position;
        }
      }

      // if connection error, break the loop
      if (w.error != null) {
        Debug.LogError("Error: " + w.error);
        break;
      }

      // check if player moved
      if (prevPosition != player.transform.position) {
        // send update if position had changed
        w.SendString(myGUID + "\t" + player.transform.position.x + "\t" + player.transform.position.y + "\t" + player.transform.position.z);
        prevPosition = player.transform.position;
      }

      yield return 0;
    }

    // if error, close connection
    w.Close();
  }
}
