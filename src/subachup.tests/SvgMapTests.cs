using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using NUnit.Framework;

namespace subachup.tests
{
    [TestFixture]
    public class SvgMapTests
    {
        private string _exeDirectory;
        private SvgMapReader _svgReader;

        [TestFixtureSetUp]
        public void SetupAllTests()
        {
            _exeDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);

            _svgReader = new SvgMapReader(Path.Combine(_exeDirectory, "sios2.svg"));
        }
        [Test]
        public void ImagePath_FindsBitMapInSameDirectory()
        {
            Assert.IsTrue(File.Exists(_svgReader.ImagePath ));
        }

        [Test]
        public void GetRegionIds_FindsIds()
        {
            var regions = _svgReader.GetRegionIds();
            Assert.AreEqual(2,regions.Count());
            Assert.AreEqual("kidHoldingHands", regions.ElementAt(0));
            Assert.AreEqual("fatKid", regions.ElementAt(1));
        }

        [Test]
        public void  GetRegion_FindsRect()
        {
            var rects = _svgReader.GetRectsForUtterance("kidHoldingHands");
            Assert.AreEqual(1, rects.Count());
            var rect = rects.First();
            Assert.AreEqual(117, rect.Width);
            Assert.AreEqual(171, rect.Height);
            Assert.AreEqual(267,    rect.Left);
            Assert.AreEqual(528, rect.Top);
        }
    }
}
