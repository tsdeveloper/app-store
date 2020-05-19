export class ShopParams {

  productBrandId = 0;
  productTypeId = 0;
  sort = 'name';
  pageIndex = 1;
  pageSize = 10;

  resetParams() {
    return new ShopParams();
   }
}
