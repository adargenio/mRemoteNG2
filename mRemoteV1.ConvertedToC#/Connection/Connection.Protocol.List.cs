using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
namespace mRemoteNG.Connection
{
	namespace Protocol
	{
		public class List : CollectionBase
		{

			#region "Public Properties"
			public Connection.Protocol.Base this[object Index] {
				get {
					if (Index is Connection.Protocol.Base) {
						return Index;
					} else {
						return (Connection.Protocol.Base)List[Index];
					}
				}
			}

			public new int Count {
				get { return List.Count; }
			}
			#endregion

			#region "Public Methods"
			public Connection.Protocol.Base Add(Connection.Protocol.Base cProt)
			{
				this.List.Add(cProt);
				return cProt;
			}

			public void AddRange(Connection.Protocol.Base[] cProt)
			{
				foreach (Connection.Protocol.Base cP in cProt) {
					List.Add(cP);
				}
			}

			public void Remove(Connection.Protocol.Base cProt)
			{
				try {
					this.List.Remove(cProt);
				} catch (Exception ex) {
				}
			}

			public new void Clear()
			{
				this.List.Clear();
			}
			#endregion
		}
	}
}
