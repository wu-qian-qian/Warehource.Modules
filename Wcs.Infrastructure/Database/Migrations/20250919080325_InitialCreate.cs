using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wcs.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Wcs.Device",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeviceType = table.Column<int>(type: "int", nullable: false),
                    DeviceName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Config = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Enable = table.Column<bool>(type: "bit", nullable: false),
                    RegionCode = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    GroupName = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifierUser = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wcs.Device", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Wcs.Event",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventStatus = table.Column<int>(type: "int", nullable: false),
                    EventName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Errored = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wcs.Event", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wcs.JobConfig",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    JobType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TimeOut = table.Column<int>(type: "int", nullable: false),
                    Timer = table.Column<int>(type: "int", nullable: false),
                    IsStart = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifierUser = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wcs.JobConfig", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Wcs.PlcMap",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeviceName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    PlcName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifierUser = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wcs.PlcMap", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wcs.Region",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CurrentNum = table.Column<int>(type: "int", nullable: false),
                    MaxNum = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifierUser = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wcs.Region", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Wcs.TaskExecuteStep",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskExecuteStepType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PathNodeGroup = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CurentDevice = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DeviceType = table.Column<int>(type: "int", nullable: true),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifierUser = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wcs.TaskExecuteStep", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "Wcs.ExecuteNodePath",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Index = table.Column<byte>(type: "tinyint", nullable: false),
                    PahtNodeGroup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentDeviceType = table.Column<int>(type: "int", nullable: false),
                    TaskType = table.Column<int>(type: "int", nullable: false),
                    RegionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Enable = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifierUser = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wcs.ExecuteNodePath", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Wcs.ExecuteNodePath_Wcs.Region_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Wcs.Region",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Wcs.WcsTask",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    IsEnforce = table.Column<bool>(type: "bit", nullable: false),
                    TaskCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SerialNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "4000, 1"),
                    Container = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TaskType = table.Column<int>(type: "int", nullable: false),
                    TaskStatus = table.Column<int>(type: "int", nullable: false),
                    CreatorSystemType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GetLocation_GetTunnel = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    GetLocation_GetFloor = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    GetLocation_GetRow = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    GetLocation_GetColumn = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    GetLocation_GetDepth = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PutLocation_PutTunnel = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PutLocation_PutFloor = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PutLocation_PutRow = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PutLocation_PutColumn = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PutLocation_PutDepth = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    StockOutPosition = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StockInPosition = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RegionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TaskExecuteStepId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifierUser = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wcs.WcsTask", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_Wcs.WcsTask_Wcs.TaskExecuteStep_TaskExecuteStepId",
                        column: x => x.TaskExecuteStepId,
                        principalTable: "Wcs.TaskExecuteStep",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wcs.Device_DeviceName",
                table: "Wcs.Device",
                column: "DeviceName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wcs.ExecuteNodePath_RegionId",
                table: "Wcs.ExecuteNodePath",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Wcs.JobConfig_Name",
                table: "Wcs.JobConfig",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wcs.Region_Code",
                table: "Wcs.Region",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wcs.WcsTask_SerialNumber",
                table: "Wcs.WcsTask",
                column: "SerialNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wcs.WcsTask_TaskCode",
                table: "Wcs.WcsTask",
                column: "TaskCode",
                unique: true,
                filter: "[TaskCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Wcs.WcsTask_TaskExecuteStepId",
                table: "Wcs.WcsTask",
                column: "TaskExecuteStepId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wcs.Device");

            migrationBuilder.DropTable(
                name: "Wcs.Event");

            migrationBuilder.DropTable(
                name: "Wcs.ExecuteNodePath");

            migrationBuilder.DropTable(
                name: "Wcs.JobConfig");

            migrationBuilder.DropTable(
                name: "Wcs.PlcMap");

            migrationBuilder.DropTable(
                name: "Wcs.WcsTask");

            migrationBuilder.DropTable(
                name: "Wcs.Region");

            migrationBuilder.DropTable(
                name: "Wcs.TaskExecuteStep");
        }
    }
}
