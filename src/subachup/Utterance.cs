using System;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing;

namespace subachup
{
	public class UtteranceHistory
	{
		public int _correctWithoutMistakesCount;
		public int _missedCount;
		public DateTime _lastQuizzedDate;
		public string _utteranceKey;
	}

    public interface IQuizItem
    {
        string IdOfLiftEntry { get; set; }

        [NormallyReadOnly]
        int Score { get; set; }

        [NormallyReadOnly, ForIndividualUser]
        int CorrectWithoutMistakesCount { get; set; }

        [NormallyReadOnly, ForIndividualUser]
        DateTime LastQuizzedDate { get; set; }

        string SubachupRegionId { get; set; }
    }

    /// <summary>
	/// Summary description for Utterance.
	/// </summary>
	public class Utterance : IQuizItem
    {
	    protected UtteranceHistory _userHistory;
		//protected com.db4o.ext.Db4oUUID _userHistoryId;

		protected HundredMilesSoftware.UltraID3Lib.UltraID3 _id3;
		protected int _score;

        public string IdOfLiftEntry { get; set; }

		protected bool _checked=false;
		protected nBASS.Stream _sound;
		protected nBASS.BASS _player;

	    public Rectangle HitRectangle{ get; set;}
	
		//		protected string _contrastGroup;
		
		/// <summary>
		/// this is a hack for making a dummy utterance used to do reflection, only because I cannot figure out how to do it properly.
		/// </summary>
		public Utterance()
		{
		}

        public Utterance(string word, string gloss, string soundPath,string imagePath)
        {
            SoundPath = soundPath;
            ImagePath = imagePath;
            Word = word;
            Gloss = gloss;

//
//            UltraID3 id3 = new UltraID3();
//            //			try
//            {
//                id3.Read(soundPath);
//            }
//            this.ID3 = id3;

            _userHistory = new UtteranceHistory();
            _userHistory._utteranceKey = this.GUID.ToString();
        }

	    public string Word{ get; set;}

	    public override bool Equals(Object o)
		{
			return o == this || ((Utterance)o).SoundPath == this.SoundPath;
		}

//		public void SaveUserHistory(ObjectContainer db)
//		{
//			//this is hokey, but we don't want duplicates each time we save,
//			//and unless we keep the db open, it won't realize this is already in there.
//			UtteranceHistory h = QueryForUserHistory(db);
//			if(h!=null)
//				db.Delete(h);
//
//			db.Set(_userHistory);
//		}   
//		public void LoadUserHistory(ObjectContainer db)
//		{
//			UtteranceHistory h = QueryForUserHistory(db);
//			if(h!=null)
//				_userHistory = h;
//		}
//
//		private UtteranceHistory QueryForUserHistory(ObjectContainer db)
//		{
//			com.db4o.query.Query query = db.Query();
//			query.Constrain(typeof(UtteranceHistory));
//			query.Descend("_utteranceKey").Constrain(this.GUID.ToString());
//			ObjectSet result = query.Execute();
//			if(result.Count > 0)
//			{
//				return (UtteranceHistory) result.Next();
//			}
//			return null;
//		}

		//	[NeverVisibleAttribute]
		public System.Guid GUID
		{
			get
			{
				string x= GetTaggedComment("GUID");
				if(x==null || x=="")
				{
					Guid g =  System.Guid.NewGuid();
					this.GUID=g;
					return g;
				}
				else
					return new System.Guid(x);
			}
			set
			{
				SetTaggedComment("GUID", value.ToString());
			}
		}

		//		[NeverVisibleAttribute]
		//		public com.db4o.ext.Db4oUUID UserHistoryId
		//		{
		//			get
		//			{
		//				string a = GetTaggedComment("UserHistoryId_longPart");
		//				string b = GetTaggedComment("UserHistoryId_signature");
		//				com.db4o.ext.Db4oUUID id = new com.db4o.ext.Db4oUUID(a,b);
		//				return  
		//			}
		//			set
		//			{
		//				SetTaggedComment("UserHistoryId", value.ToString());
		//			}
		//
		//		}

		[NormallyReadOnly]
		public string SoundPath{ get; set;}

		/// <summary>
		/// we use these nifty comments that have a label (description) for custom strings
		/// </summary>
		protected string GetTaggedComment(string label)
		{
		    return null;
//
//			string s =_id3.ID3v23Tag.Frames.GetComments(label);
//			if(s.Trim() =="")
//				return null;
//			else
//				return s;
		}

		/// <summary>
		/// we use these nifty comments that have a label (description) for custom strings
		/// </summary>
		protected void SetTaggedComment(string label, string content)
		{
//			_id3.ID3v23Tag.Frames.SetComments(label, content);
//			_id3.ID3v23Tag.WriteFlag = true;
//			_id3.Write();
		}

		/// <summary>
		/// this is a hack until we figure out how to retrieve the source utterance from a row of the grid
		/// this allows us to have a column whose data is just self
		/// </summary>
		[NeverVisible]
		public  Utterance Self
		{
			get
			{
				return this;
			}
		}
		
		
		public  DateTime SoundLastEdited
		{
			get
			{
                return File.GetCreationTime(SoundPath);
			}
		}

		[NeverVisible]
		public  HundredMilesSoftware.UltraID3Lib.UltraID3 ID3
		{
			get
			{
				return _id3;
			}
			set
			{
				_id3 = value;
			}
		}			
		public string Album
		{
			get
			{
				return _id3.Album;
			}
		}
		
		[NormallyReadOnly]
		public int Score
		{
			get
			{
				return _score;
			}
			set
			{
				_score = value;
			}
		}

		[NormallyReadOnly]
		public string Key
		{
			get
			{
                return SoundPath;
			}
			set
			{
                SoundPath = value;
			}
		}

		[NormallyReadOnly, ForIndividualUser]
		public int CorrectWithoutMistakesCount
		{
			get
			{
				return _userHistory._correctWithoutMistakesCount;
			}
			set
			{
				_userHistory._correctWithoutMistakesCount = value;
			}
		}	
	
		[NormallyReadOnly, ForIndividualUser]
		public DateTime LastQuizzedDate
		{
			get
			{
				return _userHistory._lastQuizzedDate;
			}
			set
			{
				_userHistory._lastQuizzedDate = value;
			}
		}

        public string SubachupRegionId { get; set;}

        public int DurationSeconds
		{
			get
			{
				return _id3.Duration.Seconds;
			}
		}

		private string GetExtensionForImageType(System.Drawing.Image image)
		{
			string s= "."+System.ComponentModel.TypeDescriptor.GetConverter(typeof(System.Drawing.Imaging.ImageFormat)).ConvertToString(image.RawFormat);
			if(s==".Jpeg")
				s=".jpg";
            if (s == ".MemoryBmp")
                s = ".bmp";
            return s;

			
			//			image.RawFormat)
			//			{
			//				default:
			//					throw new ApplicationException("Sorry, that image format is not supported.");
			//					break;
			//				case System.Drawing.Imaging.ImageFormat.Bmp: 
			//					return ".bmp";
			//				case System.Drawing.Imaging.ImageFormat.Jpeg: 
			//					return ".jpg";
			//				case System.Drawing.Imaging.ImageFormat.Png: 
			//					return ".png";
			//				case System.Drawing.Imaging.ImageFormat.Gif: 
			//					return ".gif";
			//			}
		}
//
//		private string ImagesDirectory
//		{
//			get {return Path.Combine(ParentDirectory,"images");;}
//		}

        public string ImagePath { get; set; }

		[NeverVisibleAttribute] //was hitting this, even when column was hidden [NormallyReadOnly,Image]
		public System.Drawing.Image TheImage
		{
			set
			{
//				string p =ImagesDirectory+"/"+Gloss+GetExtensionForImageType(value);
//				if(File.Exists(p))
//					File.Delete(p);
//				value.Save(p);
			}
			get
			{
                if (!string.IsNullOrEmpty(ImagePath) && File.Exists(ImagePath))
				{
					//					Image i = System.Drawing.Image.FromFile(s);
					//					i.Dispose();
					//					return null;
                    return System.Drawing.Image.FromFile(ImagePath);
				}
				return null;
			}
		}

		[NormallyReadOnly,Image]
		public System.Drawing.Image GetThumbNail(int width, int height)
		{
            Image image = TheImage;
            if (image == null)
            {
                return null;
            }
            else
            {
                return MakeThumbnail(image,  width, height);
            }
		}

		[DefaultVisible]
		public string ContrastGroup
		{
			get
			{
				return GetTaggedComment("ContrastGroups");
			}
			set
			{
				SetTaggedComment("ContrastGroups", value);
			}
		}

		[DefaultVisible]
		public string Comments
		{
			get
			{
				return _id3.Comments;
			}
			set
			{
				_id3.Comments = value;
				_id3.Write();
			}
		}

		[DefaultVisible, MRU]
		public string POS
		{
			get
			{
				return GetTaggedComment("POS");
			}
			set
			{
				SetTaggedComment("POS", value);
			}
		}

		[DefaultVisible(2)]
		public string ParentDirectory
		{
			get
			{
                return Path.GetDirectoryName(SoundPath);
			}
		}

        [DefaultVisible(1), NormallyReadOnly]
        public string Gloss { get; set; }
//		{
//			get
//			{
//				return Path.GetFileNameWithoutExtension(SoundPath);
//			}
//			set
//			{				
////				if(value==Gloss)// this happens a lot with the user not really meaning to change it
////					return;
////
////				if(_sound!=null)
////				{
////					_sound.Stop();
////					_sound.Dispose();
////					_sound=null;
////					_player=null;
////				}
////
////				string newPath= Path.Combine(ParentDirectory, value+".mp3");
////				string existingImagePath = GetMatchingImagePath();
////				try
////				{
////					File.Copy(SoundPath, newPath);				
////					if(existingImagePath  != null)
////						File.Copy(existingImagePath, Path.Combine(Path.GetDirectoryName(existingImagePath),value+Path.GetExtension(existingImagePath)), true);
////				}
////				catch(Exception e)
////				{
////					System.Windows.Forms.MessageBox.Show("Could not rename: "+e.Message);
////					return;
////				}
////
////				try
////				{
////					File.Delete(SoundPath);
////					if(existingImagePath!=null)
////						File.Delete(existingImagePath);
////				}
////				catch(Exception e)
////				{
////					System.Windows.Forms.MessageBox.Show("Renamed ok, but the old named file could not be deleted. Try quitting Subachup and deleting it by hand. "+e.Message);
////				}
////				_soundPath = newPath;
//			}
//		}
		
		/// <summary>
		/// supports a checkbox row
		/// </summary>
		[DefaultVisible(0)]
		public bool Checked
		{
			get
			{
				return _checked;
			}
			set
			{
				_checked = value;
			}
		}



//		private string GetMatchingImagePath()
//		{
//            if (!Directory.Exists(ImagesDirectory))
//            {
//                Directory.CreateDirectory(ImagesDirectory);
//                return null;
//            }
//
//			string[] files = Directory.GetFiles(ImagesDirectory,Gloss+".jpg");
//
//			if(files.Length == 0)
//				files = Directory.GetFiles(ImagesDirectory,Gloss+".jpeg");
//
//			//this is a bit goofy, back from when I was trying to get images out of Microsoft Office
//			if(files.Length == 0)
//			{
//				files =Directory.GetFiles(ImagesDirectory,Gloss+".wmf");
//				if(files.Length != 0)
//				{
//					Image b= Bitmap.FromFile(files[0]);
//					string s = Path.Combine(ImagesDirectory, Path.GetFileNameWithoutExtension(files[0])+".jpg");
//					b.Save(s,System.Drawing.Imaging.ImageFormat.Jpeg);
//					b.Dispose();
//					//now load that
//					files = Directory.GetFiles(ImagesDirectory,Gloss+".jpg");
//				}
//			}
//
//			if(files.Length == 0)
//				files = Directory.GetFiles(ImagesDirectory,Gloss+".bmp");
//			if(files.Length == 0)
//				files = Directory.GetFiles(ImagesDirectory,Gloss+".png");
//			if(files.Length == 0)
//				files = Directory.GetFiles(ImagesDirectory,Gloss+".gif");
//			if(files.Length == 0)
//				return null;
//
//			return files[0];
//			
//		}

		//		private string[] AddFromShortcuts(string directory, string[] audioFiles)
		//		{
		//			string[] links =System.IO.Directory.GetFiles(directory,"*.lnk");
		//			ArrayList result = new ArrayList(audioFiles);
		//			//			string[] result	= new string[links.Length + audioFiles.Length];
		//			//			audioFiles.CopyTo(result,0);
		//			foreach(string link in links)
		//			{
		//				string p = DereferenceShortcut(link);
		//				if(File.Exists(p))
		//					result.Add(p);
		//			}
		//			return (string[])result.ToArray(typeof(string));
		//		}



		public void Play(nBASS.BASS player)
		{
			player.Start();
			_sound = player.LoadStream(SoundPath);
			_sound.End+=new EventHandler(sound_End);
			_sound.Play(true, nBASS.StreamPlayFlags.Default);
		}

		private void sound_End(object sender, EventArgs e)
		{
			((nBASS.Stream)sender).Dispose();
			_sound = null;
			_player = null;
		}

		public void DoShellOpenSoundWith()
		{
			ShellHelper.OpenWith(SoundPath);
		}

		public void DoShellOpenImageWith()
		{
//			string p = GetMatchingImagePath();
//			if(p!=null)
//				ShellHelper.OpenWith(p);
		}

        public bool ThumbnailCallback()
        {
            return false;
        }


        private Image MakeThumbnail(Image image, int width, int height)
        {
            width = image.Width > width ? width : image.Width;
            height = image.Height > height ? height : image.Height;

            Bitmap retBmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format64bppPArgb);

            Graphics grp = Graphics.FromImage(retBmp);


            int tnWidth = width, tnHeight = height;

            if (image.Width > image.Height)
                tnHeight = (int)(((float)image.Height / (float)image.Width) * tnWidth);
            else if (image.Width < image.Height)
                tnWidth = (int)(((float)image.Width / (float)image.Height) * tnHeight);

            int iLeft = (width / 2) - (tnWidth / 2);
            int iTop = (height / 2) - (tnHeight / 2);

            grp.PixelOffsetMode = PixelOffsetMode.None;
            grp.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grp.DrawImage(image, iLeft, iTop, tnWidth, tnHeight);

//            Pen pn = new Pen(borderColor, 1); //Color.Wheat
//            grp.DrawRectangle(pn, 0, 0, retBmp.Width - 1, retBmp.Height - 1);

            return retBmp;
        }
	}
}
