namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetBirthdateOfCustomer : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE Customers SET Birthdate = 1/1/1995 WHERE Id = 4");
            
        }
        
        public override void Down()
        {
        }
    }
}
