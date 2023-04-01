# About

Mini Mongo DB C# Proof of Concept project. Note that design and splitting of code and UX and etc. has been skipped in this project.

# Requirements

- Installed and running Mongo Db 6.0.5 or later (Community Server will do). Download link: https://www.mongodb.com/try/download/community
- A database called "shop" (or change the constant in Constants.cs)
- A collection called "product" in database "shop" (or change the constant in Constants.cs)

# Recommended

## Database records

At least the following 2 records in database "shop", collection: "product":

```json
[{
  "_id": {
    "$oid": "6428276cb1050373fdbbbf17"
  },
  "name": "Iron plates",
  "quantity": 4,
  "price": 9.75
},{
  "_id": {
    "$oid": "64282772b1050373fdbbbf18"
  },
  "name": "Wood planks",
  "quantity": 4,
  "price": 15.73,
  "description": "Wooden planks, 1kg each."
}]
```

Note that you can use Compass to import the above collection.

## Compass

Compass for debugging and/or easier administration. This should be an additional option during the Mongo DB installation process.

# MongoDB Shell

MongoDB Shell for debugging and/or easier administration. MongoDB Shell can be found here: https://www.mongodb.com/try/download/shell.