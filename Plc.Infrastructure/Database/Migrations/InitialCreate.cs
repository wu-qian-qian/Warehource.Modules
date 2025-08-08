#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Plc.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "Plc.S7NetConfig",
            table => new
            {
                Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                S7Type = table.Column<int>("int", nullable: false),
                Solt = table.Column<short>("smallint", nullable: false),
                Rack = table.Column<short>("smallint", nullable: false),
                ReadTimeOut = table.Column<int>("int", nullable: false),
                WriteTimeOut = table.Column<int>("int", nullable: false),
                IsUse = table.Column<bool>("bit", nullable: false),
                ReadHeart = table.Column<string>("nvarchar(20)", maxLength: 20, nullable: true),
                WriteHeart = table.Column<string>("nvarchar(20)", maxLength: 20, nullable: true),
                CreationTime = table.Column<DateTime>("datetime2", nullable: false),
                LastModifierUser = table.Column<string>("nvarchar(max)", nullable: true),
                LastModificationTime = table.Column<DateTime>("datetime2", nullable: true),
                IsDeleted = table.Column<bool>("bit", nullable: false),
                Ip = table.Column<string>("nvarchar(max)", nullable: false),
                Port = table.Column<int>("int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Plc.S7NetConfig", x => x.Id)
                    .Annotation("SqlServer:Clustered", false);
            });

        migrationBuilder.CreateTable(
            "Plc.S7EntityItem",
            table => new
            {
                Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                Ip = table.Column<string>("nvarchar(20)", maxLength: 20, nullable: false),
                S7DataType = table.Column<int>("int", nullable: false),
                DBAddress = table.Column<int>("int", nullable: false),
                DataOffset = table.Column<int>("int", nullable: false),
                BitOffset = table.Column<byte>("tinyint", nullable: true),
                S7BlockType = table.Column<int>("int", nullable: false),
                Index = table.Column<int>("int", nullable: false),
                Description = table.Column<string>("nvarchar(50)", maxLength: 50, nullable: true),
                Name = table.Column<string>("nvarchar(20)", maxLength: 20, nullable: false),
                ArrtypeLength = table.Column<byte>("tinyint", nullable: true),
                DeviceName = table.Column<string>("nvarchar(max)", nullable: false),
                IsUse = table.Column<bool>("bit", nullable: false),
                NetGuid = table.Column<Guid>("uniqueidentifier", nullable: false),
                CreationTime = table.Column<DateTime>("datetime2", nullable: false),
                LastModifierUser = table.Column<string>("nvarchar(max)", nullable: true),
                LastModificationTime = table.Column<DateTime>("datetime2", nullable: true),
                IsDeleted = table.Column<bool>("bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Plc.S7EntityItem", x => x.Id)
                    .Annotation("SqlServer:Clustered", false);
                table.ForeignKey(
                    "FK_Plc.S7EntityItem_Plc.S7NetConfig_NetGuid",
                    x => x.NetGuid,
                    "Plc.S7NetConfig",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            "IX_Plc.S7EntityItem_NetGuid",
            "Plc.S7EntityItem",
            "NetGuid");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "Plc.S7EntityItem");

        migrationBuilder.DropTable(
            "Plc.S7NetConfig");
    }
}