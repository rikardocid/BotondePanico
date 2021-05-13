using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace GFDSystems.Vigitech.DAL.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    middleName = table.Column<string>(maxLength: 50, nullable: false),
                    lastName = table.Column<string>(maxLength: 50, nullable: false),
                    profilePicture = table.Column<byte[]>(nullable: true),
                    firebaseId = table.Column<string>(maxLength: 255, nullable: true),
                    firebasePassword = table.Column<string>(maxLength: 50, nullable: true),
                    authValidationCode = table.Column<string>(maxLength: 15, nullable: true),
                    isActive = table.Column<bool>(nullable: false),
                    type = table.Column<string>(maxLength: 5, nullable: false),
                    area = table.Column<string>(maxLength: 30, nullable: true),
                    IsVerified = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmergencyDirectory",
                columns: table => new
                {
                    EmergencyDirectoryId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    NameArea = table.Column<string>(maxLength: 40, nullable: false),
                    Phone = table.Column<string>(maxLength: 12, nullable: false),
                    LatLang = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyDirectory", x => x.EmergencyDirectoryId);
                });

            migrationBuilder.CreateTable(
                name: "FoliosControls",
                columns: table => new
                {
                    IdUsersControl = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 50, nullable: true),
                    Prefix = table.Column<string>(maxLength: 3, nullable: true),
                    Secuencial = table.Column<int>(nullable: false),
                    Sufix = table.Column<string>(maxLength: 3, nullable: true),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    Type = table.Column<string>(maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoliosControls", x => x.IdUsersControl);
                });

            migrationBuilder.CreateTable(
                name: "GroupStatus",
                columns: table => new
                {
                    GroupStatusId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupStatus", x => x.GroupStatusId);
                });

            migrationBuilder.CreateTable(
                name: "GroupType",
                columns: table => new
                {
                    GroupTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupType", x => x.GroupTypeId);
                });

            migrationBuilder.CreateTable(
                name: "MobileDeviceRegistrationTemp",
                columns: table => new
                {
                    MobileDeviceRegistrationId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    NumberPhone = table.Column<string>(maxLength: 15, nullable: false),
                    CellComapny = table.Column<string>(maxLength: 30, nullable: false),
                    DeviceId = table.Column<string>(maxLength: 40, nullable: false),
                    MakeModel = table.Column<string>(maxLength: 40, nullable: true),
                    DateRegister = table.Column<DateTime>(nullable: false),
                    LatLangRegister = table.Column<string>(maxLength: 40, nullable: false),
                    Platform = table.Column<string>(maxLength: 40, nullable: true),
                    VersionOS = table.Column<string>(maxLength: 40, nullable: true),
                    IsCompleteRegister = table.Column<bool>(nullable: false),
                    AppUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileDeviceRegistrationTemp", x => x.MobileDeviceRegistrationId);
                });

            migrationBuilder.CreateTable(
                name: "NotificationBases",
                columns: table => new
                {
                    NotificationBaseId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(maxLength: 6, nullable: false),
                    Message = table.Column<string>(maxLength: 250, nullable: false),
                    Status = table.Column<string>(maxLength: 6, nullable: false),
                    IdAspNetUserSend = table.Column<int>(nullable: false),
                    IdUserFirebaseSend = table.Column<string>(maxLength: 30, nullable: false),
                    UserNameSend = table.Column<string>(maxLength: 50, nullable: false),
                    IdAspNetUserReceive = table.Column<int>(nullable: false),
                    IdUserFirebaseReceive = table.Column<string>(maxLength: 30, nullable: false),
                    UserNameReceive = table.Column<string>(maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    DateRegister = table.Column<DateTime>(nullable: false),
                    IdFirebaseNotification = table.Column<string>(maxLength: 35, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationBases", x => x.NotificationBaseId);
                });

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    StateId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 40, nullable: false),
                    Estatus = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.StateId);
                });

            migrationBuilder.CreateTable(
                name: "StatusDevice",
                columns: table => new
                {
                    StatusDeviceID = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<int>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusDevice", x => x.StatusDeviceID);
                });

            migrationBuilder.CreateTable(
                name: "SystemLog",
                columns: table => new
                {
                    id_system_log = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    date_log = table.Column<DateTime>(nullable: false),
                    controller = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: false),
                    parameter = table.Column<string>(nullable: false),
                    action = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemLog", x => x.id_system_log);
                });

            migrationBuilder.CreateTable(
                name: "TypeEmergency",
                columns: table => new
                {
                    TypeEmergencyId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Status = table.Column<bool>(nullable: true),
                    Acronym = table.Column<string>(maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeEmergency", x => x.TypeEmergencyId);
                });

            migrationBuilder.CreateTable(
                name: "TypeIncidence",
                columns: table => new
                {
                    TypeIncidenceId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Status = table.Column<bool>(nullable: true),
                    Acronym = table.Column<string>(maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeIncidence", x => x.TypeIncidenceId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiKeyUsers",
                columns: table => new
                {
                    ApiKeyUserId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    AppUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiKeyUsers", x => x.ApiKeyUserId);
                    table.ForeignKey(
                        name: "FK_ApiKeyUsers_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 100, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 100, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 100, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Citizen",
                columns: table => new
                {
                    CitizenId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    CURP = table.Column<string>(maxLength: 18, nullable: true),
                    Sex = table.Column<string>(maxLength: 20, nullable: true),
                    AspNetUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citizen", x => x.CitizenId);
                    table.ForeignKey(
                        name: "FK_Citizen_AspNetUsers_AspNetUserId",
                        column: x => x.AspNetUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SecurityAgent",
                columns: table => new
                {
                    SecurityAgentId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    AgentLicence = table.Column<string>(maxLength: 50, nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    DateRegister = table.Column<int>(nullable: false),
                    AspNetUserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityAgent", x => x.SecurityAgentId);
                    table.ForeignKey(
                        name: "FK_SecurityAgent_AspNetUsers_AspNetUserId",
                        column: x => x.AspNetUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusId = table.Column<string>(maxLength: 5, nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    GroupStatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.StatusId);
                    table.ForeignKey(
                        name: "FK_Status_GroupStatus_GroupStatusId",
                        column: x => x.GroupStatusId,
                        principalTable: "GroupStatus",
                        principalColumn: "GroupStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Type",
                columns: table => new
                {
                    TypeId = table.Column<string>(maxLength: 5, nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    GroupTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type", x => x.TypeId);
                    table.ForeignKey(
                        name: "FK_Type_GroupType_GroupTypeId",
                        column: x => x.GroupTypeId,
                        principalTable: "GroupType",
                        principalColumn: "GroupTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Town",
                columns: table => new
                {
                    TownId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 60, nullable: false),
                    Status = table.Column<bool>(nullable: true),
                    StateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Town", x => x.TownId);
                    table.ForeignKey(
                        name: "FK_Town_State_StateId",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmergencyContact",
                columns: table => new
                {
                    EmergencyContactId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 80, nullable: true),
                    Relationship = table.Column<string>(maxLength: 30, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 12, nullable: true),
                    CitizenId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyContact", x => x.EmergencyContactId);
                    table.ForeignKey(
                        name: "FK_EmergencyContact_Citizen_CitizenId",
                        column: x => x.CitizenId,
                        principalTable: "Citizen",
                        principalColumn: "CitizenId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalRecord",
                columns: table => new
                {
                    MedicalRecordId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Suffering = table.Column<string>(maxLength: 100, nullable: true),
                    Alergies = table.Column<string>(maxLength: 100, nullable: true),
                    Medicines = table.Column<string>(maxLength: 100, nullable: true),
                    BloodType = table.Column<string>(maxLength: 10, nullable: true),
                    CitizenId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRecord", x => x.MedicalRecordId);
                    table.ForeignKey(
                        name: "FK_MedicalRecord_Citizen_CitizenId",
                        column: x => x.CitizenId,
                        principalTable: "Citizen",
                        principalColumn: "CitizenId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MobileDeviceRegistration",
                columns: table => new
                {
                    MobileDeviceRegistrationId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    NumberPhone = table.Column<string>(maxLength: 15, nullable: false),
                    CellComapny = table.Column<string>(maxLength: 30, nullable: false),
                    DeviceId = table.Column<string>(maxLength: 40, nullable: false),
                    MakeModel = table.Column<string>(maxLength: 40, nullable: true),
                    DateRegister = table.Column<DateTime>(nullable: false),
                    LatLangRegister = table.Column<string>(maxLength: 40, nullable: false),
                    Platform = table.Column<string>(maxLength: 40, nullable: true),
                    VersionOS = table.Column<string>(maxLength: 40, nullable: true),
                    CitizenId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileDeviceRegistration", x => x.MobileDeviceRegistrationId);
                    table.ForeignKey(
                        name: "FK_MobileDeviceRegistration_Citizen_CitizenId",
                        column: x => x.CitizenId,
                        principalTable: "Citizen",
                        principalColumn: "CitizenId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceAssigned",
                columns: table => new
                {
                    DeviceAssignedId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CarLicence = table.Column<int>(maxLength: 10, nullable: false),
                    Description = table.Column<int>(maxLength: 10, nullable: false),
                    DateAsignation = table.Column<DateTime>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    SecurityAgentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceAssigned", x => x.DeviceAssignedId);
                    table.ForeignKey(
                        name: "FK_DeviceAssigned_SecurityAgent_SecurityAgentID",
                        column: x => x.SecurityAgentID,
                        principalTable: "SecurityAgent",
                        principalColumn: "SecurityAgentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceAssigned_StatusDevice_StatusId",
                        column: x => x.StatusId,
                        principalTable: "StatusDevice",
                        principalColumn: "StatusDeviceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Suburb",
                columns: table => new
                {
                    SuburbId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 60, nullable: false),
                    PostalCode = table.Column<int>(nullable: false),
                    Status = table.Column<bool>(nullable: true),
                    TownId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suburb", x => x.SuburbId);
                    table.ForeignKey(
                        name: "FK_Suburb_Town_TownId",
                        column: x => x.TownId,
                        principalTable: "Town",
                        principalColumn: "TownId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoryDeviceAssigned",
                columns: table => new
                {
                    HistoryDeviceAssignedId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CarLicence = table.Column<int>(maxLength: 10, nullable: false),
                    Description = table.Column<int>(maxLength: 10, nullable: false),
                    DateAsignation = table.Column<DateTime>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    SecurityAgentID = table.Column<int>(nullable: false),
                    DeviceAssignedId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryDeviceAssigned", x => x.HistoryDeviceAssignedId);
                    table.ForeignKey(
                        name: "FK_HistoryDeviceAssigned_DeviceAssigned_DeviceAssignedId",
                        column: x => x.DeviceAssignedId,
                        principalTable: "DeviceAssigned",
                        principalColumn: "DeviceAssignedId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoryDeviceAssigned_SecurityAgent_SecurityAgentID",
                        column: x => x.SecurityAgentID,
                        principalTable: "SecurityAgent",
                        principalColumn: "SecurityAgentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoryDeviceAssigned_StatusDevice_StatusId",
                        column: x => x.StatusId,
                        principalTable: "StatusDevice",
                        principalColumn: "StatusDeviceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Street = table.Column<string>(maxLength: 70, nullable: false),
                    ExternalNumber = table.Column<string>(maxLength: 10, nullable: false),
                    InternalNumber = table.Column<string>(maxLength: 10, nullable: true),
                    SuburbId = table.Column<int>(nullable: false),
                    CitizenId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_Address_Citizen_CitizenId",
                        column: x => x.CitizenId,
                        principalTable: "Citizen",
                        principalColumn: "CitizenId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Address_Suburb_SuburbId",
                        column: x => x.SuburbId,
                        principalTable: "Suburb",
                        principalColumn: "SuburbId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmergencyAlert",
                columns: table => new
                {
                    EmergencyAlertId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    DateRegistration = table.Column<DateTime>(nullable: false),
                    LatpLong = table.Column<string>(maxLength: 50, nullable: false),
                    PostalCode = table.Column<int>(nullable: false),
                    Address = table.Column<string>(maxLength: 70, nullable: false),
                    Reference = table.Column<string>(maxLength: 100, nullable: false),
                    Commentary = table.Column<string>(maxLength: 150, nullable: false),
                    SuburbId = table.Column<int>(nullable: false),
                    CitizenId = table.Column<int>(nullable: false),
                    TypeEmergencyId = table.Column<int>(nullable: false),
                    TypeIncidenceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyAlert", x => x.EmergencyAlertId);
                    table.ForeignKey(
                        name: "FK_EmergencyAlert_Citizen_CitizenId",
                        column: x => x.CitizenId,
                        principalTable: "Citizen",
                        principalColumn: "CitizenId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmergencyAlert_Suburb_SuburbId",
                        column: x => x.SuburbId,
                        principalTable: "Suburb",
                        principalColumn: "SuburbId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmergencyAlert_TypeEmergency_TypeEmergencyId",
                        column: x => x.TypeEmergencyId,
                        principalTable: "TypeEmergency",
                        principalColumn: "TypeEmergencyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmergencyAlert_TypeIncidence_TypeIncidenceId",
                        column: x => x.TypeIncidenceId,
                        principalTable: "TypeIncidence",
                        principalColumn: "TypeIncidenceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SecurityAgentEmergency",
                columns: table => new
                {
                    SecurityAgentEmergencyId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Folio = table.Column<string>(maxLength: 12, nullable: false),
                    Status = table.Column<string>(maxLength: 5, nullable: false),
                    PatrolNumber = table.Column<string>(maxLength: 5, nullable: false),
                    SecurityAgentId = table.Column<int>(nullable: false),
                    EmergencyAlertId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityAgentEmergency", x => x.SecurityAgentEmergencyId);
                    table.ForeignKey(
                        name: "FK_SecurityAgentEmergency_EmergencyAlert_EmergencyAlertId",
                        column: x => x.EmergencyAlertId,
                        principalTable: "EmergencyAlert",
                        principalColumn: "EmergencyAlertId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SecurityAgentEmergency_SecurityAgent_SecurityAgentId",
                        column: x => x.SecurityAgentId,
                        principalTable: "SecurityAgent",
                        principalColumn: "SecurityAgentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_CitizenId",
                table: "Address",
                column: "CitizenId");

            migrationBuilder.CreateIndex(
                name: "IX_Address_SuburbId",
                table: "Address",
                column: "SuburbId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiKeyUsers_AppUserId",
                table: "ApiKeyUsers",
                column: "AppUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Citizen_AspNetUserId",
                table: "Citizen",
                column: "AspNetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceAssigned_SecurityAgentID",
                table: "DeviceAssigned",
                column: "SecurityAgentID");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceAssigned_StatusId",
                table: "DeviceAssigned",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyAlert_CitizenId",
                table: "EmergencyAlert",
                column: "CitizenId");

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyAlert_SuburbId",
                table: "EmergencyAlert",
                column: "SuburbId");

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyAlert_TypeEmergencyId",
                table: "EmergencyAlert",
                column: "TypeEmergencyId");

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyAlert_TypeIncidenceId",
                table: "EmergencyAlert",
                column: "TypeIncidenceId");

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyContact_CitizenId",
                table: "EmergencyContact",
                column: "CitizenId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryDeviceAssigned_DeviceAssignedId",
                table: "HistoryDeviceAssigned",
                column: "DeviceAssignedId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryDeviceAssigned_SecurityAgentID",
                table: "HistoryDeviceAssigned",
                column: "SecurityAgentID");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryDeviceAssigned_StatusId",
                table: "HistoryDeviceAssigned",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecord_CitizenId",
                table: "MedicalRecord",
                column: "CitizenId");

            migrationBuilder.CreateIndex(
                name: "IX_MobileDeviceRegistration_CitizenId",
                table: "MobileDeviceRegistration",
                column: "CitizenId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityAgent_AspNetUserId",
                table: "SecurityAgent",
                column: "AspNetUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityAgentEmergency_EmergencyAlertId",
                table: "SecurityAgentEmergency",
                column: "EmergencyAlertId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityAgentEmergency_SecurityAgentId",
                table: "SecurityAgentEmergency",
                column: "SecurityAgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Status_GroupStatusId",
                table: "Status",
                column: "GroupStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Suburb_TownId",
                table: "Suburb",
                column: "TownId");

            migrationBuilder.CreateIndex(
                name: "IX_Town_StateId",
                table: "Town",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Type_GroupTypeId",
                table: "Type",
                column: "GroupTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "ApiKeyUsers");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "EmergencyContact");

            migrationBuilder.DropTable(
                name: "EmergencyDirectory");

            migrationBuilder.DropTable(
                name: "FoliosControls");

            migrationBuilder.DropTable(
                name: "HistoryDeviceAssigned");

            migrationBuilder.DropTable(
                name: "MedicalRecord");

            migrationBuilder.DropTable(
                name: "MobileDeviceRegistration");

            migrationBuilder.DropTable(
                name: "MobileDeviceRegistrationTemp");

            migrationBuilder.DropTable(
                name: "NotificationBases");

            migrationBuilder.DropTable(
                name: "SecurityAgentEmergency");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "SystemLog");

            migrationBuilder.DropTable(
                name: "Type");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "DeviceAssigned");

            migrationBuilder.DropTable(
                name: "EmergencyAlert");

            migrationBuilder.DropTable(
                name: "GroupStatus");

            migrationBuilder.DropTable(
                name: "GroupType");

            migrationBuilder.DropTable(
                name: "SecurityAgent");

            migrationBuilder.DropTable(
                name: "StatusDevice");

            migrationBuilder.DropTable(
                name: "Citizen");

            migrationBuilder.DropTable(
                name: "Suburb");

            migrationBuilder.DropTable(
                name: "TypeEmergency");

            migrationBuilder.DropTable(
                name: "TypeIncidence");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Town");

            migrationBuilder.DropTable(
                name: "State");
        }
    }
}
