namespace DLL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class muqeet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PreVisitors", "hcontactNo", c => c.String());
            AddColumn("dbo.PreVisitors", "hemail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PreVisitors", "hemail");
            DropColumn("dbo.PreVisitors", "hcontactNo");
        }
    }
}
