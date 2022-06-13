using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Migrations
{
    public partial class RenameTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorSelectionOrder_Authors_AuthorId",
                table: "AuthorSelectionOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_BattleBook_Battles_BattleId",
                table: "BattleBook");

            migrationBuilder.DropForeignKey(
                name: "FK_BattleBook_Books_BookId",
                table: "BattleBook");

            migrationBuilder.DropForeignKey(
                name: "FK_GenreBook_Books_BookId",
                table: "GenreBook");

            migrationBuilder.DropForeignKey(
                name: "FK_GenreBook_Genres_GenreId",
                table: "GenreBook");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_Orders_OrderId",
                table: "OrderProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_Products_ProductId",
                table: "OrderProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductCloseout_Products_ProductId",
                table: "ProductCloseout");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTag_Products_ProductId",
                table: "ProductTag");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTag_Tags_TagId",
                table: "ProductTag");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_TagGroup_TagGroupId",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_TagUser_AspNetUsers_UsersId",
                table: "TagUser");

            migrationBuilder.DropForeignKey(
                name: "FK_TagUser_Tags_TagsId",
                table: "TagUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_BattleBook_BattleBookId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_UserId_BattleBookId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_BasketProducts_UserId_ProductId",
                table: "BasketProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TagUser",
                table: "TagUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TagGroup",
                table: "TagGroup");

            migrationBuilder.DropIndex(
                name: "IX_TagGroup_ColorHex",
                table: "TagGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductTag",
                table: "ProductTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductCloseout",
                table: "ProductCloseout");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderProduct",
                table: "OrderProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GenreBook",
                table: "GenreBook");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BattleBook",
                table: "BattleBook");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorSelectionOrder",
                table: "AuthorSelectionOrder");

            migrationBuilder.DeleteData(
                table: "AgeLimits",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AgeLimits",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AgeLimits",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AgeLimits",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BookTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BookTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BookTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BookTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BookTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CoverArts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CoverArts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.RenameTable(
                name: "TagUser",
                newName: "UserTagLinks");

            migrationBuilder.RenameTable(
                name: "TagGroup",
                newName: "TagGroups");

            migrationBuilder.RenameTable(
                name: "ProductTag",
                newName: "BookTagLinks");

            migrationBuilder.RenameTable(
                name: "ProductCloseout",
                newName: "Closeouts");

            migrationBuilder.RenameTable(
                name: "OrderProduct",
                newName: "OrderProducts");

            migrationBuilder.RenameTable(
                name: "GenreBook",
                newName: "BookGenreLinks");

            migrationBuilder.RenameTable(
                name: "BattleBook",
                newName: "BattleBookLinks");

            migrationBuilder.RenameTable(
                name: "AuthorSelectionOrder",
                newName: "AuthorSelectionOrders");

            migrationBuilder.RenameIndex(
                name: "IX_TagUser_UsersId",
                table: "UserTagLinks",
                newName: "IX_UserTagLinks_UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_TagGroup_Name",
                table: "TagGroups",
                newName: "IX_TagGroups_Name");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTag_TagId",
                table: "BookTagLinks",
                newName: "IX_BookTagLinks_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductTag_ProductId",
                table: "BookTagLinks",
                newName: "IX_BookTagLinks_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductCloseout_ProductId",
                table: "Closeouts",
                newName: "IX_Closeouts_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProduct_ProductId",
                table: "OrderProducts",
                newName: "IX_OrderProducts_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProduct_OrderId",
                table: "OrderProducts",
                newName: "IX_OrderProducts_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_GenreBook_GenreId",
                table: "BookGenreLinks",
                newName: "IX_BookGenreLinks_GenreId");

            migrationBuilder.RenameIndex(
                name: "IX_GenreBook_BookId",
                table: "BookGenreLinks",
                newName: "IX_BookGenreLinks_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_BattleBook_BookId",
                table: "BattleBookLinks",
                newName: "IX_BattleBookLinks_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_BattleBook_BattleId",
                table: "BattleBookLinks",
                newName: "IX_BattleBookLinks_BattleId");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorSelectionOrder_AuthorId",
                table: "AuthorSelectionOrders",
                newName: "IX_AuthorSelectionOrders_AuthorId");

            migrationBuilder.AlterColumn<string>(
                name: "TitleImageName",
                table: "Albums",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ColorHex",
                table: "TagGroups",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTagLinks",
                table: "UserTagLinks",
                columns: new[] { "TagsId", "UsersId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TagGroups",
                table: "TagGroups",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookTagLinks",
                table: "BookTagLinks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Closeouts",
                table: "Closeouts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderProducts",
                table: "OrderProducts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookGenreLinks",
                table: "BookGenreLinks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BattleBookLinks",
                table: "BattleBookLinks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorSelectionOrders",
                table: "AuthorSelectionOrders",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_UserId",
                table: "Votes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketProducts_UserId",
                table: "BasketProducts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorSelectionOrders_Authors_AuthorId",
                table: "AuthorSelectionOrders",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BattleBookLinks_Battles_BattleId",
                table: "BattleBookLinks",
                column: "BattleId",
                principalTable: "Battles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BattleBookLinks_Books_BookId",
                table: "BattleBookLinks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookGenreLinks_Books_BookId",
                table: "BookGenreLinks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookGenreLinks_Genres_GenreId",
                table: "BookGenreLinks",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookTagLinks_Products_ProductId",
                table: "BookTagLinks",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookTagLinks_Tags_TagId",
                table: "BookTagLinks",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Closeouts_Products_ProductId",
                table: "Closeouts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Orders_OrderId",
                table: "OrderProducts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Products_ProductId",
                table: "OrderProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_TagGroups_TagGroupId",
                table: "Tags",
                column: "TagGroupId",
                principalTable: "TagGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTagLinks_AspNetUsers_UsersId",
                table: "UserTagLinks",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTagLinks_Tags_TagsId",
                table: "UserTagLinks",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_BattleBookLinks_BattleBookId",
                table: "Votes",
                column: "BattleBookId",
                principalTable: "BattleBookLinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorSelectionOrders_Authors_AuthorId",
                table: "AuthorSelectionOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_BattleBookLinks_Battles_BattleId",
                table: "BattleBookLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_BattleBookLinks_Books_BookId",
                table: "BattleBookLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_BookGenreLinks_Books_BookId",
                table: "BookGenreLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_BookGenreLinks_Genres_GenreId",
                table: "BookGenreLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_BookTagLinks_Products_ProductId",
                table: "BookTagLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_BookTagLinks_Tags_TagId",
                table: "BookTagLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_Closeouts_Products_ProductId",
                table: "Closeouts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Orders_OrderId",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Products_ProductId",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_TagGroups_TagGroupId",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTagLinks_AspNetUsers_UsersId",
                table: "UserTagLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTagLinks_Tags_TagsId",
                table: "UserTagLinks");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_BattleBookLinks_BattleBookId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_UserId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_BasketProducts_UserId",
                table: "BasketProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTagLinks",
                table: "UserTagLinks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TagGroups",
                table: "TagGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderProducts",
                table: "OrderProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Closeouts",
                table: "Closeouts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookTagLinks",
                table: "BookTagLinks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookGenreLinks",
                table: "BookGenreLinks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BattleBookLinks",
                table: "BattleBookLinks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorSelectionOrders",
                table: "AuthorSelectionOrders");

            migrationBuilder.RenameTable(
                name: "UserTagLinks",
                newName: "TagUser");

            migrationBuilder.RenameTable(
                name: "TagGroups",
                newName: "TagGroup");

            migrationBuilder.RenameTable(
                name: "OrderProducts",
                newName: "OrderProduct");

            migrationBuilder.RenameTable(
                name: "Closeouts",
                newName: "ProductCloseout");

            migrationBuilder.RenameTable(
                name: "BookTagLinks",
                newName: "ProductTag");

            migrationBuilder.RenameTable(
                name: "BookGenreLinks",
                newName: "GenreBook");

            migrationBuilder.RenameTable(
                name: "BattleBookLinks",
                newName: "BattleBook");

            migrationBuilder.RenameTable(
                name: "AuthorSelectionOrders",
                newName: "AuthorSelectionOrder");

            migrationBuilder.RenameIndex(
                name: "IX_UserTagLinks_UsersId",
                table: "TagUser",
                newName: "IX_TagUser_UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_TagGroups_Name",
                table: "TagGroup",
                newName: "IX_TagGroup_Name");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProducts_ProductId",
                table: "OrderProduct",
                newName: "IX_OrderProduct_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderProducts_OrderId",
                table: "OrderProduct",
                newName: "IX_OrderProduct_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Closeouts_ProductId",
                table: "ProductCloseout",
                newName: "IX_ProductCloseout_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_BookTagLinks_TagId",
                table: "ProductTag",
                newName: "IX_ProductTag_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_BookTagLinks_ProductId",
                table: "ProductTag",
                newName: "IX_ProductTag_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_BookGenreLinks_GenreId",
                table: "GenreBook",
                newName: "IX_GenreBook_GenreId");

            migrationBuilder.RenameIndex(
                name: "IX_BookGenreLinks_BookId",
                table: "GenreBook",
                newName: "IX_GenreBook_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_BattleBookLinks_BookId",
                table: "BattleBook",
                newName: "IX_BattleBook_BookId");

            migrationBuilder.RenameIndex(
                name: "IX_BattleBookLinks_BattleId",
                table: "BattleBook",
                newName: "IX_BattleBook_BattleId");

            migrationBuilder.RenameIndex(
                name: "IX_AuthorSelectionOrders_AuthorId",
                table: "AuthorSelectionOrder",
                newName: "IX_AuthorSelectionOrder_AuthorId");

            migrationBuilder.AlterColumn<string>(
                name: "TitleImageName",
                table: "Albums",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ColorHex",
                table: "TagGroup",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TagUser",
                table: "TagUser",
                columns: new[] { "TagsId", "UsersId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TagGroup",
                table: "TagGroup",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderProduct",
                table: "OrderProduct",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductCloseout",
                table: "ProductCloseout",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductTag",
                table: "ProductTag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GenreBook",
                table: "GenreBook",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BattleBook",
                table: "BattleBook",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorSelectionOrder",
                table: "AuthorSelectionOrder",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AgeLimits",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "0+" },
                    { 2, "6+" },
                    { 3, "12+" },
                    { 4, "18+" }
                });

            migrationBuilder.InsertData(
                table: "BookTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Художественная литература" },
                    { 2, "Манга" },
                    { 3, "Ранобэ" },
                    { 4, "Графический роман" },
                    { 5, "Артбук" }
                });

            migrationBuilder.InsertData(
                table: "CoverArts",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Мягкая" },
                    { 2, "Твердая" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Драма" },
                    { 2, "Ужасы" },
                    { 3, "Научная фантастика" },
                    { 4, "Наука" },
                    { 5, "Боевик" },
                    { 6, "Детектив" },
                    { 7, "Фэнтези" },
                    { 8, "Сказка" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Votes_UserId_BattleBookId",
                table: "Votes",
                columns: new[] { "UserId", "BattleBookId" },
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BasketProducts_UserId_ProductId",
                table: "BasketProducts",
                columns: new[] { "UserId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TagGroup_ColorHex",
                table: "TagGroup",
                column: "ColorHex",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorSelectionOrder_Authors_AuthorId",
                table: "AuthorSelectionOrder",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BattleBook_Battles_BattleId",
                table: "BattleBook",
                column: "BattleId",
                principalTable: "Battles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BattleBook_Books_BookId",
                table: "BattleBook",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GenreBook_Books_BookId",
                table: "GenreBook",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GenreBook_Genres_GenreId",
                table: "GenreBook",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_Orders_OrderId",
                table: "OrderProduct",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_Products_ProductId",
                table: "OrderProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductCloseout_Products_ProductId",
                table: "ProductCloseout",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTag_Products_ProductId",
                table: "ProductTag",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTag_Tags_TagId",
                table: "ProductTag",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_TagGroup_TagGroupId",
                table: "Tags",
                column: "TagGroupId",
                principalTable: "TagGroup",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TagUser_AspNetUsers_UsersId",
                table: "TagUser",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TagUser_Tags_TagsId",
                table: "TagUser",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_BattleBook_BattleBookId",
                table: "Votes",
                column: "BattleBookId",
                principalTable: "BattleBook",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
