using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyInspection.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AQLLevels",
                columns: table => new
                {
                    AQLLevelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LevelName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AQLLevels", x => x.AQLLevelID);
                });

            migrationBuilder.CreateTable(
                name: "CustomerTypes",
                columns: table => new
                {
                    CustomerTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerTypes", x => x.CustomerTypeID);
                });

            migrationBuilder.CreateTable(
                name: "MasterCriteria",
                columns: table => new
                {
                    CriteriaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CriteriaName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterCriteria", x => x.CriteriaID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AQLDetails",
                columns: table => new
                {
                    AQLDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LotSizeMin = table.Column<int>(type: "int", nullable: false),
                    LotSizeMax = table.Column<int>(type: "int", nullable: false),
                    SampleSize = table.Column<int>(type: "int", nullable: false),
                    MajorDefectLimit = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
                    MinorDefectLimit = table.Column<decimal>(type: "decimal(4,2)", nullable: false),
                    AcceptanceMajor = table.Column<int>(type: "int", nullable: false),
                    RejectionMajor = table.Column<int>(type: "int", nullable: false),
                    AcceptanceMinor = table.Column<int>(type: "int", nullable: false),
                    RejectionMinor = table.Column<int>(type: "int", nullable: false),
                    AQLLevelID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AQLDetails", x => x.AQLDetailID);
                    table.ForeignKey(
                        name: "FK_AQLDetails_AQLLevels_AQLLevelID",
                        column: x => x.AQLLevelID,
                        principalTable: "AQLLevels",
                        principalColumn: "AQLLevelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerTypeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                    table.ForeignKey(
                        name: "FK_Customers_CustomerTypes_CustomerTypeID",
                        column: x => x.CustomerTypeID,
                        principalTable: "CustomerTypes",
                        principalColumn: "CustomerTypeID");
                });

            migrationBuilder.CreateTable(
                name: "Checkpoints",
                columns: table => new
                {
                    CheckpointID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InputType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecificationTolerance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    CriteriaID = table.Column<int>(type: "int", nullable: false),
                    MasterCriteriaCriteriaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checkpoints", x => x.CheckpointID);
                    table.ForeignKey(
                        name: "FK_Checkpoints_MasterCriteria_MasterCriteriaCriteriaID",
                        column: x => x.MasterCriteriaCriteriaID,
                        principalTable: "MasterCriteria",
                        principalColumn: "CriteriaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    LogID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.LogID);
                    table.ForeignKey(
                        name: "FK_AuditLogs_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InspectionReports",
                columns: table => new
                {
                    ReportID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseOrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SBLOrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManufacturerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InspectionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FactoryRepresentativeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OverallStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeneralRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CustomerID = table.Column<int>(type: "int", nullable: true),
                    InspectorUserID = table.Column<int>(type: "int", nullable: true),
                    InspectorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InspectionReports", x => x.ReportID);
                    table.ForeignKey(
                        name: "FK_InspectionReports_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID");
                    table.ForeignKey(
                        name: "FK_InspectionReports_Users_InspectorId",
                        column: x => x.InspectorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ReportCheckpointAnswers",
                columns: table => new
                {
                    AnswerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnswerValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuantitySampled = table.Column<int>(type: "int", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CheckpointID = table.Column<int>(type: "int", nullable: false),
                    InspectionReportReportID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportCheckpointAnswers", x => x.AnswerID);
                    table.ForeignKey(
                        name: "FK_ReportCheckpointAnswers_Checkpoints_CheckpointID",
                        column: x => x.CheckpointID,
                        principalTable: "Checkpoints",
                        principalColumn: "CheckpointID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportCheckpointAnswers_InspectionReports_InspectionReportReportID",
                        column: x => x.InspectionReportReportID,
                        principalTable: "InspectionReports",
                        principalColumn: "ReportID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportCriteriaStatuses",
                columns: table => new
                {
                    ReportCriteriaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CriteriaID = table.Column<int>(type: "int", nullable: false),
                    InspectionReportReportID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MasterCriteriaCriteriaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportCriteriaStatuses", x => x.ReportCriteriaID);
                    table.ForeignKey(
                        name: "FK_ReportCriteriaStatuses_InspectionReports_InspectionReportReportID",
                        column: x => x.InspectionReportReportID,
                        principalTable: "InspectionReports",
                        principalColumn: "ReportID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportCriteriaStatuses_MasterCriteria_MasterCriteriaCriteriaID",
                        column: x => x.MasterCriteriaCriteriaID,
                        principalTable: "MasterCriteria",
                        principalColumn: "CriteriaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportDefects",
                columns: table => new
                {
                    DefectID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DefectDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CriticalCount = table.Column<int>(type: "int", nullable: false),
                    MajorCount = table.Column<int>(type: "int", nullable: false),
                    MinorCount = table.Column<int>(type: "int", nullable: false),
                    ReportID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InspectionReportReportID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportDefects", x => x.DefectID);
                    table.ForeignKey(
                        name: "FK_ReportDefects_InspectionReports_InspectionReportReportID",
                        column: x => x.InspectionReportReportID,
                        principalTable: "InspectionReports",
                        principalColumn: "ReportID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportDetails",
                columns: table => new
                {
                    ReportDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StyleNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalQuantity = table.Column<int>(type: "int", nullable: true),
                    InspectionLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LCNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportDetails", x => x.ReportDetailID);
                    table.ForeignKey(
                        name: "FK_ReportDetails_InspectionReports_ReportID",
                        column: x => x.ReportID,
                        principalTable: "InspectionReports",
                        principalColumn: "ReportID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportMediaFiles",
                columns: table => new
                {
                    MediaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    S3BucketName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    S3ObjectKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MediaType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FriendlyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReportID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InspectionReportReportID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportMediaFiles", x => x.MediaID);
                    table.ForeignKey(
                        name: "FK_ReportMediaFiles_InspectionReports_InspectionReportReportID",
                        column: x => x.InspectionReportReportID,
                        principalTable: "InspectionReports",
                        principalColumn: "ReportID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportOverrides",
                columns: table => new
                {
                    OverrideID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OriginalStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OverrideTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReportID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OverriddenByUserID = table.Column<int>(type: "int", nullable: false),
                    InspectionReportReportID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportOverrides", x => x.OverrideID);
                    table.ForeignKey(
                        name: "FK_ReportOverrides_InspectionReports_InspectionReportReportID",
                        column: x => x.InspectionReportReportID,
                        principalTable: "InspectionReports",
                        principalColumn: "ReportID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportOverrides_Users_OverriddenByUserID",
                        column: x => x.OverriddenByUserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportQuantityItems",
                columns: table => new
                {
                    ReportQuantityItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StyleArticle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PONumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderQuantity = table.Column<int>(type: "int", nullable: false),
                    InspectedQtyPacked = table.Column<int>(type: "int", nullable: false),
                    InspectedQtyNotPacked = table.Column<int>(type: "int", nullable: false),
                    CartonsPacked = table.Column<int>(type: "int", nullable: false),
                    CartonsNotPacked = table.Column<int>(type: "int", nullable: false),
                    ReportID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InspectionReportReportID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportQuantityItems", x => x.ReportQuantityItemID);
                    table.ForeignKey(
                        name: "FK_ReportQuantityItems_InspectionReports_InspectionReportReportID",
                        column: x => x.InspectionReportReportID,
                        principalTable: "InspectionReports",
                        principalColumn: "ReportID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AQLDetails_AQLLevelID",
                table: "AQLDetails",
                column: "AQLLevelID");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_UserID",
                table: "AuditLogs",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Checkpoints_MasterCriteriaCriteriaID",
                table: "Checkpoints",
                column: "MasterCriteriaCriteriaID");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerTypeID",
                table: "Customers",
                column: "CustomerTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_InspectionReports_CustomerID",
                table: "InspectionReports",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_InspectionReports_InspectorId",
                table: "InspectionReports",
                column: "InspectorId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportCheckpointAnswers_CheckpointID",
                table: "ReportCheckpointAnswers",
                column: "CheckpointID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportCheckpointAnswers_InspectionReportReportID",
                table: "ReportCheckpointAnswers",
                column: "InspectionReportReportID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportCriteriaStatuses_InspectionReportReportID",
                table: "ReportCriteriaStatuses",
                column: "InspectionReportReportID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportCriteriaStatuses_MasterCriteriaCriteriaID",
                table: "ReportCriteriaStatuses",
                column: "MasterCriteriaCriteriaID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportDefects_InspectionReportReportID",
                table: "ReportDefects",
                column: "InspectionReportReportID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportDetails_ReportID",
                table: "ReportDetails",
                column: "ReportID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReportMediaFiles_InspectionReportReportID",
                table: "ReportMediaFiles",
                column: "InspectionReportReportID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportOverrides_InspectionReportReportID",
                table: "ReportOverrides",
                column: "InspectionReportReportID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportOverrides_OverriddenByUserID",
                table: "ReportOverrides",
                column: "OverriddenByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_ReportQuantityItems_InspectionReportReportID",
                table: "ReportQuantityItems",
                column: "InspectionReportReportID");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AQLDetails");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "ReportCheckpointAnswers");

            migrationBuilder.DropTable(
                name: "ReportCriteriaStatuses");

            migrationBuilder.DropTable(
                name: "ReportDefects");

            migrationBuilder.DropTable(
                name: "ReportDetails");

            migrationBuilder.DropTable(
                name: "ReportMediaFiles");

            migrationBuilder.DropTable(
                name: "ReportOverrides");

            migrationBuilder.DropTable(
                name: "ReportQuantityItems");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "AQLLevels");

            migrationBuilder.DropTable(
                name: "Checkpoints");

            migrationBuilder.DropTable(
                name: "InspectionReports");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "MasterCriteria");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "CustomerTypes");
        }
    }
}
