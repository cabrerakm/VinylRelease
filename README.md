# VinylReleaseAPI

## Original Idea
The original idea was to create a few tables for a VinylRelease Database that allows users to find and add new releases for vinyls, where to buy them, and the price. 

### Deviation
Because I had some issues with starting the project and wanted to have a minimum viable product, I simplified from the many tables I originally wanted down to three tables: two object tables (Artist, Master) and one relationship table (ArtistMaster). This simplified the project and amount of endpoints needed. I will be adding more tables and endpoints in the future.

# Endpoints
## Artist
### `GET api/Artists`
**Description**: Returns all the Artists in the table. Will not return a list of Masters for each Artist. To obtain a list of Masters for an Artist, an API request to `GET api/Artists/{artistId}` should be made. 

**Request Body Requirements**: None

**StatusCodes**:
- `200`   Successful fetch of Artists

<details>
<summary><b>Sample Reponse Body</b>:</summary>

```json
{
  "statusCode": 200,
  "statusDescription": "Successfully fetched all artists",
  "data": [
    {
      "artistId": 1,
      "artistName": "Turnstile",
      "artistDesc": "Hardcore band from Baltimore/DC/Ohio area, USA",
      "masters": []
    },
    {
      "artistId": 2,
      "artistName": "Beyonce",
      "artistDesc": "American singer, songwriter, actress and cultural icon",
      "masters": []
    },
    {
      "artistId": 3,
      "artistName": "Viagra Boys",
      "artistDesc": "Swedish post-punk band from Stockholm",
      "masters": []
    }
  ]
}
```
</details>


### `GET api/Artists/{artistId}`
**Description**: Returns the Artist along with a list of Masters from the Artist using the artistId.

**Request Body Requirements**: None

**StatusCodes**:
- `200`   Successful fetch of artist
- `404`   Artist not found

<details>
<summary><b>Sample Reponse Body</b>:</summary>

```json
{
  "statusCode": 200,
  "statusDescription": "Successful fetch of artist",
  "data": [
    {
      "artistId": 1,
      "artistName": "Turnstile",
      "artistDesc": "Hardcore band from Baltimore/DC/Ohio area, USA",
      "masters": [
        {
          "masterId": 5,
          "masterName": "Glow On",
          "masterYear": 2021,
          "artists": []
        },
        {
          "masterId": 6,
          "masterName": "Time & Space",
          "masterYear": 2018,
          "artists": []
        },
        {
          "masterId": 7,
          "masterName": "Nonstop Feeling",
          "masterYear": 2016,
          "artists": []
        },
        {
          "masterId": 8,
          "masterName": "Step to Rhythm",
          "masterYear": 2013,
          "artists": []
        }
      ]
    }
  ]
}
```
</details>

### `POST api/Artists`
**Description**: Submit an Artist. Returns back the entry in the database.

**Request Body Requirements**: Do not provide Artist Id; Needs to have Artist name. Artist description is optional. At this moment, can provide Masters for Artist but this will be removed later.

<details>
<summary><b>Sample Request Body</b>:</summary>

```json
{
  "artistName": "Idles",
  "artistDesc": " British heavy indie/punk band from Bristol, London, and Newport."
}
```
</details>

**StatusCodes**:
- `201`   Successfully added the Artist
- `400`   Bad request; please check request body

<details>
<summary><b>Sample Reponse Body</b>:</summary>

```json
{
  "statusCode": 201,
  "statusDescription": "Added the artist",
  "data": [
    {
      "artistId": 13,
      "artistName": "Idles",
      "artistDesc": " British heavy indie/punk band from Bristol, London, and Newport.",
      "masters": []
    }
  ]
}
```
</details>

### `DELETE api/Artists/{artist_id}`
**Description**: Deletes an Artist using the artistId

**Request Body Requirements**: None

**StatusCodes**:
- `200` Artist successfully deleted
- `404` Artist not found

<details>
<summary><b>Sample Reponse Body</b>:</summary>

```json
{
  "statusCode": 200,
  "statusDescription": "Artist deleted",
  "data": null
}
```
</details>

# Issues Encountered During Development
While coding this project, I came across a few issues that had to be addressed before continuing.

## Dependency Conflicts
One of the issues was dependency conflicts between the Pomelo.MySQL and the Microsoft.EntityFramework packages. I wasn't able to resolve the conflicts so I decided to switch from using MySQL Server to SQL Server. This database migration was beneficial since SQL Server is from Microsoft so everything from the code to the database would all be in-house and will most likely not lead to any dependency conflicts any packages from Microsoft.

## Starting and Accessing SQL Server
The next issue was starting the SQL server and accessing it. I realised that I needed to have Administrator privileges in order to start the SQL server and to access it in the Management Studio.

## Connecting to Database and Creating DbContext
Another issue was connecting the database to the code and creating the DbContext. I found a Visual Studio Extention called EF Core Power Tools that helped me streamline the DbContext creation.

## Many-to-Many Relationship Table Not a Model
Finally, I had trouble with my models. In the DB, I have a many-to-many relationship table, but the auto generated models only created the two object tables as models. I thought that the intermediate table also needed to be a model if I wanted to create queries that depend on the relationship between the two objects. However, through the documentation and Stack Overflow, I learned that C# is able to detect the many-to-many relationship without needing the intermediate table as a model.

