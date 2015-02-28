using FastDBEngineProfilerLib;
using FastDBEngineSQLProfiler;
using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;
internal static class Programe
{
	[STAThread]
	private static void Main()
	{
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(false);
		try
		{
			Programe.RegisterRemoting();
		}
		catch (Exception ex)
		{
            MessageBox.Show(ex.Message, "FastDBEngineProfiler", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
        Application.Run(new SQLProfilerFormFix());
	}
	private static void RegisterRemoting()
	{
		Hashtable hashtable = new Hashtable();
		hashtable["name"] = "ipc";
		hashtable["priority"] = "20";
		hashtable["portName"] = MyConst.ChannelPortName;
		CommonSecurityDescriptor commonSecurityDescriptor = new CommonSecurityDescriptor(false, false, string.Empty);
		SecurityIdentifier sid = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
		commonSecurityDescriptor.DiscretionaryAcl.AddAccess(AccessControlType.Allow, sid, -1, InheritanceFlags.None, PropagationFlags.None);
		IpcServerChannel chnl = new IpcServerChannel(hashtable, null, commonSecurityDescriptor);
		ChannelServices.RegisterChannel(chnl, false);
		RemotingConfiguration.RegisterWellKnownServiceType(typeof(ProfilerService), MyConst.ObjectURI, WellKnownObjectMode.Singleton);
	}
}
