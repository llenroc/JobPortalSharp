namespace JobPortalSharp.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employers", "EmployerTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Employers", "EmployerTypeId");
            AddForeignKey("dbo.Employers", "EmployerTypeId", "dbo.EmployerTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employers", "EmployerTypeId", "dbo.EmployerTypes");
            DropIndex("dbo.Employers", new[] { "EmployerTypeId" });
            DropColumn("dbo.Employers", "EmployerTypeId");
        }
    }
}
