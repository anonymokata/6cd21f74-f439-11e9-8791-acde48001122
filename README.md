# WordSearchKata
This kata is an exercise in test-driven development to write a class capable of finding words in a word search puzzle.

To build the solution, simply run "build.bat" or run the following command:

`<Path to MsBuild.exe> WordSearchKata.sln /p:Configuration=Release`

The default `<Path to MsBuild.exe>` specified in "build.bat" assumes Visual Studio 2019 Enterprise is installed: 

>"C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\MsBuild.exe"


To run the tests, either run "run_tests.bat" or enter the following command:

`<Path to vstest.console.exe> WordSearchKataTests\bin\Release\WordSearchKataTests.dll`

The default `<Path to vstest.console.exe>` specified in "run_tests.bat" assumes Visual Studio 2019 Enterprise is installed: 

>"C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\Common7\IDE\Extensions\TestPlatform\vstest.console.exe"

If a different version of Visual Studio is installed, the paths to "MsBuild.exe" and "vstest.console.exe" must be modified to point to their proper respective locations.


Running the tests will generate the file "output.txt" in "WordSearchKataTests\bin\Release", containing the word search puzzle itself, along with the locations of the found words. Feel free to view Solutions.png to verify that all words are found at the proper locations, and that words are indeed being detected in all eight axes.

*Solutions.png*
![Highlighted solution](/Solutions.png)
