// This work is licensed under the Creative Commons Attribution-ShareAlike 4.0 International License. 
// To view a copy of this license, visit http://creativecommons.org/licenses/by-sa/4.0/ 
// or send a letter to Creative Commons, PO Box 1866, Mountain View, CA 94042, USA.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class socket_client : MonoBehaviour {  	
	#region private members 	
	private TcpClient socketConnection; 	
	private Thread clientReceiveThread; 	
    private int data_type;
	#endregion  	
	// Use this for initialization 	
	void Start () {
		ConnectToTcpServer();    
        data_type=0; 
	}  	
	// Update is called once per frame
	void Update () {         
		if (Input.GetKeyDown(KeyCode.Space)) {             
			SendMessage();         
		}     
	}  	
	/// Setup socket connection. 	

	private void ConnectToTcpServer () { 		
		try {  			
			clientReceiveThread = new Thread (new ThreadStart(ListenForData)); 			
			clientReceiveThread.IsBackground = true; 			
			clientReceiveThread.Start();  		
		} 		
		catch (Exception e) { 			
			Debug.Log("On client connect exception " + e); 		
		} 	
	}  	
  
	private void ListenForData() { 		
		try { 			
			socketConnection = new TcpClient("192.168.0.42", 9051);  			
			Byte[] bytes = new Byte[1024];             
			while (true) { 				
				// Get a stream object for reading 				
				using (NetworkStream stream = socketConnection.GetStream()) { 					
					int length; 					
					// Read incomming stream into byte arrary. 					
					while ((length = stream.Read(bytes, 0, 2)) != 0) { 	
						length = Int32.Parse(Encoding.ASCII.GetString(bytes));
						int lengthData = stream.Read(bytes, 0, length);	
						var incommingData = new byte[lengthData]; 						
						Array.Copy(bytes, 0, incommingData, 0, lengthData); 						
						// Convert byte array to string message. 						

						// Debug.Log("server message received as: " + serverMessage); 	
                        if(data_type==0){

                            string translation = Encoding.ASCII.GetString(incommingData); 
                            Debug.Log("Translation: " + translation); 	
                            data_type=1;
							Array.Clear(bytes, 0, bytes.Length);

                        }else{
                            string rotation = Encoding.ASCII.GetString(incommingData); 
                            Debug.Log("Rotation: " + rotation); 	
                            data_type=0;
							Array.Clear(bytes, 0, bytes.Length);
                        }			
					} 				
				} 			
			}         
		}         
		catch (SocketException socketException) {             
			Debug.Log("Socket exception: " + socketException);         
		}     
	}  	
	/// Send message to server using socket connection. 	

	private void SendMessage() {         
		if (socketConnection == null) {             
			return;         
		}  		
		try { 			
			// Get a stream object for writing. 			
			NetworkStream stream = socketConnection.GetStream(); 			
			if (stream.CanWrite) {                 
				string clientMessage = "This is a message from one of your clients."; 				
				// Convert string message to byte array.                 
				byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(clientMessage); 				
				// Write byte array to socketConnection stream.                 
				stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);                 
				Debug.Log("Client sent his message - should be received by server");             
			}         
		} 		
		catch (SocketException socketException) {             
			Debug.Log("Socket exception: " + socketException);         
		}     
	} 
}














// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System.Text;

// using System.Net.Sockets;

// using System.IO;


// using System;
// using System.Collections;
// using System.Threading;
// using UnityEngine;

// public class socket_client : MonoBehaviour
// {
//     // Start is called before the first frame update    
//     private BinaryReader le;
//     private TcpClient cliente;
//     private NetworkStream sockStream;
//     public TextMesh verification;
//     string serverMessage;

//     private Byte[] bytes;
//     void Start()
//     {
//         cliente = new TcpClient();

//         cliente.Connect( "192.168.0.214", 801 );
//         sockStream = cliente.GetStream();
//    		bytes = new Byte[1024];             

//         StartCoroutine(getData());

     

        
//     }

//     // Update is called once per frame
//     void Update()
//     {

//         // le = new BinaryReader( sockStream );
//         // print(le);
//         getData();
     
    
//         // verification.text="foiii";

        
//     }

//     IEnumerator  getData(){

//         using (NetworkStream stream = cliente.GetStream()) { 					
// 					int length; 					
// 					// Read incomming stream into byte arrary. 					
// 					while ((length = stream.Read(bytes, 0, bytes.Length)) > 0) { 						
// 						var incommingData = new byte[length]; 						
// 						Array.Copy(bytes, 0, incommingData, 0, length); 						
// 						// Convert byte array to string message. 						
// 						serverMessage = Encoding.ASCII.GetString(incommingData); 
//                         // verification.text=serverMessage;	
//                         // Debug.Log("server message received as: " + serverMessage); 
//                         // yield return new WaitForSecondsRealtime(1);		
// 					} 				
// 					Debug.Log("server message received as: " +serverMessage ); 					
//                     yield return new WaitForSecondsRealtime(10);
// 		}
 		

//     }
// }
