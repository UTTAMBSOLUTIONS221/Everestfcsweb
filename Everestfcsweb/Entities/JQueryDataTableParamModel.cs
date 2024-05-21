namespace Everestfcsweb.Entities
{
    public class JQueryDataTableParamModel
    {
        // Draw counter for DataTables
        public string? draw { get; set; }

        // Global search value
        public string? searchValue { get; set; }

        // Number of records to fetch
        public int length { get; set; }

        // Start index for the records to fetch
        public int start { get; set; }

        // Total number of records in the database (for DataTables)
        public int totalRecords { get; set; }

        // Total number of filtered records in the database (for DataTables)
        public int filteredRecords { get; set; }

        // Comma-separated list of column names
        public string? sColumns { get; set; }

        // Custom property for ShiftId
        public int ShiftId { get; set; }

        // Array of column definitions
        public Column[]? columns { get; set; }

        // Array of order definitions
        public Order[]? order { get; set; }

        // Nested class for column definitions
        public class Column
        {
            public string? data { get; set; }
            public string? name { get; set; }
            public bool searchable { get; set; }
            public bool orderable { get; set; }
            public Search? search { get; set; }
        }

        // Nested class for order definitions
        public class Order
        {
            public int column { get; set; }
            public string? dir { get; set; }
        }

        // Nested class for search definitions
        public class Search
        {
            public string? value { get; set; }
            public bool regex { get; set; }
        }
    }

}
