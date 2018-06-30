using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nevara.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
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
                    FullName = table.Column<string>(maxLength: 255, nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: true),
                    Avatar = table.Column<string>(nullable: true),
                    Address = table.Column<string>(maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Image = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Collections",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CollectionName = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Image = table.Column<string>(maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ColorName = table.Column<string>(maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ManufacturerName = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MaterialName = table.Column<string>(maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppRoleClaims_AppRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AppRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUserClaims_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLogins", x => x.UserId);
                    table.UniqueConstraint("AK_AppUserLogins_LoginProvider_ProviderKey", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AppUserLogins_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserRole",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserRole", x => new { x.RoleId, x.UserId });
                    table.UniqueConstraint("AK_AppUserRole_UserId_RoleId", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AppUserRole_AppRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AppRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserRole_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserTokens", x => x.UserId);
                    table.UniqueConstraint("AK_AppUserTokens_UserId_LoginProvider_Name", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AppUserTokens_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CustomerName = table.Column<string>(maxLength: 255, nullable: false),
                    CustomerMobile = table.Column<string>(maxLength: 20, nullable: false),
                    CustomerAddress = table.Column<string>(maxLength: 255, nullable: false),
                    CustomerMessage = table.Column<string>(nullable: true),
                    PaymentMethod = table.Column<int>(nullable: false),
                    BillStatus = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AppUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Width = table.Column<double>(nullable: true),
                    Height = table.Column<double>(nullable: true),
                    Thumbnail = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Depth = table.Column<double>(nullable: true),
                    OriginalPrice = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    Price = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    PromotionPrice = table.Column<decimal>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    ColorId = table.Column<int>(nullable: false),
                    MaterialId = table.Column<int>(nullable: false),
                    CollectionId = table.Column<int>(nullable: false),
                    ManufacturerId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: true),
                    HomeFlag = table.Column<bool>(nullable: true),
                    NewFlag = table.Column<bool>(nullable: true),
                    HotFlag = table.Column<bool>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImagePath = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    ProductId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Image", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, "/images/Category/cat3.jpg", false, "Beds" },
                    { 2, "/images/Category/cat1.jpg", false, "Tables" },
                    { 3, "/images/Category/cat2.jpg", false, "Chair" },
                    { 4, "/images/Category/cat2.jpg", false, "Kitchen Furniture" }
                });

            migrationBuilder.InsertData(
                table: "Collections",
                columns: new[] { "Id", "CollectionName", "Description", "Image", "IsDeleted" },
                values: new object[,]
                {
                    { 1, "Autumn", "This is autumn description", null, false },
                    { 2, "Fall", "This is autumn description", null, false },
                    { 3, "Summner", "This is autumn description", null, false },
                    { 4, "Spring", "This is autumn description", null, false }
                });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Id", "Code", "ColorName", "IsDeleted" },
                values: new object[,]
                {
                    { 4, "#000000", "White", false },
                    { 3, "#29ab87", "Green", false },
                    { 1, "#d80000", "Red", false },
                    { 2, "#0099cc", "Blue", false }
                });

            migrationBuilder.InsertData(
                table: "Manufacturers",
                columns: new[] { "Id", "IsDeleted", "ManufacturerName" },
                values: new object[,]
                {
                    { 1, false, "Yokohama" },
                    { 2, false, "Samsung" },
                    { 3, false, "Honda" }
                });

            migrationBuilder.InsertData(
                table: "Materials",
                columns: new[] { "Id", "IsDeleted", "MaterialName" },
                values: new object[,]
                {
                    { 2, false, "Plastic" },
                    { 1, false, "Wood" },
                    { 3, false, "Wood" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CollectionId", "ColorId", "Depth", "Description", "Height", "HomeFlag", "HotFlag", "IsDeleted", "ManufacturerId", "MaterialId", "Name", "NewFlag", "OriginalPrice", "Price", "PromotionPrice", "Quantity", "Thumbnail", "Width" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, null, null, null, null, null, false, 1, 1, "Bed 1", null, 0m, 2000m, null, null, "/images/Product/pro1.jpg", null },
                    { 4, 2, 2, 1, null, null, null, null, null, false, 1, 1, "Table 1", null, 0m, 2000m, null, 10, "/images/Product/pro1.jpg", null },
                    { 7, 3, 4, 1, null, null, null, null, null, false, 1, 1, "Chair 1", null, 0m, 2000m, null, 25, "/images/Product/pro1.jpg", null },
                    { 10, 4, 3, 4, null, null, null, null, null, false, 1, 1, "Kitchen 1 ", null, 0m, 2000m, null, null, "/images/Product/pro1.jpg", null },
                    { 2, 1, 1, 1, null, null, null, null, null, false, 2, 2, "Bed 2", null, 0m, 3000m, null, 10, "/images/Product/pro2.jpg", null },
                    { 5, 2, 3, 2, null, null, null, null, null, false, 2, 2, "Table 2", null, 0m, 2000m, null, 20, "/images/Product/pro2.jpg", null },
                    { 8, 3, 4, 2, null, null, null, null, null, false, 2, 2, "Chair 2", null, 0m, 1000m, null, 30, "/images/Product/pro2.jpg", null },
                    { 11, 4, 4, 1, null, null, null, null, null, false, 2, 2, "Kitchen 2", null, 0m, 2000m, null, null, "/images/Product/pro2.jpg", null },
                    { 3, 1, 2, 1, null, null, null, null, null, false, 2, 3, "Bed 3", null, 0m, 2000m, null, null, "/images/Product/pro2.jpg", null },
                    { 6, 2, 4, 3, null, null, null, null, null, false, 2, 3, "Table 3", null, 0m, 1000m, null, 50, "/images/Product/pro2.jpg", null },
                    { 9, 3, 2, 3, null, null, null, null, null, false, 2, 3, "Chair 3", null, 0m, 5000m, null, null, "/images/Product/pro2.jpg", null },
                    { 12, 4, 1, 2, null, null, null, null, null, false, 2, 3, "Kitchen 3", null, 0m, 2000m, null, null, "/images/Product/pro2.jpg", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppRoleClaims_RoleId",
                table: "AppRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AppRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserClaims_UserId",
                table: "AppUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AppUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AppUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ProductId",
                table: "Images",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CollectionId",
                table: "Products",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ColorId",
                table: "Products",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ManufacturerId",
                table: "Products",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_MaterialId",
                table: "Products",
                column: "MaterialId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppRoleClaims");

            migrationBuilder.DropTable(
                name: "AppUserClaims");

            migrationBuilder.DropTable(
                name: "AppUserLogins");

            migrationBuilder.DropTable(
                name: "AppUserRole");

            migrationBuilder.DropTable(
                name: "AppUserTokens");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "AppRoles");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Collections");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "Manufacturers");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "AppUsers");
        }
    }
}
