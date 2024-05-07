namespace DASH_BOOKING.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStatusToEvents : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "Status");
        }
    }
}
