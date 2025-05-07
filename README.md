# Million Real State Assessment

## Property Management Project

This project is an application for managing real estate properties, allowing you to search and filter properties, and display specific details about each property.

## Technologies

- **Frontend**: React, TypeScript, CSS  
- **Backend**: .NET 8, C#, Clean Architecture  
- **Database**: MongoDB  
- **Testing**: NUnit, Moq  

## Installation

### Clone this repository:

```bash
git clone https://github.com/jgarciadev12/MillionRealState.git
```

---

## Backend Setup

1. Make sure MongoDB is running locally.
2. Configure the connection string and database name in `appsettings.json`:

```json
"MongoDB": {
  "ConnectionString": "mongodb://localhost:27017",
  "DatabaseName": "MillionRealState"
}
```

3. Open the solution in Visual Studio or your preferred IDE.
4. Set the startup project to `Million.RealState.WebAPI`.
5. Run the project.

---

## Frontend Setup

1. Navigate to the `reactapp` folder:

```bash
cd reactapp
```

2. Install dependencies:

```bash
npm install
```

3. Update the `API_URL` in `src/services/propertyService.ts` to match your backend endpoint, e.g.:

```ts
const API_URL = "http://localhost:5000/api/property";
```

4. Start the React development server:

```bash
npm run dev
```

---

## Usage

- Visit `http://localhost:5173` in your browser to use the application.  
- Use the filters on the main page to search properties by name, address, or price.  
- Click on a property to view more detailed information.

---

## Testing

### Backend

To run unit tests:

```bash
dotnet test
```
