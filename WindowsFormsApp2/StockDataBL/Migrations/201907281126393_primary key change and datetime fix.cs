namespace StockDataBL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class primarykeychangeanddatetimefix : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.dane_gieldowe");
            AlterColumn("dbo.dane_gieldowe", "nazwa", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.dane_gieldowe", "data", c => c.DateTime(nullable: false));
            AlterColumn("dbo.dane_gieldowe", "wolumen", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.dane_gieldowe", new[] { "nazwa", "data" });
            DropColumn("dbo.dane_gieldowe", "id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.dane_gieldowe", "id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.dane_gieldowe");
            AlterColumn("dbo.dane_gieldowe", "wolumen", c => c.Double(nullable: false));
            AlterColumn("dbo.dane_gieldowe", "data", c => c.Double(nullable: false));
            AlterColumn("dbo.dane_gieldowe", "nazwa", c => c.String(maxLength: 50, unicode: false));
            AddPrimaryKey("dbo.dane_gieldowe", "id");
        }
    }
}
