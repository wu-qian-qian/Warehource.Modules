#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Wcs.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "Wcs.JobConfig",
            table => new
            {
                Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                Name = table.Column<string>("nvarchar(50)", maxLength: 50, nullable: false),
                Description = table.Column<string>("nvarchar(50)", maxLength: 50, nullable: true),
                JobType = table.Column<string>("nvarchar(50)", maxLength: 50, nullable: false),
                TimeOut = table.Column<int>("int", nullable: false),
                Timer = table.Column<int>("int", nullable: false),
                IsStart = table.Column<bool>("bit", nullable: false),
                CreationTime = table.Column<DateTime>("datetime2", nullable: false),
                LastModifierUser = table.Column<string>("nvarchar(max)", nullable: true),
                LastModificationTime = table.Column<DateTime>("datetime2", nullable: true),
                IsDeleted = table.Column<bool>("bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Wcs.JobConfig", x => x.Id)
                    .Annotation("SqlServer:Clustered", false);
            });

        migrationBuilder.CreateTable(
            "Wcs.Region",
            table => new
            {
                Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                Code = table.Column<string>("nvarchar(20)", maxLength: 20, nullable: false),
                Description = table.Column<string>("nvarchar(50)", maxLength: 50, nullable: true),
                CreationTime = table.Column<DateTime>("datetime2", nullable: false),
                LastModifierUser = table.Column<string>("nvarchar(max)", nullable: true),
                LastModificationTime = table.Column<DateTime>("datetime2", nullable: true),
                IsDeleted = table.Column<bool>("bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Wcs.Region", x => x.Id)
                    .Annotation("SqlServer:Clustered", false);
            });

        migrationBuilder.CreateTable(
            "Wcs.TaskExecuteStep",
            table => new
            {
                Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                StepIndex = table.Column<int>("int", nullable: false),
                Description = table.Column<string>("nvarchar(50)", maxLength: 50, nullable: true),
                ExecuteNodePath = table.Column<Guid>("uniqueidentifier", nullable: true),
                RegionId = table.Column<Guid>("uniqueidentifier", nullable: true),
                WcsTaskType = table.Column<int>("int", nullable: false),
                Version = table.Column<byte[]>("rowversion", rowVersion: true, nullable: true),
                CreationTime = table.Column<DateTime>("datetime2", nullable: false),
                LastModifierUser = table.Column<string>("nvarchar(max)", nullable: true),
                LastModificationTime = table.Column<DateTime>("datetime2", nullable: true),
                IsDeleted = table.Column<bool>("bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Wcs.TaskExecuteStep", x => x.Id)
                    .Annotation("SqlServer:Clustered", false);
            });

        migrationBuilder.CreateTable(
            "Wcs.ExecuteNodePath",
            table => new
            {
                Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                CurrentDeviceType = table.Column<int>("int", nullable: false),
                CurrentDeviceName = table.Column<string>("nvarchar(20)", maxLength: 20, nullable: false),
                RegionId = table.Column<Guid>("uniqueidentifier", nullable: true),
                CreationTime = table.Column<DateTime>("datetime2", nullable: false),
                LastModifierUser = table.Column<string>("nvarchar(max)", nullable: true),
                LastModificationTime = table.Column<DateTime>("datetime2", nullable: true),
                IsDeleted = table.Column<bool>("bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Wcs.ExecuteNodePath", x => x.Id)
                    .Annotation("SqlServer:Clustered", false);
                table.ForeignKey(
                    "FK_Wcs.ExecuteNodePath_Wcs.Region_RegionId",
                    x => x.RegionId,
                    "Wcs.Region",
                    "Id");
            });

        migrationBuilder.CreateTable(
            "Wcs.WcsTask",
            table => new
            {
                Id = table.Column<Guid>("uniqueidentifier", nullable: false),
                TaskCode = table.Column<string>("nvarchar(50)", maxLength: 50, nullable: true),
                SerialNumber = table.Column<int>("int", nullable: false),
                TaskType = table.Column<int>("int", nullable: false),
                TaskStatus = table.Column<int>("int", nullable: false),
                Description = table.Column<string>("nvarchar(max)", nullable: true),
                GetLocation_GetTunnel = table.Column<string>("nvarchar(10)", maxLength: 10, nullable: true),
                GetLocation_GetFloor = table.Column<string>("nvarchar(10)", maxLength: 10, nullable: true),
                GetLocation_GetRow = table.Column<string>("nvarchar(10)", maxLength: 10, nullable: true),
                GetLocation_GetColumn = table.Column<string>("nvarchar(10)", maxLength: 10, nullable: true),
                GetLocation_GetDepth = table.Column<string>("nvarchar(10)", maxLength: 10, nullable: true),
                PutLocation_PutTunnel = table.Column<string>("nvarchar(10)", maxLength: 10, nullable: true),
                PutLocation_PutFloor = table.Column<string>("nvarchar(10)", maxLength: 10, nullable: true),
                PutLocation_PutRow = table.Column<string>("nvarchar(10)", maxLength: 10, nullable: true),
                PutLocation_PutColumn = table.Column<string>("nvarchar(10)", maxLength: 10, nullable: true),
                PutLocation_PutDepth = table.Column<string>("nvarchar(10)", maxLength: 10, nullable: true),
                IsRead = table.Column<bool>("bit", nullable: false),
                DeviceId = table.Column<Guid>("uniqueidentifier", nullable: true),
                RegionId = table.Column<Guid>("uniqueidentifier", nullable: true),
                TaskExecuteStepId = table.Column<Guid>("uniqueidentifier", nullable: false),
                Version = table.Column<byte[]>("rowversion", rowVersion: true, nullable: true),
                CreationTime = table.Column<DateTime>("datetime2", nullable: false),
                LastModifierUser = table.Column<string>("nvarchar(max)", nullable: true),
                LastModificationTime = table.Column<DateTime>("datetime2", nullable: true),
                IsDeleted = table.Column<bool>("bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Wcs.WcsTask", x => x.Id)
                    .Annotation("SqlServer:Clustered", false);
                table.ForeignKey(
                    "FK_Wcs.WcsTask_Wcs.TaskExecuteStep_TaskExecuteStepId",
                    x => x.TaskExecuteStepId,
                    "Wcs.TaskExecuteStep",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            "IX_Wcs.ExecuteNodePath_RegionId",
            "Wcs.ExecuteNodePath",
            "RegionId",
            unique: true,
            filter: "[RegionId] IS NOT NULL");

        migrationBuilder.CreateIndex(
            "IX_Wcs.JobConfig_Name",
            "Wcs.JobConfig",
            "Name",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_Wcs.Region_Code",
            "Wcs.Region",
            "Code",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_Wcs.WcsTask_SerialNumber",
            "Wcs.WcsTask",
            "SerialNumber",
            unique: true);

        migrationBuilder.CreateIndex(
            "IX_Wcs.WcsTask_TaskCode",
            "Wcs.WcsTask",
            "TaskCode",
            unique: true,
            filter: "[TaskCode] IS NOT NULL");

        migrationBuilder.CreateIndex(
            "IX_Wcs.WcsTask_TaskExecuteStepId",
            "Wcs.WcsTask",
            "TaskExecuteStepId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "Wcs.ExecuteNodePath");

        migrationBuilder.DropTable(
            "Wcs.JobConfig");

        migrationBuilder.DropTable(
            "Wcs.WcsTask");

        migrationBuilder.DropTable(
            "Wcs.Region");

        migrationBuilder.DropTable(
            "Wcs.TaskExecuteStep");
    }
}