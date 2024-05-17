using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Home_Haven.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => new { x.OrderID, x.ProductID });
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "Description", "ImageURL", "Name", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, "A beautiful vintage chair made of oak.", "/images/vintage_chair.jpg", "Vintage Chair", 120.99m, 10 },
                    { 2, "A sleek modern table made of glass and steel.", "/images/modern_table.jpg", "Modern Table", 350.50m, 5 },
                    { 3, "A classic wooden bed with elegant carvings.", "/images/bed1.jpg", "Classic Bed", 499.99m, 7 },
                    { 4, "A comfortable contemporary sofa with plush cushions.", "/images/easysofa2.jpg", "Contemporary Sofa", 899.99m, 3 },
                    { 5, "A rustic table made from reclaimed wood.", "/images/table2.jpg", "Rustic Table", 249.99m, 4 },
                    { 6, "A stylish leather chair with a modern design.", "/images/chair2.jpg", "Leather Chair", 199.99m, 8 },
                    { 7, "An elegant bed with a plush headboard.", "/images/bed4.jpg", "Elegant Bed", 699.99m, 5 },
                    { 8, "A minimalist sofa with clean lines and a modern look.", "/images/easysofa3.jpg", "Minimalist Sofa", 799.99m, 4 },
                    { 9, "An industrial-style table with metal and wood elements.", "/images/table3.jpg", "Industrial Table", 299.99m, 6 },
                    { 10, "A comfy chair with soft upholstery and a cozy design.", "/images/comchair2.jpg", "Comfy Chair", 149.99m, 12 },
                    { 11, "A modern bed with a sleek design and a low profile.", "/images/bed3.jpg", "Modern Bed", 549.99m, 5 },
                    { 12, "A reclining sofa with luxurious padding for maximum comfort.", "/images/sofa4.jpg", "Reclining Sofa", 1099.99m, 2 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Address", "Email", "Name", "Password", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "123 Main St", "zaid@example.com", "Zaid Alam", "Zaid1234", "1234567890" },
                    { 2, "456 Elm St", "aniketh@example.com", "Kazi Aniketh", "Aniketh1234", "0987654321" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderID", "OrderDate", "UserID" },
                values: new object[] { 1, new DateTime(2024, 4, 14, 17, 54, 38, 161, DateTimeKind.Local).AddTicks(1632), 1 });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderID", "OrderDate", "UserID" },
                values: new object[] { 2, new DateTime(2024, 4, 14, 17, 54, 38, 161, DateTimeKind.Local).AddTicks(1672), 2 });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "OrderID", "ProductID", "Quantity", "TotalPrice" },
                values: new object[] { 1, 1, 2, 241.98m });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "OrderID", "ProductID", "Quantity", "TotalPrice" },
                values: new object[] { 1, 2, 1, 350.50m });

            migrationBuilder.InsertData(
                table: "OrderDetails",
                columns: new[] { "OrderID", "ProductID", "Quantity", "TotalPrice" },
                values: new object[] { 2, 2, 3, 1051.50m });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductID",
                table: "OrderDetails",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserID",
                table: "Orders",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
