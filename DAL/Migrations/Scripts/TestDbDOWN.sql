IF EXISTS (SELECT * FROM sys.databases WHERE name = 'Test')
BEGIN
  DROP DATABASE Test;
END;