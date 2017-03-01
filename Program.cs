using Bpmn20XmlValidator.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace Bpmn20XmlValidator {
    class Program {
        static void Main(string[] args) {
            if (args.Length < 1) {
                Console.Error.WriteLine("Bpmn20XmlValidator test.bpmn2");
                Environment.ExitCode = 1;
                return;
            }
            using (var fs = File.OpenRead(args[0])) {
                var readerSettings = new XmlReaderSettings();
                readerSettings.Schemas.Add(XmlSchema.Read(new StringReader(Resources.BPMN20), null));
                readerSettings.Schemas.Add(XmlSchema.Read(new StringReader(Resources.BPMNDI), null));
                readerSettings.Schemas.Add(XmlSchema.Read(new StringReader(Resources.DC), null));
                readerSettings.Schemas.Add(XmlSchema.Read(new StringReader(Resources.DI), null));
                readerSettings.Schemas.Add(XmlSchema.Read(new StringReader(Resources.Semantic), null));
                readerSettings.ValidationType = ValidationType.Schema;
                readerSettings.ValidationEventHandler += (sender, e) => {
                    var lineInfo = (IXmlLineInfo)sender;
                    Console.Error.WriteLine("{0}({1},{2}): {3}: {4}"
                        , fs.Name
                        , lineInfo.LineNumber, lineInfo.LinePosition, e.Severity, e.Message);
                    Environment.ExitCode = 1;
                };
                using (var reader = XmlReader.Create(fs, readerSettings)) {
                    while (reader.Read()) {

                    }
                }
            }
        }
    }
}
