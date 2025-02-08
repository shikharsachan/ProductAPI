Clone the repository on your local machine.
Open the csproj file in the visual studio.
Build the solution before proceeding ahead.
Open AppSettings.Json - Update ConnectionString with ServerName, UserName and Password.
Once build is complete, open the Package Manager Console to run the migration 
1) Add-Migration "DB_Setup" - Once migration file is created, it will be reflected in the repository. You can go through it.
2) Run - Update-Database command to create the DB as per the migration file.
3) After executing both the commands, DB should be created that user can validate.
Host the application - Swagger UI will appear with 7 endpoints.
![image](https://github.com/user-attachments/assets/c0ab7321-24c9-4c15-b636-2c33693e2bd3)
Play around with the endpoints and validate the DB.
UTC has been added that user can validate.
