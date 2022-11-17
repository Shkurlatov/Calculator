# Calculator

The repository contains my work on self-completion of a study Task 
while taking specialized online courses for training C# developers.

An experienced Mentor checked the result and made his remarks on 
the quality of the work performed. The Task could not be completed 
until the Mentor decided that the result was up to industry standards.

The commit called “First implementation of the Task” is my original 
implementation, without any hints. All subsequent commits (if any) 
are the results of my attempts to solve Mentor's remarks and his 
suggestions for improvement the work.

According to the conditions of the school, the Mentor does not provide 
ways to solve shortcomings and sources of information. The search for 
the necessary educational information was carried out independently.
<br/><br/>

## Task Conditions

Implement two modes for application "Calculator":

__1.__ Users work in a console application with simple operations 
(whithout brackets). Operations should be executed with math priority (*/+-).

__Examples:__
* _Input_ `"2+2*3"` => _Output_ `"8"`
* _Input_ `"2/0"` => _Output_ `"Exception. Divide by zero."`
<br/><br/>

__2.__ Work with files. Application read data from file line by line. 
Implement calculation with brackets. Each line should be calculated and 
the result written to a different file.

__Example:__

* _Input file content_<br/>
  <pre>
  1+2*(3+2)<br/>
  1+x+4<br/>
  2+15/3+4*2
  </pre>
<br/>

* _Output file content_<br/>
  <pre>
  1+2*(3+2) = 11<br/>
  1+x+4 = Exception. Wrong input.<br/>
  2+15/3+4*2 = 15
  </pre>
<br/>

__Noties:__ Implementing parsing, calculation and math operation priorities 
without using third party libraries or components, which returns calculation 
result (like DataTable.Compute etc).
<br/><br/>

## Solution Structure

📁Converter<br/>
┣ 📁ConverterLibrary<br/>
┃ &nbsp;┣ 📄FileConverter.cs<br/>
┃ &nbsp;┣ 📄Filter.cs<br/>
┃ &nbsp;┗ 📄InputConverter.cs<br/>
┗ 📁ConverterLibrary.Tests<br/>
&nbsp; &nbsp; ┣ 📄FileConverterTests.cs<br/>
&nbsp; &nbsp; ┗ 📄InputConverterTests.cs<br/>
📁MathUnits<br/>
┗ 📁MathUnitsLibrary<br/>
&nbsp; &nbsp; ┣ 📄MathMember.cs<br/>
&nbsp; &nbsp; ┣ 📄MathOperation.cs<br/>
&nbsp; &nbsp; ┗ 📄PriorityAttribute.cs<br/>
📁Processor<br/>
┣ 📁ProcessorLibrary<br/>
┃ &nbsp;┗ 📄Processor.cs<br/>
┗ 📁ProcessorLibrary.Tests<br/>
&nbsp; &nbsp; ┗ 📄ProcessorTests.cs<br/>
__📁NumberProject__<br/>
&nbsp;┣ 📄FileHandling.cs<br/>
&nbsp;┣ 📄Program.cs<br/>
&nbsp;┗ 📄UserConsole.cs
<br/><br/>

## Prerequisites

Microsoft Visual Studio 2019 or newer

* Workloads<br/>
    * ASP.NET and web development

- Individual components<br/>
    - .NET Core 3.1 Runtime (LTS) 
<br/><br/>

## Getting Started

Clone the remote repository on your local machine.<br/>
`$ git clone https://github.com/Shkurlatov/Calculator.git`
<br/><br/>
Go to the project directory.<br/>
`$ cd Calculator`
<br/><br/>
Open project solution in Microsoft Visual Studio.<br/>
`$ start Task5.sln`
<br/><br/>
There are two suggested ways to use the program:<br/>
* Press <kbd>Ctrl</kbd>+<kbd>F5</kbd> to run the application.<br/>
Use the application following directions in console window.<br/><br/>
* Press <kbd>Ctrl</kbd>+<kbd>`</kbd> to open an integrated Terminal.<br/>
In the Terminal enter the following command, including the full path to the content file:<br/>
    <pre>dotnet run --project CalculatorApp <kbd>path</kbd></pre>

Press <kbd>Ctrl</kbd>+<kbd>R</kbd>,<kbd>A</kbd> to run tests.<br/>
* If a System.DivideByZeroException warning is received during the test, 
expand the Exception Settings and check the ProcessorLibrary.dll box. 
Then click the Continue button in the IDE interface. The Exception is
handled in the calling class and this warning should not appear in the 
rerun of the test.
<br/><br/>


## Usage Example

__1.__ An example of user input to the console:
<br/>
<pre>
Please, enter your math expression or type "exit" to end the program
- 1 - 2 + 4 / 1 * 2 + 1
- 1 - 2 + 4 / 1 * 2 + 1 = 6

Please, enter your math expression or type "exit" to end the program
4 - 2 / 2 + 8 / 4 * 5 * 3
4 - 2 / 2 + 8 / 4 * 5 * 3 = 33

Please, enter your math expression or type "exit" to end the program
2 ^ 3
2 ^ 3 = 8

Please, enter your math expression or type "exit" to end the program
2 ^ - 3
2 ^ - 3 = 0.125

Please, enter your math expression or type "exit" to end the program
2 ^ - 3 * + 4
2 ^ - 3 * + 4 = 0.5

Please, enter your math expression or type "exit" to end the program
2/0
2/0 = The result is not achievable, division by zero occured

Please, enter your math expression or type "exit" to end the program
x\*y
x\*y = Invalid expression format, the processing ended unsuccessefully

Please, enter your math expression or type "exit" to end the program
* 1 + 2
* 1 + 2 = Wrong math operator at the begining of the string

Please, enter your math expression or type "exit" to end the program
1 + 2 +
1 + 2 + = The math operator at the end of the string

Please, enter your math expression or type "exit" to end the program
+ + 1 + 2
+ + 1 + 2 = Two math operators at the begining of the string

Please, enter your math expression or type "exit" to end the program
1 + * 2
1 + * 2 = Two not acceptable math operators in a row

Please, enter your math expression or type "exit" to end the program
1 + + + 2
1 + + + 2 = Three math operators in a row

Please, enter your math expression or type "exit" to end the program
exit

Press any key to close this window . . .
</pre>
<br/>

__2.__ The application reads and processes data from a content file:
<br/>
<pre>
The processing results:

2++2 = 4
2*+2 = 4
(+2)+3 = 5
- (- 1 - (2 + 4) / 1 * (2 + 1)) = 19
(((4 - 2) / 2) + (8 / 4 * (5 * 3))) = 31
16 - 2 * 2 ^ (1 + 2) = 0
2^-(3-4)+2 = 4
16 - 2 * - 2 ^ - (1 + 2) = 16.25
 =
* 1 + 2 = Wrong math operator at the begining of the string
1 + 2 + = The math operator at the end of the string
+ + 1 + 2 = Two math operators at the begining of the string
1 + * 2 = Two not acceptable math operators in a row
1 + + + 2 = Three math operators in a row
)1 + 2) = Closing brace at the begining of the string
(1 + 2( = Opening brace at the end of the string
(1 + 2 +) = The math operator before the closing brace
(* 1 + 2) = Wrong math operator after the opening brace
(+ + 1 + 2) = Two math operators after the opening brace
( ) = Missing value inside braces
(1 + 2) 3 = Missing math operator after the closing brace
1 (2 + 3) = Missing math operator before the opening brace
</pre>