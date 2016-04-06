# <img src="https://raw.githubusercontent.com/rahulpnath/clal/master/Resources/CLAL.png" width="48">   Command Line Application Launcher      

[![Build status](https://ci.appveyor.com/api/projects/status/wdy4hj72r2k5tsuc/branch/master?svg=true)](https://ci.appveyor.com/project/rahulpnath/clal/branch/master)


Work in progress.

CLAL (Command Line Application Launcher) is a desktop application to launch any (currently supports only SQL Server Management Studio - ssms) command line application. It helps manage different configurations with which a command line application can be launched - such as different connection strings to various databases.

Install the latest version here

CLAL allows you to first specify the meta data of the command line application first and then create the various configurations for that by filling in the parameters as specified in the metadata. Currently since this only supports ssms, the metadata edit screen is not present and is hard coded into the application. The image below shows the various database servers that I connect to, and CLAL helps me reach them quickly

![](http://www.rahulpnath.com/images/clal.png)

Use the ‘Add Configuration’ button to add a new configuration. You can specify a Friendly Name for the configuration and then fill in the other details required by the command line. Alternatively for ssms you can also paste in a connection string and have all the fields automatically populated.

![](http://www.rahulpnath.com/images/clal_new.png)

Once new configuration is saved you can launch the application with the specified configuration either using the Launch button or double click on the configuration name in the list.

Work in progress to support other command line applications and to update the look and feel.
Contribute to the development by coding or reporting issues that you find file using the application. Check out these articles for my learnings while building this application.

Credits

Icons/Logo: Rosh TS
 
