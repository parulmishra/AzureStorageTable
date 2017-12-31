using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureTable
{
    class Program
    {
        static void Main(string[] args)
        {
            string storageconnection = System.Configuration.ConfigurationManager.AppSettings.Get("StorageConnectionString");
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageconnection);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            
            //Create a table named FirstTestTable
            CloudTable table = tableClient.GetTableReference("FirstTestTable");
            table.CreateIfNotExists();

            //Make CarEntity objects and insert in the table
            /*
            CarEntity newCar = new CarEntity(01, 2017, "BMW", "X1", "Black");
            TableOperation insert = TableOperation.Insert(newCar);
            table.Execute(insert);
            newCar = new CarEntity(2, 2017, "Audi", "Q5", "White");
            insert = TableOperation.Insert(newCar);
            table.Execute(insert);
            newCar = new CarEntity(3, 2017, "Mercedes", "A1", "Black");
            insert = TableOperation.Insert(newCar);
            table.Execute(insert);
            newCar = new CarEntity(4, 2017, "Audi", "Q1", "Red");
            insert = TableOperation.Insert(newCar);
            table.Execute(insert);
            newCar = new CarEntity(5, 2017, "Tesla", "X5", "Silver");
            insert = TableOperation.Insert(newCar);
            table.Execute(insert);
            */

            //Retrive objects from the table using partition key and rowkey 
            /*TableOperation retrieve = TableOperation.Retrieve<CarEntity>("car", "1");
            TableResult result = table.Execute(retrieve);
            if (result.Result == null)
            {
                Console.WriteLine("result not found");
            }
            else
            {
                Console.WriteLine("the car found is " + ((CarEntity)result.Result).Make + " model is " + ((CarEntity)result.Result).Model);
            }

            */
            //using batch opeartion

            /*
            TableBatchOperation tbo = new TableBatchOperation();
            CarEntity newCar = new CarEntity(1, 2017, "BMW", "X1", "Black");
            tbo.Insert(newCar);
            newCar = new CarEntity(2, 2017, "Audi", "Q5", "White");
            tbo.Insert(newCar);
            newCar = new CarEntity(3, 2017, "Mercedes", "A1", "Red");
            tbo.Insert(newCar);
            table.ExecuteBatch(tbo);
            */


            TableQuery<CarEntity> query = new TableQuery<CarEntity>();
            foreach(CarEntity thisCar in table.ExecuteQuery(query))
            {
                Console.WriteLine(thisCar.Year.ToString() + " " + thisCar.Make + " " + thisCar.Model);
            }
            Console.ReadKey();
        }
    }

    class CarEntity : TableEntity
    {
        public CarEntity(int id, int year, string make, string model, string color)
        {
            this.UniqueId = id;
            this.Year = year;
            this.Make = make;
            this.Model = model;
            this.Color = color;
            this.PartitionKey = "car";
            this.RowKey = id.ToString();
        }
        public CarEntity() {}
        public int UniqueId { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
    }
}
