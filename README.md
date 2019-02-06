### Road Status

##### Building the application
This is a Visual Studio 2017 project. Once opened in Visual Studio, the application can be built by simply selecting the _Build Solution_ option from the  _Build_ menu.

##### Executing the application
By default, the executable file will be located in the `/RoadStatus/RoadStatus/bin/Debug/` or `./RoadStatus/RoadStatus/bin/Release/` folder, depending on the build configuration selected.

The application can be executed either from a _Command Prompt_ or _PowerShell_ window. Change to the executable folder and enter the application name and road designator -

    .\RoadStatus.exe A3

From a _PowerShell_ window the exit status can be checked using -

    echo $lastexitcode

##### Providing a licence key
A license key is required to run the application. This should be stored in a file call `devKey.json`, located in the same folder as the executable file. This is a text file in the _JSON_ format. A dummy file is provided that will need editing before executing the application.

The contents of the file are -

    {
      "appId": "<app_id>",
      "appKey": "<app_key>"
    }

The `<app_id>` and `<app_key>` placeholders should be updated with valid key values.

##### Unit testing
A unit test project is included in this solution. These tests can be executed from within Visual Studio by selecting _Test --> Run --> All Tests_ from the menu.

These tests check for the existence of the license key file, the status returned when providing a valid or invalid road name, and an incorrect API URL.
