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
            String currentXsd = null;
            var readerSettings = new XmlReaderSettings();
            readerSettings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
            readerSettings.ValidationType = ValidationType.Schema;
            readerSettings.ValidationEventHandler += (sender, e) => {
                if (e.Severity == XmlSeverityType.Warning) {
                    Console.WriteLine("{0}({1},{2}): {3}: {4}"
                        , currentXsd
                        , e.Exception.LineNumber, e.Exception.LinePosition, e.Severity, e.Message);
                }
                else {
                    Console.Error.WriteLine("{0}({1},{2}): {3}: {4}"
                        , currentXsd
                        , e.Exception.LineNumber, e.Exception.LinePosition, e.Severity, e.Message);
                    Environment.ExitCode = 1;
                }
            };
            String schemaDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SCHEMA");
            if (Directory.Exists(schemaDir)) {
                foreach (var fpxsd in Directory.GetFiles(schemaDir, "*.xsd", SearchOption.AllDirectories)) {
                    using (var fsxsd = File.OpenRead(fpxsd)) {
                        currentXsd = fpxsd;
                        readerSettings.Schemas.Add(XmlSchema.Read(fsxsd, (sender, e) => {
                            Console.Error.WriteLine("{0}({1},{2}): {3}: {4}"
                                , currentXsd
                                , e.Exception.LineNumber, e.Exception.LinePosition, e.Severity, e.Message);
                            Environment.ExitCode = 1;
                        }));
                    }
                }
            }
            using (var fs = File.OpenRead(args[0])) {
                currentXsd = fs.Name;
                using (var reader = XmlReader.Create(fs, readerSettings)) {
                    while (reader.Read()) {

                    }
                }
            }
        }
    }
}
