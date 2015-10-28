using UnityEngine;
using System.Collections;
using VoxelBusters.NativePlugins;
using VoxelBusters.Utility;

namespace VoxelBusters.NativePlugins.Demo
{
	public class SharePlugin : MonoBehaviour {
	[SerializeField, Header("Share Properties ")]
	private 	string			m_shareMessage		= "share message";
	[SerializeField, Header("Share Sheet Properties")]
	private 	eShareOptions[]	m_excludedOptions	= new eShareOptions[0];

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	private void FinishedSharing (eShareResult _result)
	{
			Debug.Log ("Finish sharing");
		//AddNewResult("Finished sharing");
		//AppendResult("Share Result = " + _result);
	}

	public void ShareScreenShotUsingShareSheet ()
	{
		// Create share sheet
		ShareSheet _shareSheet 	= new ShareSheet();	
		_shareSheet.Text		= m_shareMessage;
		_shareSheet.ExcludedShareOptions	= m_excludedOptions;
		_shareSheet.AttachScreenShot();
		
		// Show composer
		NPBinding.UI.SetPopoverPointAtLastTouchPosition();
		NPBinding.Sharing.ShowView(_shareSheet, FinishedSharing);
	}
}
}
