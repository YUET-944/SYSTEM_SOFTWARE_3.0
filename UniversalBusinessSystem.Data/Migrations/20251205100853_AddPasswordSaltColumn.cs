using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UniversalBusinessSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordSaltColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Key = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Icon = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ModuleType = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsRequired = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    SortOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    AssemblyName = table.Column<string>(type: "TEXT", nullable: false),
                    EntryPointClass = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Key = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Category = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShopTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Icon = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    SortOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DefaultUnits = table.Column<string>(type: "text", nullable: true),
                    DefaultModules = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Symbol = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    UnitType = table.Column<int>(type: "INTEGER", nullable: false),
                    BaseConversionFactor = table.Column<decimal>(type: "TEXT", nullable: false),
                    BaseUnitId = table.Column<Guid>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    SortOrder = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Units_Units_BaseUnitId",
                        column: x => x.BaseUnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Address = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    LicenseKey = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    DatabasePath = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    ShopTypeId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organizations_ShopTypes_ShopTypeId",
                        column: x => x.ShopTypeId,
                        principalTable: "ShopTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Color = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Icon = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    ParentCategoryId = table.Column<Guid>(type: "TEXT", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    SortOrder = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Categories_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationModules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ModuleId = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Configuration = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationModules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationModules_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationModules_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UnitId = table.Column<Guid>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SortOrder = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationUnits_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationUnits_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsSystemRole = table.Column<bool>(type: "INTEGER", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ContactPerson = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    TaxNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    CreditLimit = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Balance = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suppliers_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Sku = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Barcode = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    OrganizationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UnitId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CostPrice = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    SellingPrice = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    DiscountPrice = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: true),
                    CurrentStock = table.Column<decimal>(type: "TEXT", precision: 18, scale: 4, nullable: false),
                    MinStockLevel = table.Column<decimal>(type: "TEXT", precision: 18, scale: 4, nullable: false),
                    MaxStockLevel = table.Column<decimal>(type: "TEXT", precision: 18, scale: 4, nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    TrackStock = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsTaxable = table.Column<bool>(type: "INTEGER", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Attributes = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    SortOrder = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    RoleId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PermissionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    PasswordSalt = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastLoginAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsEmailVerified = table.Column<bool>(type: "INTEGER", nullable: false),
                    EmailVerificationToken = table.Column<string>(type: "TEXT", maxLength: 255, nullable: true),
                    EmailVerificationExpires = table.Column<DateTime>(type: "TEXT", nullable: true),
                    FailedLoginAttempts = table.Column<int>(type: "INTEGER", nullable: false),
                    LockedUntil = table.Column<DateTime>(type: "TEXT", nullable: true),
                    OrganizationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RoleId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SupplierId = table.Column<Guid>(type: "TEXT", nullable: false),
                    OrderNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    OrderDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ExpectedDeliveryDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ActualDeliveryDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    SubTotal = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    TaxAmount = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    ShippingCost = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    TotalAmount = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    CreatedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockAdjustments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AdjustedBy = table.Column<Guid>(type: "TEXT", nullable: false),
                    AdjustmentType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    QuantityChange = table.Column<int>(type: "INTEGER", nullable: false),
                    UnitCost = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    Reason = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Reference = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockAdjustments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockAdjustments_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockAdjustments_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockAdjustments_Users_AdjustedBy",
                        column: x => x.AdjustedBy,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PurchaseOrderId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Quantity = table.Column<decimal>(type: "TEXT", precision: 18, scale: 3, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    DiscountPercent = table.Column<decimal>(type: "TEXT", precision: 5, scale: 2, nullable: false),
                    LineTotal = table.Column<decimal>(type: "TEXT", precision: 18, scale: 2, nullable: false),
                    ReceivedQuantity = table.Column<decimal>(type: "TEXT", precision: 18, scale: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderItems_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Modules",
                columns: new[] { "Id", "AssemblyName", "CreatedAt", "Description", "EntryPointClass", "Icon", "IsActive", "IsRequired", "Key", "ModuleType", "Name", "SortOrder", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "UniversalBusinessSystem.Modules", new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9712), "Manage users and roles", "UniversalBusinessSystem.Modules.UserManagement.UserManagementModule", null, true, true, "user_management", 1, "User Management", 1, null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "UniversalBusinessSystem.Modules", new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9725), "Manage products and categories", "UniversalBusinessSystem.Modules.Inventory.InventoryModule", null, true, true, "inventory_management", 1, "Inventory Management", 2, null },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "UniversalBusinessSystem.Modules", new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9731), "Manage product variants for clothing", "UniversalBusinessSystem.Modules.Variants.VariantsModule", null, true, false, "size_color_variants", 7, "Size/Color Variants", 3, null },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "UniversalBusinessSystem.Modules", new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9735), "Manage product warranties", "UniversalBusinessSystem.Modules.Warranty.WarrantyModule", null, true, false, "warranty_management", 7, "Warranty Management", 4, null },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "UniversalBusinessSystem.Modules", new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9740), "Weight-based pricing for groceries", "UniversalBusinessSystem.Modules.WeightPricing.WeightPricingModule", null, true, false, "weight_pricing", 7, "Weight-based Pricing", 5, null },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "UniversalBusinessSystem.Modules", new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9745), "Track IMEI numbers for mobile devices", "UniversalBusinessSystem.Modules.IMEI.IMEIModule", null, true, false, "imei_tracking", 7, "IMEI Tracking", 6, null },
                    { new Guid("77777777-7777-7777-7777-777777777777"), "UniversalBusinessSystem.Modules", new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9750), "Track batch numbers and expiry dates", "UniversalBusinessSystem.Modules.BatchExpiry.BatchExpiryModule", null, true, false, "batch_expiry", 7, "Batch & Expiry", 7, null }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Category", "CreatedAt", "Description", "IsActive", "Key", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 1, new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9804), "View main dashboard", true, "dashboard.view", "View Dashboard", null },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaab"), 9, new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9813), "Access application settings", true, "settings.access", "Access Settings", null },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), 2, new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9821), "View user list", true, "users.view", "View Users", null },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbc"), 2, new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9825), "Create new users", true, "users.create", "Create Users", null },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbd"), 2, new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9828), "Edit existing users", true, "users.edit", "Edit Users", null },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbe"), 2, new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9832), "Delete users", true, "users.delete", "Delete Users", null },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbf"), 2, new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9836), "Manage user roles and permissions", true, "roles.manage", "Manage Roles", null },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), 3, new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9839), "View product list", true, "products.view", "View Products", null },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccd"), 3, new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9843), "Create new products", true, "products.create", "Create Products", null },
                    { new Guid("cccccccc-cccc-cccc-cccc-ccccccccccce"), 3, new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9846), "Edit existing products", true, "products.edit", "Edit Products", null },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccf"), 3, new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9852), "Delete products", true, "products.delete", "Delete Products", null },
                    { new Guid("dddddddd-dddd-dddd-dddd-ddddddddddd0"), 4, new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9856), "View category list", true, "categories.view", "View Categories", null },
                    { new Guid("dddddddd-dddd-dddd-dddd-ddddddddddd1"), 4, new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9860), "Create new categories", true, "categories.create", "Create Categories", null },
                    { new Guid("dddddddd-dddd-dddd-dddd-ddddddddddd2"), 4, new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9864), "Edit existing categories", true, "categories.edit", "Edit Categories", null },
                    { new Guid("dddddddd-dddd-dddd-dddd-ddddddddddd3"), 4, new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9867), "Delete categories", true, "categories.delete", "Delete Categories", null },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), 10, new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9873), "View available modules", true, "modules.view", "View Modules", null },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeef"), 10, new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9876), "Activate/deactivate modules", true, "modules.manage", "Manage Modules", null }
                });

            migrationBuilder.InsertData(
                table: "ShopTypes",
                columns: new[] { "Id", "CreatedAt", "DefaultModules", "DefaultUnits", "Description", "Icon", "IsActive", "Name", "SortOrder", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9282), "[\"11111111-1111-1111-1111-111111111111\",\"22222222-2222-2222-2222-222222222222\"]", "[\"11111111-1111-1111-1111-111111111111\",\"22222222-2222-2222-2222-222222222222\"]", "General retail store", null, true, "General Store", 1, null },
                    { new Guid("11111111-1111-1111-1111-111111111112"), new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9303), "[\"11111111-1111-1111-1111-111111111111\",\"22222222-2222-2222-2222-222222222222\",\"33333333-3333-3333-3333-333333333333\"]", "[\"11111111-1111-1111-1111-111111111111\"]", "Fashion and clothing store", null, true, "Clothing/Boutique", 2, null },
                    { new Guid("11111111-1111-1111-1111-111111111113"), new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9309), "[\"11111111-1111-1111-1111-111111111111\",\"22222222-2222-2222-2222-222222222222\",\"44444444-4444-4444-4444-444444444444\"]", "[\"11111111-1111-1111-1111-111111111111\"]", "Electronics and appliances", null, true, "Electronics", 3, null },
                    { new Guid("11111111-1111-1111-1111-111111111114"), new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9314), "[\"11111111-1111-1111-1111-111111111111\",\"22222222-2222-2222-2222-222222222222\",\"55555555-5555-5555-5555-555555555555\"]", "[\"22222222-2222-2222-2222-222222222222\",\"33333333-3333-3333-3333-333333333333\"]", "Grocery and food items", null, true, "Grocery", 4, null },
                    { new Guid("11111111-1111-1111-1111-111111111115"), new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9321), "[\"11111111-1111-1111-1111-111111111111\",\"22222222-2222-2222-2222-222222222222\",\"66666666-6666-6666-6666-666666666666\"]", "[\"11111111-1111-1111-1111-111111111111\"]", "Mobile phones and accessories", null, true, "Mobile Shop", 5, null },
                    { new Guid("11111111-1111-1111-1111-111111111116"), new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9329), "[\"11111111-1111-1111-1111-111111111111\",\"22222222-2222-2222-2222-222222222222\",\"77777777-7777-7777-7777-777777777777\"]", "[\"11111111-1111-1111-1111-111111111111\",\"22222222-2222-2222-2222-222222222222\"]", "Pharmacy and medical supplies", null, true, "Pharmacy", 6, null }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "BaseConversionFactor", "BaseUnitId", "CreatedAt", "Description", "IsActive", "Name", "SortOrder", "Symbol", "UnitType", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), 1m, null, new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9599), null, true, "Piece", 1, "pcs", 1, null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 1m, null, new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9623), null, true, "Kilogram", 2, "kg", 2, null },
                    { new Guid("33333333-3333-3333-3333-333333333333"), 1m, null, new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9629), null, true, "Liter", 3, "L", 4, null },
                    { new Guid("44444444-4444-4444-4444-444444444444"), 1m, null, new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9633), null, true, "Meter", 4, "m", 3, null },
                    { new Guid("77777777-7777-7777-7777-777777777777"), 1m, null, new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9651), null, true, "Box", 7, "box", 1, null },
                    { new Guid("88888888-8888-8888-8888-888888888888"), 12m, null, new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9655), null, true, "Dozen", 8, "doz", 1, null },
                    { new Guid("55555555-5555-5555-5555-555555555555"), 0.001m, new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9637), null, true, "Gram", 5, "g", 2, null },
                    { new Guid("66666666-6666-6666-6666-666666666666"), 0.001m, new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 12, 5, 10, 8, 49, 470, DateTimeKind.Utc).AddTicks(9644), null, true, "Milliliter", 6, "mL", 4, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_OrganizationId_Name",
                table: "Categories",
                columns: new[] { "OrganizationId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_Key",
                table: "Modules",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationModules_ModuleId",
                table: "OrganizationModules",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationModules_OrganizationId_ModuleId",
                table: "OrganizationModules",
                columns: new[] { "OrganizationId", "ModuleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_LicenseKey",
                table: "Organizations",
                column: "LicenseKey",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_ShopTypeId",
                table: "Organizations",
                column: "ShopTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationUnits_OrganizationId_UnitId",
                table: "OrganizationUnits",
                columns: new[] { "OrganizationId", "UnitId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationUnits_UnitId",
                table: "OrganizationUnits",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Key",
                table: "Permissions",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrganizationId_Barcode",
                table: "Products",
                columns: new[] { "OrganizationId", "Barcode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrganizationId_Sku",
                table: "Products",
                columns: new[] { "OrganizationId", "Sku" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_UnitId",
                table: "Products",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderItems_ProductId",
                table: "PurchaseOrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderItems_PurchaseOrderId",
                table: "PurchaseOrderItems",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_OrganizationId",
                table: "PurchaseOrders",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_OrganizationId_OrderNumber",
                table: "PurchaseOrders",
                columns: new[] { "OrganizationId", "OrderNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_Status",
                table: "PurchaseOrders",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_SupplierId",
                table: "PurchaseOrders",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_RoleId_PermissionId",
                table: "RolePermissions",
                columns: new[] { "RoleId", "PermissionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_OrganizationId_Name",
                table: "Roles",
                columns: new[] { "OrganizationId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockAdjustments_AdjustedBy",
                table: "StockAdjustments",
                column: "AdjustedBy");

            migrationBuilder.CreateIndex(
                name: "IX_StockAdjustments_CreatedAt",
                table: "StockAdjustments",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_StockAdjustments_OrganizationId",
                table: "StockAdjustments",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_StockAdjustments_ProductId",
                table: "StockAdjustments",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_Name",
                table: "Suppliers",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_OrganizationId",
                table: "Suppliers",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_OrganizationId_Code",
                table: "Suppliers",
                columns: new[] { "OrganizationId", "Code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Units_BaseUnitId",
                table: "Units",
                column: "BaseUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_OrganizationId_Email",
                table: "Users",
                columns: new[] { "OrganizationId", "Email" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_OrganizationId_Username",
                table: "Users",
                columns: new[] { "OrganizationId", "Username" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrganizationModules");

            migrationBuilder.DropTable(
                name: "OrganizationUnits");

            migrationBuilder.DropTable(
                name: "PurchaseOrderItems");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "StockAdjustments");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "ShopTypes");
        }
    }
}
