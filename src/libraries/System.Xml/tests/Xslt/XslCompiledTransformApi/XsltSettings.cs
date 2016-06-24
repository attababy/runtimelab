using Xunit;
using Xunit.Abstractions;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace System.Xml.Tests
{
    //[TestCase(Name = "XsltSettings-Retail", Desc = "This testcase tests the different settings on XsltSettings and the corresponding behavior in retail mode", Param = "Retail")]
    //[TestCase(Name = "XsltSettings-Debug", Desc = "This testcase tests the different settings on XsltSettings and the corresponding behavior in debug mode", Param = "Debug")]
    public class CXsltSettings : XsltApiTestCaseBase
    {
        private ITestOutputHelper _output;
        public CXsltSettings(ITestOutputHelper output) : base(output)
        {
            _output = output;
        }

        private XslCompiledTransform xsl = null;
        private string XmlFile = string.Empty;
        private string XslFile = string.Empty;

        private bool _debug = false;

        private void Init(string xmlFile, string xslFile)
        {
            //if (Param.ToString() == "Debug")
            //{
            //    xsl = new XslCompiledTransform(true);
            //    _debug = true;
            //}
            //else
            xsl = new XslCompiledTransform();

            XmlFile = FullFilePath(xmlFile);
            XslFile = FullFilePath(xslFile);
        }

        private StringWriter Transform()
        {
            StringWriter sw = new StringWriter();
            xsl.Transform(XmlFile, null, sw);
            return sw;
        }

        private void VerifyResult(object actual, object expected, string message)
        {
            _output.WriteLine("Expected : {0}", expected);
            _output.WriteLine("Actual : {0}", actual);

            Assert.Equal(actual, expected);
        }

        //[Variation(id = 1, Desc = "Test the script block with EnableScript, should work", Pri = 0, Params = new object[] { "XsltSettings.xml", "XsltSettings1.xsl", false, true })]
        [InlineData(1, "XsltSettings.xml", "XsltSettings1.xsl", false, true)]
        //[Variation(id = 2, Desc = "Test the script block with Default Settings, should fail", Pri = 0, Params = new object[] { "XsltSettings.xml", "XsltSettings1.xsl", false, false })]
        [InlineData(2, "XsltSettings.xml", "XsltSettings1.xsl", false, false)]
        //[Variation(id = 3, Desc = "Test the script block with EnableDocumentFunction, should fail", Pri = 2, Params = new object[] { "XsltSettings.xml", "XsltSettings1.xsl", true, false })]
        [InlineData(3, "XsltSettings.xml", "XsltSettings1.xsl", true, false)]
        //[Variation(id = 4, Desc = "Test the script block with TrustedXslt, should work", Pri = 1, Params = new object[] { "XsltSettings.xml", "XsltSettings1.xsl", true, true })]
        [InlineData(4, "XsltSettings.xml", "XsltSettings1.xsl", true, true)]
        //[Variation(id = 5, Desc = "Test the document function with EnableDocumentFunction, should work", Pri = 0, Params = new object[] { "XsltSettings.xml", "XsltSettings2.xsl", true, false })]
        [InlineData(5, "XsltSettings.xml", "XsltSettings2.xsl", true, false)]
        //[Variation(id = 6, Desc = "Test the document function with Default Settings, should fail", Pri = 0, Params = new object[] { "XsltSettings.xml", "XsltSettings2.xsl", false, false })]
        [InlineData(6, "XsltSettings.xml", "XsltSettings2.xsl", false, false)]
        //[Variation(id = 7, Desc = "Test the document function with EnableScript, should fail", Pri = 2, Params = new object[] { "XsltSettings.xml", "XsltSettings2.xsl", false, true })]
        [InlineData(7, "XsltSettings.xml", "XsltSettings2.xsl", false, true)]
        //[Variation(id = 8, Desc = "Test the document function with TrustedXslt, should work", Pri = 1, Params = new object[] { "XsltSettings.xml", "XsltSettings2.xsl", true, true })]
        [InlineData(8, "XsltSettings.xml", "XsltSettings2.xsl", true, true)]
        //[Variation(id = 9, Desc = "Test the combination of script and document function with TrustedXslt, should work", Pri = 0, Params = new object[] { "XsltSettings.xml", "XsltSettings3.xsl", true, true })]
        [InlineData(9, "XsltSettings.xml", "XsltSettings3.xsl", true, true)]
        //[Variation(id = 10, Desc = "Test the combination of script and document function with Default Settings, should fail", Pri = 0, Params = new object[] { "XsltSettings.xml", "XsltSettings3.xsl", false, false })]
        [InlineData(10, "XsltSettings.xml", "XsltSettings3.xsl", false, false)]
        //[Variation(id = 11, Desc = "Test the combination of script and document function with EnableScript, only script should work", Pri = 2, Params = new object[] { "XsltSettings.xml", "XsltSettings3.xsl", false, true })]
        [InlineData(11, "XsltSettings.xml", "XsltSettings3.xsl", false, true)]
        //[Variation(id = 12, Desc = "Test the combination of script and document function with EnableDocumentFunction, only document should work", Pri = 2, Params = new object[] { "XsltSettings.xml", "XsltSettings3.xsl", true, false })]
        [InlineData(12, "XsltSettings.xml", "XsltSettings3.xsl", true, false)]
        //[Variation(id = 13, Desc = "Test the stylesheet with no script and document function with Default Settings", Pri = 1, Params = new object[] { "XsltSettings.xml", "XsltSettings4.xsl", false, false })]
        [InlineData(13, "XsltSettings.xml", "XsltSettings4.xsl", false, false)]
        //[Variation(id = 14, Desc = "Test the stylesheet with no script and document function with TrustedXslt", Pri = 1, Params = new object[] { "XsltSettings.xml", "XsltSettings4.xsl", true, true })]
        [InlineData(14, "XsltSettings.xml", "XsltSettings4.xsl", true, true)]
        //[Variation(id = 15, Desc = "Test 1 with Default settings, should fail", Pri = 1, Params = new object[] { "XsltSettings.xml", "XsltSettings1.xsl", false, true, false, false })]
        [InlineData(15, "XsltSettings.xml", "XsltSettings1.xsl", false, true, false, false)]
        //[Variation(id = 16, Desc = "Test 2 with EnableScript override, should work", Pri = 1, Params = new object[] { "XsltSettings.xml", "XsltSettings1.xsl", false, false, false, true })]
        [InlineData(16, "XsltSettings.xml", "XsltSettings1.xsl", false, false, false, true)]
        //[Variation(id = 17, Desc = "Test 5 with Default settings override, should fail", Pri = 1, Params = new object[] { "XsltSettings.xml", "XsltSettings2.xsl", true, false, false, false })]
        [InlineData(17, "XsltSettings.xml", "XsltSettings2.xsl", true, false, false, false)]
        //[Variation(id = 18, Desc = "Test 6 with EnableDocumentFunction override, should work", Pri = 1, Params = new object[] { "XsltSettings.xml", "XsltSettings2.xsl", false, false, true, false })]
        [InlineData(18, "XsltSettings.xml", "XsltSettings2.xsl", false, false, true, false)]
        //[Variation(id = 19, Desc = "Test 9 with Default settings override, should fail", Pri = 1, Params = new object[] { "XsltSettings.xml", "XsltSettings3.xsl", true, true, false, false })]
        [InlineData(19, "XsltSettings.xml", "XsltSettings3.xsl", true, true, false, false)]
        //[Variation(id = 20, Desc = "Test 10 with TrustedXslt override, should work", Pri = 1, Params = new object[] { "XsltSettings.xml", "XsltSettings3.xsl", false, false, true, true })]
        [InlineData(20, "XsltSettings.xml", "XsltSettings3.xsl", false, false, true, true)]
        /*
         * enable all disable scripting
         * enable all disable document()
         * enable all disable none
         * enable all disable all

         */
        [Theory]
        public void XsltSettings1(object param0, object param1, object param2, object param3, object param4, object param5, object param6)
        {
            Init(param1.ToString(), param2.ToString());

            // In Proc Skip
            // XsltSettings - Debug (1-20)
            // XsltSettings - Retail (1,4,9,11,15,16,19,20)
            if (_isInProc)
            {
                if (_debug)
                    return; //TEST_SKIPPED;
                else
                {
                    switch ((int)param0)
                    {
                        case 1:
                        case 4:
                        case 9:
                        case 11:
                        case 15:
                        case 16:
                        case 19:
                        case 20:
                            return; //TEST_SKIPPED;
                        //                  default:
                        //just continue;
                    }
                }
            }

            XsltSettings xs = new XsltSettings((bool)param3, (bool)param4);
            xsl.Load(XslFile, xs, new XmlUrlResolver());

            if ((int)param0 >= 15 && (int)param0 <= 20)
            {
                xs.EnableDocumentFunction = (bool)param5;
                xs.EnableScript = (bool)param6;
                xsl.Load(XslFile, xs, new XmlUrlResolver());
            }

            try
            {
                StringWriter sw = Transform();

                switch ((int)param0)
                {
                    case 1:
                    case 4:
                    case 16:
                        VerifyResult(sw.ToString(), "30", "Unexpected result, expected 30");
                        return;

                    case 5:
                    case 8:
                    case 18:
                        VerifyResult(sw.ToString(), "7", "Unexpected result, expected 7");
                        return;

                    case 9:
                    case 20:
                        VerifyResult(sw.ToString(), "17", "Unexpected result, expected 17");
                        return;

                    case 13:
                    case 14:
                        VerifyResult(sw.ToString(), "PASS", "Unexpected result, expected PASS");
                        return;

                    default:
                        Assert.True(false);
                        return;
                }
            }
            catch (XsltException ex)
            {
                switch ((int)param0)
                {
                    case 2:
                    case 3:
                    case 6:
                    case 7:
                    case 10:
                    case 11:
                    case 12:
                    case 15:
                    case 17:
                    case 19:
                        _output.WriteLine(ex.ToString());
                        _output.WriteLine(ex.Message);
                        return;

                    default:
                        Assert.True(false);
                        return;
                }
            }
        }

        //[Variation(id = 21, Desc = "Disable Scripting and load a stylesheet which includes another sytlesheet with script block", Pri = 1, Params = new object[] { "XsltSettings.xml", "XsltSettings5.xsl", false, false })]
        [InlineData("XsltSettings.xml", "XsltSettings5.xsl", false, false)]
        //[Variation(id = 22, Desc = "Disable Scripting and load a stylesheet which imports XSLT which includes another XSLT with script block", Pri = 1, Params = new object[] { "XsltSettings.xml", "XsltSettings6.xsl", false, false })]
        [InlineData("XsltSettings.xml", "XsltSettings6.xsl", false, false)]
        //[Variation(id = 23, Desc = "Disable Scripting and load a stylesheet which has an entity expanding to a script block", Pri = 1, Params = new object[] { "XsltSettings.xml", "XsltSettings7.xsl", false, false })]
        [InlineData("XsltSettings.xml", "XsltSettings7.xsl", false, false)]
        //[Variation(id = 24, Desc = "Disable Scripting and load a stylesheet with multiple script blocks with different languages", Pri = 1, Params = new object[] { "XsltSettings.xml", "XsltSettings8.xsl", false, false })]
        [InlineData("XsltSettings.xml", "XsltSettings8.xsl", false, false)]
        [Theory]
        public void XsltSettings2(object param0, object param1, object param2, object param3)
        {
            Init(param0.ToString(), param1.ToString());

            if (_isInProc)
                if (_debug)
                    return; //TEST_SKIPPED;

            XsltSettings xs = new XsltSettings((bool)param2, (bool)param3);
            XPathDocument doc = new XPathDocument(XslFile);
            xsl.Load(doc, xs, new XmlUrlResolver());

            try
            {
                StringWriter sw = Transform();
                _output.WriteLine("Execution of the scripts was allowed even when XsltSettings.EnableScript is false");
                Assert.True(false);
            }
            catch (XsltException ex)
            {
                _output.WriteLine(ex.ToString());
                return;
            }
        }

        //[Variation(id = 25, Desc = "Disable DocumentFunction and Malicious stylesheet has document(url) opening a URL to an external system", Pri = 1, Params = new object[] { "XsltSettings.xml", "XsltSettings9.xsl", false, false })]
        [InlineData("XsltSettings.xml", "XsltSettings9.xsl", false, false)]
        //[Variation(id = 26, Desc = "Disable DocumentFunction and Malicious stylesheet has document(nodeset) opens union of all URLs referenced", Pri = 1, Params = new object[] { "XsltSettings.xml", "XsltSettings10.xsl", false, false })]
        [InlineData("XsltSettings.xml", "XsltSettings10.xsl", false, false)]
        //[Variation(id = 27, Desc = "Disable DocumentFunction and Malicious stylesheet has document(url, nodeset) nodeset is a base URL to 1st arg", Pri = 1, Params = new object[] { "XsltSettings.xml", "XsltSettings11.xsl", false, false })]
        [InlineData("XsltSettings.xml", "XsltSettings11.xsl", false, false)]
        //[Variation(id = 28, Desc = "Disable DocumentFunction and Malicious stylesheet has document(nodeset, nodeset) 2nd arg is a base URL to 1st arg", Pri = 1, Params = new object[] { "XsltSettings.xml", "XsltSettings12.xsl", false, false })]
        [InlineData("XsltSettings.xml", "XsltSettings12.xsl", false, false)]
        //[Variation(id = 29, Desc = "Disable DocumentFunction and Malicious stylesheet has document(''), no threat but just to verify if its considered", Pri = 1, Params = new object[] { "XsltSettings.xml", "XsltSettings13.xsl", false, false })]
        [InlineData("XsltSettings.xml", "XsltSettings13.xsl", false, false)]
        //[Variation(id = 30, Desc = "Disable DocumentFunction and Stylesheet includes another stylesheet with document() function", Pri = 1, Params = new object[] { "XsltSettings.xml", "XsltSettings14.xsl", false, false })]
        [InlineData("XsltSettings.xml", "XsltSettings14.xsl", false, false)]
        //[Variation(id = 31, Desc = "Disable DocumentFunction and Stylesheet has an entity reference to doc(), ENTITY s document('foo.xml')", Pri = 1, Params = new object[] { "XsltSettings.xml", "XsltSettings15.xsl", false, false })]
        [InlineData("XsltSettings.xml", "XsltSettings15.xsl", false, false)]
        [Theory]
        public void XsltSettings3(object param0, object param1, object param2, object param3)
        {
            Init(param0.ToString(), param1.ToString());

            if (_isInProc)
                if (_debug)
                    return; //TEST_SKIPPED;

            XsltSettings xs = new XsltSettings((bool)param2, (bool)param3);
            XPathDocument doc = new XPathDocument(XslFile);
            xsl.Load(doc, xs, new XmlUrlResolver());

            try
            {
                StringWriter sw = Transform();
                _output.WriteLine("Execution of the document function was allowed even when XsltSettings.EnableDocumentFunction is false");
                Assert.True(false);
            }
            catch (XsltException ex)
            {
                _output.WriteLine(ex.ToString());
                return;
            }
        }
    }
}