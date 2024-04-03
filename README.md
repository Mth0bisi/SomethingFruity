# ser-base-setup
Branches :
-Master : PostgreSQL packages, Hangfire
-release-mssql-server-version : Microsoft SQL Server packages, Hangfire SQL Server

Styling based of mdbootstrap 5, bootstrap 5 and template Adminkit v2.2.0 Free version https://demo.adminkit.io/

Contains 2 Layout versions, AdminKit dashboard (default) and Material Boostrap Dashboard.
To switch to MB Layout, update the ViewStart.cs file from "_Layout" to "_LayoutMB"
Also, update _LoginPartial.cs by uncommenting all the commented out code and commenting out the live code.

To run application:
-Update the ConnectionString to your PostgreSQL database and Add in the ADAuthentication Password in the appsettings.Development file
-Run migration via Package Manager Console, "update-database" to generate database and tables.
-Update the user details in CreateRoles function within Program.cs
-Start Application.
