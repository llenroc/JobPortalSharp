namespace JobPortalSharp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employers", "StateId", c => c.Int(nullable: false, defaultValue: 1));
            CreateIndex("dbo.Employers", "StateId");
            AddForeignKey("dbo.Employers", "StateId", "dbo.States", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employers", "StateId", "dbo.States");
            DropIndex("dbo.Employers", new[] { "StateId" });
            DropColumn("dbo.Employers", "StateId");
        }
    }
}
