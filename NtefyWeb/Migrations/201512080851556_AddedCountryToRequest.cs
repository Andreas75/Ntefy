namespace NtefyWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCountryToRequest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Request", "Country", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Request", "Country");
        }
    }
}
