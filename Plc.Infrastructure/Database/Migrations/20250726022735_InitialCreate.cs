using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plc.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plc.S7NetConfig",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    S7Type = table.Column<int>(type: "int", nullable: false),
                    Solt = table.Column<short>(type: "smallint", nullable: false),
                    Rack = table.Column<short>(type: "smallint", nullable: false),
                    ReadTimeOut = table.Column<int>(type: "int", nullable: false),
                    WriteTimeOut = table.Column<int>(type: "int", nullable: false),
                    IsUse = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifierUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Ip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Port = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plc.S7NetConfig", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Plc.S7EntityItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    S7DataType = table.Column<int>(type: "int", nullable: false),
                    DBAddress = table.Column<int>(type: "int", nullable: false),
                    DataOffset = table.Column<int>(type: "int", nullable: false),
                    BitOffset = table.Column<int>(type: "int", nullable: true),
                    S7BlockType = table.Column<int>(type: "int", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ArrtypeLength = table.Column<int>(type: "int", nullable: true),
                    DeviceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsUse = table.Column<bool>(type: "bit", nullable: false),
                    NetGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifierUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plc.S7EntityItem", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Plc.S7EntityItem_Plc.S7NetConfig_NetGuid",
                        column: x => x.NetGuid,
                        principalTable: "Plc.S7NetConfig",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plc.S7EntityItem_NetGuid",
                table: "Plc.S7EntityItem",
                column: "NetGuid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Plc.S7EntityItem");

            migrationBuilder.DropTable(
                name: "Plc.S7NetConfig");
        }
    }
}
