# ATTS
Interview ATTS, temporary repository

##### Steps to run/debug the application

1. Create an MSSQL Table with the script in Database\DBInit.sql

2. Set connection string for the database what you want to use in the application.

  **Location:** ATTS.View\App.config
 
  `<add name="DatabaseToStoreTransactions" connectionString="[your connection string]"/>`

3. Enjoy!

##### Test file

You can find a big test file in CSV format. It contains 1million randomly generated rows. Good for performance testing and with some tweaking also for testing.
  
   **Location:** TestFiles\Transactions - 1m.csv
