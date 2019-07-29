namespace StockDataBL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class wolumentypefix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.dane_gieldowe", "wolumen", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.dane_gieldowe", "wolumen", c => c.Int(nullable: false));
        }
    }
}
