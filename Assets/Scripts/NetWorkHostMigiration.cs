using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class NetWorkHostMigiration : NetworkMigrationManager
{
    public SceneChangeOption _sceneChange;


   
    private void Start()
    {
        _sceneChange = SceneChangeOption.StayInOnlineScene;
    }

    protected override void OnClientDisconnectedFromHost(NetworkConnection conn, out SceneChangeOption sceneChange)
    {
        Debug.Log("###### OnClientDisconnectedFromHost");
        base.OnClientDisconnectedFromHost(conn, out sceneChange);
    }

    // called on host after the host is lost. host MUST change Scenes
    protected override void OnServerHostShutdown()
    {
        Debug.Log("###### OnServerHostShutdown");
        base.OnServerHostShutdown();
    }

    // called on new host (server) when a client from the old host re-connects a player
    protected override void OnServerReconnectPlayer(
    NetworkConnection newConnection,
    GameObject oldPlayer,
    int oldConnectionId,
    short playerControllerId)
    {
        Debug.Log("###### OnServerReconnectPlayer");
        base.OnServerReconnectPlayer(newConnection, oldPlayer, oldConnectionId, playerControllerId);

    }

    // called on new host (server) when a client from the old host re-connects a player
    protected override void OnServerReconnectPlayer(
    NetworkConnection newConnection,
    GameObject oldPlayer,
    int oldConnectionId,
    short playerControllerId,
    NetworkReader extraMessageReader)
    {
        Debug.Log("###### OnServerReconnectPlayer");
        base.OnServerReconnectPlayer(newConnection, oldPlayer, oldConnectionId, playerControllerId, extraMessageReader);
    }

// called on new host (server) when a client from the old host re-connects a non-player GameObject
    protected override void OnServerReconnectObject(
    NetworkConnection newConnection,
    GameObject oldObject,
    int oldConnectionId)
    {
        Debug.Log("###### OnServerReconnectObject");
        base.OnServerReconnectObject(newConnection, oldObject, oldConnectionId);
    }

// called on both host and client when the set of peers is updated
    protected override void OnPeersUpdated(
    PeerListMessage peers)
    {
        Debug.Log("###### OnPeersUpdated");
        base.OnPeersUpdated(peers);
    }

// utility function called by the default UI on client after connection to host was lost, to pick a new host.

    public override bool FindNewHost(out PeerInfoMessage newHostInfo, out bool youAreNewHost)
    {
        Debug.Log("###### FindNewHost");
        return base.FindNewHost(out newHostInfo, out youAreNewHost);
    }


// called when the authority of a non-player GameObject changes
    protected override void OnAuthorityUpdated(
    GameObject go,
    int connectionId,
    bool authorityState)
    {
        Debug.Log("###### OnAuthorityUpdated");
        base.OnAuthorityUpdated(go, connectionId, authorityState);
    }
}
