using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;

namespace subachup
{
    public class SvgMapReader
    {
        private readonly string _path;
        private XmlDocument _mapDom;
        private XmlNamespaceManager _nameSpaceManager;

        public SvgMapReader(string path)
        {
            _path = path;
            _mapDom = new XmlDocument();
            _mapDom.Load(path);
            _nameSpaceManager = new XmlNamespaceManager(_mapDom.NameTable);
            _nameSpaceManager.AddNamespace("x", @"http://www.w3.org/2000/svg");
            var image = Image.FromFile(ImagePath);
            ImageWidth = image.Width;
            image.Dispose();
        }

        public string ImagePath
        {
            get
            {
                var imageNode = GetImageNode();
                var p = Path.Combine(Path.GetDirectoryName(_path), imageNode.Attributes["xlink:href"].Value);
                return p.Replace(@"file:\", "");
            }
        }

        private XmlNode GetImageNode()
        {
            return _mapDom.SelectSingleNode("x:svg/x:g/x:image", _nameSpaceManager);
        }

        public int ImageWidth{ get; private set;}
    
        public IEnumerable<string> GetRegionIds()
        {
            XmlNodeList nodes = _mapDom.SelectNodes("x:svg/x:g/x:rect", _nameSpaceManager);
            if(nodes==null)
                yield return null; //review
           
           
            foreach (XmlNode node in nodes)
            {
                yield return node.Attributes.GetNamedItem("id").Value;
            }
        }

        public Rectangle GetRegion(string id)
        {
            var node = _mapDom.SelectSingleNode(string.Format("x:svg/x:g/x:rect[@id='{0}']", id), _nameSpaceManager);
            var x = GetIntAttribute(node, "x");
            XmlNode imageNode= GetImageNode();
            x -= GetIntAttribute(imageNode, "x");
            var y = GetIntAttribute(node, "y");
            y -= GetIntAttribute(imageNode, "y");
            return new Rectangle(x, y, GetIntAttribute(node, "width"), GetIntAttribute(node, "height"));
        }

        private int GetIntAttribute(XmlNode node, string attr)
        {
            return (int) double.Parse(node.Attributes.GetNamedItem(attr).Value);
        }
    }
}
