using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using subachup.utility;

namespace subachup
{
    public class LiftProject
    {
        public XmlDocument LiftDom { get; set; }
        public string ImageDirectory { get; set; }
        public string AudioDirectory { get; set; }

        public LiftProject()
        {
            
        }
        public bool LoadFromSubachupDataDir(string subachupFilePath)
        {
            string projectPath = Directory.GetParent(subachupFilePath).Parent.FullName;
            string projectName = Path.GetFileName(projectPath);         //TODO: this isn't guaranteed to work
            ImageDirectory = Path.Combine(projectPath, "pictures");
            AudioDirectory = Path.Combine(projectPath, "audio");
            string liftPath = Path.Combine(projectPath, projectName + ".lift");
            if (!File.Exists(liftPath))
            {
                MessageBox.Show("could not find a lift file at " + liftPath);
                return false;
            }
            LiftDom = new XmlDocument();
            LiftDom.Load(liftPath);
            return true;
        }

    

        public LexEntry GetEntryFromHitRegionId(string subachupRegionId)
        {
            var entryNode = LiftDom.SelectSingleNode(string.Format("lift/entry[field[@type='SubachupRegion']/form/text='{0}']", subachupRegionId));
            if(entryNode==null)
                return null;
            return new LexEntry(this, entryNode);
        }

        public LexEntry GetEntryFromWord(string word)
        {
            var entryNodes = LiftDom.SelectNodes(string.Format("lift/entry[lexical-unit/form/text='{0}']", word.Trim()));
            if(entryNodes.Count ==0)
                return null;
            
            return new LexEntry(this, entryNodes[0]);
        }
    }

    public class LexEntry
    {
        private readonly LiftProject _project;
        private readonly XmlNode _entryNode;

        public LexEntry(LiftProject project, XmlNode entryNode)
        {
            _project = project;
            _entryNode = entryNode;
        }


        public Guid GUID
        {
            get
            {
                var id = XmlUtils.GetManditoryAttributeValue(_entryNode, "guid");
                return new Guid(id);
            }
        }

        public string Id
        {
            get
            {
                return XmlUtils.GetManditoryAttributeValue(_entryNode, "id");
            }
        }

        public string ImagePath
        {
            get
            {
                if (!Directory.Exists(_project.ImageDirectory))
                    return null;

                var imageNode = _entryNode.SelectSingleNode("sense/illustration");
                if (imageNode != null)
                {
                    var name = imageNode.Attributes.GetNamedItem("href");
                    if (name != null)
                    {
                        return Path.Combine(_project.ImageDirectory, name.Value);
                    }
                }
                return null;
            }
        }

        public string SoundPath
        {
            get
            {
                if (!Directory.Exists(_project.AudioDirectory))
                    return null;

                var audioNode = _entryNode.SelectSingleNode("lexical-unit/form[@lang='voice']/text");
                if (audioNode != null)
                {
                    return Path.Combine(_project.AudioDirectory, audioNode.InnerText);
                }
                return null;
            }
        }


        public string Gloss
        {
            get
            {
                var glossNode = _entryNode.SelectSingleNode("sense/definition/form/text");
                if (glossNode != null)
                {
                    var name = glossNode.InnerText;
                    if (name != null)
                    {
                        return name;
                    }
                }
                return "?";
            }
        }

        public string SubachupRegion
        {
            get 
            {
                var n = _entryNode.SelectSingleNode("field[@type='SubachupRegion']/form/text");
                if(n==null)
                    return string.Empty;
                return n.InnerText;
            }
        }
    }
}
