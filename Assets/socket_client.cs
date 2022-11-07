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
using System.Globalization;
using System.Threading.Tasks;

public class socket_client : MonoBehaviour {  	
	#region private members 	
	private TcpClient socketConnection; 	
	private Thread clientReceiveThread; 	
	Task m_Task;
    private int data_type;
	public string verText;
	public TextMesh verificationText;
	Transform Root;
	#endregion  	
	// Use this for initialization 	
	void Start () {
		verText = "not yet";
		ConnectToTcpServer();    
        data_type=1; 
	}  	
	// Update is called once per frame
	void Update () {         
		Root = transform;
		verificationText.text = verText;
		if (Input.GetKeyDown(KeyCode.Space)) {             
			SendMessage();         
		}     
	}  	
	/// Setup socket connection. 	

	private void ConnectToTcpServer () { 		
		try {  			
			// ListenForData();
			// m_Task = Task.Run((Action) ListenForData);
			// m_Task = new Task(() => ListenForData());
			// m_Task.Start();

			clientReceiveThread = new Thread (ListenForData); 			
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
			while (true && socketConnection.Connected) { 				
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
						verText="foiii";
						// Debug.Log("server message received as: " + serverMessage); 	
                        if(data_type==1){

                            string translation = Encoding.ASCII.GetString(incommingData); 
							translation=translation.Remove(0, 1);
							translation=translation.Remove((translation.Length-1), 1);
							string[] pos_trans;
							pos_trans=translation.Split(',');
							float x_trans = float.Parse(pos_trans[0], CultureInfo.InvariantCulture.NumberFormat);
							float y_trans= float.Parse(pos_trans[1], CultureInfo.InvariantCulture.NumberFormat);
							float z_trans = float.Parse(pos_trans[2], CultureInfo.InvariantCulture.NumberFormat);
							// Root.localPosition = new Vector3(-(float)y_trans * 0.001f, (float)z_trans * 0.001f, (float)x_trans * 0.001f);
                            Debug.Log("Translation x : " + x_trans);
                            Debug.Log("Translation y : " + y_trans); 	
                            Debug.Log("Translation z : " + z_trans); 	

                            data_type=0;
							Array.Clear(bytes, 0, bytes.Length);

                        }else{
                            string rotation = Encoding.ASCII.GetString(incommingData); 
							rotation=rotation.Remove(0, 1);
							rotation=rotation.Remove((rotation.Length-1), 1);
							//  Debug.Log("Rotation x : " + rotation);
							string[] pos_rot;
							pos_rot=rotation.Split(','); 
							// float x_rot = float.Parse(pos_rot[0], CultureInfo.InvariantCulture.NumberFormat);
							// float y_rot= float.Parse(pos_rot[1], CultureInfo.InvariantCulture.NumberFormat);
							// float z_rot = float.Parse(pos_rot[2], CultureInfo.InvariantCulture.NumberFormat);
							// float a_rot = float.Parse(pos_rot[3], CultureInfo.InvariantCulture.NumberFormat);
							// // Root.localRotation = new Quaternion((float)y_rot, -(float)z_rot, -(float)x_rot, (float)a_rot);
                            // Debug.Log("Rotation x : " + x_rot);
                            // Debug.Log("Rotation y : " + y_rot); 	
                            // Debug.Log("Rotation z : " + z_rot); 	
 							// Debug.Log("Rotation a : " + a_rot); 	
                          	
                            data_type=1;
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