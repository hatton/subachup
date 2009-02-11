// --------------------------------------------------------------------------------------------
#region // Copyright (c) 2003, SIL International. All Rights Reserved.   
// <copyright from='2003' to='2003' company='SIL International'>
//		Copyright (c) 2003, SIL International. All Rights Reserved.   
//    
//		Distributable under the terms of either the Common Public License or the
//		GNU Lesser General Public License, as specified in the LICENSING.txt file.
// </copyright> 
#endregion
// 
// File: PropertyTable.cs
// Authorship History: John Hatton  
// Last reviewed: 
// 
// <remarks>
// </remarks>
// --------------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Diagnostics;
using System.Xml.Serialization; 
using System.Runtime.Serialization;
using System.IO;
using System.Threading;		// for Monitor (dlh)

namespace Subachup
{
	/// <summary>
	/// Summary description for PropertyTable.
	/// </summary>
	[Serializable] 
	public class PropertyTable : IDisposable
	{
		protected Hashtable m_properties;

		/// <summary>
		/// Control how much output we send to the application's listeners (e.g. visual studio output window)
		/// </summary>
		protected TraceSwitch m_traceSwitch = new TraceSwitch("PropertyTable", "");

		public static PropertyTable _singleton;

		static public PropertyTable Singleton
		{
			get
			{
				if(_singleton == null)
					_singleton = new PropertyTable();
				return _singleton;
			}

		}
	
		/// -----------------------------------------------------------------------------------
		/// <summary>
		/// Initializes a new instance of the <see cref="PropertySet"/> class.
		/// </summary>
		/// -----------------------------------------------------------------------------------
		public PropertyTable()
		{
			_singleton = this;

			m_properties = new Hashtable(100);
			//			m_properties = Hashtable.Synchronized(new Hashtable(100));	// quick way to make hash threadsafe

		}

		#region IDisposable & Co. implementation
		// Region last reviewed: never

		/// <summary>
		/// True, if the object has been disposed.
		/// </summary>
		private bool m_isDisposed = false;

		/// <summary>
		/// See if the object has been disposed.
		/// </summary>
		public bool IsDisposed
		{
			get { return m_isDisposed; }
		}

		/// <summary>
		/// Finalizer, in case client doesn't dispose it.
		/// Force Dispose(false) if not already called (i.e. m_isDisposed is true)
		/// </summary>
		/// <remarks>
		/// In case some clients forget to dispose it directly.
		/// </remarks>
		~PropertyTable()
		{
			Dispose(false);
			// The base class finalizer is called automatically.
		}

		/// <summary>
		/// 
		/// </summary>
		/// <remarks>Must not be virtual.</remarks>
		public void Dispose()
		{
			Dispose(true);
			// This object will be cleaned up by the Dispose method.
			// Therefore, you should call GC.SupressFinalize to
			// take this object off the finalization queue 
			// and prevent finalization code for this object
			// from executing a second time.
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// Executes in two distinct scenarios.
		/// 
		/// 1. If disposing is true, the method has been called directly
		/// or indirectly by a user's code via the Dispose method.
		/// Both managed and unmanaged resources can be disposed.
		/// 
		/// 2. If disposing is false, the method has been called by the 
		/// runtime from inside the finalizer and you should not reference (access) 
		/// other managed objects, as they already have been garbage collected.
		/// Only unmanaged resources can be disposed.
		/// </summary>
		/// <param name="disposing"></param>
		/// <remarks>
		/// If any exceptions are thrown, that is fine.
		/// If the method is being done in a finalizer, it will be ignored.
		/// If it is thrown by client code calling Dispose,
		/// it needs to be handled by fixing the bug.
		/// 
		/// If subclasses override this method, they should call the base implementation.
		/// </remarks>
		protected virtual void Dispose(bool disposing)
		{
			Debug.WriteLineIf(!disposing, "****************** " + GetType().Name + " 'disposing' is false. ******************");
			// Must not be run more than once.
			if (m_isDisposed)
				return;

			if (disposing)
			{
				// Dispose managed resources here.
				foreach(DictionaryEntry entry in m_properties)
				{
					Property property = (Property)entry.Value;
					if (property.doDispose)
						((IDisposable)property.value).Dispose();
					property.name = null;
					property.value = null;
				}
				m_properties.Clear();
			}

			// Dispose unmanaged resources here, whether disposing is true or false.
			m_properties = null;
			m_traceSwitch = null;

			m_isDisposed = true;
		}

		#endregion IDisposable & Co. implementation

		protected void BroadcastPropertyChange(string name)
		{
		}

		public string GetPropertiesDumpString ()
		{
			string output= "";
			lock( m_properties.SyncRoot)	// lock it during the enumeration
			{
				foreach(DictionaryEntry entry in m_properties)
				{
					Property property = (Property)entry.Value;
					output += entry.Value.ToString() + "\r\n";
				}
			}
			return output;
		}
		

		#region getting and setting
		/// <summary>
		/// get the value of a property
		/// </summary>
		/// <param name="name"></param>
		/// <returns>returns null if the property is not found</returns>
		public object GetValue(string name)
		{
			if(!Monitor.TryEnter(m_properties))
			{
//				SIL.FieldWorks.Common.Utils.MiscUtils.ErrorBeep();
//				TraceVerboseLine(">>>>>>>*****  colision: <A>  ********<<<<<<<<<<<");
//				Monitor.Enter(m_properties);
			}
			Property property = (Property )m_properties[name];
			Monitor.Exit(m_properties);
			if (property == null)
				return null;
			else
				return property.value;
		}
		/// <summary>
		/// get the value of a property
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public object GetValue(string name, object defaultValue)
		{
			if(!Monitor.TryEnter(m_properties))
			{
				TraceVerboseLine(">>>>>>>*****  colision: <b>  ********<<<<<<<<<<<");
				Monitor.Enter(m_properties);
			}
			Property property = (Property )m_properties[name];
			Monitor.Exit(m_properties);

			if(property==null)
			{
				SetProperty(name, defaultValue);
				return defaultValue;
			}
			else
				return property.value;
		}

		/// <summary>
		/// set a default; does nothing if this value is already in the PropertyTable.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="defaultValue"></param>
		/// <param name="doBroadcastChange">>if true, will fire in the OnPropertyChanged() methods of all colleagues</param>
		public void SetDefault(string name, object defaultValue, bool doBroadcastChange)
		{
			if(!Monitor.TryEnter(m_properties))
			{
				TraceVerboseLine(">>>>>>>*****  colision: <c>  ********<<<<<<<<<<<");
				Monitor.Enter(m_properties);
			}
			if(m_properties[name]==null)
			{
				SetProperty(name, defaultValue,doBroadcastChange);
			}
			Monitor.Exit(m_properties);
		}

		/// <summary>
		/// set the value and broadcast the change if so instructed
		/// </summary>
		/// <param name="name"></param>
		/// <param name="newValue"></param>
		/// <param name="doBroadcastChange">if true & the property is actually different,
		/// will fire the OnPropertyChanged() methods of all colleagues</param>
		public void SetProperty(string name, object newValue, bool doBroadcastChange)
		{
			bool didChange = true;
			if(!Monitor.TryEnter(m_properties))
			{
				TraceVerboseLine(">>>>>>>*****  colision: <d>  ********<<<<<<<<<<<");
				Monitor.Enter(m_properties);
			}
			Property property = (Property)m_properties[name];
			if (property == null)
			{
				didChange = true;
				property = new Property(name, newValue);
				m_properties[name] = property;
			}
			else
			{
				if (property.value != null)
				{
					didChange = !property.value.Equals(newValue);
					if (didChange && property.doDispose)
						(property.value as IDisposable).Dispose(); // Get rid of the old value.
				}
				property.value = newValue;
			}
			Monitor.Exit(m_properties);

#if SHOWTRACE
			if (newValue != null)
			{
				TraceVerboseLine("Property '"+name+"' --> '"+newValue.ToString()+"'");
			}
#endif
			if (didChange && doBroadcastChange)
				BroadcastPropertyChange (name);
		}

		/// <summary>
		/// set the value and broadcast the change
		/// </summary>
		/// <param name="name"></param>
		/// <param name="newValue"></param>
		public void SetProperty (string name, object newValue)
		{
			SetProperty(name, newValue, true);
		}
		
		public bool GetBoolProperty (string name, bool defaultValue)
		{
			object o =GetValue(name, defaultValue);
			if(o is bool)
				return (bool)o;
			
			throw new ApplicationException("The property "+name+" is not currently a boolean.");
		}
		
		public string GetStringProperty (string name, string defaultValue)
		{
			return (string)GetValue(name, defaultValue);
		}
		public int GetIntProperty (string name, int defaultValue)
		{
			return (int)GetValue(name, defaultValue);
		}

		public void SetPropertyDispose (string name, bool doDispose)
		{
			if(!Monitor.TryEnter(m_properties))
			{
				TraceVerboseLine(">>>>>>>*****  colision: <e>  ********<<<<<<<<<<<");
				Monitor.Enter(m_properties);
			}
			Property property = (Property )m_properties[name];
			Monitor.Exit(m_properties);

			Debug.Assert(property!=null);
			Debug.Assert(property.value is IDisposable);

			property.doDispose=doDispose;
		}
		#endregion

		#region persistence stuff
		public void SetPropertyPersistence (string name, bool doPersist)
		{
			if(!Monitor.TryEnter(m_properties))
			{
				TraceVerboseLine(">>>>>>>*****  colision: <f>  ********<<<<<<<<<<<");
				Monitor.Enter(m_properties);
			}
			Property property = (Property )m_properties[name];
			Monitor.Exit(m_properties);

			Debug.Assert(property!=null);
			property.doPersist=doPersist;
		}


		//		/// <summary>
		//		/// constructor used during deserialization
		//		/// </summary>
		//		/// <param name="info"></param>
		//		/// <param name="context"></param>
		//		protected PropertyTable(SerializationInfo info, StreamingContext context)
		//		{
		//			foreach(SerializationEntry entry in info)
		//			{
		//				TraceVerboseLine( entry.Name+": "+ entry.Value.ToString());
		//			}
		//		}
		/// <remarks> note that initially, we are only saving simple types and therefore
		///  are not benefiting a lot from using this utilization architecture.  However, 
		///  it doesn't cost us anything, and would make it easier in the future if we want 
		///  to serialize deserialize more complicated classes.</remarks>


		/// <summary>
		/// save the project and its contents to a file
		/// </summary>
		/// <param name="settingsId">save settings starting with this, and use as part of file name</param>
		/// <param name="omitPrefixes">skip settings starting with any of these.</param>
		public void Save(string settingsId, string[] omitPrefixes)
		{
			System.IO.StreamWriter writer =null;
			try
			{
				XmlSerializer szr = new XmlSerializer(typeof(Property[]));
				string path = SettingsPath(settingsId);
				writer = new System.IO.StreamWriter(path);
						
				szr.Serialize(writer, MakePropertyArrayForSerializing(settingsId, omitPrefixes));
			}
			catch(Exception err)
			{
				throw new ApplicationException ("There was a problem saving your settings.", err);
			}
			finally
			{
				if (writer != null)
					writer.Close();
			}
		}

		private string SettingsPath(string settingsId)
		{
			return System.IO.Path.Combine(UserSettingDirectory, settingsId + "Settings.xml");
		}

		/// <summary>
		/// where to save user settings
		/// </summary>
		public string UserSettingDirectory
		{
			get
			{
				string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);
				path = System.IO.Path.Combine(path,System.Windows.Forms.Application.CompanyName+"\\"+ System.Windows.Forms.Application.ProductName);
				System.IO.Directory.CreateDirectory(path);
				return path;
			}
		}

		/// <summary>
		/// load the PropertyTable out of a file stored in the users application data directory
		/// </summary>
		/// <param name="settingsId"></param>
		//		public void Load(string settingsId)
		//		{
		//			System.IO.StreamWriter writer =null;
		//			try
		//			{
		//				XmlSerializer szr = new XmlSerializer(typeof(Property[]));
		//				string path =System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData)
		//					+"\\"+settingsId+".xml";
		//				writer = new System.IO.StreamWriter(path);
		//						
		//				szr.Serialize(writer, MakePropertyArrayForSerializing());
		//			}
		//			catch(Exception err)
		//			{
		//				throw new ApplicationException ("There was a problem saving your settings.", err);
		//			}
		//			finally
		//			{
		//				if (writer != null)
		//					writer.Close();
		//			}
		//		}

		/// <summary>
		/// load with properties stored
		///  in the settings file, if that file is found.
		/// </summary>
		/// <param name="settingsId">e.g. "itinerary"</param>
		/// <returns></returns>
		public void  RestoreFromFile (string settingsId)
		{
			string path = SettingsPath(settingsId);
			
			if(!System.IO.File.Exists(path))
				return;

			System.IO.StreamReader reader =null;
			try
			{
				XmlSerializer szr = new XmlSerializer(typeof(Property[]));
				reader = new System.IO.StreamReader(path);
						
				Property[] list = (Property[])szr.Deserialize(reader);
				ReadPropertyArrayForDeserializing(list);
			}
			catch(FileNotFoundException)
			{
				//don't do anything
			}
			catch(Exception )
			{
				System.Windows.Forms.MessageBox.Show("There was a problem while trying to restore the settings.  Your settings will be restored to their default values.");
			}
			finally
			{
				if (reader != null)
					reader.Close();
			}
		}

		private void ReadPropertyArrayForDeserializing(Property[] list)
		{
			//TODO: make a property which contains the date and time that the configuration file we are using.
			//then, when reading this back in, ignore the properties if they were saved under an old configuration file.

			foreach(Property property in list)
			{
				//I know it is strange, but the serialization code will give us a 
				//	null property if there were no other properties.
				if (property != null)
				{
					if(!Monitor.TryEnter(m_properties))
					{
						TraceVerboseLine(">>>>>>>*****  colision: <g>  ********<<<<<<<<<<<");
						Monitor.Enter(m_properties);
					}

					// REVIEW JohnH(RandyR): I added the Remove call,
					// because one of the properties was already there, and 'Add' thorws an exception,
					// if it is there.
					//ANSWER (JH): But how could a duplicate get in there? This is only called once, and no code should ever putting duplicates when saving.

					m_properties.Remove(property.name); // In case it is there.
					m_properties.Add(property.name, property);
					Monitor.Exit(m_properties);
				}
			}
		}

		private Property[] MakePropertyArrayForSerializing(string settingsId, string[] omitPrefixes)
		{
			if(!Monitor.TryEnter(m_properties))
			{
				TraceVerboseLine(">>>>>>>*****  colision: <i>  ********<<<<<<<<<<<");
				Monitor.Enter(m_properties);
			}
			ArrayList list = new ArrayList(m_properties.Count);
			foreach(DictionaryEntry entry in m_properties)
			{
				Property property = (Property) entry.Value;
				if (!property.doPersist)
					continue;
				if (settingsId!=null && !property.name.StartsWith(settingsId))
					continue;
				bool fIncludeThis = true;
				if(omitPrefixes !=null)
				{
					foreach (string prefix in omitPrefixes)
					{
						if (property.name.StartsWith(prefix))
						{
							fIncludeThis = false;
							break;
						}
					}
				}
				if (fIncludeThis)
					list.Add(property);
			}
			Monitor.Exit(m_properties);
			//convert the ArrayList to a Property[].
			Property[] result = new Property[list.Count];
			list.CopyTo(result);
			return result;
		}
		#endregion

		#region TraceSwitch methods
		protected void TraceVerbose(string s)
		{
			if(m_traceSwitch.TraceVerbose)
				Trace.Write(s);
		}
		protected void TraceVerboseLine(string s)
		{
			if(m_traceSwitch.TraceVerbose)
				Trace.WriteLine("PTID="+System.Threading.Thread.CurrentThread.GetHashCode()+": "+s);
		}
		protected void TraceInfoLine(string s)
		{
			if(m_traceSwitch.TraceInfo || m_traceSwitch.TraceVerbose)
				Trace.WriteLine("PTID="+System.Threading.Thread.CurrentThread.GetHashCode()+": "+s);
		}	

		#endregion
	}

	[Serializable]
	//TODO: we can't very well change this source code every time someone adds a new value type!!!
	[XmlInclude(typeof(System.Drawing.Point))]
	[XmlInclude(typeof(System.Drawing.Size))]
	[XmlInclude(typeof(System.Windows.Forms.FormWindowState))]
	public class Property
	{
		public string name=null;
		public object value=null;

		//it is not clear yet what to do about default persistence;
		//normally we would want to say false, but we don't you have 
		//a good way to indicate that the property should be saved except for beer code.
		//therefore, for now, the default will be true said that properties which are introduced 
		//in the configuration file will still be persisted.
		public bool doPersist=true;

		//Up until now there was no way to pass ownership of the object/property
		//to the property table so that the objects would be disposed of at the
		//time the property table goes away.
		public bool doDispose=false;

		/// <summary>
		/// required for XML serialization
		/// </summary>						   
		public Property()
		{
		}

		public Property(string name, object value)
		{
			this.name = name;
			this.value = value;
		}

		override public string ToString ()
		{
			if(value==null)
				return name +"= null";
			else
				return name +"= "+value.ToString();
		}
	}
}
