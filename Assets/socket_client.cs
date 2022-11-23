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
using UnityEngine.XR;
using Microsoft.MixedReality.Toolkit;

public class socket_client : MonoBehaviour {  	
	#region private members 	
	private TcpClient socketConnection; 	
	private Thread clientReceiveThread; 	
	Task m_Task;
    private int data_type;
	public string verText;
	private string piText;

	public TextMesh verificationText;
	public TextMesh pivotText;

	public GameObject origin;
	private bool originSet = false;

	Vector3 offset_vec;
	Vector3 temp_vec;
	// oculos
	private float x_trans;
	private float y_trans;
	private float z_trans;
	private float x_rot;
	private float y_rot;
	private float z_rot;
	private float a_rot;
	private float teste;
	public CenterBounds script;
	private float offset_x;
	private float offset_y;
	private float offset_z;

	// eugenia
	private float x_trans_eug;
	private float y_trans_eug;
	private float z_trans_eug;
	private float x_rot_eug;
	private float y_rot_eug;
	private float z_rot_eug;
	private float a_rot_eug;

	private string nome;
	// Transform Root;
	public GameObject unidos;
	public GameObject cam;
	public GameObject Pivot;
	public Camera camera;
	private bool offset=false;
	#endregion  	
	// Use this for initialization 	
	void Start () {
		// XRDevice.DisableAutoXRCameraTracking(camera,true);
		// var observer = CoreServices.GetSpatialAwarenessSystemDataProvider<IMixedRealitySpatialAwarenessMeshObserver>();
		// observer.DisplayOption = SpatialAwarenessMeshDisplayOptions.None;
		verText = "not yet";
		piText=" pera ai";
		nome = "";
		script= GameObject.Find("cranio").GetComponent<CenterBounds>();

		ConnectToTcpServer();    
        data_type=2; 

	}  	
	// Update is called once per frame
	void Update () {         
		// Root = transform;
		piText= " pos x: " + Pivot.transform.position.x.ToString() + " pos y: " +  Pivot.transform.position.y.ToString()+ " pos z: " + Pivot.transform.position.z.ToString() ;
		pivotText.text= piText;
		verificationText.text = verText;
		Debug.Log("nome para transform " + nome); 
		if(nome == "oculos_obj"){
			if(!offset){
				print("oculos"+z_trans+ ",   " + y_trans + ",   "+ z_trans);
				print("eug"+z_trans_eug);
				offset=true;
				// teste = z_trans;
				// offset_vec.x = -y_trans * 0.001f;
				// offset_vec.y =  z_trans * 0.001f;
				// offset_vec.z =  x_trans * 0.001f;
				offset_x= x_trans;
				offset_y=y_trans;
				offset_z=z_trans;
				// temp_vec.x = y_trans_eug * 0.001f;
				// temp_vec.y = z_trans_eug * 0.001f;
				// temp_vec.z = x_trans_eug * 0.001f;
				
				// unidos.transform.localPosition = new Vector3(-(float)(y_trans_eug) * 0.001f, (float)(z_trans_eug)* 0.001f, (float)(x_trans_eug) * 0.001f);
				// unidos.transform.localPosition = new Vector3(-(float)(y_trans_eug - y_trans) * 0.001f, (float)(z_trans_eug - z_trans)* 0.001f, (float)(x_trans_eug-x_trans) * 0.001f);


			}
			// if(!originSet){
			// 	origin.transform.localPosition = new Vector3(0,0,0) - new Vector3(-(float)y_trans * 0.001f, (float)z_trans * 0.001f, (float)x_trans * 0.001f);
			// 	originSet = true;
			// }
			camera.transform.localPosition = new Vector3(-(float)y_trans * 0.001f, (float)z_trans * 0.001f, (float)x_trans * 0.001f);
			camera.transform.localRotation = new Quaternion((float)y_rot, -(float)z_rot, -(float)x_rot, (float)a_rot);

		}
		else if (script.ready){
			
			unidos.transform.localPosition = new Vector3(-(float)(y_trans_eug ) * 0.001f, (float)((z_trans_eug)* 0.001f ), (float)(x_trans_eug) * 0.001f) ;
			// offset_vec = new Vector3((-y_trans)-(-y_trans_eug), z_trans-z_trans_eug, x_trans-x_trans_eug );
			// unidos.transform.localPosition = new Vector3(camera.transform.position.x - (offset_vec.x)* 0.001f, camera.transform.position.y - (offset_vec.y)* 0.001f + 0.12f, camera.transform.position.z - (offset_vec.z)* 0.001f);
			
			
			
			// camera.transform.position - (offset_vec* 0.001f);
			print(unidos.transform.localPosition.y);

			unidos.transform.localRotation =  new Quaternion((float)y_rot_eug, -(float)z_rot_eug, -(float)x_rot_eug, (float)a_rot_eug);
			// unidos.transform.localRotation = Quaternion.AngleAxis(-180, Vector3.right);
			
		}
		

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
						if(data_type==2){

                            nome = Encoding.ASCII.GetString(incommingData); 
							// Debug.Log("nome : " + nome);

                            data_type=1;
							Array.Clear(bytes, 0, bytes.Length);

                        }
                        else if(data_type==1){

                            string translation = Encoding.ASCII.GetString(incommingData); 
							translation=translation.Remove(0, 1);
							translation=translation.Remove((translation.Length-1), 1);
							string[] pos_trans;
							pos_trans=translation.Split(',');

							if(nome == "oculos_obj"){
								x_trans = (float)Math.Round(float.Parse(pos_trans[0], CultureInfo.InvariantCulture.NumberFormat));
								y_trans= (float)Math.Round(float.Parse(pos_trans[1], CultureInfo.InvariantCulture.NumberFormat));
								z_trans = (float)Math.Round(float.Parse(pos_trans[2], CultureInfo.InvariantCulture.NumberFormat));
							}else{
								x_trans_eug = (float)Math.Round(float.Parse(pos_trans[0], CultureInfo.InvariantCulture.NumberFormat));
								y_trans_eug = (float)Math.Round(float.Parse(pos_trans[1], CultureInfo.InvariantCulture.NumberFormat));
								z_trans_eug = (float)Math.Round(float.Parse(pos_trans[2], CultureInfo.InvariantCulture.NumberFormat));
								x_trans_eug-=offset_x - 35.0f;  // quanto menor mais pra direita
								y_trans_eug-=offset_y + 20.0f; // quanto maior mais pra perto dentro
								z_trans_eug-=offset_z-175.0f;  // n sei

								// offset_x = x_trans

							}
							// Vector3 cam_pos = new Vector3(-y_trans, z_trans, x_trans);
							// Vector3 eug_pos = new Vector3(-y_trans_eug, z_trans_eug, x_trans_eug);

							// offset_vec = new Vector3((-y_trans)-(-y_trans_eug), z_trans-z_trans_eug, x_trans-x_trans_eug );

                            // Debug.Log("Translation x : " + x_trans);
                            // Debug.Log("Translation y : " + y_trans); 	
                            Debug.Log("Translation z : " + z_trans_eug); 	

                            data_type=0;
							Array.Clear(bytes, 0, bytes.Length);

                        }else{
                            string rotation = Encoding.ASCII.GetString(incommingData); 
							rotation=rotation.Remove(0, 1);
							rotation=rotation.Remove((rotation.Length-1), 1);
							//  Debug.Log("Rotation x : " + rotation);
							string[] pos_rot;

							pos_rot=rotation.Split(','); 
							if(nome == "oculos_obj"){
								x_rot = float.Parse(pos_rot[0], CultureInfo.InvariantCulture.NumberFormat);
								y_rot= float.Parse(pos_rot[1], CultureInfo.InvariantCulture.NumberFormat);
								z_rot = float.Parse(pos_rot[2], CultureInfo.InvariantCulture.NumberFormat);
								a_rot = float.Parse(pos_rot[3], CultureInfo.InvariantCulture.NumberFormat);
							}else{
								x_rot_eug = float.Parse(pos_rot[0], CultureInfo.InvariantCulture.NumberFormat);
								y_rot_eug= float.Parse(pos_rot[1], CultureInfo.InvariantCulture.NumberFormat);
								z_rot_eug = float.Parse(pos_rot[2], CultureInfo.InvariantCulture.NumberFormat);
								a_rot_eug = float.Parse(pos_rot[3], CultureInfo.InvariantCulture.NumberFormat);
							}

                            // Debug.Log("Rotation x : " + x_rot);
                            // Debug.Log("Rotation y : " + y_rot); 	
                            // Debug.Log("Rotation z : " + z_rot); 	
 							// Debug.Log("Rotation a : " + a_rot); 	
                          	
                            data_type=2;
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
