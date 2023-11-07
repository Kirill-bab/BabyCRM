IF EXISTS (SELECT * FROM sys.databases WHERE name = 'MainDb')
BEGIN
  DROP DATABASE MainDb;
END;