using System;

namespace CosmosTableSamples
{
    using System.Threading.Tasks;
    using Microsoft.Azure.Cosmos.Table;
    using Model;

    class BasicSamples
    {
        public async Task RunSamples()
        {
            Console.WriteLine("Azure Cosmos DB Table - Basic Samples\n");
            Console.WriteLine();

            string tableName = "rmgsptable" + Guid.NewGuid().ToString().Substring(0, 5);
            CloudTable table = await CreateTableAsync(tableName);
            await BasicDataOperationsAsync(table);

        }

        private static async Task BasicDataOperationsAsync(CloudTable table)
        {

            EmployeeEntity emp = new EmployeeEntity("Sapna", "patil")
            {
                EmpName = "Priya",
                EmpJob = "Teacher",
                EmpSalary = 20000,
                EmpEmailID = "piyu@gmail.com",
                EmpAddress = "Hyderabad"
            };




            // Demonstrate how to insert the entity
            Console.WriteLine("Insert an Entity.");
            emp = await InsertOrMergeEntityAsync(table, emp);
            Console.WriteLine();
        }

        public static async Task<EmployeeEntity> InsertOrMergeEntityAsync(CloudTable table, EmployeeEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            try
            {
                // Create the InsertOrReplace table operation
                TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(entity);

                // Execute the operation.
                TableResult result = await table.ExecuteAsync(insertOrMergeOperation);
                EmployeeEntity insertedEmployee = result.Result as EmployeeEntity;

                if (result.RequestCharge.HasValue)
                {
                    Console.WriteLine("Request Charge of InsertOrMerge Operation: " + result.RequestCharge);
                }

                return insertedEmployee;
            }
            catch (StorageException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
                throw;
            }
        }
        public static async Task<CloudTable> CreateTableAsync(string tableName)
        {
            string storageConnectionString = AppSettings.LoadAppSettings().StorageConnectionString;

            // Retrieve storage account information from connection string.
            CloudStorageAccount storageAccount = CreateStorageAccountFromConnectionString(storageConnectionString);

            // Create a table client for interacting with the table service
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());

            Console.WriteLine("Create a Table for the demo");

            // Create a table client for interacting with the table service 
            CloudTable table = tableClient.GetTableReference(tableName);
            if (await table.CreateIfNotExistsAsync())
            {
                Console.WriteLine("Created Table named: {0}", tableName);
            }
            else
            {
                Console.WriteLine("Table {0} already exists", tableName);
            }

            Console.WriteLine();
            return table;
        }
        public static CloudStorageAccount CreateStorageAccountFromConnectionString(string storageConnectionString)
        {
            CloudStorageAccount storageAccount;
            try
            {
                storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the application.");
                throw;
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
                Console.ReadLine();
                throw;
            }

            return storageAccount;
        }
    }
}
