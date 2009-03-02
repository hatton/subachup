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

        public int ImageWidth { get; private set; }

        public IEnumerable<string> GetRegionIds()
        {
            XmlNodeList nodes = _mapDom.SelectNodes("x:svg/x:g/x:rect", _nameSpaceManager);
            if (nodes == null)
                yield return null; //review


            foreach (XmlNode node in nodes)
            {
                string id = GetTrimmedIdFromNode(node);

                yield return id;
            }
        }

        private string GetTrimmedIdFromNode(XmlNode node)
        {
//bit of a hack here... svg requires unique ids, but we're using ids to match utterances,
            //and  there can be multiple hit regions for a single utterance(e.g. *2* kids not holding Mom's hand).
            //So for now, we just add a number to the id in the svg program, and remove it here.
            var id = node.Attributes.GetNamedItem("id").Value;
            id = id.TrimEnd(new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' });
            return id;
        }

        public IEnumerable<Rectangle> GetRectsForUtterance(string id)
        {
            var nodes = _mapDom.SelectNodes(string.Format("x:svg/x:g/x:rect[contains(@id,id)]"), _nameSpaceManager);
            foreach (XmlNode node in nodes)
            {
                yield return GetRectangleFromNode(node);
            }
        }

        private Rectangle GetRectangleFromNode(XmlNode node)
        {
            var x = GetIntAttribute(node, "x");
            XmlNode imageNode = GetImageNode();
            x -= GetIntAttribute(imageNode, "x");
            var y = GetIntAttribute(node, "y");
            y -= GetIntAttribute(imageNode, "y");
            return new Rectangle(x, y, GetIntAttribute(node, "width"), GetIntAttribute(node, "height"));
        }

        private int GetIntAttribute(XmlNode node, string attr)
        {
            return (int)double.Parse(node.Attributes.GetNamedItem(attr).Value);
        }

        public IEnumerable<string> GetIdsOfUtterancesInMap()
        {
            return GetRegionIds();
        }

        public IEnumerable<string> GetRegionIds(int x, int y)
        {
             var nodes = _mapDom.SelectNodes(string.Format("x:svg/x:g/x:rect[contains(@id,id)]"), _nameSpaceManager);
            foreach (XmlNode node in nodes)
            {
                 var r =  GetRectangleFromNode(node);

                  if (r.Contains(x, y))
                {
                    yield return GetTrimmedIdFromNode(node);
                }
            }
        }
    }
}

