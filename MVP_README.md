# Group1hospitalproject

## MVP README
##### This is a minimum viable product (MVP) that serves as a prototype for a larger project. It is a simple, stripped-down version of a product that has just enough features to be useful to early adopters and get feedback on.

### Getting Started
To use this MVP, follow these steps:
- Clone the repository
- Install any necessary dependencies
- Run the app

### MVP Features
below CRUD functions are built for following entities :
[Job, Application]
- Listed entities in your database.
- Found an individual entity in the database by its ID.
- Created a new entity in the database given POST input.
- Updated an entity in the database given POST input and an ID.
- Deleted an entity in the database given an ID.
- Listed associated records given an entity ID.
- Added a new association to a record.
- Deleted an association from a record.
- Built a Controller, Views, and ViewModels which rendered the following pages:
  - Listing all Entities
  - Showing an entity (and entities related to it)
  - Creating a new entity (and creating associations to other entities)
  - Updating an entity (and updating associations to other entities)
  - Deleting an entity

- CRUD for Doctor and Department has been made, views will be available soon.

[ParkingSchedule/Cars/Spots]
- CRUD for all three tables including relational display and data.
- Creating and Updating a Car allows dropdown selection for a related Doctors.
- Creating and Updating a schedule allows dropdown selection for both a Spot and a Car.
- Views for all functions, with necessary view models created.
- Cars display a list of all parking bookings related to them.


### Future Development
- Separate admin page and client page
- Pagination for parking schedule

### Contributing
- Ka Wing Chan - Doctors - doctors/department, CSS for nav menu animation, homepage design, showing doctors in department.
- Andrew Barker - Parking - parkingSchedule/cars/spots, car/booking relational list. Nav dropdown menu and links. A little home page styling.
- Gahee Choi - Career - jobs/applications, CSS styling, API document
- Lukmon Lasisi - Appointments - patient/appointment
