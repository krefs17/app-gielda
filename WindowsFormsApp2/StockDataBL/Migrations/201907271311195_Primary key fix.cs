namespace StockDataBL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Primarykeyfix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.dane_gieldowe", "id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.dane_gieldowe", "nazwa", c => c.String(maxLength: 50, unicode: false));
            AddPrimaryKey("dbo.dane_gieldowe", "id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.dane_gieldowe");
            AlterColumn("dbo.dane_gieldowe", "nazwa", c => c.String(nullable: false, maxLength: 50, unicode: false));
            DropColumn("dbo.dane_gieldowe", "id");
        }
    }
}
