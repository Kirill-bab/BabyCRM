IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'MainDb')
BEGIN
  CREATE DATABASE MainDb;
END;