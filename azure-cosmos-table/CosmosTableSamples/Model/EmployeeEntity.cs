using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Microsoft.Azure.Cosmos.Table;
using Newtonsoft.Json.Serialization;

namespace CosmosTableSamples.Model
{
    class EmployeeEntity:TableEntity
    {

        public EmployeeEntity(string partitionkey,string rowkey)
        {
            PartitionKey = partitionkey;
            RowKey = rowkey;
        }

        public string EmpName { get; set; }
        public string EmpJob{ get; set; }
        public decimal EmpSalary{ get; set; }
        public string EmpEmailID { get; set; }
        public string EmpAddress { get; set; }

    }
}
