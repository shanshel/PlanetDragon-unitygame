using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class NetworkOverwite : NetworkManager
{
    public GameObject gameNetPrefab;
    private GameObject sharedGameInfoObjectInstance;
    private bool isSpawnedPrefab = false;
    private bool isDestroyPrefab = false;




    private void Start()
    {
       
        Debug.Log("&&& Start");
        if (this.matchMaker == null)
        {
            this.StartMatchMaker();
        }

    }

   
    private void Update()
    {


        /*
         NetworkManager.singleton.migrationManager.showGUI = true;
        NetworkManager.singleton.migrationManager.hostMigration = true;
        //Debug.Log(NetworkManager.singleton.migrationManager.peers[0].port + " " + NetworkManager.singleton.migrationManager.peers[0].connectionId);
        
       
        */
        /*
        NetworkManager.singleton.migrationManager.waitingToBecomeNewHost = true;
        NetworkManager.singleton.migrationManager.peers[0].isYou;
        NetworkManager.singleton.migrationManager.FindNewHost();
        NetworkManager.singleton.migrationManager.waitingReconnectToNewHost = true;
        NetworkManager.singleton.migrationManager.BecomeNewHost(NetworkManager.singleton.migrationManager.peers[0].port);
        */
    }




    public override void OnServerConnect(NetworkConnection conn)
    {
        Debug.Log("****: OnServerConnect");
        Debug.Log("A client connected to the server: " + conn);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        Debug.Log("****: OnServerDisconnect");
        NetworkServer.DestroyPlayersForConnection(conn);
        if (conn.lastError != NetworkError.Ok)
        {
            if (LogFilter.logError) { Debug.LogError("ServerDisconnected due to error: " + conn.lastError); }
        }
        Debug.Log("A client disconnected from the server: " + conn);
    }
    public override void OnServerReady(NetworkConnection conn)
    {
        Debug.Log("****: OnServerReady");
        NetworkServer.SetClientReady(conn);
        Debug.Log("Client is set to the ready state (ready to receive state updates): " + conn);
    }
    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        Debug.Log("****: OnServerAddPlayer");
        var player = (GameObject)GameObject.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        Debug.Log("Client has requested to get his player added to the game");
    }

    public override void OnServerRemovePlayer(NetworkConnection conn, PlayerController player)
    {
        Debug.Log("****: OnServerRemovePlayer");
        if (player.gameObject != null)
            NetworkServer.Destroy(player.gameObject);
    }

    public override void OnServerError(NetworkConnection conn, int errorCode)
    {
        Debug.Log("****: OnServerError");
        Debug.Log("Server network error occurred: " + (NetworkError)errorCode);
    }

    public override void OnStartHost()
    {
        Debug.Log("****: OnStartHost");
        MultiPlayerStat.enableMultiMode();
    }

    public override void OnStartServer()
    {
        Debug.Log("****: OnStartServer");
        MultiPlayerStat.enableMultiMode();
    }

    public override void OnStopServer()
    {
        Debug.Log("****: OnStopServer");
        MultiPlayerStat.disableMultiMode();
    }

    public override void OnStopHost()
    {
        Debug.Log("****: OnStopHost");
        MultiPlayerStat.disableMultiMode();
    }

    // Client callbacks


    public override void OnClientConnect(NetworkConnection conn)
    {
        Debug.Log("****CLIENT: OnClientConnect");
        base.OnClientConnect(conn);
    }

    public override void OnDropConnection(bool success, string extendedInfo)
    {
        Debug.Log("connection Dropped");
        return;
        base.OnDropConnection(success, extendedInfo);
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        Debug.Log("****CLIENT: OnClientDisconnect");
        StopClient();
        if (conn.lastError != NetworkError.Ok)
        {
            if (LogFilter.logError) { Debug.LogError("ClientDisconnected due to error: " + conn.lastError); }
        }
        
        Debug.Log("Client disconnected from server: " + conn);
    }

    public override void OnClientError(NetworkConnection conn, int errorCode)
    {
        Debug.Log("****CLIENT: OnClientError");
        Debug.Log("Client network error occurred: " + (NetworkError)errorCode);
    }

    public override void OnClientNotReady(NetworkConnection conn)
    {
        Debug.Log("****CLIENT: OnClientNotReady");
        Debug.Log("Server has set client to be not-ready (stop getting state updates)");
    }

   
    public override void OnStartClient(NetworkClient client)
    {
        Debug.Log("****CLIENT: OnStartClient");
        LevelLoader.instance.stopFakeLoader();
        MultiPlayerStat.enableMultiMode();
        
        if (!isSpawnedPrefab)
        {
            isSpawnedPrefab = true;
            sharedGameInfoObjectInstance = GameObject.Instantiate(gameNetPrefab);
        }

        Debug.Log("Client has started");
    }

    public override void OnStopClient()
    {
        Debug.Log("****CLIENT: OnStopClient");
        Debug.Log("Client has stopped");
        MultiPlayerStat.disableMultiMode();
        if (!isDestroyPrefab)
        {
            isSpawnedPrefab = false;
            Destroy(sharedGameInfoObjectInstance);
        }
    }

    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        Debug.Log("****CLIENT: OnClientSceneChanged");
        base.OnClientSceneChanged(conn);
        Debug.Log("Server triggered scene change and we've done the same, do any extra work here for the client...");
    }


    /* Custom */


}
