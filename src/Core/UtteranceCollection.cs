using System;
using System. Text;
using System.IO;
using System.Collections;


namespace Subachup
{
	/// <summary>
	/// Summary description for UtteranceCollection.
	/// </summary>
	public class UtteranceCollection: ArrayList
	{
		protected  static UtteranceCollection _currentUtteranceSet;
        public event System.EventHandler Changed;

		public UtteranceCollection()
		{
           
		}

		public static UtteranceCollection CurrentUtteranceSet
		{
			get
			{
				if(_currentUtteranceSet==null)
					CurrentUtteranceSet= new UtteranceCollection();

				return _currentUtteranceSet;
			}
			set
			{
				_currentUtteranceSet = value;
			}
		}

        public void DidChange()
        {
            if(Changed!=null)
                Changed.Invoke(this, null);
        }

		public void SaveUserStuff()
		{
//			ObjectContainer db=null;
//			try
//			{
//				db = GetDb();
//				foreach(Utterance u in this)
//				{
//					u.SaveUserHistory(db);
//				}
//			}
//			finally
//			{
//				db.Close();
//			}		
		}

//		private ObjectContainer GetDb()
//		{
//			ObjectContainer db;
//            string s = @"c:\subachupUser.yap";
//   			db= Db4oFactory.OpenFile(s);
//
//            System.Diagnostics.Debug.Assert(db!=null);
//			Db4oFactory.Configure().MarkTransient("Subachup.DontSaveAttribute");
//			com.db4o.config.Configuration config = Db4o.Configure();
//			UtteranceHistory dummy=new UtteranceHistory();
//			com.db4o.config.ObjectClass oc = config.ObjectClass(dummy);//("Subachup.UserHistory");
//			com.db4o.config.ObjectField of = oc.ObjectField("_utteranceKey");
//			of.Indexed(true);
//
//			return db;
//		}

		public void  LoadUserStuff()
		{

//			ObjectContainer db=null;
//			try
//			{
//				db= GetDb();
//				if(db==null)
//					return ;
//
//				foreach(Utterance u in this)
//				{
//					u.LoadUserHistory(db);
//				}
//			}
//			finally
//			{
//				if(db!=null)
//					db.Close();
//			}
		}
		
    	public void SavePlayList(string path)
		{
	/*		if(System.IO.Directory.Exists(path))
			{
				string[] files = Directory.GetFiles(path,  "*.lnk");
				foreach (string link in files)
				{
					File.Delete(link);
				}				
				//System.IO.Directory.Delete(path,false);
			}

			ShellLink.IShellLinkW shellLink = (ShellLink.IShellLinkW) new ShellLink.ShellLink();
			ShellLink.IPersistFile pf = (ShellLink.IPersistFile) shellLink;

			try
			{
				System.IO.Directory.CreateDirectory(path);
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e);
			}

			foreach(Utterance utterance in this)
			{ 
				shellLink.SetPath(utterance.FilePath);
				try
				{
					pf.Save(Path.Combine(path,utterance.Gloss+".mp3.lnk"), true );
				}
				catch(Exception e)
				{
					System.Diagnostics.Debug.WriteLine(e);
				}
			}
     * */
		}

		
		public void LoadFromDirectory(string directory, bool doIncludeShortcuts)
		{
			this.Clear();
			LoadFromDirectoryInternal(directory, doIncludeShortcuts);
		}

		private void LoadFromDirectoryInternal(string directory, bool doIncludeShortcuts)
		{
			//suddenly, you cannot combine multiple patterns in one call to GetFiles()
			string[] files = Directory.GetFiles(directory,  "*.mp3");
			foreach (string soundFile in files)
			{
				AddSoundFile(soundFile);
			}

			if(doIncludeShortcuts)
			{
				files = Directory.GetFiles(directory,  "*.lnk");
				foreach (string soundFile in files)
				{
					string actualFile = DereferenceShortcut(soundFile);
					if(!File.Exists(soundFile))
						continue;
					if(Path.GetExtension(actualFile)!=".mp3")
						continue;
					if(File.Exists(actualFile))//no big deal if its gone
						AddSoundFile(actualFile);
				}
			}

			// loop through all folders
			foreach (string dir in Directory.GetDirectories(directory))
			{
				if(directory.IndexOf("_data") ==-1)//skip audacity folders
					LoadFromDirectoryInternal(dir, doIncludeShortcuts);
			}	
		}



		private void AddSoundFile(string actualFile)
		{
			Utterance utterance = new Utterance(actualFile);
			this.Add(utterance);
		}
		//
		// If Path returns an empty string, the shortcut is associated with
		// a PIDL instead, which can be retrieved with IShellLink.GetIDList().
		// This is beyond the scope of this wrapper class.
		//
		/// <value>
		///   Gets or sets the target path of the shortcut.
		/// </value>
		private static string DereferenceShortcut(string link)
		{  
			ShellLink.IShellLinkW shellLink = (ShellLink.IShellLinkW) new ShellLink.ShellLink();

			const int MAX_PATH = 260;

			StringBuilder sb = new StringBuilder( MAX_PATH );
			try
			{
				ShellLink.IPersistFile pf = (ShellLink.IPersistFile)shellLink;
				pf.Load( link, 0 );
				ShellLink.WIN32_FIND_DATAW wfd = new ShellLink.WIN32_FIND_DATAW();
				shellLink.GetPath( sb, sb.Capacity, out wfd, ShellLink.SLGP_FLAGS.SLGP_UNCPRIORITY );
			}
			finally
			{
				System.Runtime.InteropServices.Marshal.ReleaseComObject( shellLink );
			}
			return sb.ToString();
		}	
	}
}
