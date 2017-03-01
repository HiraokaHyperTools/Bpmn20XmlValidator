# Bpmn20XmlValidator

Usage: `Bpmn20XmlValidator test.bpmn2`

Require Microsoft .NET Framework 4.0 or later.

Hints:
* ExitCode = 0 if no Error (however may contain Warnings).
* All warnings to stdout.
* All errors to stderr.
* Put your additional BPMN schema .xsd files to SCHEMA folder.

Validation error output sample:
```
C:\Users\ku_000\git\myrepo\test7\src\main\resources\myteam\test7\SequentialTest.bpmn2(25,9): Error: The element 'ioSpecification' in namespace 'http://www.omg.org/spec/BPMN/20100524/MODEL' has incomplete content. List of possible elements expected: 'inputSet, outputSet' in namespace 'http://www.omg.org/spec/BPMN/20100524/MODEL'.
C:\Users\ku_000\git\myrepo\test7\src\main\resources\myteam\test7\SequentialTest.bpmn2(47,9): Error: The element 'ioSpecification' in namespace 'http://www.omg.org/spec/BPMN/20100524/MODEL' has incomplete content. List of possible elements expected: 'inputSet, outputSet' in namespace 'http://www.omg.org/spec/BPMN/20100524/MODEL'.
```
