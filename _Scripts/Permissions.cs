using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Permissions : MonoBehaviour
{
	// Start is called before the first frame update
	public Pedometer p;
	public async void RequestPermission()
	{
		AndroidRuntimePermissions.Permission result = await AndroidRuntimePermissions.RequestPermissionAsync("android.permission.ACTIVITY_RECOGNITION");
		//AndroidRuntimePermissions.Permission result = AndroidRuntimePermissions.RequestPermission( "android.permission.ACCESS_FINE_LOCATION" ); // Synchronous version (not recommended)
		if (result == AndroidRuntimePermissions.Permission.Granted)
		{
			Debug.Log("We have permission to access fine location!");
			p.enabled = true;
		}

		else
			Debug.Log("Permission state: " + result);

		// Requesting ACCESS_FINE_LOCATION and CAMERA permissions simultaneously
		//AndroidRuntimePermissions.Permission[] result = await AndroidRuntimePermissions.RequestPermissionsAsync( "android.permission.ACCESS_FINE_LOCATION", "android.permission.CAMERA" );
		//if( result[0] == AndroidRuntimePermissions.Permission.Granted && result[1] == AndroidRuntimePermissions.Permission.Granted )
		//	Debug.Log( "We have all the permissions!" );
		//else
		//	Debug.Log( "Some permission(s) are not granted..." );
	}
}
