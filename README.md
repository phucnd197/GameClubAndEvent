A CRUD project for creating and managing Game Club and its Events

Project has the following characteristics:
- Project created using the vertical slice architecture coupled with MediatR for easier maintenance and develop new features
- Using a EFcore for database interaction
- Using SQLite as the database
- Using MediatR for communicating between components for reduced coupling
- Using Serilog for logging with configured sink to Sqlite
- Using FluentValidation for basic model validating
- Using Middleware to log and return result for un-handled exceptions

List of missing features because of time constraints:
- Update and Delete for Events and Clubs
- Unit Test project
- Authentication and Authorization for updating and deleting clubs -> creating a users database
- Create a client-side app
- Possibility of adding a mapping library in the future

How to run the project:
- Pull the source code from Github
- Open Visual studio 
- Open Package Manager Console
- Run Update-Database
- Run the API