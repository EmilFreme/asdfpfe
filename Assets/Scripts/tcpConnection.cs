// using UnityEngine;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.IO;

// #if !UNITY_EDITOR
//     using Windows.Networking;
//     using Windows.Networking.Sockets;
//     using Windows.Storage.Streams;
// #endif

// public class tcpConnection : MonoBehaviour
// {
    
// #if !UNITY_EDITOR
//         StreamSocket socket;
//         StreamSocketListener listener;
//         String port;
//         String message;
// #endif

//     // Use this for initialization
//     void Start()
//     {
// #if !UNITY_EDITOR
//         listener = new StreamSocketListener();
//         port = "801";
//         listener.ConnectionReceived += Listener_ConnectionReceived;
//         listener.Control.KeepAlive = false;

//         Listener_Start();
// #endif
//     }

// #if !UNITY_EDITOR
//     private async void Listener_Start()
//     {
//         Debug.Log("Listener started");
//         try
//         {
//             await listener.BindServiceNameAsync(port);
//         }
//         catch (Exception e)
//         {
//             Debug.Log("Error: " + e.Message);
//         }

//         Debug.Log("Listening");
//     }

//     private async void Listener_ConnectionReceived(StreamSocketListener sender, StreamSocketListenerConnectionReceivedEventArgs args)
//     {
//         Debug.Log("Connection received");

//         try
//         {
//             while (true) {

//                     using (var dw = new DataWriter(args.Socket.OutputStream))
//                     {
//                         dw.WriteString("Hello There");
//                         await dw.StoreAsync();
//                         dw.DetachStream();
//                     }  

//                     using (var dr = new DataReader(args.Socket.InputStream))
//                     {
//                         dr.InputStreamOptions = InputStreamOptions.Partial;
//                         await dr.LoadAsync(12);
//                         var input = dr.ReadString(12);
//                         Debug.Log("received: " + input);
//                         _input = input;

//                     }
//             }
//         }
//         catch (Exception e)
//         {
//             Debug.Log("disconnected!!!!!!!! " + e);
//         }

//     }

// #endif

//     void Update()
//     {
//         this.GetComponent<TextMesh>().text = _input;
//     }
// }
