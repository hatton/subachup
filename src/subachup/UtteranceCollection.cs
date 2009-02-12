using System;
using System. Text;
using System.IO;
using System.Collections;
using System.Windows.Forms;
using System.Xml;


namespace Subachup
{
	/// <summary>
	/// Summary description for UtteranceCollection.
	/// </summary>
	public class UtteranceCollection: ArrayList
	{
		protected  static UtteranceCollection _currentUtteranceSet;
	    private XmlDocument _liftDom;
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

		
		public void LoadFromCollectionFile(string collectionPath)
		{
			this.Clear();
            _liftDom = new XmlDocument();
		    string projectPath = Directory.GetParent(collectionPath).Parent.Parent.FullName;
		    string projectName = Path.GetFileName(projectPath);         //TODO: this isn't guaranteed to work
		    string imageDirectory = Path.Combine(projectPath, "pictures"); 
		    string audioDirectory = Path.Combine(projectPath, "audio"); 
		    string liftPath = Path.Combine(projectPath, projectName + ".lift"); 
            if(!File.Exists(liftPath))
            {
                MessageBox.Show("could not find a lift file at " + liftPath);
                return;
            }
		    _liftDom.Load(liftPath);

		    string[] words = File.ReadAllLines(collectionPath);
		    foreach (var word in words)
		    {
		        var entryNodes = _liftDom.SelectNodes(string.Format("lift/entry[lexical-unit/form/text='{0}']", word.Trim()));
                //todo: what if we don't find it?

                if(entryNodes.Count > 0)
                {
                    //todo: what if there is more than one?
                    string imagePath = GetImagePath(imageDirectory, entryNodes[0]);
                    string soundPath = GetSoundPath(audioDirectory, entryNodes[0]);
                    string gloss = GetGloss(entryNodes[0]);

		            var utterance = new Utterance(word, gloss, soundPath, imagePath);
			        this.Add(utterance);
                }
		    }
		}

	    private string GetGloss(XmlNode entryNode)
	    {
            var glossNode = entryNode.SelectSingleNode("sense/definition/form/text");
            if(glossNode != null)
            {
                var name =glossNode.InnerText;
                if(name!=null)
                {
                    return name;
                }
            }
	        return "?";
        }

	    private string GetImagePath(string imageDirectory, XmlNode entryNode)
	    {
            if(!Directory.Exists(imageDirectory))
                return null;

            var imageNode = entryNode.SelectSingleNode("sense/illustration");
            if(imageNode != null)
            {
                var name =imageNode.Attributes.GetNamedItem("href");
                if(name!=null)
                {
                    return Path.Combine(imageDirectory, name.Value);
                }
            }
	        return null;	   
        }

	    private string GetSoundPath(string audioDirectory, XmlNode entryNode)
	    {
            if (!Directory.Exists(audioDirectory))
                return null;

            var audioNode = entryNode.SelectSingleNode("lexical-unit/form[@lang='voice']/text");
            if(audioNode != null)
            {
                return Path.Combine(audioDirectory,audioNode.InnerText);
            }
	        return null;
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
