namespace HelpDeskN.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CHEEEESE : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Incidencias",
                c => new
                    {
                        IdIncidencia = c.Int(nullable: false, identity: true),
                        DataAltaIncidencia = c.DateTime(nullable: false),
                        DescripcioCurta = c.String(),
                        IdTecnicQueObreLaIncidencia = c.Int(nullable: false),
                        IdTecnicQueTancaLaIncidencia = c.Int(),
                    })
                .PrimaryKey(t => t.IdIncidencia)
                .ForeignKey("dbo.Tecnics", t => t.IdTecnicQueObreLaIncidencia)
                .ForeignKey("dbo.Tecnics", t => t.IdTecnicQueTancaLaIncidencia)
                .Index(t => t.IdTecnicQueObreLaIncidencia)
                .Index(t => t.IdTecnicQueTancaLaIncidencia);
            
            CreateTable(
                "dbo.Tecnics",
                c => new
                    {
                        IdTecnic = c.Int(nullable: false, identity: true),
                        NomTecnic = c.String(maxLength: 70),
                        EsActiu = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdTecnic);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Incidencias", "IdTecnicQueTancaLaIncidencia", "dbo.Tecnics");
            DropForeignKey("dbo.Incidencias", "IdTecnicQueObreLaIncidencia", "dbo.Tecnics");
            DropIndex("dbo.Incidencias", new[] { "IdTecnicQueTancaLaIncidencia" });
            DropIndex("dbo.Incidencias", new[] { "IdTecnicQueObreLaIncidencia" });
            DropTable("dbo.Tecnics");
            DropTable("dbo.Incidencias");
        }
    }
}
