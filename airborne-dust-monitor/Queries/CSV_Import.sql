BULK INSERT TestHumidity
FROM 'C:\Users\nagyn\Desktop\rawData21Sept\2.csv'
WITH (
    FORMAT = 'CSV',
    FIELDTERMINATOR = ',',  -- specify the field terminator
    ROWTERMINATOR = '0x0A',    -- specify the row terminator
    FIRSTROW = 2            -- specify the first row of data (if necessary)
);