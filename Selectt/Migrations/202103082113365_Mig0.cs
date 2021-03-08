namespace Selectt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mig0 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Polls",
                c => new
                    {
                        PollId = c.Int(nullable: false, identity: true),
                        OwnerId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        Question = c.String(nullable: false),
                        EndDateTime = c.DateTime(),
                        AreResultsPublic = c.Boolean(nullable: false),
                        WasSent = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PollId)
                .ForeignKey("dbo.AspNetUsers", t => t.OwnerId, cascadeDelete: true)
                .Index(t => t.OwnerId);
            
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        AnswerId = c.Int(nullable: false, identity: true),
                        PollId = c.Int(nullable: false),
                        PollAnswer = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AnswerId)
                .ForeignKey("dbo.Polls", t => t.PollId, cascadeDelete: true)
                .Index(t => t.PollId);
            
            CreateTable(
                "dbo.Responses",
                c => new
                    {
                        ResponseId = c.Int(nullable: false, identity: true),
                        AnswerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ResponseId)
                .ForeignKey("dbo.Answers", t => t.AnswerId, cascadeDelete: true)
                .Index(t => t.AnswerId);
            
            CreateTable(
                "dbo.Respondents",
                c => new
                    {
                        PollId = c.Int(nullable: false),
                        Email = c.String(nullable: false, maxLength: 128),
                        Token = c.String(nullable: false),
                        HasVoted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.PollId, t.Email })
                .ForeignKey("dbo.Polls", t => t.PollId, cascadeDelete: true)
                .Index(t => t.PollId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Respondents", "PollId", "dbo.Polls");
            DropForeignKey("dbo.Polls", "OwnerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Responses", "AnswerId", "dbo.Answers");
            DropForeignKey("dbo.Answers", "PollId", "dbo.Polls");
            DropIndex("dbo.Respondents", new[] { "PollId" });
            DropIndex("dbo.Responses", new[] { "AnswerId" });
            DropIndex("dbo.Answers", new[] { "PollId" });
            DropIndex("dbo.Polls", new[] { "OwnerId" });
            DropTable("dbo.Respondents");
            DropTable("dbo.Responses");
            DropTable("dbo.Answers");
            DropTable("dbo.Polls");
        }
    }
}
