using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;
using mRemoteNG.App.Runtime;
using System.Net.NetworkInformation;
using System.Net;

namespace mRemoteNG.Tools
{
	namespace PortScan
	{
		public class ScanHost
		{
			#region "Properties"
			private static int _SSHPort = mRemoteNG.Connection.Protocol.SSH1.Defaults.Port;
			public static int SSHPort {
				get { return _SSHPort; }
				set { _SSHPort = value; }
			}

			private static int _TelnetPort = mRemoteNG.Connection.Protocol.Telnet.Defaults.Port;
			public static int TelnetPort {
				get { return _TelnetPort; }
				set { _TelnetPort = value; }
			}

			private static int _HTTPPort = mRemoteNG.Connection.Protocol.HTTP.Defaults.Port;
			public static int HTTPPort {
				get { return _HTTPPort; }
				set { _HTTPPort = value; }
			}

			private static int _HTTPSPort = mRemoteNG.Connection.Protocol.HTTPS.Defaults.Port;
			public static int HTTPSPort {
				get { return _HTTPSPort; }
				set { _HTTPSPort = value; }
			}

			private static int _RloginPort = mRemoteNG.Connection.Protocol.Rlogin.Defaults.Port;
			public static int RloginPort {
				get { return _RloginPort; }
				set { _RloginPort = value; }
			}

			private static int _RDPPort = mRemoteNG.Connection.Protocol.RDP.Defaults.Port;
			public static int RDPPort {
				get { return _RDPPort; }
				set { _RDPPort = value; }
			}

			private static int _VNCPort = mRemoteNG.Connection.Protocol.VNC.Defaults.Port;
			public static int VNCPort {
				get { return _VNCPort; }
				set { _VNCPort = value; }
			}

			private string _hostName = "";
			public string HostName {
				get { return _hostName; }
				set { _hostName = value; }
			}

			public string HostNameWithoutDomain {
				get {
					if (string.IsNullOrEmpty(HostName) || HostName == HostIp)
						return HostIp;
					return HostName.Split(".")[0];
				}
			}

			private string _hostIp;
			public string HostIp {
				get { return _hostIp; }
				set { _hostIp = value; }
			}

			private ArrayList _openPorts = new ArrayList();
			public ArrayList OpenPorts {
				get { return _openPorts; }
				set { _openPorts = value; }
			}

			private ArrayList _closedPorts;
			public ArrayList ClosedPorts {
				get { return _closedPorts; }
				set { _closedPorts = value; }
			}

			private bool _RDP;
			public bool RDP {
				get { return _RDP; }
				set { _RDP = value; }
			}

			private bool _VNC;
			public bool VNC {
				get { return _VNC; }
				set { _VNC = value; }
			}

			private bool _SSH;
			public bool SSH {
				get { return _SSH; }
				set { _SSH = value; }
			}

			private bool _Telnet;
			public bool Telnet {
				get { return _Telnet; }
				set { _Telnet = value; }
			}

			private bool _Rlogin;
			public bool Rlogin {
				get { return _Rlogin; }
				set { _Rlogin = value; }
			}

			private bool _HTTP;
			public bool HTTP {
				get { return _HTTP; }
				set { _HTTP = value; }
			}

			private bool _HTTPS;
			public bool HTTPS {
				get { return _HTTPS; }
				set { _HTTPS = value; }
			}
			#endregion

			#region "Methods"
			public ScanHost(string host)
			{
				_hostIp = host;
				_openPorts = new ArrayList();
				_closedPorts = new ArrayList();
			}

			public override string ToString()
			{
				try {
					return "SSH: " + _SSH + " Telnet: " + _Telnet + " HTTP: " + _HTTP + " HTTPS: " + _HTTPS + " Rlogin: " + _Rlogin + " RDP: " + _RDP + " VNC: " + _VNC;
				} catch (Exception ex) {
					mRemoteNG.App.Runtime.MessageCollector.AddMessage(mRemoteNG.Messages.MessageClass.WarningMsg, "ToString failed (Tools.PortScan)", true);
					return "";
				}
			}

			public ListViewItem ToListViewItem(bool import)
			{
				try {
					ListViewItem listViewItem = new ListViewItem();
					listViewItem.Tag = this;
					if (!string.IsNullOrEmpty(_hostName)) {
						listViewItem.Text = _hostName;
					} else {
						listViewItem.Text = _hostIp;
					}

					if (import) {
						listViewItem.SubItems.Add(BoolToYesNo(_SSH));
						listViewItem.SubItems.Add(BoolToYesNo(_Telnet));
						listViewItem.SubItems.Add(BoolToYesNo(_HTTP));
						listViewItem.SubItems.Add(BoolToYesNo(_HTTPS));
						listViewItem.SubItems.Add(BoolToYesNo(_Rlogin));
						listViewItem.SubItems.Add(BoolToYesNo(_RDP));
						listViewItem.SubItems.Add(BoolToYesNo(_VNC));
					} else {
						string strOpen = "";
						string strClosed = "";

						foreach (int p in _openPorts) {
							strOpen += p + ", ";
						}

						foreach (int p in _closedPorts) {
							strClosed += p + ", ";
						}

						listViewItem.SubItems.Add(strOpen.Substring(0, (strOpen.Length > 0 ? strOpen.Length - 2 : strOpen.Length)));
						listViewItem.SubItems.Add(strClosed.Substring(0, (strClosed.Length > 0 ? strClosed.Length - 2 : strClosed.Length)));
					}

					return listViewItem;
				} catch (Exception ex) {
					mRemoteNG.App.Runtime.MessageCollector.AddExceptionMessage("Tools.PortScan.ToListViewItem() failed.", ex, mRemoteNG.Messages.MessageClass.WarningMsg, true);
					return null;
				}
			}

			private string BoolToYesNo(bool value)
			{
				if (value) {
					return mRemoteNG.My.Language.strYes;
				} else {
					return mRemoteNG.My.Language.strNo;
				}
			}

			public void SetAllProtocols(bool value)
			{
				_VNC = value;
				_Telnet = value;
				_SSH = value;
				_Rlogin = value;
				_RDP = value;
				_HTTPS = value;
				_HTTP = value;
			}
			#endregion
		}

		public class Scanner
		{
			#region "Private Members"
			private readonly List<IPAddress> _ipAddresses = new List<IPAddress>();

			private readonly List<int> _ports = new List<int>();
			private Thread _scanThread;
				#endregion
			private readonly List<ScanHost> _scannedHosts = new List<ScanHost>();

			#region "Public Methods"
			public Scanner(IPAddress ipAddress1, IPAddress ipAddress2)
			{
				IPAddress ipAddressStart = IpAddressMin(ipAddress1, ipAddress2);
				IPAddress ipAddressEnd = IpAddressMax(ipAddress1, ipAddress2);

				_ports.Clear();
				_ports.AddRange(new int[] {
					ScanHost.SSHPort,
					ScanHost.TelnetPort,
					ScanHost.HTTPPort,
					ScanHost.HTTPSPort,
					ScanHost.RloginPort,
					ScanHost.RDPPort,
					ScanHost.VNCPort
				});

				_ipAddresses.Clear();
				_ipAddresses.AddRange(IpAddressArrayFromRange(ipAddressStart, ipAddressEnd));

				_scannedHosts.Clear();
			}

			public Scanner(IPAddress ipAddress1, IPAddress ipAddress2, int port1, int port2) : this(ipAddress1, ipAddress2)
			{

				int portStart = Math.Min(port1, port2);
				int portEnd = Math.Max(port1, port2);

				_ports.Clear();
				for (int port = portStart; port <= portEnd; port++) {
					_ports.Add(port);
				}
			}

			public void StartScan()
			{
				_scanThread = new Thread(ScanAsync);
				_scanThread.SetApartmentState(ApartmentState.STA);
				_scanThread.IsBackground = true;
				_scanThread.Start();
			}

			public void StopScan()
			{
				_scanThread.Abort();
			}

			public static bool IsPortOpen(string hostname, string port)
			{
				try {
					// ReSharper disable UnusedVariable
					System.Net.Sockets.TcpClient tcpClient = new System.Net.Sockets.TcpClient(hostname, port);
					// ReSharper restore UnusedVariable
					return true;
				} catch (Exception ex) {
					return false;
				}
			}
			#endregion

			#region "Private Methods"
			private void ScanAsync()
			{
				try {
					int hostCount = 0;

					foreach (IPAddress ipAddress in _ipAddresses) {
						if (BeginHostScan != null) {
							BeginHostScan(ipAddress.ToString());
						}

						ScanHost scanHost = new ScanHost(ipAddress.ToString());
						hostCount += 1;

						if (!IsHostAlive(ipAddress)) {
							scanHost.ClosedPorts.AddRange(_ports);
							scanHost.SetAllProtocols(false);
						} else {
							foreach (int port in _ports) {
								bool isPortOpen = false;

								try {
									// ReSharper disable UnusedVariable
									System.Net.Sockets.TcpClient tcpClient = new System.Net.Sockets.TcpClient(ipAddress.ToString(), port);
									// ReSharper restore UnusedVariable

									isPortOpen = true;
									scanHost.OpenPorts.Add(port);
								} catch (Exception ex) {
									isPortOpen = false;
									scanHost.ClosedPorts.Add(port);
								}

								switch (port) {
									case ScanHost.SSHPort:
										scanHost.SSH = isPortOpen;
										break;
									case ScanHost.TelnetPort:
										scanHost.Telnet = isPortOpen;
										break;
									case ScanHost.HTTPPort:
										scanHost.HTTP = isPortOpen;
										break;
									case ScanHost.HTTPSPort:
										scanHost.HTTPS = isPortOpen;
										break;
									case ScanHost.RloginPort:
										scanHost.Rlogin = isPortOpen;
										break;
									case ScanHost.RDPPort:
										scanHost.RDP = isPortOpen;
										break;
									case ScanHost.VNCPort:
										scanHost.VNC = isPortOpen;
										break;
								}
							}
						}

						try {
							scanHost.HostName = Dns.GetHostEntry(scanHost.HostIp).HostName;
						} catch (Exception ex) {
						}
						if (string.IsNullOrEmpty(scanHost.HostName))
							scanHost.HostName = scanHost.HostIp;

						_scannedHosts.Add(scanHost);
						if (HostScanned != null) {
							HostScanned(scanHost, hostCount, _ipAddresses.Count);
						}
					}

					if (ScanComplete != null) {
						ScanComplete(_scannedHosts);
					}
				} catch (Exception ex) {
					mRemoteNG.App.Runtime.MessageCollector.AddMessage(mRemoteNG.Messages.MessageClass.WarningMsg, "StartScanBG failed (Tools.PortScan)" + Constants.vbNewLine + ex.Message, true);
				}
			}

			private static bool IsHostAlive(IPAddress ipAddress)
			{
				Ping pingSender = new Ping();
				PingReply pingReply = null;

				try {
					pingReply = pingSender.Send(ipAddress);

					if (pingReply.Status == IPStatus.Success) {
						return true;
					} else {
						return false;
					}
				} catch (Exception ex) {
					return false;
				}
			}

			private static IPAddress[] IpAddressArrayFromRange(IPAddress ipAddress1, IPAddress ipAddress2)
			{
				IPAddress startIpAddress = IpAddressMin(ipAddress1, ipAddress2);
				IPAddress endIpAddress = IpAddressMax(ipAddress1, ipAddress2);

				Int32 startAddress = IpAddressToInt32(startIpAddress);
				Int32 endAddress = IpAddressToInt32(endIpAddress);
				int addressCount = endAddress - startAddress;

				IPAddress[] addressArray = new IPAddress[addressCount + 1];
				int index = 0;
				for (Int32 address = startAddress; address <= endAddress; address++) {
					addressArray[index] = IpAddressFromInt32(address);
					index = index + 1;
				}

				return addressArray;
			}

			private static IPAddress IpAddressMin(IPAddress ipAddress1, IPAddress ipAddress2)
			{
				// ipAddress1 < ipAddress2
				if ((IpAddressCompare(ipAddress1, ipAddress2) < 0)) {
					return ipAddress1;
				} else {
					return ipAddress2;
				}
			}

			private static IPAddress IpAddressMax(IPAddress ipAddress1, IPAddress ipAddress2)
			{
				// ipAddress1 > ipAddress2
				if ((IpAddressCompare(ipAddress1, ipAddress2) > 0)) {
					return ipAddress1;
				} else {
					return ipAddress2;
				}
			}

			private static int IpAddressCompare(IPAddress ipAddress1, IPAddress ipAddress2)
			{
				return IpAddressToInt32(ipAddress1) - IpAddressToInt32(ipAddress2);
			}

			private static Int32 IpAddressToInt32(IPAddress ipAddress)
			{
				if (!(ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork))
					throw new ArgumentException("ipAddress");

				byte[] addressBytes = ipAddress.GetAddressBytes();
				// in network order (big-endian)
				if (BitConverter.IsLittleEndian)
					Array.Reverse(addressBytes);
				// to host order (little-endian)
				Debug.Assert(addressBytes.Length == 4);

				return BitConverter.ToInt32(addressBytes, 0);
			}

			private static IPAddress IpAddressFromInt32(Int32 ipAddress)
			{
				byte[] addressBytes = BitConverter.GetBytes(ipAddress);
				// in host order
				if (BitConverter.IsLittleEndian)
					Array.Reverse(addressBytes);
				// to network order (big-endian)
				Debug.Assert(addressBytes.Length == 4);

				return new IPAddress(addressBytes);
			}
			#endregion

			#region "Events"
			public event BeginHostScanEventHandler BeginHostScan;
			public delegate void BeginHostScanEventHandler(string host);
			public event HostScannedEventHandler HostScanned;
			public delegate void HostScannedEventHandler(ScanHost scanHost, int scannedHostCount, int totalHostCount);
			public event ScanCompleteEventHandler ScanComplete;
			public delegate void ScanCompleteEventHandler(List<ScanHost> hosts);
			#endregion
		}
	}
}
