from __future__ import print_function
from vicon_dssdk import ViconDataStream
import socket

client = ViconDataStream.RetimingClient()
host="192.168.0.42" 
port= 9051

try:
    client.Connect( "192.168.0.214:801" )

    # Check the version
    print( 'Version', client.GetVersion() )

    client.SetAxisMapping( ViconDataStream.Client.AxisMapping.EForward, ViconDataStream.Client.AxisMapping.ELeft, ViconDataStream.Client.AxisMapping.EUp )
    xAxis, yAxis, zAxis = client.GetAxisMapping()
    print( 'X Axis', xAxis, 'Y Axis', yAxis, 'Z Axis', zAxis )

    #client.SetMaximumPrediction( 10 )
    print( 'Maximum Prediction', client.MaximumPrediction() )

    #debug_log = 'e:\\tmp\\debug_log.txt'
    #output_log = 'e:\\tmp\\output_log.txt'
    #client_log = 'e:\\tmp\\client_log.txt'
    #stream_log = 'e:\\tmp\\stream_log.txt'

    #client.SetDebugLogFile( debug_log )
    #client.SetOutputFile( output_log )
    #client.SetTimingLog( client_log, stream_log )

    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as soc:
        soc.bind((host,port))
        soc.listen()
        clientSoc, address = soc.accept()
        print("Connection found at IP ", address)
        with clientSoc:

            while( True ):
                try:
                    client.UpdateFrame()

                    subjectNames = client.GetSubjectNames()
                    for subjectName in subjectNames:
                        print( subjectName )
                        segmentNames = client.GetSegmentNames( subjectName )
                        for segmentName in segmentNames:
                            segmentChildren = client.GetSegmentChildren( subjectName, segmentName )
                            for child in segmentChildren:
                                try:
                                    print( child, 'has parent', client.GetSegmentParentName( subjectName, segmentName ) )
                                except ViconDataStream.DataStreamException as e:
                                    print( 'Error getting parent segment', e )
                    
                            try:
                                print( segmentName, 'has static scale', client.GetSegmentStaticScale( subjectName, segmentName )) 
                            except ViconDataStream.DataStreamException as e:
                                print( 'Scale Error', e )    
                                print( segmentName, 'has global translation', client.GetSegmentGlobalTranslation( subjectName, segmentName ) )                       

                            subject_t, segment_t = client.GetSegmentLocalTranslation( subjectName, segmentName)
                            len_t=str(len(str(subject_t)))
                            len_name = len(str(segmentName))
                            print("oii")
                            print(subject_t)
                            
                            clientSoc.send(str(len_name).encode())
                            clientSoc.send(str(segmentName).encode())

                            clientSoc.send(len_t.encode())
                            clientSoc.send(str(subject_t).encode())
                                                    
                            subject_r, segment_r = client.GetSegmentLocalRotationQuaternion( subjectName, segmentName )     
                            # subject_r, segment_r = client.GetSegmentLocalRotationEulerXYZ( subjectName, segmentName )                         
                            # print("aaa",subject_r)
                            # print( segmentName, 'has local rotation( Quaternion )', len(str(subject_r) )) 
                            len_r=str(len(str(subject_r)))
    
                            clientSoc.send(len_r.encode())
                            clientSoc.send(str(subject_r).encode())
                            
                            # print( segmentName, 'has local translation',str(subject_r), str(segment_r))
                           

                except ViconDataStream.DataStreamException as e:
                    print( 'Handled data stream error', e )
 
except ViconDataStream.DataStreamException as e:
    print( 'Handled data stream error', e )
