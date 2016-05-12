using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;


namespace subachup
{
	/// <summary>
	/// Summary description for UtteranceCollection.
	/// </summary>
	public class UtteranceCollection: List<IQuizItem>
	{
	    private readonly LiftProject _liftProject;
	    public event System.EventHandler Changed;

		public UtteranceCollection(LiftProject liftProject)
		{
		    _liftProject = liftProject;
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

		
		


	 


//	    //
//		// If Path returns an empty string, the shortcut is associated with
//		// a PIDL instead, which can be retrieved with IShellLink.GetIDList().
//		// This is beyond the scope of this wrapper class.
//		//
//		/// <value>
//		///   Gets or sets the target path of the shortcut.
//		/// </value>
//		private static string DereferenceShortcut(string link)
//		{  
//			ShellLink.IShellLinkW shellLink = (ShellLink.IShellLinkW) new ShellLink.ShellLink();
//
//			const int MAX_PATH = 260;
//
//			StringBuilder sb = new StringBuilder( MAX_PATH );
//			try
//			{
//				ShellLink.IPersistFile pf = (ShellLink.IPersistFile)shellLink;
//				pf.Load( link, 0 );
//				ShellLink.WIN32_FIND_DATAW wfd = new ShellLink.WIN32_FIND_DATAW();
//				shellLink.GetPath( sb, sb.Capacity, out wfd, ShellLink.SLGP_FLAGS.SLGP_UNCPRIORITY );
//			}
//			finally
//			{
//				System.Runtime.InteropServices.Marshal.ReleaseComObject( shellLink );
//			}
//			return sb.ToString();
//		}

        public void LoadFromCollectionFile(string collectionPath)
        {
            string[] words = File.ReadAllLines(collectionPath);
            foreach (var word in words)
            {
                var entry = _liftProject.GetEntryFromWord(word);
                //todo: what if we don't find it?

                     //todo: what if there is more than one?
                string imagePath = entry.ImagePath;
                string soundPath = entry.SoundPath;
                string gloss = entry.Gloss;

                var utterance = new Utterance(word, gloss, soundPath, imagePath);
                utterance.IdOfLiftEntry = entry.Id;

                utterance.SubachupRegionId = entry.SubachupRegion;
                Add(utterance);
            }
        }

        public void Load(SvgMapReader svg)
	    {
            foreach(string hitRegionId in svg.GetRegionIds())
            {
                var entry= _liftProject.GetEntryFromHitRegionId(hitRegionId);
                if(entry == null)
                {
                    MessageBox.Show("Couldn't find an entry with the subachup id of '" + hitRegionId + "'.");
                    continue;
                }
                var utterance = new Utterance("??", entry.Gloss,entry.SoundPath, entry.ImagePath);
                utterance.SubachupRegionId = entry.SubachupRegion;
                Add(utterance);
            }
	    }

   
	}
}
